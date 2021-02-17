using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    public bool sePuedeDashHorizontal = false;
    public bool sePuedeDashVertical = false;
    GameObject hijoDireccion;
    public float fImpulso = 100;
    public float fImpulsoDash = 130;
    GameObject posicionSalto;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        hijoDireccion = transform.GetChild(0).gameObject;
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
        posicionSalto = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            if (player.GetComponent<ControllerPersonaje>().yaimpulsado == false)
            {
                collision.gameObject.transform.position = posicionSalto.transform.position;

                if (sePuedeDashHorizontal && !sePuedeDashVertical)
                {
                    if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f)
                    {
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                    else
                    {
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                }
                else if (!sePuedeDashHorizontal && sePuedeDashVertical)
                {
                    if (collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida == true)
                    {

                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                    else
                    {
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                }
                else
                {
                    if ((collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f) || (collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida == true))
                    {

                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                    else
                    {
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                }
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            if (player.GetComponent<ControllerPersonaje>().yaimpulsado == false)
            {
                collision.gameObject.transform.position = posicionSalto.transform.position;

                if (sePuedeDashHorizontal && !sePuedeDashVertical)
                {
                    if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f)
                    {
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                    else
                    {
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                }
                else if (!sePuedeDashHorizontal && sePuedeDashVertical)
                {
                    if (collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida == true)
                    {

                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                    else
                    {
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                }
                else
                {
                    if ((collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f) || (collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida == true))
                    {

                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                    else
                    {
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                    }
                }
            }
        }

    }
}
