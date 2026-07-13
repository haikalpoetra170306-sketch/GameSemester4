using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float leftLimit = -3f;
    public float rightLimit = 3f;

    private bool movingRight = true;

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}