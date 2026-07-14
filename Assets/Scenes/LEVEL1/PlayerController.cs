using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float KecepatanJalan = 5f;
    public float KecepatanNaikTurun = 5f;

    public float batasAtas = 3f;
    public float batasBawah = -3f;

    private Rigidbody2D rb;
    private Animator anim;

    // Variabel status pedang
    private bool membawaPedang = false;
    private bool diDekatPedang = false;
    private GameObject pedangYangMauDiambil;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 1. Logika Pergerakan
        float moveInput = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector2(
            moveInput * KecepatanJalan,
            moveVertical * KecepatanNaikTurun
        );

        // 1b. Batas atas dan bawah
        Vector3 posisi = transform.position;
        posisi.y = Mathf.Clamp(posisi.y, batasBawah, batasAtas);
        transform.position = posisi;

        // 2. Logika Flip Karakter (Aman dari bug Scale)
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            if (moveInput > 0) sr.flipX = false;
            else if (moveInput < 0) sr.flipX = true;
        }

        // 3. Logika Mengambil Pedang (Tombol Enter)
        // KeyCode.Return adalah tombol Enter di keyboard
        if (diDekatPedang && Input.GetKeyDown(KeyCode.Return))
        {
            AmbilPedang();
        }

        // 4. Update Animasi
        if (anim != null)
        {
            anim.SetBool("isWalking", moveInput != 0 || moveVertical != 0);
            anim.SetBool("BawaPedang", membawaPedang);
        }
    }

    // Fungsi bawaan Unity saat Player MASUK ke area Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Senjata"))
        {
            diDekatPedang = true;
            pedangYangMauDiambil = collision.gameObject;
            Debug.Log("Berada di dekat pedang! Tekan ENTER untuk mengambil.");
        }
    }

    // Fungsi bawaan Unity saat Player KELUAR dari area Trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Senjata"))
        {
            diDekatPedang = false;
            pedangYangMauDiambil = null;
            Debug.Log("Menjauh dari pedang.");
        }
    }

    // Fungsi khusus untuk mengeksekusi pengambilan pedang
    private void AmbilPedang()
    {
        membawaPedang = true;       // Status berubah jadi bawa pedang
        diDekatPedang = false;      // Reset status area

        // Hancurkan (hilangkan) objek pedang yang ada di tanah
        if (pedangYangMauDiambil != null)
        {
            Destroy(pedangYangMauDiambil);
        }

        Debug.Log("Pedang berhasil diambil! Animasi BawaPedang aktif.");
    }
    public bool SudahAmbilPedang()
{
    return membawaPedang;
}
}