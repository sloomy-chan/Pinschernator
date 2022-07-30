using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodigoGranada : MonoBehaviour
{
    public vidaDoInimigo vidaInimigo;
    public atira scriptDano;
    public VidaInimigoExplosivo VidaExplosivo;
    public ParticleSystem fumaca, explosao;

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
            if (collision is CapsuleCollider2D)
            {
                scriptDano.timer = 0;
                vidaInimigo.vida_atual += scriptDano.danoExplosao;
                VidaExplosivo.vida_atual += scriptDano.danoExplosao;
            }
        }
    }
        
}
