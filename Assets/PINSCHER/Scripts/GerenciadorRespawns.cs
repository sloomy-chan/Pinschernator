using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorRespawns : MonoBehaviour
{
    public MoveJogador JogadorCodigoBiscoitos;
    public GameObject[] biscoitos;
    
    public GameObject[] Inimigos;
    private Vector2[] posicaoInicial;
    public GameObject[] ItensVida;
  
    public dano playerCode;
    public Text textoBiscoito;

    // Start is called before the first frame update
    void Start()
    {
        // A array posicaoInicial copia a quantidade total dos Inimigos, isso é se são 5 ele repassa o total de slots para a posicaoinicial
        posicaoInicial = new Vector2[Inimigos.Length];
        for (int pos = 0; pos < Inimigos.Length; pos++)
        {

            posicaoInicial[pos] = Inimigos[pos].transform.position;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (JogadorCodigoBiscoitos.morto)
        {
  

            if (Input.GetKeyDown(KeyCode.F))
            {
                JogadorCodigoBiscoitos.morto = false;
                for (int x = 0; x < biscoitos.Length; x++)
                {
                    textoBiscoito.text = " ";
                    JogadorCodigoBiscoitos.QuantBiscoito = 0;
                    biscoitos[x].GetComponent<SpriteRenderer>().enabled = true;
                    biscoitos[x].GetComponent<CircleCollider2D>().enabled = true;
 

                }
                for (int y = 0; y < ItensVida.Length; y++)
                {
                    ItensVida[y].GetComponent<SpriteRenderer>().enabled = true;
                    ItensVida[y].GetComponent<CircleCollider2D>().enabled = true;

                }
                for (int ini = 0; ini < Inimigos.Length; ini++)
                {             
                    
                        Inimigos[ini].transform.position = posicaoInicial[ini];
                        Inimigos[ini].GetComponent<vidaDoInimigo>().vida_atual = 100;
                        Inimigos[ini].gameObject.SetActive(true);
                        Inimigos[ini].GetComponent<iaInimigo>().aggro = false;
                        Inimigos[ini].GetComponent<IaTank>().aggro = false;
                        Inimigos[ini].GetComponent<InimigoExplosivo>().aggro = false;


                }
            }

        }

    }
}
