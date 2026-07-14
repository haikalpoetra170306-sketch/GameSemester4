using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public string namaLevelBerikut = "LEVEL2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null && player.SudahAmbilPedang())
            {
                SceneManager.LoadScene(namaLevelBerikut);
            }
        }
    }
}