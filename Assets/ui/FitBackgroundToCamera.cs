using UnityEngine;

[ExecuteAlways]
public class FitBackgroundToCamera : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        FitToCamera();
    }

    void FitToCamera()
    {
        Camera cam = Camera.main;
        if (cam == null || spriteRenderer == null || spriteRenderer.sprite == null) return;

        float worldScreenHeight = cam.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * cam.aspect;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector3 newScale = transform.localScale;
        newScale.x = worldScreenWidth / spriteSize.x;
        newScale.y = worldScreenHeight / spriteSize.y;
        transform.localScale = newScale;
    }
}