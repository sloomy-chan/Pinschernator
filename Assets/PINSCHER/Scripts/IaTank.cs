using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaTank : MonoBehaviour
{
    public float cooldownMissil;
    public int acaoInimigo;
    public bool ativarCrono;
    public float tempoParaDecidir;
    public GameObject Inimigo;
    public bool atirando;
    public Animator animacaoAtirar, animacaoAndar;
    public bool atiraMissil;
    public AudioSource andarSom;

    public float tempoDestruirProj;
    public Transform posicaoA;
    public Transform posicaoB;
    public GameObject MissilPrefab;
    public Transform posicaoDoTiro;
    public float velocidadeProj;
    public SpriteRenderer robo;  
    public bool aggro;
    public bool parado, andando;
    public bool paraPraAtirar;
    public float tempoPraAtirar;
    public float tempoAtirarMax;


    public bool NoChao;
    public float velocidade = 1;
    public float tempoMovimento;
    public bool reverterMovimento = false;
    public float tempoMaximoDeEspera = 2;
    public float tempoDeEspera = 0;
    public int repeticaoDePatrulha = 0;
    public int repeticaoMaxDePatrulha = 3;
    public Rigidbody2D corpo;
    public ParticleSystem ExplosaoParticula;

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
        if (cooldownMissil <= 1.5f)
        {
            atiraMissil = true;
        }
        else
        {
            atiraMissil = false;
        }

        animacaoAtirar.SetBool("atiraMissil", atiraMissil);
            animacaoAndar.SetBool("andando", andando);
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
            andando = true;
            //Debug.Log("Inimigo Patrulhando");
        }
        if (acaoInimigo == 2)
        {
            Debug.Log("Inimigo Parado");
            Resetar();
        }
        if (aggro == true)
        {
            cooldownMissil -= 1f * Time.deltaTime;
            AtirarMissil();
        }

        if (acaoInimigo >= 3)
        {
            Debug.Log("Fazer outra coisa");
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
            if (transform.position.x >= posicaoA.position.x && transform.position.x <= posicaoB.position.x)
            {
                Debug.Log("dentro da area de patrulha");
            }
            else
            {
                Debug.Log("fora da area de patrulha");
            }

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
            Debug.Log("Escolheu a acao " + acaoInimigo);
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
                andarSom.Play(0);
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

    public void AtirarMissil()
    {
        if (cooldownMissil <= 0.2f)
        {       
            Vector3 posicaoDoJogador = player.transform.position;
            Vector3 miraNoAlvo = (posicaoDoJogador - this.transform.position).normalized;
            float angle = Mathf.Atan2(miraNoAlvo.y, miraNoAlvo.x) * Mathf.Rad2Deg;
            GameObject missil = Instantiate(MissilPrefab, posicaoDoTiro.position, Quaternion.Euler(0,0, angle));
            ExplosaoParticula.transform.position = missil.transform.position;
            missil.GetComponent<MissilCode>().explosao = ExplosaoParticula;
            missil.GetComponent<MissilCode>().scriptTank = this;
            missil.GetComponent<MissilCode>().jogador = player;
            missil.GetComponent<MissilCode>().posicaoDoJogador = posicaoDoJogador;
            cooldownMissil = 5f;          
        }
    }


    void Perseguir()
    {
        tempoPraAtirar -= 1f * Time.deltaTime;
        andando = true;
        andarSom.Play(0);
        if (tempoPraAtirar <= 0f)
        {
            andarSom.Pause();
            paraPraAtirar = true;
            andando = false;
        }
        if (aggro == true && paraPraAtirar == false)
        {   
            Vector2 PosicaoInicial = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 PosicaoFinal = new Vector2(JogadorPos.transform.position.x, this.transform.position.y);
            this.transform.position = Vector2.MoveTowards(PosicaoInicial, PosicaoFinal, Time.deltaTime);
        }
    }
}