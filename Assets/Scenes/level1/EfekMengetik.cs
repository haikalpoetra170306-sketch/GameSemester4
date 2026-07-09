using System.Collections;
using UnityEngine;
using TMPro; // Sangat penting: Ini untuk memanggil TextMeshPro

public class EfekMengetik : MonoBehaviour
{
    [Header("Pengaturan Teks")]
    public TextMeshProUGUI teksUI; // Tempat memasukkan komponen Teks
    
    [TextArea(3, 5)] // Membuat kotak input teks di Inspector lebih besar
    public string kalimatYangDitulis = "Ketik kalimat Anda di sini...";
    
    public float kecepatanNgetik = 0.05f; // Semakin kecil angkanya, semakin cepat ngetiknya

    void Start()
    {
        // Kosongkan teks saat game baru dimulai
        teksUI.text = ""; 
        
        // Panggil fungsi mesin tiknya
        StartCoroutine(JalankanEfekNgetik());
    }

    // Ini adalah Coroutine (Mesin Tik)
    IEnumerator JalankanEfekNgetik()
    {
        // Pecah kalimat menjadi huruf-huruf tunggal
        foreach (char huruf in kalimatYangDitulis.ToCharArray())
        {
            teksUI.text += huruf; // Tambahkan satu huruf ke layar
            yield return new WaitForSeconds(kecepatanNgetik); // Jeda sebelum huruf berikutnya
        }
    }
}