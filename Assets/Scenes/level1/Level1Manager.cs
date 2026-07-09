using System.Collections; // Wajib ditambahkan untuk memanggil Coroutine (IEnumerator)
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct BarisDialog
{
    public string namaKarakter;
    [TextArea(2, 5)] public string kalimat;
    public Sprite potretKarakter;
}

public class Level1Manager : MonoBehaviour
{
    [Header("Pengaturan Efek Teks")]
    public float kecepatanNgetik = 0.03f; // Atur kecepatan ngetik di Inspector

    [Header("Cutscene Awal")]
    public GameObject panelCutsceneAwal;
    public Image uiPotretAwal;
    public TextMeshProUGUI uiTeksNamaAwal;
    public TextMeshProUGUI uiTeksKalimatAwal;
    public BarisDialog[] percakapanAwal; 
    private int indeksDialogAwal = 0;
    private Coroutine efekNgetikAwal; // Mencegah teks bertumpuk jika tombol diklik cepat

    [Header("Pilih Karakter")]
    public GameObject panelPilihKarakter;
    public GameObject[] gambarKarakterUI; // Karakter 1, 2, 3 di UI
    private int indeksPilihan = 0;

    [Header("Cutscene Akhir")]
    public GameObject panelCutsceneAkhir;
    public Image uiPotretAkhir;
    public TextMeshProUGUI uiTeksNamaAkhir;
    public TextMeshProUGUI uiTeksKalimatAkhir;
    public BarisDialog[] percakapanAkhir; 
    private int indeksDialogAkhir = 0;
    private Coroutine efekNgetikAkhir; // Mencegah teks bertumpuk jika tombol diklik cepat

    [Header("Gameplay")]
    public GameObject[] visualKarakterPlayer; // VisualNinja 1, 2, 3 di Player
    public PlayerController playerScript;

    void Start()
    {
        panelCutsceneAwal.SetActive(true);
        panelPilihKarakter.SetActive(false);
        panelCutsceneAkhir.SetActive(false);

        if (playerScript != null) playerScript.SetBisaGerak(false);

        if (percakapanAwal.Length > 0) TampilkanDialogAwal(0);
        UpdateUIKarakter();
    }

    // --- FUNGSI CUTSCENE AWAL ---
    public void LanjutPercakapanAwal()
    {
        indeksDialogAwal++;
        if (indeksDialogAwal < percakapanAwal.Length) TampilkanDialogAwal(indeksDialogAwal);
        else
        {
            panelCutsceneAwal.SetActive(false);
            panelPilihKarakter.SetActive(true);
        }
    }
    
    private void TampilkanDialogAwal(int indeks)
    {
        uiTeksNamaAwal.text = percakapanAwal[indeks].namaKarakter;
        if(uiPotretAwal != null) uiPotretAwal.sprite = percakapanAwal[indeks].potretKarakter;
        
        // Hentikan animasi ngetik sebelumnya (jika ada) agar tidak balapan
        if (efekNgetikAwal != null) StopCoroutine(efekNgetikAwal);
        
        // Jalankan animasi ngetik yang baru
        efekNgetikAwal = StartCoroutine(KetikTeksAwal(percakapanAwal[indeks].kalimat));
    }

    private IEnumerator KetikTeksAwal(string kalimat)
    {
        uiTeksKalimatAwal.text = ""; // Kosongkan teks dulu
        foreach (char huruf in kalimat.ToCharArray())
        {
            uiTeksKalimatAwal.text += huruf; // Tambahkan huruf satu per satu
            yield return new WaitForSeconds(kecepatanNgetik); // Jeda
        }
    }

    // --- FUNGSI PILIH KARAKTER ---
    public void GeserKanan()
    {
        indeksPilihan++;
        if (indeksPilihan >= gambarKarakterUI.Length) indeksPilihan = 0;
        UpdateUIKarakter();
    }
    
    public void GeserKiri()
    {
        indeksPilihan--;
        if (indeksPilihan < 0) indeksPilihan = gambarKarakterUI.Length - 1;
        UpdateUIKarakter();
    }
    
    private void UpdateUIKarakter()
    {
        for (int i = 0; i < gambarKarakterUI.Length; i++)
        {
            if (gambarKarakterUI[i] != null) 
                gambarKarakterUI[i].SetActive(i == indeksPilihan);
        }
    }
    
    public void KonfirmasiPilihKarakter()
    {
        panelPilihKarakter.SetActive(false);
        panelCutsceneAkhir.SetActive(true);
        if (percakapanAkhir.Length > 0)
        {
            indeksDialogAkhir = 0;
            TampilkanDialogAkhir(0);
        }
        else MulaiGameplay();
    }

    // --- FUNGSI CUTSCENE AKHIR ---
    public void LanjutPercakapanAkhir()
    {
        indeksDialogAkhir++;
        if (indeksDialogAkhir < percakapanAkhir.Length) TampilkanDialogAkhir(indeksDialogAkhir);
        else MulaiGameplay();
    }
    
    private void TampilkanDialogAkhir(int indeks)
    {
        uiTeksNamaAkhir.text = percakapanAkhir[indeks].namaKarakter;
        if(uiPotretAkhir != null) uiPotretAkhir.sprite = percakapanAkhir[indeks].potretKarakter;

        // Hentikan animasi ngetik sebelumnya (jika ada) agar tidak balapan
        if (efekNgetikAkhir != null) StopCoroutine(efekNgetikAkhir);
        
        // Jalankan animasi ngetik yang baru
        efekNgetikAkhir = StartCoroutine(KetikTeksAkhir(percakapanAkhir[indeks].kalimat));
    }

    private IEnumerator KetikTeksAkhir(string kalimat)
    {
        uiTeksKalimatAkhir.text = ""; // Kosongkan teks dulu
        foreach (char huruf in kalimat.ToCharArray())
        {
            uiTeksKalimatAkhir.text += huruf; // Tambahkan huruf satu per satu
            yield return new WaitForSeconds(kecepatanNgetik); // Jeda
        }
    }

    // --- FUNGSI GAMEPLAY ---
    private void MulaiGameplay()
    {
        panelCutsceneAkhir.SetActive(false);
        for (int i = 0; i < visualKarakterPlayer.Length; i++)
        {
            if (visualKarakterPlayer[i] != null) 
                visualKarakterPlayer[i].SetActive(i == indeksPilihan);
        }
        if (playerScript != null) playerScript.SetBisaGerak(true);
    }
}