using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MoveJogador : MonoBehaviour
{


    //variáveis base
    public Text TextoVidas;
    public int QuantVidas;
    public int vidaAtual;
    public int vidaMaxima;
    public int Velocidade = 2;
    public float velocidadeX;
    public float velocidadeY;
    public Rigidbody2D Corpo;
    public bool pulando = false;
    public int ForcaDoPulo = 10;
    public SpriteRenderer sprite;
    public int QuantBiscoito;
    public iaInimigo inimigoCode;
    public bool invincibilidade;
     public GameObject telaGameOver;

    //variáveis da animação
    public bool puloAnimation;
    public bool puloCaindo;
    public bool DandoDash;
    public Animator animacao;
    public bool EmMovimento;
    public bool flipX;
    public bool dashAni;
    public bool animationDash;
    public Image barraVida;

    //variáveis de dash
    public float ForcaDoDash = 4.5f;
    public bool Dando_dash = false;
    public float tempo_atual = 2f;
    public float tempo_total = 2f;
    public float tempo_animDash;
    public bool pausado;
    public AudioSource Pular;
    public bool morto = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        TextoVidas.text = QuantVidas.ToString();
        if (pausado == false)
        {
            if(telaGameOver.activeSelf && Input.anyKeyDown)
            {
                Time.timeScale = 1;
                SceneManager.LoadSceneAsync("cenaMenu");
            }

            ParticleSystem particulaPoeira = GetComponent<ParticleSystem>();
            var emission = particulaPoeira.emission;
            //controles, e o sprite virar de acordo com a direção          
            if (vidaAtual >= 0)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    Corpo.AddForce(this.transform.right * Velocidade, ForceMode2D.Force);
                    sprite.flipX = false;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    Corpo.AddForce(-this.transform.right * Velocidade, ForceMode2D.Force);
                    sprite.flipX = true;
                }
            }

            //Controla as particulas da poeira
            if (Corpo.velocity.x != 0f)
            {
                emission.enabled = true;
            }
            else
            {
                emission.enabled = false;
            }
            if (Corpo.velocity.y != 0f)
            {
                emission.enabled = false;
            }

            //pulo
            if (vidaAtual > 0)
            {
                if (Input.GetKey(KeyCode.W))
                {

                    if (pulando == false)
                    {
                        Pular.Play(0);
                        Corpo.AddForce(this.transform.up * ForcaDoPulo, ForceMode2D.Impulse);
                        pulando = true;
                    }
                }
                else
                {
                    Pular.Stop();
                    pulando = false;
                }
            }


            //O que controla quando você tá pulando ou não(pro jogador não pular infinitamente)
            if (Corpo.velocity.y != 0)
            {
                pulando = true;
            }

            //sistema das animações
            //ANDANDO
            if (pulando == false)
            {
                if (Corpo.velocity.x != 0)
                {
                    EmMovimento = true;
                }
                else
                {
                    EmMovimento = false;
                }
                velocidadeX = Corpo.velocity.SqrMagnitude();
                velocidadeX = Corpo.velocity.x;
                animacao.SetBool("EmMovimento", EmMovimento);
            }

            //PULANDO
            if (pulando == true)
            {
                puloAnimation = true;
            }
            else
            {
                puloAnimation = false;
            }
            velocidadeY = Corpo.velocity.y;
            animacao.SetBool("puloAnimation", puloAnimation);

            //animacaoDash
            if (dashAni == true)
            {
                invincibilidade = true;
                tempo_animDash = tempo_animDash - 1f * Time.deltaTime;
            }
            else
            {
                invincibilidade = false;
            }
            animacao.SetBool("dashAni", dashAni);
            if (tempo_animDash <= 0f)
            {
                dashAni = false;
                tempo_animDash = 0.5f;
            }


            //Dashes
            //Dash pra direita
            if (Dando_dash == false)
            {
                if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.P))
                {
                    dashAni = true;
                    Corpo.AddForce(this.transform.right * ForcaDoDash, ForceMode2D.Impulse);
                    Dando_dash = true;
                }


                //Dash pra esquerda
                if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.P))
                {
                    dashAni = true;
                    Corpo.AddForce(-this.transform.right * ForcaDoDash, ForceMode2D.Impulse);
                    Dando_dash = true;
                }
            }
            //Se o jogador cair, ele volta na posição inicial
            if (this.transform.position.y < -2.5f)
            {
                this.transform.position = new Vector2(0.0f, 0.0f);
                QuantVidas -= 1;
            }


            //game over
            if (QuantVidas < 0)
            {
                telaGameOver.SetActive(true);
            }
            else
            {
                telaGameOver.SetActive(false);
            }


            if (telaGameOver.activeSelf)
            {
                Time.timeScale = 0;
            }

            //Contador de tempo (do dash)
            if (tempo_atual > 0 && tempo_atual <= tempo_total && Dando_dash == true)
            {
                tempo_atual = tempo_atual - 1.0f * Time.deltaTime;
            }

            if (tempo_atual <= 0)
            {
                Dando_dash = false;
                tempo_atual = tempo_total;
            }
        }
        //controla a barra de vida
        if (vidaAtual == vidaMaxima / 4)
        {
            barraVida.fillAmount = 0.25f;
        }
        if (vidaAtual == vidaMaxima / 2)
        {
            barraVida.fillAmount = 0.50f;
        }
        if (vidaAtual == vidaMaxima - 25)
        {
            barraVida.fillAmount = 0.75f;
        }
        if (vidaAtual == vidaMaxima)
        {
            barraVida.fillAmount = 1;
        }
    }


    private void OnTriggerEnter2D(Collider2D outroObjeto)
    {
        if (outroObjeto.CompareTag("Enemy"))
        {
            if (outroObjeto is CircleCollider2D)
            {
                inimigoCode.aggro = true;
                inimigoCode.acaoInimigo = 0;
                inimigoCode.Atacar();
            }
        }
    }
}