﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscudoScript : MonoBehaviour
{

    //public GameObject enemigoSeguido;
    //Vector2 distanciaEnemigo;
    // Vector2 distanciaEnemigoNeg;
    // Start is called before the first frame update
    public GameObject padre;
    void Start()
    {
        padre = this.transform.parent.gameObject;
        //distanciaEnemigo =this.gameObject.transform.position-enemigoSeguido.transform.position;
        //distanciaEnemigoNeg = -distanciaEnemigo;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (padre.GetComponent<EnemigoEmbestida2>().stun == true)
        //{
        //    this.GetComponent<Collider2D>().enabled = false;
        //}
        //else
        //{
        //    this.GetComponent<Collider2D>().enabled = true;
        //}
        //if (enemigoSeguido.transform.localScale.x == 1)
        //{
        //    this.transform.position = new Vector2(distanciaEnemigo.x + enemigoSeguido.transform.position.x, distanciaEnemigo.y + enemigoSeguido.transform.position.y);
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ControllerPersonaje>()!=null)
        {
            if (collision.gameObject.GetComponent<ControllerPersonaje>().haciendoCombate == true)
            {
                print("AAAAA");
                FindObjectOfType<NewAudioManager>().Play("EnemigoEmbestidaRebotarEscudo");

                collision.gameObject.GetComponent<ControllerPersonaje>().combateBloqueado = true;
                collision.gameObject.GetComponent<ControllerPersonaje>().rb.velocity = Vector3.zero;


                collision.gameObject.GetComponent<ControllerPersonaje>().rb.gravityScale = collision.gameObject.GetComponent<ControllerPersonaje>().originalgravity;


                collision.gameObject.GetComponent<ControllerPersonaje>().enemigosPasados.Clear();
                collision.gameObject.GetComponent<ControllerPersonaje>().direccionCombate = Vector3.zero;
                collision.gameObject.GetComponent<ControllerPersonaje>().velocidadCombateUltima = Vector3.zero; collision.gameObject.GetComponent<ControllerPersonaje>().saltoBloqueado = false;
                collision.gameObject.GetComponent<ControllerPersonaje>().dashBloqueado = false;
                collision.gameObject.GetComponent<ControllerPersonaje>().haciendoCombate = false;
                collision.gameObject.GetComponent<PlayerInput>().inputHorizBlock = false;
                collision.gameObject.GetComponent<ControllerPersonaje>().pulsadoChispazo = false;
                collision.gameObject.GetComponent<ControllerPersonaje>().movimientoBloqueado = false;
                collision.gameObject.GetComponent<ControllerPersonaje>().auxTiempoUsar = collision.gameObject.GetComponent<ControllerPersonaje>().tiempoUsarCombateTrasEscudo;
                collision.gameObject.GetComponent<VidaPlayer>().RecibirDaño(0, this.transform.position, collision.GetContact(0).point + new Vector2(1 * Mathf.Sign(Vector2.Distance(collision.GetContact(0).point, this.transform.position)), -0.5f));
                //collision.gameObject.GetComponent<VidaPlayer>().RecibirDaño(0, collision.transform.position + new Vector3(0, -1f), collision.GetContact(0).point /*+ new Vector2(0, -1f)*/);

            }
            else
            {
                if (collision.gameObject.GetComponent<VidaPlayer>().auxcdTrasdaño <= 0 &&((collision.gameObject.GetComponent<ControllerPersonaje>().chispazoUnlook&& collision.gameObject.GetComponent<ControllerPersonaje>().auxtiempoTrasSalirCombateInvuln <= 0)|| !collision.gameObject.GetComponent<ControllerPersonaje>().chispazoUnlook)) ManagerLogros.Instance.DesbloquearLogro(10);

                collision.gameObject.GetComponent<VidaPlayer>().RecibirDaño(4, this.transform.position, collision.GetContact(0).point + new Vector2(1 * Mathf.Sign(Vector2.Distance(collision.GetContact(0).point, this.transform.position)), -0.5f));
                //collision.gameObject.GetComponent<VidaPlayer>().RecibirDaño(1, collision.transform.position + new Vector3(0, -1f), collision.GetContact(0).point /*+ new Vector2(0, -1f)*/);
            }

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ControllerPersonaje>() != null)
        {
            if (collision.gameObject.GetComponent<ControllerPersonaje>().haciendoCombate == true)
            {

                collision.gameObject.GetComponent<ControllerPersonaje>().combateBloqueado = true;



                collision.gameObject.GetComponent<ControllerPersonaje>().rb.gravityScale = collision.gameObject.GetComponent<ControllerPersonaje>().originalgravity;


                collision.gameObject.GetComponent<ControllerPersonaje>().enemigosPasados.Clear();
                collision.gameObject.GetComponent<ControllerPersonaje>().direccionCombate = Vector3.zero;
                collision.gameObject.GetComponent<ControllerPersonaje>().velocidadCombateUltima = Vector3.zero;
                collision.gameObject.GetComponent<ControllerPersonaje>().haciendoCombate = false;
                collision.gameObject.GetComponent<ControllerPersonaje>().saltoBloqueado = false;
                collision.gameObject.GetComponent<ControllerPersonaje>().dashBloqueado = false;
                collision.gameObject.GetComponent<PlayerInput>().inputHorizBlock = false;
                collision.gameObject.GetComponent<ControllerPersonaje>().pulsadoChispazo = false;
                collision.gameObject.GetComponent<ControllerPersonaje>().movimientoBloqueado = false;
                collision.gameObject.GetComponent<ControllerPersonaje>().auxTiempoUsar = collision.gameObject.GetComponent<ControllerPersonaje>().tiempoUsarCombateTrasEscudo;
               
                collision.gameObject.GetComponent<VidaPlayer>().RecibirDaño(0, this.transform.position, collision.GetContact(0).point + new Vector2(1*Mathf.Sign(Vector2.Distance(collision.GetContact(0).point,this.transform.position)), -0.5f));
                //collision.gameObject.GetComponent<VidaPlayer>().RecibirDaño(0, collision.transform.position + new Vector3(0, -1f), collision.GetContact(0).point /*+ new Vector2(0, -1f)*/);

            }
            else
            {
                if (collision.gameObject.GetComponent<VidaPlayer>().auxcdTrasdaño <= 0 && ((collision.gameObject.GetComponent<ControllerPersonaje>().chispazoUnlook && collision.gameObject.GetComponent<ControllerPersonaje>().auxtiempoTrasSalirCombateInvuln <= 0) || !collision.gameObject.GetComponent<ControllerPersonaje>().chispazoUnlook)) ManagerLogros.Instance.DesbloquearLogro(10);
                collision.gameObject.GetComponent<VidaPlayer>().RecibirDaño(4, this.transform.position, collision.GetContact(0).point + new Vector2(1 * Mathf.Sign(Vector2.Distance(collision.GetContact(0).point, this.transform.position)), -0.5f));
                //collision.gameObject.GetComponent<VidaPlayer>().RecibirDaño(1, collision.transform.position + new Vector3(0, -1f), collision.GetContact(0).point /*+ new Vector2(0, -1f)*/);
            }

        }
    }
}
