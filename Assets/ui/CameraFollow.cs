using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // drag Player_Kael ke sini
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0, 1, -10);

    // Batas kiri-kanan level (opsional, biar kamera tidak keluar background)
    public float minX = -10f;
    public float maxX = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}