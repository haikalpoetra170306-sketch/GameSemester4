using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text narasiText;
    public TMP_Text pageIndicator;
    public GameObject nextButton;

    [Header("Scene Tujuan")]
    public string nextSceneName = "Level3";

    // Isi narasi sebelum Level 3
    private string[] narasiList = {
        "Setelah perjalanan panjang melewati hutan dan perbukitan yang gelap, sang pahlawan akhirnya tiba di belakang kastil musuh.",
        "Dari kejauhan ia dapat melihat obor-obor yang menyala di sepanjang tembok kastil dan para penjaga yang berjaga di setiap sudut.",
        "Sang pahlawan menemukan lorong kuno yang menghubungkan bagian utama kastil dengan ruang bawah tanah.\n\nLorong tersebut sudah berusia ratusan tahun dan dipenuhi berbagai jebakan yang dibuat untuk menghentikan para penyusup.",
        "Suara batu yang bergemuruh terdengar dari kejauhan, menandakan bahaya yang menunggu di depan.\n\nDengan tekad yang kuat, sang pahlawan melanjutkan perjalanannya..."
    };

    private int currentIndex = 0;

    private void Start()
    {
        currentIndex = 0;
        TampilkanNarasi(currentIndex);
    }

    private void Update()
    {
        // Bisa juga pakai keyboard Space/Enter untuk lanjut
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            OnNextButtonClick();
    }

    void TampilkanNarasi(int index)
    {
        narasiText.text = narasiList[index];

        // Update indikator halaman
        if (pageIndicator != null)
            pageIndicator.text = (index + 1) + " / " + narasiList.Length;

        // Ganti teks tombol di halaman terakhir
        TMP_Text btnText = nextButton.GetComponentInChildren<TMP_Text>();
        if (index >= narasiList.Length - 1)
            btnText.text = "Mulai Level 3 >";
        else
            btnText.text = "Lanjut >";
    }

    public void OnNextButtonClick()
    {
        currentIndex++;

        if (currentIndex >= narasiList.Length)
        {
            // Semua narasi selesai → masuk Level 3
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            TampilkanNarasi(currentIndex);
        }
    }
}