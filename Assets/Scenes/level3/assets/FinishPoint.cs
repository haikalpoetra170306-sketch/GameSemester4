using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [Header("UI Pengumuman")]
    public GameObject winPanel; 

    // Yg diubah hanya baris ini (pakai OnCollisionEnter2D, bukan OnTrigger)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah yang menabrak adalah Player
        if (collision.gameObject.CompareTag("Player"))
        {
            if (winPanel != null)
            {
                winPanel.SetActive(true);
            }

            Debug.Log("Level 3 Selesai! Layar kemenangan muncul.");
        }
    }
}