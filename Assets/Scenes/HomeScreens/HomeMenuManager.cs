using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenuManager : MonoBehaviour
{
    [Header("Pengaturan Audio")]
    public AudioSource sumberBGM;
    public Slider sliderVolume;

    void Start()
    {
        // Menyinkronkan nilai Slider dengan volume asli dari AudioSource
        if (sumberBGM != null && sliderVolume != null)
        {
            sliderVolume.value = sumberBGM.volume;
            // Menambahkan pendengar (listener) agar volume otomatis berubah saat slider digeser
            sliderVolume.onValueChanged.AddListener(AturVolume);
        }
    }

    // Fungsi yang dipanggil oleh Slider
    public void AturVolume(float nilai)
    {
        if (sumberBGM != null)
        {
            sumberBGM.volume = nilai;
        }
    }

    // Fungsi untuk Tombol PLAY
    public void MulaiGame()
    {
        Debug.Log("Memulai Petualangan! Pindah ke Level 1...");
        // Pastikan scene "level1" sudah ditambahkan di File -> Build Settings
        SceneManager.LoadScene("level1"); 
    }

    // Fungsi untuk Tombol EXIT
    public void KeluarGame()
    {
        Debug.Log("Game Ditutup!");
        Application.Quit();
    }
}