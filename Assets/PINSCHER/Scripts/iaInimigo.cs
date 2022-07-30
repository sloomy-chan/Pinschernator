using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iaInimigo : MonoBehaviour
{
    public AudioSource tiroSom;
    public int acaoInimigo;
    public bool ativarCrono;
    public float tempoParaDecidir;
    public GameObject Inimigo;
    public bool atirando;
    public Animator animacaoAtirar;
 
    public float tempoDestruirProj;
    public Transform posicaoA;
    public Transform posicaoB;
    public GameObject projetil;
    public Transform posicaoDoTiro;
    public float velocidadeProj;
    public SpriteRenderer robo;
    public float QuantTiro;
    public float cooldownTiro;
    public float cooldownTiroTotal;
    public bool aggro;
    public float cooldownMenor;
    public float cooldownMenorTotal;
    public bool parado, andando;

    public float velocidade = 1;
    public float tempoMovimento;
    public bool reverterMovimento = false;
    public float tempoMaximoDeEspera = 2;
    public float tempoDeEspera = 0;
    public int repeticaoDePatrulha = 0;
    public int repeticaoMaxDePatrulha = 3;
    public Rigidbody2D corpo;

    public Transform JogadorPos;
    public MoveJogador player;
    public float velocidadePerseguir;

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
        //controla o intervalo entre os tiros
        if (QuantTiro >= 3)
        {
            cooldownTiro -= 1f * Time.deltaTime;
            if (cooldownTiro <= 0f)
            {
                cooldownTiro = cooldownTiroTotal;
                QuantTiro = 0;
            }
        }
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
        if (acaoInimigo == 0)
        {
            Atacar();
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
            acaoInimigo = 0;
            Atacar();
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
        if(aggro == true)
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

   
    public void Resetar()
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

    public void Atacar()
    {
        cooldownMenor -= 1f * Time.deltaTime;

        if (cooldownTiro == cooldownTiroTotal)
            {
            if (cooldownMenor <= 0f)
               {
                tiroSom.Play(0);
                atirando = true;
                Vector3 posicaoPlayer = player.transform.position;
                Vector3 miraNoAlvo = (posicaoPlayer - transform.position).normalized;
                float angle = Mathf.Atan2(miraNoAlvo.y, miraNoAlvo.x) * Mathf.Rad2Deg;
                GameObject tiro = Instantiate(projetil, posicaoDoTiro.position, Quaternion.Euler(0, 0, angle));
                Destroy(tiro, tempoDestruirProj);
                QuantTiro += 1;
                cooldownMenor = cooldownMenorTotal;
               }
            else
            {
                atirando = false;
            }

            }
        animacaoAtirar.SetBool("atirando", atirando);
    }


    void Perseguir()
    {
        if (aggro == true)
        {
            Vector2 PosicaoInicial = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 PosicaoFinal = new Vector2(JogadorPos.transform.position.x, JogadorPos.transform.position.y);      
            this.transform.position = Vector2.MoveTowards(PosicaoInicial, PosicaoFinal, Time.deltaTime);
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
}