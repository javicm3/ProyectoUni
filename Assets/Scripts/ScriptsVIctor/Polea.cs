using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polea : MonoBehaviour
{

    ControllerPersonaje CP;
    public Animator animCuerda;
    public Animator animGancho;
    public Animator animPuerta;


    public GameObject escombroPrimero;
    public GameObject puerta;
    public bool primerovaArriba = false;
    public float velocidadPrimeroArriba = 5;
    public GameObject escombroSegundo;
    public bool segundovaArriba = false;
    public float velocidadSegundoArriba = 5;
    //public bool seRompe;
    public float massCae = 20f;
    public float grav = 20f;

    float impulse = 1000f;

    void Update()
    {
        if (escombroPrimero == null)
            return;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.2f)
            {
                FindObjectOfType<NewAudioManager>().Play("CortarCable");
                if (animCuerda != null) animCuerda.SetTrigger("DashHecho");
                if (animPuerta != null)
                {
                    animPuerta.SetTrigger("DashHecho");
                    puerta.GetComponent<BoxCollider2D>().enabled = false;
                }

                if (escombroSegundo != null)
                {
                    if (segundovaArriba)
                    {
                        if (escombroSegundo.GetComponent<Contrapeso>() != null && escombroSegundo.GetComponent<Contrapeso>().subiendo == false) {
                           
                            escombroSegundo.GetComponent<Contrapeso>().velocidadSubida = velocidadSegundoArriba;
                            escombroSegundo.GetComponent<Contrapeso>().subiendo = true;
                        }
                        escombroSegundo.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        escombroSegundo.GetComponent<Rigidbody2D>().velocity = (Vector2.up * velocidadPrimeroArriba);
                        escombroSegundo.GetComponent<Rigidbody2D>().gravityScale = 0f;
                    }
                    else
                    {
                        escombroSegundo.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        escombroSegundo.GetComponent<Rigidbody2D>().mass = massCae;
                        escombroSegundo.GetComponent<Rigidbody2D>().gravityScale = grav;
                    }


                    if (animGancho != null && animGancho.GetBool("Escombros") == true) animGancho.SetTrigger("DashHecho");
                    else if (animGancho != null && animGancho.GetBool("Escombros") == false) animGancho.SetTrigger("DashHecho");
                }

                if (escombroPrimero != null)
                {
                    if (primerovaArriba)
                    {
                        if (escombroPrimero.GetComponent<Contrapeso>() != null && escombroPrimero.GetComponent<Contrapeso>().subiendo == false) {
                            escombroPrimero.GetComponent<Contrapeso>().velocidadSubida = velocidadPrimeroArriba;
                            escombroPrimero.GetComponent<Contrapeso>().subiendo = true;

                        }
                        escombroPrimero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        //escombroPrimero.GetComponent<BoxCollider2D>().enabled = false;
                        escombroPrimero.GetComponent<Rigidbody2D>().gravityScale = 0f;
                        escombroPrimero.GetComponent<Rigidbody2D>().velocity = (Vector2.up * velocidadSegundoArriba);
                    }
                    else
                    {

                        escombroPrimero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        escombroPrimero.GetComponent<Rigidbody2D>().mass = massCae;
                        escombroPrimero.GetComponent<Rigidbody2D>().gravityScale = grav;
                    }

                }
                //if (seRompe)
                //{
                //    Destroy(this.gameObject);
                //}

            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.2f)
            {
                if (animCuerda != null) animCuerda.SetTrigger("DashHecho");

                if (escombroSegundo != null)
                {
                    if (segundovaArriba)
                    {
                        if (escombroSegundo.GetComponent<Contrapeso>() != null && escombroSegundo.GetComponent<Contrapeso>().subiendo == false)
                        {

                            escombroSegundo.GetComponent<Contrapeso>().velocidadSubida = velocidadSegundoArriba;
                            escombroSegundo.GetComponent<Contrapeso>().subiendo = true;
                        }
                        escombroSegundo.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        escombroSegundo.GetComponent<Rigidbody2D>().velocity = (Vector2.up * velocidadPrimeroArriba);
                        escombroSegundo.GetComponent<Rigidbody2D>().gravityScale = 0f;
                    }
                    else
                    {
                        escombroSegundo.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        escombroSegundo.GetComponent<Rigidbody2D>().mass = massCae;
                        escombroSegundo.GetComponent<Rigidbody2D>().gravityScale = grav;
                    }


                    if (animGancho != null && animGancho.GetBool("Escombros") == true) animGancho.SetTrigger("DashHecho");
                    else if (animGancho != null && animGancho.GetBool("Escombros") == false) animGancho.SetTrigger("DashHecho");
                }

                if (escombroPrimero != null)
                {
                    if (primerovaArriba)
                    {
                        if (escombroPrimero.GetComponent<Contrapeso>() != null && escombroPrimero.GetComponent<Contrapeso>().subiendo == false)
                        {
                            escombroPrimero.GetComponent<Contrapeso>().velocidadSubida = velocidadPrimeroArriba;
                            escombroPrimero.GetComponent<Contrapeso>().subiendo = true;

                        }
                        escombroPrimero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        //escombroPrimero.GetComponent<BoxCollider2D>().enabled = false;
                        escombroPrimero.GetComponent<Rigidbody2D>().gravityScale = 0f;
                        escombroPrimero.GetComponent<Rigidbody2D>().velocity = (Vector2.up * velocidadSegundoArriba);
                    }
                    else
                    {

                        escombroPrimero.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        escombroPrimero.GetComponent<Rigidbody2D>().mass = massCae;
                        escombroPrimero.GetComponent<Rigidbody2D>().gravityScale = grav;
                    }

                }
                //if (seRompe)
                //{
                //    Destroy(this.gameObject);
                //}

            }
        }
    }
}
