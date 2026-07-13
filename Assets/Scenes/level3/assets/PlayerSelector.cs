using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public GameObject player1; // Player tanpa pedang
    public GameObject player2; // Player dengan pedang (merah)
    public GameObject player3; // Player dengan pedang (hitam)

    private void Start()
    {
        // Ambil pilihan dari level sebelumnya
        int selectedPlayer = PlayerPrefs.GetInt("selectedPlayer", 1);

        // Matikan semua dulu
        player1.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);

        // Aktifkan sesuai pilihan
        if (selectedPlayer == 1) player1.SetActive(true);
        else if (selectedPlayer == 2) player2.SetActive(true);
        else if (selectedPlayer == 3) player3.SetActive(true);
    }
}