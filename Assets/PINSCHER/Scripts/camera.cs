using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Rigidbody2D target;
    public float smoothSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);


        /*
        Vector3 posicaoplayer = target.position;
        Vector3 posicao = this.transform.position;
        Vector3 transicaoCamera = Vector3.Lerp(posicao, posicaoplayer, Mathf.Abs(target.velocity.x));
        /*

        this.transform.position = new Vector3(transicaoCamera.x, transicaoCamera.y, this.transform.position.z);

       /* if (target.velocity.x < 0)
        { 
            transform.position = new Vector3(transicaoCamera.x - 0.4f, transicaoCamera.y, transform.position.z); 
        }
        if (target.velocity.x > 0)
        {
            transform.position = new Vector3(transicaoCamera.x + 0.4f, transicaoCamera.y, transform.position.z);
        }
        */

        float step = smoothSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, (new Vector3(target.position.x, target.position.y, this.transform.position.z)), step);
    }
}
