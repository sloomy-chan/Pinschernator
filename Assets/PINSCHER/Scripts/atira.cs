using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class atira : MonoBehaviour
{
    public Rigidbody2D projetilPrefab;
    public float velocidadeProjetil = 400f;
    public SpriteRenderer jogador;
    public Transform origem;
    public Transform origemEsquerda;
    public float tempoTotal = 2.5f;
    public Rigidbody2D granadaPrefab;
    public float forcaGranada;
    public float timer;
    public float timerTotal;
    public bool iniciarTimer;
    public CircleCollider2D explosao_trigger;
    public bool explodir;
    public vidaDoInimigo vidaInimigo;
    public int danoExplosao;
    public float tempoExplosao;
    public float TempoExplosaoTotal;
    public Image GranadaOff;
    public Animator animacao;
    public bool atirando;
    public AudioSource tiro, somGranada;
    private ParticleSystem fumaca, explosao;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        explosao_trigger.enabled = false;

        //Atira as balas normais ;D
        if (Input.GetKeyDown(KeyCode.O) && (jogador.flipX == false))
        {
            tiro.Play(0);
            Rigidbody2D Tiro = Instantiate(projetilPrefab, origem.position, transform.rotation);
            Tiro.AddForce(this.transform.right * velocidadeProjetil, ForceMode2D.Impulse);
                Destroy(Tiro.gameObject, tempoTotal);
        }
  
        if (Input.GetKeyDown(KeyCode.O) && (jogador.flipX == true))
        {
            tiro.Play(0);
            atirando = true;
            Rigidbody2D Tiro = Instantiate(projetilPrefab, origemEsquerda.position, transform.rotation);
            Tiro.AddForce(-this.transform.right * velocidadeProjetil, ForceMode2D.Impulse);     
                Destroy(Tiro.gameObject, tempoTotal);
        }
          
            //Atira as granadas :000
            if (timer == 3f)
            {
               if (Input.GetKeyDown(KeyCode.I) && (jogador.flipX == false))
               {
                Rigidbody2D granada = Instantiate(granadaPrefab, origem.position, transform.rotation);
                granada.AddForce(this.transform.right * forcaGranada + this.transform.up, ForceMode2D.Impulse);
                fumaca = granada.GetComponent<CodigoGranada>().fumaca;
                explosao = granada.GetComponent<CodigoGranada>().explosao;
                iniciarTimer = true;               
                    Destroy(granada.GetComponent<SpriteRenderer>(), 3f);
                    Destroy(granada.GetComponent<CapsuleCollider2D>(), 3f);
            }
                if (Input.GetKeyDown(KeyCode.I) && (jogador.flipX == true))
                {
                    Rigidbody2D granada = Instantiate(granadaPrefab, origemEsquerda.position, transform.rotation);       
                    granada.AddForce(-this.transform.right * forcaGranada + this.transform.up, ForceMode2D.Impulse);
                    iniciarTimer = true;
                    Destroy(granada.GetComponent<SpriteRenderer>(), 3f);
                    Destroy(granada.GetComponent<CapsuleCollider2D>(), 3f);
            }
            }
     //inicia o timer da explosão    
        if (iniciarTimer == true)
        {
            timer -= 1f * Time.deltaTime;
            if (timer <= 0f)
            {
                explosao.Play();
                fumaca.Play();
                somGranada.Play(0);
                timer = timerTotal;
                iniciarTimer = false;
                explodir = true;
                GranadaOff.enabled = false;
            }
            else
            {
                GranadaOff.enabled = true;
                explodir = false;
            }
        }
     
     //KILLER QUEEN!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (explodir == true)
        {
            explosao_trigger.enabled = true;
            tempoExplosao -= 1f * Time.deltaTime;
            if (tempoExplosao <= 0f)
            {
                explosao_trigger.enabled = false;
                explodir = false;
                tempoExplosao = TempoExplosaoTotal;
            }
        }
       
    }
}


