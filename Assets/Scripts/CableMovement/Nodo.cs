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

    public GameObject cartel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (entrada == true)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null) if ((Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position) < 5) && (GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().viajando == false))
                {
                    cartel.gameObject.SetActive(true);

                    //if (Input.GetButtonDown("Interact")|| GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action3.WasPressed)
                    if (Input.GetButtonDown("Interact") && GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick == null)
                    {
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().viajando == false)
                        {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().entradaCables);
                            GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().viajando = true;
                            GameObject.FindGameObjectWithTag("Player").transform.position = this.transform.position;
                            if (puedeAbajo)
                            {
                                //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(0, -1);
                            }
                            else if (puedeArriba)
                            {
                                //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(0, 1);
                            }
                            else if (puedeDerecha)
                            {
                                //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(1, 0);
                            }
                            else if (puedeIzquierda)
                            {
                                //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(-1, 0);
                            }

                        }
                    }
                    else if (GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick!=null)
                    {
                        if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action3.WasPressed)
                        {
                            if (GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().viajando == false)
                            {
                                GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().entradaCables);
                                GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().viajando = true;
                                GameObject.FindGameObjectWithTag("Player").transform.position = this.transform.position;
                                if (puedeAbajo)
                                {
                                    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(0, -1);
                                }
                                else if (puedeArriba)
                                {
                                    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(0, 1);
                                }
                                else if (puedeDerecha)
                                {
                                    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(1, 0);
                                }
                                else if (puedeIzquierda)
                                {
                                    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(-1, 0);
                                }

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
