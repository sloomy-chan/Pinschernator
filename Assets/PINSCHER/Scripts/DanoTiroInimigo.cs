using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanoTiroInimigo : MonoBehaviour
{
    public MoveJogador player;
    public Rigidbody2D Player;
    public int danoProjetil;
    public dano script;
    public float impulsoDanoTiro;
    public iaInimigo scriptIa;
    public Rigidbody2D corpo;
    public Rigidbody2D Inimigo;
    // Start is called before the first frame update
    void Start()
    {
      corpo.AddForce(this.transform.right * scriptIa.velocidadeProj, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision is BoxCollider2D)
            {
                if (player.invincibilidade == false)
                {
                    player.vidaAtual = player.vidaAtual + danoProjetil;

                    if (player.sprite.flipX == true)
                    {
                        Player.AddForce(Player.transform.right * impulsoDanoTiro, ForceMode2D.Impulse);
                        Player.AddForce(Player.transform.up * impulsoDanoTiro, ForceMode2D.Impulse);
                    }
                    else
                    {
                        Player.AddForce(-Player.transform.right * impulsoDanoTiro, ForceMode2D.Impulse);
                        Player.AddForce(Player.transform.up * impulsoDanoTiro, ForceMode2D.Impulse);
                    }
                    script.invencivel = true;
                    Destroy(this.gameObject);
                }
            }
        }
        if (collision.CompareTag("Cenario"))
        {
            Destroy(this.gameObject);
        }
    }
}
