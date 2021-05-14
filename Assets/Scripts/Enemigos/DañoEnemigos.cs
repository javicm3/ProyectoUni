using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoEnemigos : MonoBehaviour
{
    GameObject player;
    Collider2D colliderHijo;
    public float dañoQueHace = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ControllerPersonaje>().gameObject;
        //foreach (Collider2D go in this.GetComponentsInChildren<Collider2D>())
        //{
        //    if (go.gameObject.tag == "Enemigo")
        //    {
        //        colliderHijo = go;
        //    }
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) < 20)
        {
            if (player.GetComponent<ControllerPersonaje>().haciendoCombate == true)
            {
                //colliderHijo.enabled = false;

                this.GetComponent<Collider2D>().enabled = false;
            }
            else
            {  //colliderHijo.enabled = true;
                if (player.GetComponent<ControllerPersonaje>().auxtiempoTrasSalirCombateInvuln > 0)
                {
                    this.GetComponent<Collider2D>().enabled = false;
                }
                else
                {
                    if (this.transform.parent.GetComponent<MovimientoEnemigoVolador>() != null)
                    {
                        if (this.transform.parent.GetComponent<MovimientoEnemigoVolador>().stun!= true)
                        {
                            this.GetComponent<Collider2D>().enabled = true;
                        }
                    }
                    else if (GetComponentInParent<EnemigoEmbestida2>() != null)
                    {
                        if (GetComponentInParent<EnemigoEmbestida2>().stun != true)
                        {
                            this.GetComponent<Collider2D>().enabled = true;
                        }
                    }
                    else if (GetComponentInParent<EnemigoSaltamontes>() != null)
                    {
                        if (GetComponentInParent<EnemigoSaltamontes>().stun != true)
                        {
                            this.GetComponent<Collider2D>().enabled = true;
                        }
                    }


                }



            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<ControllerPersonaje>() != null)
        {
            if (player.GetComponent<ControllerPersonaje>().haciendoCombate == false)
            {
                if (player.GetComponentInChildren<VidaPlayer>().auxcdTrasdaño <= 0 && player.GetComponent<ControllerPersonaje>().auxtiempoTrasSalirCombateInvuln <= 0) ManagerLogros.Instance.DesbloquearLogro(10);

                player.GetComponentInChildren<VidaPlayer>().RecibirDaño(dañoQueHace, this.transform.position, collision.GetContact(0).point + new Vector2(0, -1));
            }
         
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<ControllerPersonaje>() != null)
        {
            if (player.GetComponent<ControllerPersonaje>().haciendoCombate == false)
            {
                if (player.GetComponentInChildren<VidaPlayer>().auxcdTrasdaño <= 0 && player.GetComponent<ControllerPersonaje>().auxtiempoTrasSalirCombateInvuln <= 0) ManagerLogros.Instance.DesbloquearLogro(10);

                player.GetComponentInChildren<VidaPlayer>().RecibirDaño(dañoQueHace, this.transform.position, collision.GetContact(0).point + new Vector2(0, -1));
            }

        }

    }
}
