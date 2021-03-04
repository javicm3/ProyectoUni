using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Rata : MonoBehaviour
{
    Transform posicionInicial;
    [SerializeField] float speed; 
    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = this.transform; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.transform.position = posicionInicial.position; 
    }
}
