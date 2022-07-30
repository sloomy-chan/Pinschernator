using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class parallax : MonoBehaviour
{

    public Transform target;
    public float distancia;


    private float lastX, lastY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = transform.position.y + (target.transform.position.y - lastY) * distancia;
        lastY = target.transform.position.y;
        float x = transform.position.x + (target.transform.position.x - lastX) * distancia;
        lastX = target.transform.position.x;
    }

    void LateUpdate()
    {
        this.transform.position = new Vector3(lastX, lastY, transform.position.z);
    }
}
