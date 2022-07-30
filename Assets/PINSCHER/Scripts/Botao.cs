using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Botao : MonoBehaviour

{
    public string nomeDaCena;
    public Button botaoStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   //     Physics.queriesHitTriggers = true;
    }

   public void Click()
    {
        SceneManager.LoadSceneAsync(nomeDaCena);
        Debug.Log("Apertou o botão");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
