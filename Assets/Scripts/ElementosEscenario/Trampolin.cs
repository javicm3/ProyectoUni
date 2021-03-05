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
    Animator trampoAnim;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        hijoDireccion = transform.GetChild(0).gameObject;
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
        posicionSalto = transform.GetChild(1).gameObject;
        trampoAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Desactivar()
    {
        trampoAnim.SetBool("Dash", false);
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
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);
                    }
                    else
                    {
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);

                    }
                }
                else if (!sePuedeDashHorizontal && sePuedeDashVertical)
                {
                    if (collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida == true)
                    {
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);

                    }
                    else
                    {
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);

                    }
                }
                else
                {
                    if ((collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f) || (collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida == true))
                    {
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);

                    }
                    else
                    {
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);

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
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);

                    }
                    else
                    {
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);

                    }
                }
                else if (!sePuedeDashHorizontal && sePuedeDashVertical)
                {
                    if (collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida == true)
                    {
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);

                    }
                    else
                    {
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);

                    }
                }
                else
                {
                    if ((collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f) || (collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida == true))
                    {
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulsoDash);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);


                    }
                    else
                    {
                        trampoAnim.SetBool("Dash", true);
                        player.GetComponent<ControllerPersonaje>().AplicarImpulso(hijoDireccion, this.gameObject, fImpulso);
                        player.GetComponent<ControllerPersonaje>().yaimpulsado = true;
                        Invoke("Desactivar", 0.35f);

                    }
                }
            }
        }

    }
}
