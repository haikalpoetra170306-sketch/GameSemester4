using UnityEngine;

public class BoulderController : MonoBehaviour
{
    public float rollSpeed = 4f;
    public float spawnPositionX = 10f;
    public float destroyPositionX = -12f;

    private Rigidbody2D rb;
    private float startY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startY = transform.position.y;
        transform.position = new Vector3(spawnPositionX, startY, 0);
    }

    private void FixedUpdate()
    {
        // Hanya paksa X, biarkan Y ikut gravitasi
        rb.linearVelocity = new Vector2(-rollSpeed, rb.linearVelocity.y);

        if (transform.position.x < destroyPositionX)
            ResetBoulder();
    }

    void ResetBoulder()
    {
        transform.position = new Vector3(spawnPositionX, startY, 0);
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.gameObject.GetComponent<PlayerController3>().Die();
    }
}