using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activador : MonoBehaviour
{
    public Sprite activado;
    public Sprite apagado;
    public GameObject[] objetosDestruidos;
    public GameObject[] objetosActivados;
    public GameObject puertaTemporizadorActivada;
    public bool abridorDePuertaPermanente = false;
    public bool abrePuertas = false;
    float tiempoPuerta;
    float auxtiempoPuerta;
    public AudioClip clip;
    public AudioSource source;
    public Canvas cartel;
    // Start is called before the first frame update
    void Start()
    {
        cartel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (puertaTemporizadorActivada.GetComponentInChildren<PuertaTemporizador>().abiertaPermanente == true)
        {
            this.GetComponent<SpriteRenderer>().sprite = activado;
        }
        else
        {


            if (tiempoPuerta > 0)
            {
                tiempoPuerta -= Time.deltaTime;
                if (tiempoPuerta <= 0)
                {
                    this.GetComponent<SpriteRenderer>().sprite = apagado;
                    source.PlayOneShot(clip);
                }
            }
        }

    }
    public void Activar()
    {
        if (!abrePuertas)
        {
            if (this.GetComponent<SpriteRenderer>().sprite == activado)
            {
                this.GetComponent<SpriteRenderer>().sprite = apagado;
            }
            else
            {
                if (this.GetComponent<SpriteRenderer>().sprite == apagado)
                {
                    this.GetComponent<SpriteRenderer>().sprite = activado;

                }
            }
            source.PlayOneShot(clip);
        }

        if (abrePuertas)
        {
            if (this.GetComponent<SpriteRenderer>().sprite == apagado)
            {
                this.GetComponent<SpriteRenderer>().sprite = activado;

                if (puertaTemporizadorActivada != null)
                {
                    tiempoPuerta = puertaTemporizadorActivada.GetComponentInChildren<PuertaTemporizador>().tiempoTrasPulsar;
                    if (!abridorDePuertaPermanente)
                    {
                        puertaTemporizadorActivada.GetComponentInChildren<PuertaTemporizador>().Activar();
                    }
                    else
                    {
                        puertaTemporizadorActivada.GetComponentInChildren<PuertaTemporizador>().abiertaPermanente = true;
                    }


                }
            }


        }
        foreach (GameObject go in objetosDestruidos)
        {
            go.SetActive(!go.activeSelf);
        }
        foreach (GameObject go in objetosActivados)
        {
            go.SetActive(!go.activeSelf);
        }

        source.PlayOneShot(clip);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            Activar();
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.GetComponent<SpriteRenderer>().sprite == apagado)
            {


                cartel.enabled = true;
                if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action3.WasPressed)
                {
                    Activar();
                }
            }
            else
            {
                cartel.enabled = false;
            }
        }
        else
        {

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cartel.enabled = false;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            Activar();
    //        }

    //        //if ((collision.gameObject.GetComponent<ControllerPersonaje>().dashing == true) || (collision.gameObject.GetComponent<CharacterController2D>().justdashed == true))
    //        //{
    //        //    Activar();
    //        //}
    //        //else if (collision.gameObject.GetComponent<Movimiento>().cayendoS == true)
    //        //{

    //        //    Activar();


    //        //}

    //    }
    //}
}
