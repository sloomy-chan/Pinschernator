using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDePause : MonoBehaviour
{
    public GameObject pauseMenu;
    public MoveJogador script;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
                script.pausado = false;
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
                script.pausado = true;
            }

        }     
    }
}