using UnityEngine;

public class GuardScriptedAnim : MonoBehaviour
{
    [Header("Pengaturan Jalan Patroli")]
    public bool bisaPatroli = true;
    public float kecepatanJalan = 2f;
    public float jarakPatroli = 3f;
    public float waktuBerhenti = 1.2f;
    public float kecepatanFlip = 10f;

    [Header("Efek Langkah (bob halus saat jalan)")]
    public float tinggiLangkah = 0.02f;
    public float kecepatanLangkah = 8f;

    private Vector3 posisiAwal;
    private Vector3 skalaAwal;
    private float posisiTargetX;
    private bool sedangBerhenti = false;
    private float timerBerhenti = 0f;
    private bool menghadapKanan = true;

    void Start()
    {
        posisiAwal = transform.position;
        skalaAwal = transform.localScale;
        posisiTargetX = posisiAwal.x + jarakPatroli;
    }

    void Update()
    {
        if (!bisaPatroli) return;

        if (sedangBerhenti)
        {
            timerBerhenti -= Time.deltaTime;
            if (timerBerhenti <= 0f)
            {
                sedangBerhenti = false;
                posisiTargetX = (posisiTargetX > posisiAwal.x)
                    ? posisiAwal.x - jarakPatroli
                    : posisiAwal.x + jarakPatroli;
            }
            return;
        }

        // --- BERJALAN ---
        float posisiX = transform.position.x;
        float arah = (posisiTargetX > posisiX) ? 1f : -1f;

        float posisiXBaru = Mathf.MoveTowards(posisiX, posisiTargetX, kecepatanJalan * Time.deltaTime);
        transform.position = new Vector3(posisiXBaru, transform.position.y, transform.position.z);

        menghadapKanan = arah > 0;

        // Bob kecil saat melangkah saja (opsional, tanpa napas)
        float bob = Mathf.Abs(Mathf.Sin(Time.time * kecepatanLangkah)) * tinggiLangkah;

        float targetScaleX = menghadapKanan ? Mathf.Abs(skalaAwal.x) : -Mathf.Abs(skalaAwal.x);
        float scaleXBaru = Mathf.MoveTowards(transform.localScale.x, targetScaleX, kecepatanFlip * Time.deltaTime);

        transform.localScale = new Vector3(scaleXBaru, skalaAwal.y + bob, skalaAwal.z);

        if (Mathf.Abs(posisiXBaru - posisiTargetX) < 0.02f)
        {
            sedangBerhenti = true;
            timerBerhenti = waktuBerhenti;
        }
    }
}