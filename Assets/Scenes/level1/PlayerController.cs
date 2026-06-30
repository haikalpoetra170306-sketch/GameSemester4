using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float kecepatan = 5f;
    [HideInInspector] public bool sudahPunyaPedang = false;
    
    private bool bisaGerak = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!bisaGerak) return;

        float inputGerak = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(inputGerak * kecepatan, rb.linearVelocity.y);

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