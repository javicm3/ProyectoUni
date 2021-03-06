﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contrapeso : MonoBehaviour
{
    [SerializeField] bool contraPesoRompible;
    Animator animacionRompiendo;
    float startx;
    public bool subiendo;
    public float velocidadSubida;
    public GameObject particulasPolvo;
    Vector3 posicionParticulas;
    public bool seDesliza = false;
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 4);
        startx = this.transform.position.x;
        animacionRompiendo = this.gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        if (subiendo)
        {
        GetComponent<Rigidbody2D>().velocity = (Vector2.up *velocidadSubida );
         
        }
     if(!seDesliza)   this.transform.position = new Vector2(startx, this.transform.position.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (contraPesoRompible)
        {
            if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                animacionRompiendo.Play("ContrapesoRompiendo");
                posicionParticulas = collision.contacts[0].point;
                FindObjectOfType<NewAudioManager>().Play("CaídaEscombroRoto");

            }
        }
        if (subiendo)
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                if (GetComponent<Rigidbody2D>().velocity.y < 1)
                {
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    velocidadSubida = 0;
                }


            }

        }
        else
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                if (GetComponent<Rigidbody2D>().velocity.y < 1f)
                {
                    if (!seDesliza) { GetComponent<Rigidbody2D>().isKinematic = true; } else { GetComponent<Rigidbody2D>().isKinematic = false; }
                    if (!seDesliza) FindObjectOfType<NewAudioManager>().Play("CaídaEscombros");
                }
               
            }
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (contraPesoRompible)
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                animacionRompiendo.Play("ContrapesoRompiendo");
                posicionParticulas = collision.contacts[0].point;
                FindObjectOfType<NewAudioManager>().Play("CaídaEscombroRoto");

            }
        }
        if (subiendo)
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                if (GetComponent<Rigidbody2D>().velocity.y < 1)
                {
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    velocidadSubida = 0;
                }


            }

        }
        else
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                if (GetComponent<Rigidbody2D>().velocity.y < 1f)
                {
                    if (!seDesliza) { GetComponent<Rigidbody2D>().isKinematic = true; } else { GetComponent<Rigidbody2D>().isKinematic = false; }
                    if (!seDesliza) FindObjectOfType<NewAudioManager>().Play("CaídaEscombros");
                }

            }
        }

    }
    public void InstanciarParticulasEscombro()
    {
        //Instantiate(particulasPolvo, transform.position,  Quaternion.identity);
        Instantiate(particulasPolvo, posicionParticulas, Quaternion.identity);
    }
    
}
