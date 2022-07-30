using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GanhouCode : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject vida, QuantidadeVidas, Granadas, granadaOff, quantiaBiscoitos,ganhou;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        if(ganhou.activeSelf && Input.anyKeyDown)
        {
            SceneManager.LoadSceneAsync("cenaMenu");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            vida.SetActive(false);
            QuantidadeVidas.SetActive(false);
            quantiaBiscoitos.SetActive(false);
            Granadas.SetActive(false);
            granadaOff.SetActive(false);
            Time.timeScale = 0;
        }
    }
}
