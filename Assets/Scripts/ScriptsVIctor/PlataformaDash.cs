using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaDash : MonoBehaviour
{
    public bool activado;
    public bool playerporencimaunavez;
    public bool collideractivado = true;
    GameObject player;

    public Transform puntoArriba;
    public Transform puntoArribaTodo;
    public Transform puntoAbajo;
    public bool subiendo1;
    public bool subiendo2;
    public bool bajando;
    public bool arriba;
    public float tiempoEsperaP1 = 4f;
    public float auxtiempoEsperaP1;
    public bool arribadeltodo;
    public float tiempoEsperaP2 = 4f;
    public float auxtiempoEsperaP2;
    public bool abajo;
    public bool enContacto;
    public float velocidadSubida1 = 5f;
    public float velocidadSubida2 = 5f;
    public float velocidadBajada = 5f;
    public float tiempoSinCollider = 1f;
    public float auxTiempoSinCollider;
    public bool spriteApagado;
    public bool spriteArriba;
    public bool spriteAbajo;

    public float WaitFor = 5;

    void Start()
    {
        player = FindObjectOfType<ControllerPersonaje>().gameObject;
        this.transform.position = puntoAbajo.transform.position;
        auxTiempoSinCollider = tiempoSinCollider;
        abajo = true;

    }
    private void Update()
    {

        if (collideractivado)
        {
            if (this.GetComponent<BoxCollider2D>().enabled == false)
            {
                this.GetComponent<BoxCollider2D>().enabled = true;
            }

        }
        else
        {

            if (this.GetComponent<BoxCollider2D>().enabled == true)
            {

                this.GetComponent<BoxCollider2D>().enabled = false;
            }
            auxTiempoSinCollider -= Time.deltaTime;
            if (auxTiempoSinCollider <= 0)
            {
                auxTiempoSinCollider = tiempoSinCollider;
                collideractivado = true;
            }

        }
        if (activado == true)
        {
            if (collideractivado == true)
            {
                if (player.transform.position.y > this.transform.position.y)
                {
                    playerporencimaunavez = true;
                }
                else
                {
                    if (playerporencimaunavez == true)
                    {
                        if (player.transform.position.y < this.transform.position.y && player.GetComponent<ControllerPersonaje>().grounded == true) {
                            Bajar();
                        }

                    }
                }
            }

        }
        if (subiendo1)
        {
            this.GetComponentInChildren<Animator>().SetBool("Subiendo", true);
            this.GetComponentInChildren<Animator>().SetBool("Bajando", false);
            if (this.transform.position.y < puntoArriba.transform.position.y)
            {
                this.transform.position = this.transform.position + ((Vector3.up * velocidadSubida1) * Time.deltaTime);
            }
            else
            {
                arriba = true;
                auxtiempoEsperaP1 = tiempoEsperaP1;
                subiendo1 = false;
            }

        }
        if (arriba == true)
        {
            this.GetComponentInChildren<Animator>().SetBool("Subiendo", false);
            this.GetComponentInChildren<Animator>().SetBool("Bajando", false);
            if (auxtiempoEsperaP1 > 0)
            {
                auxtiempoEsperaP1 -= Time.deltaTime;
            }
            else
            {
                subiendo2 = true;
                arriba = false;
                auxtiempoEsperaP1 = tiempoEsperaP1;
            }
        }
        if (subiendo2)
        {
            this.GetComponentInChildren<Animator>().SetBool("Subiendo", true);
            this.GetComponentInChildren<Animator>().SetBool("Bajando", false);
            if (this.transform.position.y < puntoArribaTodo.transform.position.y)
            {
                this.transform.position = this.transform.position + ((Vector3.up * velocidadSubida2) * Time.deltaTime);
            }
            else
            {
                arribadeltodo = true;
                subiendo2 = false;
                auxtiempoEsperaP2 = tiempoEsperaP2;
            }

        }
        if (arribadeltodo == true)
        {
            this.GetComponentInChildren<Animator>().SetBool("Subiendo", false);
            this.GetComponentInChildren<Animator>().SetBool("Bajando", false);
            if (auxtiempoEsperaP2 > 0)
            {
                auxtiempoEsperaP2 -= Time.deltaTime;
            }
            else
            {
                bajando = true;
                auxtiempoEsperaP2 = tiempoEsperaP2;
                arribadeltodo = false;
            }
        }
        if (bajando)
        {
           
            if (this.transform.position.y > puntoAbajo.transform.position.y)
            {
                this.GetComponentInChildren<Animator>().SetBool("Subiendo", false);
                this.GetComponentInChildren<Animator>().SetBool("Bajando", true);
                this.transform.position = this.transform.position - ((Vector3.up * velocidadBajada) * Time.deltaTime);
            }
            else
            {
                this.GetComponentInChildren<Animator>().SetBool("Subiendo", false);
                this.GetComponentInChildren<Animator>().SetBool("Bajando", false);
                activado = false;
                abajo = true;
                arribadeltodo = false; arriba = false; subiendo1 = false; subiendo2 = false;playerporencimaunavez = false;bajando = false;
            }

        }
    }
    void Bajar()
    {
        bajando = true;
        arribadeltodo = false; arriba = false; subiendo1 = false; subiendo2 = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          
            if (player.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f&&activado==false)
            {
               
                collideractivado = false;
                activado = true;
                abajo = false;
                subiendo1 = true;
            }
            else
            {

            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //transform.position = Up.transform.position;
            ////up = true;
            ////if (UpUp && up)
            ////{
            ////    transform.position = Ups.transform.position;
            ////}
            ////else
            ////{
            ////    return;
            ////}
            //StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitFor);

    
        //up = false;
    }
}
