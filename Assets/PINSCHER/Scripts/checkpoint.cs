using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkpoint : MonoBehaviour
{
    public Transform localização;
    public GameObject dialogo;
    public Text texto;
    public Image granada;

    public string msg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D outroObjeto)
    {
        if(outroObjeto.CompareTag("Player"))
        {
            dialogo.gameObject.SetActive(true);
            texto.text = msg;
        }
        
    }

    private void OnTriggerExit2D(Collider2D outroObjeto)
    {
        dialogo.gameObject.SetActive(false);
    }
}
