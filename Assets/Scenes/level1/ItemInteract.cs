using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public GameObject tombolTanganUI;
    public PlayerController playerScript;
    private bool bisaDiambil = false;

    void Start()
    {
        if (tombolTanganUI != null) tombolTanganUI.SetActive(false);
    }

    void Update()
    {
        if (bisaDiambil && Input.GetKeyDown(KeyCode.Space))
        {
            ProsesAmbilPedang();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bisaDiambil = true;
            if (tombolTanganUI != null) tombolTanganUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bisaDiambil = false;
            if (tombolTanganUI != null) tombolTanganUI.SetActive(false);
        }
    }

    public void TombolLayarDitekan()
    {
        if (bisaDiambil) ProsesAmbilPedang();
    }

    private void ProsesAmbilPedang()
    {
        if (playerScript != null) playerScript.AmbilSenjata();
        if (tombolTanganUI != null) tombolTanganUI.SetActive(false);
        Destroy(gameObject);
    }
}