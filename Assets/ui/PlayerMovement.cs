using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jarakSerangan = 2f; // Jarak jangkauan serangan pahlawan
    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;

    void Start()
    {
        // Mengenalkan komponen Rigidbody2D dan Animator yang ada di objek Player
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Mengambil input tombol panah kanan/kiri atau A/D dari keyboard
        moveInput = Input.GetAxisRaw("Horizontal");

        // Menggerakkan fisik objek Player ke kanan atau ke kiri (menggunakan linearVelocity)
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Jika moveInput tidak sama dengan 0 (artinya tombol ditekan), isWalking menjadi true
        anim.SetBool("isWalking", moveInput != 0);

        // Membalikkan arah hadap karakter (Flip Sprite)
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Hadap Kanan
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Hadap Kiri
        }

        // --- KODE MENYERANG BARU ---
        // Jika tombol 'J' ditekan pada keyboard
        if (Input.GetKeyDown(KeyCode.J))
        {
            SerangMusuh();
        }
    }

    void SerangMusuh()
    {
        // Mencari semua objek di layar yang memiliki Tag "Musuh"
        GameObject[] semuaMusuh = GameObject.FindGameObjectsWithTag("Musuh");

        // Mengecek satu per satu musuh yang ada
        foreach (GameObject musuh in semuaMusuh)
        {
            // Menghitung jarak antara Pahlawan dan Musuh
            float jarak = Vector2.Distance(transform.position, musuh.transform.position);

            // Jika musuh berada cukup dekat (lebih kecil atau sama dengan jarak serangan)
            if (jarak <= jarakSerangan)
            {
                // Hancurkan objek musuh dari dalam game!
                Destroy(musuh);
            }
        }
    }
}