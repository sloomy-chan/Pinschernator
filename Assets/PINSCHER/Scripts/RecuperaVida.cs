using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuperaVida : MonoBehaviour
{
    public MoveJogador jogador;
    public int valorVida;
    public SpriteRenderer imagem;
    public CircleCollider2D colisao;
    public AudioSource somVida;
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
        if(collision.CompareTag("Player"))
        {
            if (jogador.vidaAtual != 100)
            {
                somVida.Play(0);
                colisao.enabled = false;
                imagem.enabled = false;
                jogador.vidaAtual += valorVida;
            }         
        }
    }
}
