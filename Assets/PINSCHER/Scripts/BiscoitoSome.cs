using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiscoitoSome : MonoBehaviour

{
    public AudioSource somBiscoito;
    CircleCollider2D colisao; 
    public bool isTrigger = true;
    public Text CaixaTexto;
    private CircleCollider2D trigger;
    private SpriteRenderer imagem;
    public MoveJogador jogador;
    public int valorBiscoito = 1;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<CircleCollider2D>();
        imagem = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void OnTriggerEnter2D(Collider2D objeto)
    {
            if(objeto is BoxCollider2D)
            {
               this.GetComponent<ParticleSystem>().Play(true);             
                jogador.QuantBiscoito += valorBiscoito;
                somBiscoito.Play(0);
                CaixaTexto.text = jogador.QuantBiscoito.ToString();
                imagem.enabled = false;
                trigger.enabled = false;
            }
        
    }

}












