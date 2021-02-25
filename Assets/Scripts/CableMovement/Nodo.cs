using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    public GameObject siguienteNodo;
    public float numeroDirecciones;
    public bool puedeArriba = false;
    public bool puedeAbajo = false;
    public bool puedeDerecha = false;
    public bool puedeIzquierda = false;
    public bool salida;
    public bool salidaArriba = false;
    public bool salidaAbajo = false;
    public bool salidaDerecha = false;
    public bool salidaIzquierda = false;
    [Header("0W1S2D3A")]
    public float direccionDefault = 0;
    public bool entrada;
    public bool entradaArriba = false;
    public bool entradaAbajo = false;
    public bool entradaDerecha = false;
    public bool entradaIzquierda = false;
    GameObject player;
    cableadoviaje cab;
    public float auxtime = 0.15f;



    public GameObject cartel;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cab = FindObjectOfType<cableadoviaje>();
    }

    // Update is called once per frame
    void Update()
    {
        if (auxtime > 0)
        {
            auxtime -= Time.deltaTime;
            if (auxtime <= 0)
            {
                auxtime = 0.15f;
                if (cab.viajando == false)
                {
                    if (salida)
                    {
                        salida = false;
                        entrada = true;
                    }
                }
                else
                {
                    if (entrada)
                    {
                        entrada = false;
                        salida = true;
                    }
                }

            }
        }

        if (entrada == true)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null) if ((Vector2.Distance(player.transform.position, this.transform.position) < 5) && (cab.viajando == false))
                {
                    if (cartel != null) cartel.gameObject.SetActive(true);
                    auxtime = 0.15f;
                    print(auxtime + "prineeeetweito");
                    //if (Input.GetButtonDown("Interact")|| GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action3.WasPressed)
                    if (Input.GetButtonDown("Interact") && player.GetComponent<ControllerPersonaje>().joystick == null)
                    {
                        if (cab.viajando == false)
                        {
                            auxtime = 0.15f;

                            print(auxtime + "printweito");
                            player.GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().entradaCables);
                            cab.viajando = true;
                            player.transform.position = this.transform.position;
                            //if (puedeAbajo)
                            //{
                            //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(0, -1);
                            //}
                            //else if (puedeArriba)
                            //{
                            //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(0, 1);
                            //}
                            //else if (puedeDerecha)
                            //{
                            //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(1, 0);
                            //}
                            //else if (puedeIzquierda)
                            //{
                            //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(-1, 0);
                            //}

                        }
                    }
                    else if (player.GetComponent<ControllerPersonaje>().joystick != null)
                    {
                        if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action2.WasPressed)
                        {
                            if (cab.viajando == false)
                            {
                                auxtime = 0.15f;

                                print(auxtime+"printeito");
                                player.GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().entradaCables);
                                cab.viajando = true;
                                player.transform.position = this.transform.position;
                                //if (puedeAbajo)
                                //{
                                //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(0, -1);
                                //}
                                //else if (puedeArriba)
                                //{
                                //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(0, 1);
                                //}
                                //else if (puedeDerecha)
                                //{
                                //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(1, 0);
                                //}
                                //else if (puedeIzquierda)
                                //{
                                //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(-1, 0);
                                //}

                            }
                        }
                    }
                }
                else
                {
                    cartel.gameObject.SetActive(false);
                }
        }
    }

}
