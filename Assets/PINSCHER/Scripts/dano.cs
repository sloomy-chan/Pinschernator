using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dano : MonoBehaviour
{
    public float impulsoDano;
    public MoveJogador script;
    public int damage;
    public Rigidbody2D Player;
    public float tempoInvi;
    public bool invencivel;
    public GameObject telaMorte;
 

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        //respawn do jogador
        if (script.QuantVidas > 0)
        {
            if (script.vidaAtual <= 0)
            {
                telaMorte.SetActive(true);
                script.morto = true;
            }
            if (telaMorte.activeSelf && Input.GetKeyDown(KeyCode.F))
            {
                script.QuantVidas -= 1;
                telaMorte.SetActive(false);
                Player.transform.position = new Vector2(-1.738f, 0.212f);
                script.vidaAtual = 100;
            }

        }

        //"invincibilidade"
        if (invencivel == true)
        {
            tempoInvi = tempoInvi - 1.0f * Time.deltaTime;
            if (tempoInvi > 0f)
            {
                //Inserir aqui a animação/feedback de invencível
            }
            if (tempoInvi <= 0f)
            {
                tempoInvi = 1f;
                invencivel = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        //faz o jogador tomar dano, e um knockback pra trás
        if (outro.CompareTag("Enemy"))       
        {
            if (outro is BoxCollider2D)
            {        
                if (invencivel == false)
                {
                    if(script.invincibilidade == false)
                    {
                        script.vidaAtual = script.vidaAtual + damage;
                        if (script.sprite.flipX == true)
                        {
                            Player.AddForce(Player.transform.right * impulsoDano, ForceMode2D.Impulse);
                            Player.AddForce(Player.transform.up * impulsoDano, ForceMode2D.Impulse);
                        }
                        else
                        {
                            Player.AddForce(-Player.transform.right * impulsoDano, ForceMode2D.Impulse);
                            Player.AddForce(Player.transform.up * impulsoDano, ForceMode2D.Impulse);
                        }
                    }            
                }
            }
        }      
    }
}
