using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGate : MonoBehaviour
{
    public PlayerController playerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah yang menyentuh pintu adalah Player
        if (collision.CompareTag("Player"))
        {
            // Mengecek apakah Player sudah mengambil pedang
            if (playerScript.sudahPunyaPedang)
            {
                Debug.Log("Misi Berhasil! Pindah ke Level 2...");
                SceneManager.LoadScene("Level2"); // Pastikan Level2 ada di Build Settings
            }
            else
            {
                Debug.Log("Pintu terkunci! Kamu harus mengambil pedang terlebih dahulu.");
            }
        }
    }
}