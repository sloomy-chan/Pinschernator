using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilCode : MonoBehaviour
{
    public IaTank scriptTank;
    public MoveJogador jogador;
    public int danoMissil = -50;
    public Vector3 posicaoDoJogador;
    public ParticleSystem explosao;
    
   
    // Start is called before the first frame update
    void Start()
    {
        //Faz a particula da fumaça aparecer
        ParticleSystem explosao = this.GetComponentInChildren<ParticleSystem>();
        explosao.Pause(true);
        ParticleSystem particula = this.GetComponentInParent<ParticleSystem>();
        particula.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Faz o missil olhar pro jogador
        Vector3 miraNoAlvo = (posicaoDoJogador - this.transform.position).normalized;
       float angle = Mathf.Atan2(miraNoAlvo.y, miraNoAlvo.x) * Mathf.Rad2Deg;
        Vector3 PosicaoInicial = new Vector3(this.transform.position.x, this.transform.position.y);
        Vector3 posicaoPlayer = new Vector3(scriptTank.JogadorPos.transform.position.x, scriptTank.JogadorPos.transform.position.y);
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
        this.transform.position = Vector2.MoveTowards(PosicaoInicial, posicaoPlayer, Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (jogador.invincibilidade == false)
            {

                //Pega as partículas do objeto
    
                ParticleSystem particula = this.GetComponent<ParticleSystem>();
                var fumaca = particula.emission;

                //Dá o dano no jogador, e ativa e desativa as partículas necessárias
                jogador = collision.GetComponent<MoveJogador>();
                jogador.vidaAtual += danoMissil;
                this.GetComponent<SpriteRenderer>().enabled = false;
                this.GetComponent<BoxCollider2D>().enabled = false;
                fumaca.enabled = false;
                explosao.Play(true);
                Destroy(this.gameObject, 1f);
            }          
        }
       
        if (collision.CompareTag("projetil"))
        {
            ParticleSystem explosao = this.GetComponentInChildren<ParticleSystem>();
            ParticleSystem particula = this.GetComponent<ParticleSystem>();
            var fumaca = particula.emission;
            fumaca.enabled = false;
            explosao.Play(true);
            Destroy(this.gameObject, 1f);
        }
    }
}
