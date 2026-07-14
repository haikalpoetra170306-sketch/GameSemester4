using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController3 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (anim != null)
        {
            anim.SetBool("isWalking", moveInput != 0);
            anim.SetBool("isGrounded", isGrounded); 
        }

        // Membalikkan arah hadap karakter (Flip)
        if (moveInput > 0)
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1.5f, 1.5f, 1);

        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        rb.linearVelocity = Vector2.zero; // Hentikan gerakan
        Debug.Log("Player mati menabrak batu! Restart...");
        Invoke("RestartLevel", 1.5f);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // --- FITUR BARU: DETEKSI TABRAKAN ---

    // 1. Deteksi Tabrakan Keras (Batu)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    // 2. Deteksi Area Finish (Finish Point)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (isDead) return; 
            isDead = true; // Kunci pergerakan player
            rb.linearVelocity = Vector2.zero; // Rem mendadak
            
            if (anim != null) 
                anim.SetBool("isWalking", false); // Matikan gaya jalan
            
            Debug.Log("Hore! Kamu mencapai Finish Point!");
            // Nanti kode untuk pindah ke Menu/Level selanjutnya ditaruh di sini
        }
    }
}