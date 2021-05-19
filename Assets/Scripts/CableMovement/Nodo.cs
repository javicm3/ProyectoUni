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
    public float time=0.2f;
    public bool modificaMaxZoomViaje = true;
    public float tamañoCamaraViaje = 40;
    public float tiempoSinabsorber = 0.4f;
    float auxtiempoSin;
    public GameObject cartel;
    public float distanciaAbsorcion = 10f;
    public float speedAbsorcion = 50f;

    // Start is called before the first frame update
    void Start()
    {
      if(cartel!=null)  cartel.gameObject.SetActive(false);
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
        cab = FindObjectOfType<cableadoviaje>();
    }

    // Update is called once per frame
    void Update()
    {
        if (auxtiempoSin > 0)
        {
            auxtiempoSin -= Time.deltaTime;
            if (auxtiempoSin < 0)
            {
                auxtiempoSin = 0;
            }
        }
        if (auxtime > 0)
        {
            auxtime -= Time.deltaTime;
            if (auxtime <= 0)
            {
                auxtime = time;
                if (cab.viajando == false)
                {
                    if (salida)
                    {
                        salida = false;
                        entrada = true;
                        auxtiempoSin = tiempoSinabsorber;
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
            if (GameObject.FindObjectOfType< ControllerPersonaje >()!= null&& GameObject.FindObjectOfType<ControllerPersonaje>().movCablesUnlook) if ((Vector2.Distance(player.transform.position, this.transform.position) < distanciaAbsorcion) && (cab.viajando == false))
                {
                    //if (cartel != null) cartel.gameObject.SetActive(true);
                    auxtime =time;
                    //FindObjectOfType<NewAudioManager>().Play("PlayerCable");
                    //if (Input.GetButtonDown("Interact")|| GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action3.WasPressed)
                    //if (Input.GetButtonDown("Interact") && player.GetComponent<ControllerPersonaje>().joystick == null&& player.GetComponent<ControllerPersonaje>().movCablesUnlook)
                    //{
                    if (cab.viajando == false)
                    {
                      
                        if(auxtiempoSin<=0) player.transform.position = Vector2.MoveTowards(player.transform.position, this.transform.position, speedAbsorcion * Time.deltaTime);
                        if(Vector2.Distance(player.transform.position, this.transform.position) < 2){
                            player.transform.position = this.transform.position;
                            auxtime =time;

                            player.GetComponent<AudioManager>().Play(player.GetComponent<AudioManager>().sonidosUnaVez, player.GetComponent<AudioManager>().entradaCables);
                            cab.viajando = true;
                            player.GetComponent<CameraZoom>().tamañoCamaraViaje = tamañoCamaraViaje;
                        }
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
                    //}
                    //    else if (player.GetComponent<ControllerPersonaje>().joystick != null && player.GetComponent<ControllerPersonaje>().movCablesUnlook)
                    //    {
                    //        //if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action2.WasPressed)
                    //        //{
                    //            if (cab.viajando == false)
                    //            {
                    //                auxtime = 0.15f;


                    //                player.GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().entradaCables);
                    //                cab.viajando = true;
                    //                player.transform.position = Vector2.MoveTowards(player.transform.position,this.transform.position,50f*Time.deltaTime);
                    //                player.GetComponent<CameraZoom>().tamañoCamaraViaje = tamañoCamaraViaje;
                    //                //if (puedeAbajo)
                    //                //{
                    //                //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(0, -1);
                    //                //}
                    //                //else if (puedeArriba)
                    //                //{
                    //                //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(0, 1);
                    //                //}
                    //                //else if (puedeDerecha)
                    //                //{
                    //                //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(1, 0);
                    //                //}
                    //                //else if (puedeIzquierda)
                    //                //{
                    //                //    //GameObject.FindGameObjectWithTag("Player").GetComponent<cableadoviaje>().ordenDireccion = new Vector2(-1, 0);
                    //                //}

                    //            }
                    //        //}
                    //    }
                    //}
                    //else
                    //{
                    //    //cartel.gameObject.SetActive(false);
                    //}
                }
        }
    }
}
