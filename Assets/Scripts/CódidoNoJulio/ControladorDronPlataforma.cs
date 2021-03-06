﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDronPlataforma : MonoBehaviour
{
    public Sprite activado;
    public Sprite apagado;

    public GameObject[] objetosActivados;
    public float tiempoEntrePlataformas = 0.4f;
    public bool secuenciaPlat = false;
    public bool seDesactivaConPlatf = false;
    public float auxTiempoEntre;
    public AudioClip clip;
    public AudioSource source;
    public Canvas cartel;
    public bool activadoBool;
    public int posicionArray = 0;
    // Start is called before the first frame update
    void Start()
    {
        posicionArray = 0;
        cartel.enabled = false;
        auxTiempoEntre = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (secuenciaPlat)
        {


            if (activadoBool == true)
            {
                auxTiempoEntre -= Time.deltaTime;
                if (objetosActivados != null)
                {
                    if (auxTiempoEntre <= 0)
                    {
                        if (objetosActivados[posicionArray] != null)
                        {
                            objetosActivados[posicionArray].SetActive(true);
                            if (objetosActivados[posicionArray].GetComponent<PlataformaND1>() != null)
                            {
                                //print("no null componente platf");
                                objetosActivados[posicionArray].GetComponent<PlataformaND1>().transform.position = objetosActivados[posicionArray].GetComponent<PlataformaND1>().startPos.position;

                                objetosActivados[posicionArray].GetComponent<PlataformaND1>().nextPos = objetosActivados[posicionArray].GetComponent<PlataformaND1>().startPos.transform.position;
                                objetosActivados[posicionArray].GetComponent<PlataformaND1>().auxtiempoParada = objetosActivados[posicionArray].GetComponent<PlataformaND1>().tiempoParada;
                                if (posicionArray < objetosActivados.Length - 1)
                                {


                                    posicionArray++;
                                    auxTiempoEntre = tiempoEntrePlataformas;
                                }
                                else
                                {
                                    activadoBool = false;
                                    foreach (GameObject go in objetosActivados)
                                    {
                                        go.GetComponentInChildren<Animator>().SetBool("Activado", false);
                                    }
                                }


                            }
                        }

                    }


                }

            }
        }
        if ((FindObjectOfType<VidaPlayer>().reiniciando))
        {
            if (this.GetComponent<SpriteRenderer>().sprite == activado)
            {
                posicionArray = 0;
                activadoBool = false;
                this.GetComponent<SpriteRenderer>().sprite = apagado;
            }
        }
        if ((seDesactivaConPlatf) && (this.GetComponent<SpriteRenderer>().sprite == activado))
        {
            bool hayActivos = false;
            foreach (GameObject go in objetosActivados)
            {
                if (go.activeSelf) hayActivos = true;
            }
            if (hayActivos == false)
            {

                posicionArray = 0;
                activadoBool = false;
                this.GetComponent<SpriteRenderer>().sprite = apagado;

            }
        }
    }
    public void Activar()
    {
        //PlataformaND1.vuelta = false;
        posicionArray = 0;
        //StartCoroutine(ScuttleV2());
        activadoBool = true;
        if (!secuenciaPlat)
        {
            foreach (GameObject go in objetosActivados)
            {

                if (go != null)
                {
                    go.GetComponentInChildren<Animator>().SetBool("Activado", true);
                    
                    //if (go.GetComponent<PlataformaND1>() != null)
                    //{

                    //    go.GetComponent<PlataformaND1>().transform.position = go.GetComponent<PlataformaND1>().startPos.position;

                    //    go.GetComponent<PlataformaND1>().nextPos = go.GetComponent<PlataformaND1>().startPos.transform.position;
                    //    go.GetComponent<PlataformaND1>().auxtiempoParada = go.GetComponent<PlataformaND1>().tiempoParada;
                    //}

                }

            }
        }
        source.PlayOneShot(clip);
        //this.GetComponent<SpriteRenderer>().sprite = activado;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.GetComponent<SpriteRenderer>().sprite == apagado)
            {
                //cartel.enabled = true;
                //if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action3.WasPressed)
                //{
                Activar();
                //}
            }
            else
            {
                cartel.enabled = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cartel.enabled = false;
        }
    }

    public IEnumerator ScuttleV2()
    {
        if (objetosActivados != null)
        {
            foreach (GameObject go in objetosActivados)
            {
                if (go != null) go.SetActive(true);

                yield return new WaitForSeconds(tiempoEntrePlataformas);
            }
        }

    }
}
