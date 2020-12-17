using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour
{
    public float dañoenemigoEmbestida = 1;
    public float dañopinchos = 1;
    public float dañoAgua = 1;
    public float dañoColliderMuerte = 4;
    public float vidaMax = 4f;
    public float vidaActual;
    public Image[] spritesvida;
    Color colorinicial;
    public float cdTrasDaño = 1f;
    float auxcdTrasdaño;
    public bool recienAtacado = false;
    public float cdInvulnAtaq = 1f;
    bool unavez = false;
    public float auxcdinvuln;
    ControllerPersonaje cc;
    Animator animCC;
    Movimiento mov;
    public AudioClip dañoPlayer;
    public AudioClip muertePlayer;
    public AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            //spritesvida[0] = GameObject.Find("Vida0").GetComponent<Image>();
            //spritesvida[1] = GameObject.Find("Vida1").GetComponent<Image>();
            //spritesvida[2] = GameObject.Find("Vida2").GetComponent<Image>();
            //spritesvida[3] = GameObject.Find("Vida3").GetComponent<Image>();
            //colorinicial = spritesvida[0].GetComponent<Image>().color;
        }
        auxcdinvuln = cdInvulnAtaq;
        //cc = this.GetComponent<CharacterController2D>();
        cc = this.GetComponent<ControllerPersonaje>();
        //mov = this.GetComponent<Movimiento>();
        animCC = GetComponentInChildren<Animator>();
        vidaActual = vidaMax;
        auxcdTrasdaño = 0;



    }
    public void RecibirDaño(float daño, Vector3 puntoimpacto, Vector3 puntocontacto)
    {

        if (auxcdTrasdaño <= 0)
        {
            //if ((this.GetComponent<Movimiento>().cayendoS == false) && (this.GetComponent<CharacterController2D>().dashing == false) && (this.GetComponent<CharacterController2D>().justdashed == false) && (recienAtacado == false))

            AplicarFuerza(puntoimpacto, puntocontacto);
            vidaActual -= daño;
            auxcdTrasdaño += cdTrasDaño;
            if (vidaActual == 0)
            {
                //this.GetComponentInChildren<Animator>().SetTrigger("Die");
                //source.PlayOneShot(muertePlayer);
                if (this.GetComponent<AudioManager>().sonidosUnaVez.isPlaying == false) this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().morir);
                GameManager.Instance.MuertePJ();
                animCC.SetTrigger("Die");
            }
            else
            {
                GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasDaño, transform.position);
                //source.PlayOneShot(dañoPlayer);
                if(this.GetComponent<AudioManager>().sonidosUnaVez.isPlaying==false) this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().daño);
                animCC.SetTrigger("Daño");
            }





        }
        else
        {

        }



    }
    public void AplicarFuerza(Vector3 puntoimpacto, Vector3 puntocontacto)
    {
        print("fuerzaAplicadaq");
        Vector3 direccion = (puntoimpacto - (puntocontacto + new Vector3(0, 0.6f, 0))).normalized;
        if (this.GetComponent<ControllerPersonaje>().grounded == false)
        {
            direccion = (puntoimpacto - (puntocontacto + new Vector3(0, 0.6f, 0))).normalized;
        }
        else
        {
            direccion = (puntoimpacto - (puntocontacto + new Vector3(0, -0.6f, 0))).normalized;
        }

        //direccion = new Vector3(direccion.x * 3, direccion.y);
        this.transform.position = this.transform.position + new Vector3(-direccion.x * 1.6f, 0.8f, 0);
        //invulnerable = true;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //this.GetComponent<CharacterController2D>().dashing = true;

        this.GetComponent<Rigidbody2D>().AddForce(-direccion * 7, ForceMode2D.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
               vidaMax = 10000;
                vidaActual = 10000;
            }



            if (recienAtacado == true)
            {
                if (unavez == false)
                {
                    auxcdinvuln = cdInvulnAtaq;
                    unavez = true;
                }
                auxcdinvuln -= Time.deltaTime;
                if (auxcdinvuln <= 0)
                {
                    unavez = false;
                    recienAtacado = false;
                }
            }
            if (auxcdTrasdaño > 0) auxcdTrasdaño -= Time.deltaTime;
            //if (vidaActual <= 0)
            //{
            //    spritesvida[0].GetComponent<Image>().color = Color.black;
            //    spritesvida[1].GetComponent<Image>().color = Color.black;
            //    spritesvida[2].GetComponent<Image>().color = Color.black;
            //    spritesvida[3].GetComponent<Image>().color = Color.black;
            //}
            //else if (vidaActual == 1)
            //{
            //    spritesvida[0].GetComponent<Image>().color = colorinicial;
            //    spritesvida[1].GetComponent<Image>().color = Color.black;
            //    spritesvida[2].GetComponent<Image>().color = Color.black;
            //    spritesvida[3].GetComponent<Image>().color = Color.black;
            //}
            //else if (vidaActual == 2)
            //{
            //    spritesvida[0].GetComponent<Image>().color = colorinicial;
            //    spritesvida[1].GetComponent<Image>().color = colorinicial;
            //    spritesvida[2].GetComponent<Image>().color = Color.black;
            //    spritesvida[3].GetComponent<Image>().color = Color.black;
            //}
            //else if (vidaActual == 3)
            //{
            //    spritesvida[0].GetComponent<Image>().color = colorinicial;
            //    spritesvida[1].GetComponent<Image>().color = colorinicial;
            //    spritesvida[2].GetComponent<Image>().color = colorinicial;
            //    spritesvida[3].GetComponent<Image>().color = Color.black;
            //}
            //else if (vidaActual == 4)
            //{
            //    spritesvida[0].GetComponent<Image>().color = colorinicial;
            //    spritesvida[1].GetComponent<Image>().color = colorinicial;
            //    spritesvida[2].GetComponent<Image>().color = colorinicial;
            //    spritesvida[3].GetComponent<Image>().color = colorinicial;
            //}
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColliderMuerte")
        {
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoColliderMuerte, collision.gameObject.transform.position, this.transform.position);

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColliderMuerte")
        {
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoColliderMuerte, collision.gameObject.transform.position, this.transform.position);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pinchos")
        {
            //if (this.GetComponent<Movimiento>().cayendoS == true)
            //{
            //    this.GetComponent<Movimiento>().cayendoS = false;
            //}
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañopinchos, collision.gameObject.transform.position, collision.contacts[0].point);
        }
        if (collision.gameObject.tag == "ColliderMuerte")
        {
            //if (this.GetComponent<Movimiento>().cayendoS == true)
            //{
            //    this.GetComponent<Movimiento>().cayendoS = false;
            //}
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoColliderMuerte, collision.gameObject.transform.position, collision.contacts[0].point);

        }
        if (collision.gameObject.tag == "Agua")
        {
            //if (this.GetComponent<Movimiento>().cayendoS == true)
            //{
            //    this.GetComponent<Movimiento>().cayendoS = false;
            //}
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoAgua, collision.gameObject.transform.position, collision.contacts[0].point);
            this.transform.position = new Vector3(this.transform.position.x - 0.1f, this.transform.position.y + 0.2f, 0);
        }
        //if (collision.gameObject.tag == "EnemigoEmbestida")
        //{


        //    if ((cc.justdashed == false) && (cc.dashing == false) && (this.GetComponent<Movimiento>().cayendoS == false)) this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoenemigoEmbestida, collision.gameObject.transform.position, collision.contacts[0].point);
        //    else if (this.GetComponent<Movimiento>().cayendoS == true)
        //    {

        //        cc.ReboteEnemigo();


        //        cc.puedomoverme = true;
        //        this.GetComponent<Movimiento>().multSpeed = 0;


        //    }
        //    if ((cc.dashing == true) || (cc.justdashed == true))
        //    {
        //        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 0);
        //        //this.GetComponent<VidaPlayer>().AplicarFuerza(collision.gameObject.transform.position, collision.contacts[0].point);
        //    }


        //}
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pinchos")
        {
            //if (this.GetComponent<Movimiento>().cayendoS == true)
            //    {
            //        this.GetComponent<Movimiento>().cayendoS = false;
            //    }
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañopinchos, collision.gameObject.transform.position, collision.contacts[0].point);
        }
        if (collision.gameObject.tag == "Agua")
        {
            //if (this.GetComponent<Movimiento>().cayendoS == true)
            //{
            //    this.GetComponent<Movimiento>().cayendoS = false;
            //}
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoAgua, collision.gameObject.transform.position, collision.contacts[0].point);

        }
        if (collision.gameObject.tag == "ColliderMuerte")
        {
            //if (this.GetComponent<Movimiento>().cayendoS == true)
            //{
            //    this.GetComponent<Movimiento>().cayendoS = false;
            //}
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoColliderMuerte, collision.gameObject.transform.position, collision.contacts[0].point);

        }
        //if (collision.gameObject.tag == "EnemigoEmbestida")
        //{


        //    if ((cc.justdashed == false) && (cc.dashing == false) && (this.GetComponent<Movimiento>().cayendoS == false)) this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoenemigoEmbestida, collision.gameObject.transform.position, collision.contacts[0].point);
        //    else if (this.GetComponent<Movimiento>().cayendoS == true)
        //    {

        //        //cc.ReboteEnemigo();


        //        cc.puedomoverme = true;
        //        this.GetComponent<Movimiento>().multSpeed = 0;


        //    }
        //    if ((cc.dashing == true) || (cc.justdashed == true))
        //    {
        //        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 0);
        //        //this.GetComponent<VidaPlayer>().AplicarFuerza(collision.gameObject.transform.position, collision.contacts[0].point);
        //    }
        //}

    }
}
