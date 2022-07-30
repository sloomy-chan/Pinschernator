using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoExplosivo : MonoBehaviour
{
    public bool explodir;
    public int acaoInimigo;
    public bool ativarCrono;
    public float tempoParaDecidir;
    public GameObject Inimigo;
    public Animator animacao;
    public ParticleSystem fumacaPart;
    public AudioSource somExplosao;

    public float tempoDestruirProj;
    public Transform posicaoA;
    public Transform posicaoB;
    public SpriteRenderer robo;
    public bool aggro;
    public bool parado, andando;
    public int danoExplosivo;
    public bool flipX;

    public float velocidade = 1;
    public float tempoMovimento;
    public bool reverterMovimento = false;
    public float tempoMaximoDeEspera = 2;
    public float tempoDeEspera = 0;
    public int repeticaoDePatrulha = 0;
    public int repeticaoMaxDePatrulha = 3;
    public Rigidbody2D corpo;

    public GameObject jogadorObj;
    public SpriteRenderer sprite;
    public Rigidbody2D playerCol;
    public Transform JogadorPos;
    public MoveJogador player;
    public float velocidadePerseguir;
    public CircleCollider2D trigger_interno;
    public int impulsoDanoExp;
    public dano dano;
    public CircleCollider2D triggerExterno;
    public CapsuleCollider2D corpoNormal;
    public BoxCollider2D corpoTrigger;

    /*
   -1 - vazio
    0 - atacar
    1 - patrulha
    2 - parado
    */

    // Start is called before the first frame update
    void Start()
    {
        ativarCrono = true;
        acaoInimigo = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //controla o sprite flip
        if (JogadorPos.transform.position.x > this.transform.position.x)
        {
            robo.flipX = true;
        }
        else
        {
            robo.flipX = false;
        }

        if (aggro == true)
        {
            Perseguir();
        }

        if (acaoInimigo == 1)
        {
            Patrulhando();
        }
        if (acaoInimigo == 2)
        {     
            Resetar();
        }

        if (acaoInimigo >= 3)
        {
            Resetar();
        }

        /* ------ */
        if (ativarCrono == true)
        {
            Cronometro();
        }
    }

    private void OnTriggerEnter2D(Collider2D outroObjeto)
    {
        if (outroObjeto.CompareTag("Player"))
        {
            aggro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D outroObjeto)
    {
        if (outroObjeto.CompareTag("Player"))
        {
            Perseguir();
            Resetar();
        }
    }

    void Cronometro()
    {
        if (aggro == true)
        {
            ativarCrono = false;
        }
        else
        {
            ativarCrono = true;
        }
        if (tempoParaDecidir > 0)
        {
            tempoParaDecidir -= 1 * Time.deltaTime;
        }
        if (tempoParaDecidir <= 0)
        {
            acaoInimigo = Random.Range(1, 5);
            ativarCrono = false;
        }
    }

    void Patrulhando()
    {
        if (repeticaoDePatrulha < repeticaoMaxDePatrulha)
        {
            if (tempoDeEspera <= 0)
            {
                Vector2 posicaoFinalA = new Vector2(posicaoA.position.x, this.transform.position.y);
                Vector2 posicaoFinalB = new Vector2(posicaoB.position.x, this.transform.position.y);
                tempoDeMovimento();
                this.transform.position = Vector2.Lerp(posicaoFinalA, posicaoFinalB, tempoMovimento);
                tempoDeEspera = 0;
            }
            else
            {
                tempoDeEspera -= Time.deltaTime;
            }
        }

        if (repeticaoDePatrulha >= repeticaoMaxDePatrulha && ativarCrono == false && tempoParaDecidir <= 0)
        {
            ativarCrono = true;
            tempoParaDecidir = 6;
            repeticaoDePatrulha = 0;
        }
    }
    void Resetar()
    {
        tempoParaDecidir = 6;
        ativarCrono = true;
        repeticaoDePatrulha = 0;
        acaoInimigo = -1;
    }

    void tempoDeMovimento()
    {
        if (reverterMovimento == false)
        {
            robo.flipX = true;
            if (tempoMovimento < 1)
            {
                tempoMovimento += Time.deltaTime / velocidade;
            }
            else
            {
                tempoMovimento = 1;
                tempoDeEspera = tempoMaximoDeEspera;
                reverterMovimento = true;
                repeticaoDePatrulha++;
                robo.flipX = false;
            }
        }
        else
        {
            if (tempoMovimento > 0)
            {
                tempoMovimento -= Time.deltaTime / velocidade;
            }
            else
            {
                tempoMovimento = 0;
                tempoDeEspera = tempoMaximoDeEspera;
                reverterMovimento = false;
                repeticaoDePatrulha++;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Perseguir();
    }
    public void Explodir()
    {
        explodir = true;
        if(explodir == true)
        {
            player.vidaAtual += danoExplosivo;

            if (sprite.flipX == true)
            {
                playerCol.AddForce(playerCol.transform.right * impulsoDanoExp, ForceMode2D.Impulse);
                playerCol.AddForce(playerCol.transform.up * impulsoDanoExp, ForceMode2D.Impulse);
            }
            else
            {
                playerCol.AddForce(-playerCol.transform.right * impulsoDanoExp, ForceMode2D.Impulse);
                playerCol.AddForce(playerCol.transform.up * impulsoDanoExp, ForceMode2D.Impulse);
            }

            ParticleSystem ParticulaExplosao = this.GetComponent<ParticleSystem>();

            ParticulaExplosao.Play(true);
            fumacaPart.Play(true);

            somExplosao.Play(0);
            dano.invencivel = true;
            triggerExterno.enabled = false;
            corpoNormal.enabled = false;
            corpoTrigger.enabled = false;
            sprite.enabled = false;
        }     
    }


    void Perseguir()
    {
        if(explodir == false)
        {
            animacao.SetBool("Aggro", aggro);
            Vector2 PosicaoInicial = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 PosicaoFinal = new Vector2(JogadorPos.transform.position.x, JogadorPos.transform.position.y);
            //Vector2 Distancia = new Vector2(PosicaoFinal.x - 5f, this.transform.position.y);
            this.transform.position = Vector2.MoveTowards(PosicaoInicial, PosicaoFinal, Time.deltaTime);
        }
    }
}