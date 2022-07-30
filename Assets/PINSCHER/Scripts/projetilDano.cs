using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetilDano : MonoBehaviour
{
    private vidaDoInimigo inimigo;
    public atira script;
    public float dano_projetil;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision is BoxCollider2D)
            {
                inimigo = collision.GetComponent<vidaDoInimigo>();
                inimigo.vida_atual += dano_projetil;
                Destroy(this.gameObject);
            }
        }
        if (collision.CompareTag("Cenario"))
        {
            Destroy(this.gameObject);
        }
        if (collision.CompareTag("ProjetilInimigo"))
        {
            Destroy(this.gameObject);
        }
    }
}

