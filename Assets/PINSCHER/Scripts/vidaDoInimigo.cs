using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaDoInimigo : MonoBehaviour
{
    public float vida_atual;
    public float vida_maxima;
 
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vida_atual <= 0f)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

}
