using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contrapeso : MonoBehaviour
{
    [SerializeField] bool contraPesoRompible;
    Animator animacionRompiendo;

    private void Start()
    {
        animacionRompiendo = this.gameObject.GetComponent<Animator>();
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
