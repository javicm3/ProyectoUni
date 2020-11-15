using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaEnemigo : MonoBehaviour
{
    public float dañoDash = 1;
    public float dañoDisparo = 1;
    public float dañoDashAbajo = 1f;
    public float vidaMax = 2f;
    public float vidaActual;
    public GameObject[] spritesvida;
    Color colorinicial;
    public float cdTrasDaño = 1f;
    public float auxcdTrasdaño;
    public bool invulnerable = false;
    Animator anim;
    public GameObject particulasDaño;
    public GameObject pies;
    public AudioClip dañoEnemigo;
    public AudioClip muerteEnemigo;

    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        spritesvida[0].GetComponent<Image>().enabled = false;
        spritesvida[1].GetComponent<Image>().enabled = false;
        vidaActual = vidaMax;
        auxcdTrasdaño = 0;
        colorinicial = spritesvida[0].GetComponent<Image>().color;
        anim = GetComponentInChildren<Animator>();
    }
    public void RecibirDaño(float daño)
    {
        Instantiate(particulasDaño, pies.transform);
        if (auxcdTrasdaño <= 0)
        {
            
            invulnerable = false;

            vidaActual -= daño;
            auxcdTrasdaño += cdTrasDaño;
            if (vidaActual == 0)
            {
                this.GetComponent<EnemigoEmbestida2>().estado = EnemigoEmbestida2.States.Muerte;
                anim.SetTrigger("Die");
                Destroy(this.GetComponent<Collider2D>());
                Destroy(this.GetComponent<EnemigoEmbestida2>());
                this.GetComponent<SpriteRenderer>().color = Color.black;
                source.PlayOneShot(muerteEnemigo);
                Destroy(this.gameObject, 1.2f);
            }
            else
            {
                source.PlayOneShot(dañoEnemigo);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (auxcdTrasdaño == 0)
        {

            if (this.GetComponent<Collider2D>() != null) this.GetComponent<Collider2D>().enabled = true;
        }
        if (auxcdTrasdaño > 0)
        {
            auxcdTrasdaño -= Time.deltaTime;
        }
        else
        {
            auxcdTrasdaño = 0;
        }
        if (vidaActual == 0)
        {
            spritesvida[0].GetComponent<Image>().color = Color.black;
            spritesvida[1].GetComponent<Image>().color = Color.black;


        }
        else if (vidaActual == 1)
        {
            spritesvida[0].GetComponent<Image>().enabled = true;
            spritesvida[1].GetComponent<Image>().enabled = true;
            spritesvida[0].GetComponent<Image>().color = colorinicial;
            spritesvida[1].GetComponent<Image>().color = Color.black;

        }
        else if (vidaActual == 2)
        {
            spritesvida[0].GetComponent<Image>().enabled = false;
            spritesvida[1].GetComponent<Image>().enabled = false;

        }

    }
    public void AplicarFuerza(Vector3 puntoimpacto, Vector3 puntocontacto)
    {
        Vector3 direccion = (GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position).normalized;
        //direccion = new Vector3(direccion.x * 3, direccion.y);
        direccion.y = 0;
        //invulnerable = true;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //this.GetComponent<CharacterController2D>().dashgolpeo = true;
        //this.GetComponent<CharacterController2D>().puedomoverme = false;
        //this.GetComponent<CharacterController2D>().Invoke("StopDash", 0.15f);
        this.GetComponent<Rigidbody2D>().AddForce(-direccion * 2f, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TriggerPlayer")
        {
            if (auxcdTrasdaño <= 0)
            {
                if (collision.gameObject.GetComponent<TriggerColliderPLayer>().ataquedashing)
                {
                    RecibirDaño(dañoDash);
                    this.GetComponent<Collider2D>().enabled = false;
                }
                    if (collision.gameObject.GetComponent<TriggerColliderPLayer>().ataqueabajo)
                {
                    GameObject.FindObjectOfType<Movimiento>().cayendoS = false;
                    collision.gameObject.GetComponent<TriggerColliderPLayer>().Desactivar("abajo");
                    collision.gameObject.GetComponentInParent<CharacterController2D>().ReboteEnemigo();
                    RecibirDaño(dañoDashAbajo);
                    this.GetComponent<Collider2D>().enabled = false;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            if (auxcdTrasdaño <= 0)
            {
                AplicarFuerza(collision.gameObject.transform.position, collision.contacts[0].point);
            }

            RecibirDaño(dañoDisparo);
        }
        //if (collision.gameObject.tag == "Player")
        //{
        //    print("enemigo choca " + collision.gameObject.tag + "auxcd trasdaño es :" + auxcdTrasdaño);

        //    if ((collision.gameObject.GetComponent<CharacterController2D>().dashing == true) )
        //    {
        //        if (auxcdTrasdaño <= 0)
        //        {
        //            collision.gameObject.GetComponent<CharacterController2D>().StopDash();
        //            print("dashattack");
        //            RecibirDaño(dañoDash);
        //        }
        //    }
        //    else
        //    {

        //        if (collision.gameObject.GetComponent<Movimiento>().cayendoS == true)
        //        {
        //            if (collision.gameObject.GetComponent<Movimiento>() != null) collision.gameObject.GetComponent<Movimiento>().cayendoS = false;
        //            if (auxcdTrasdaño <= 0)
        //            {
        //                AplicarFuerza(collision.gameObject.transform.position, collision.contacts[0].point);
        //                print("hola");
        //                RecibirDaño(dañoDashAbajo);

        //            }
        //        }
        //    }
        //}

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Bala")
        //{
        //    if (auxcdTrasdaño <= 0)
        //    {
        //        AplicarFuerza(collision.gameObject.transform.position, collision.contacts[0].point);
        //    }

        //    RecibirDaño(dañoDisparo);
        //}
        //if (collision.gameObject.tag == "Player")
        //{
        //    print("enemigo choca plauer" + "auxcd trasdaño es :" + auxcdTrasdaño);

        //    if ((collision.gameObject.GetComponent<CharacterController2D>().dashing == true) || (collision.gameObject.GetComponent<CharacterController2D>().justdashed == true))
        //    {
        //        if (auxcdTrasdaño <= 0)
        //        {
        //            collision.gameObject.GetComponent<CharacterController2D>().StopDash();
        //            print("dashattack");
        //            RecibirDaño(dañoDash);
        //        }
        //    }
        //    else if (collision.gameObject.GetComponent<Movimiento>().cayendoS == true)
        //    {

        //        if (auxcdTrasdaño <= 0)
        //        {
        //            AplicarFuerza(collision.gameObject.transform.position, collision.contacts[0].point);
        //            print("hola");
        //            RecibirDaño(dañoDashAbajo);
        //            //collision.gameObject.GetComponent<Movimiento>().cayendoS =false;
        //        }
        //    }
        //}
    }
}

