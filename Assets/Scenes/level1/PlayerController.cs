using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float kecepatan = 5f;
    [HideInInspector] public bool sudahPunyaPedang = false;
    
    private bool bisaGerak = false;
    private Rigidbody2D rb;
    private Animator anim; // Variabel untuk mengontrol animasi

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Mengambil komponen Animator dari objek anak (VisualNinja1)
        anim = GetComponentInChildren<Animator>(); 
    }

    void Update()
    {
        if (!bisaGerak) return;

        float inputGerak = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(inputGerak * kecepatan, rb.linearVelocity.y);

        // Mengirimkan nilai kecepatan gerak ke parameter "Speed" di Animator
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(inputGerak));
        }

        if (inputGerak > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (inputGerak < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    public void SetBisaGerak(bool status)
    {
        bisaGerak = status;
    }

    public void AmbilSenjata()
    {
        sudahPunyaPedang = true;
        Animator animatorKarakter = GetComponentInChildren<Animator>();
        if (animatorKarakter != null)
        {
            animatorKarakter.SetBool("BawaPedang", true);
        }
    }
}