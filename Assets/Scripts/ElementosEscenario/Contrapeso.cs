using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contrapeso : MonoBehaviour
{
    [SerializeField] bool contraPesoRompible;
    Animator animacionRompiendo;
    float startx;
    public bool subiendo;
    public float velocidadSubida;

    private void Start()
    {
        startx = this.transform.position.x;
        animacionRompiendo = this.gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (subiendo)
        {
        GetComponent<Rigidbody2D>().velocity = (Vector2.up *velocidadSubida );
        }
        this.transform.position = new Vector2(startx, this.transform.position.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (contraPesoRompible)
        {
            if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {

                animacionRompiendo.Play("ContrapesoRompiendo");

            }
        }
        
    }
    
}
