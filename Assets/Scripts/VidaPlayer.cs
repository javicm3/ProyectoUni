using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class VidaPlayer : MonoBehaviour
{
    public float dañoenemigoEmbestida = 1;
    public float dañopinchos = 1;
    public float dañoAgua = 4;
    public float dañoCascada = 1;
    public float dañoColliderMuerte = 4;
    public float vidaMax = 4f;
    public float vidaActual;
    public float dañoBalaVolador = 1;

    //public Image[] spritesvida;
    GameObject barraVida;
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
    public bool reiniciando;
    public CinemachineTargetGroup targetGroup;
    //public AudioClip dañoPlayer;
    //public AudioClip muertePlayer;
    //public AudioSource source;


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
            //if (GameObject.Find("LifeBar").gameObject != null) barraVida = GameObject.Find("LifeBar").gameObject;
        }
        auxcdinvuln = cdInvulnAtaq;
        //cc = this.GetComponent<CharacterController2D>();
        cc = this.GetComponent<ControllerPersonaje>();
        //mov = this.GetComponent<Movimiento>();
        animCC = GetComponentInChildren<Animator>();
        vidaActual = vidaMax;
        auxcdTrasdaño = 0;
        targetGroup = FindObjectOfType<CinemachineTargetGroup>();


    }

    public void RecibirDaño(float daño, Vector3 puntoimpacto, Vector3 puntocontacto)
    {//EEEEEEEEEEEEEEEEEEEEEEEEEEEEEELIMINARRRRRRRRRRRRRRRRRRR linea de abajo
        if(daño!=0)daño = 55;
        //EEEEEEEEEEEEEEEEEEEEEEEEEEEEEELIMINARRRRRRRRRRRRRRRRRRR linea de arriba
        if (auxcdTrasdaño <= 0)
        {
            //if ((this.GetComponent<Movimiento>().cayendoS == false) && (this.GetComponent<CharacterController2D>().dashing == false) && (this.GetComponent<CharacterController2D>().justdashed == false) && (recienAtacado == false))

            AplicarFuerza(puntoimpacto, puntocontacto);
            vidaActual -= daño;
            if (vidaActual < 0 && reiniciando == false)
            {
                vidaActual = 0;
            }

            auxcdTrasdaño += cdTrasDaño;


            if (vidaActual == 0)
            {
                //this.GetComponentInChildren<Animator>().SetTrigger("Die");
                //source.PlayOneShot(muertePlayer);
                FindObjectOfType<NewAudioManager>().Play("PlayerDeath");
                //if (this.GetComponent<AudioManager>().sonidosUnaVez.isPlaying == false) this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().morir);
                //GameManager.Instance.MuertePJ();
                animCC.SetTrigger("Die");
                cc.movimientoBloqueado = true;
                cc.combateBloqueado = true;
                reiniciando = true;
                if (FindObjectOfType<CinemachineTargetGroup>() != null)
                {
                    for (int i = 0; i < targetGroup.m_Targets.Length; i++)
                    {
                        if (i == 0)
                        {
                            targetGroup.m_Targets[0].target = this.transform;
                        }
                        else if (i == 1)
                        {
                            targetGroup.m_Targets[1].target = this.GetComponent<CameraZoom>().ceboCamara.transform;
                        }
                        else
                        {
                            if (targetGroup.m_Targets[i].target != null)
                            {
                                targetGroup.m_Targets[i].target = null;

                            }
                        }
                    }

                    FindObjectOfType<CameraZoom>().soloplayer = true;
                }
                if (SceneManager.GetActiveScene().name == "ND-3")
                {
                    GameManager.Instance.MuertePJ();
                }
                else
                {
                    Invoke("IraCheckpoint", 1f);
                }

                vidaActual = -1;
            }
            else
            {
                GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasDaño, transform.position, transform);
                //source.PlayOneShot(dañoPlayer);
                FindObjectOfType<NewAudioManager>().Play("PlayerHurt");
                //if(this.GetComponent<AudioManager>().sonidosUnaVez.isPlaying==false) this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().daño);
                animCC.SetTrigger("Daño");
            }





        }
        else
        {

        }



    }
    void IraCheckpoint()//CREO QUE ESTO NO SE ESTÁ USANDO
    {
       
        if (GameManager.Instance.UltimoCheck!=null)
        {
            Checkpoint check = GameManager.Instance.UltimoCheck;
            this.transform.position = check.transform.position;
            vidaActual = vidaMax;
            this.GetComponent<ManagerEnergia>().actualEnergy = 0;
            cc.movimientoBloqueado = false;
            cc.combateBloqueado = false;
            reiniciando = false;
            GetComponent<CameraZoom>().ceboCamara.transform.position = this.transform.position;
            check.CargarColeccionables();
        }

        /*Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint check in checkpoints)
        {
            if (check.ultimoCheck == true)
            {
                this.transform.position = check.gameObject.transform.position;
                vidaActual = vidaMax;
                this.GetComponent<ManagerEnergia>().actualEnergy = 0;
                cc.movimientoBloqueado = false;
                cc.combateBloqueado = false;
                reiniciando = false;
                break;
            }
        }*/

        if (reiniciando)
        {
            this.transform.position = GameObject.FindGameObjectWithTag("InicioNivel").gameObject.transform.position;
            vidaActual = vidaMax;
            this.GetComponent<ManagerEnergia>().actualEnergy = 0;
            cc.movimientoBloqueado = false;
            cc.combateBloqueado = false;
            GetComponent<CameraZoom>().ceboCamara.transform.position = this.transform.position;
            reiniciando = false;
        }

    }
    public void AplicarFuerza(Vector3 puntoimpacto, Vector3 puntocontacto)
    {
        //print("fuerzaAplicadaq");
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
        //this.transform.position = this.transform.position + new Vector3(-direccion.x * 1.6f, 0.8f, 0);
        //invulnerable = true;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //this.GetComponent<CharacterController2D>().dashing = true;
        this.GetComponent<ControllerPersonaje>().movimientoBloqueado = true;
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-(puntocontacto - this.transform.position).x, -(puntocontacto - this.transform.position).y).normalized * 35, ForceMode2D.Impulse);
        //print("FUERSITA" + new Vector2(-(puntocontacto - this.transform.position).x, -(puntocontacto - this.transform.position).y).normalized * 35);

    }
    // Update is called once per frame
    void Update()
    {
        //if (auxTiempoUsar > 0)
        //{
        //    auxTiempoUsar -= Time.deltaTime;

        //}
        //else
        //{
        //    auxTiempoUsar = 0;
        //    this.GetComponent<ControllerPersonaje>().combateBloqueado = false;
        //}
        if (barraVida != null)
        {
            barraVida.GetComponent<Image>().fillAmount = vidaActual / vidaMax;
        }
        if (auxcdTrasdaño > 0.2f)
        {
            this.GetComponent<ControllerPersonaje>().movimientoBloqueado = true;
        }
        else if (auxcdTrasdaño > 0.0f)
        {
            this.GetComponent<ControllerPersonaje>().movimientoBloqueado = false;
        }
        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            if (Input.GetKeyDown(KeyCode.K))
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
        if (collision.gameObject.tag == "Boss")
        {
            GetComponent<VidaPlayer>().RecibirDaño(GetComponent<VidaPlayer>().vidaActual, collision.gameObject.transform.position, this.transform.position);
            //this.transform.position = new Vector3(this.transform.position.x - 0.1f, this.transform.position.y + 0.2f, 0);
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
        if (collision.gameObject.tag == "balaVolador")
        {
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoBalaVolador, collision.gameObject.transform.position, collision.contacts[0].point);
        }
        if (collision.gameObject.tag == "Pinchos")
        {
            //if (this.GetComponent<Movimiento>().cayendoS == true)
            //{
            //    this.GetComponent<Movimiento>().cayendoS = false;
            //}
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañopinchos, collision.gameObject.transform.position, collision.contacts[0].point);
        }
        if (collision.gameObject.tag == "Pinchos")
        {
            //if (this.GetComponent<Movimiento>().cayendoS == true)
            //{
            //    this.GetComponent<Movimiento>().cayendoS = false;
            //}
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañopinchos, collision.gameObject.transform.position, collision.contacts[0].point);
        }

        if (collision.gameObject.tag == "CascadaGoo")
        {
            if (this.GetComponent<ControllerPersonaje>().auxCdDash < 0.2f) this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoCascada, collision.gameObject.transform.position, collision.contacts[0].point);

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
        if (collision.gameObject.tag == "Boss")
        {
            this.gameObject.transform.parent = null;
            GetComponent<VidaPlayer>().RecibirDaño(GetComponent<VidaPlayer>().vidaActual, collision.gameObject.transform.position, this.transform.position);
        }
        //if (collision.gameObject.tag == "Escudo")
        //{
        //    if (this.GetComponent<ControllerPersonaje>().haciendoCombate == true)
        //    {
        //        this.GetComponent<ControllerPersonaje>().rb.velocity = Vector3.zero;
        //        this.GetComponent<ControllerPersonaje>().haciendoCombate = false;

        //        this.GetComponent<ControllerPersonaje>().rb.gravityScale = this.GetComponent<ControllerPersonaje>().originalgravity;


        //        this.GetComponent<ControllerPersonaje>().enemigosPasados.Clear();
        //        this.GetComponent<ControllerPersonaje>().direccionCombate = Vector3.zero;
        //        this.GetComponent<ControllerPersonaje>().velocidadCombateUltima = Vector3.zero;
        //        this.GetComponent<ControllerPersonaje>().combateBloqueado = true;
        //        auxTiempoUsar = tiempoUsarCombateTrasEscudo;

        //        GetComponent<VidaPlayer>().RecibirDaño(0, (new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 1)), (new Vector2(this.transform.gameObject.transform.position.x, this.transform.gameObject.transform.position.y - 1)));
        //    }
        //    else
        //    {
        //        print("dañooo");
        //        GetComponent<VidaPlayer>().RecibirDaño(1, (new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 1)), (new Vector2(this.transform.gameObject.transform.position.x, this.transform.gameObject.transform.position.y - 1)));
        //    }

        //}
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
        if (collision.gameObject.tag == "balaVolador")
        {

            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoBalaVolador, collision.gameObject.transform.position, collision.contacts[0].point);
        }
        if (collision.gameObject.tag == "Pinchos")
        {
            //if (this.GetComponent<Movimiento>().cayendoS == true)
            //    {
            //        this.GetComponent<Movimiento>().cayendoS = false;
            //    }
            this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañopinchos, collision.gameObject.transform.position, collision.contacts[0].point);
        }
        if (collision.gameObject.tag == "CascadaGoo")
        {

            if (this.GetComponent<ControllerPersonaje>().auxCdDash < 0.2f) this.GetComponent<VidaPlayer>().RecibirDaño(this.GetComponent<VidaPlayer>().dañoCascada, collision.gameObject.transform.position, collision.contacts[0].point);
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
        //if (collision.gameObject.tag == "Escudo")
        //{
        //    if (this.GetComponent<ControllerPersonaje>().haciendoCombate == true)
        //    {
        //        this.GetComponent<ControllerPersonaje>().rb.velocity = Vector3.zero;
        //        this.GetComponent<ControllerPersonaje>().haciendoCombate = false;

        //        this.GetComponent<ControllerPersonaje>().rb.gravityScale = this.GetComponent<ControllerPersonaje>().originalgravity;


        //        this.GetComponent<ControllerPersonaje>().enemigosPasados.Clear();
        //        this.GetComponent<ControllerPersonaje>().direccionCombate = Vector3.zero;
        //        this.GetComponent<ControllerPersonaje>().velocidadCombateUltima = Vector3.zero;
        //        this.GetComponent<ControllerPersonaje>().combateBloqueado = true;
        //        auxTiempoUsar = tiempoUsarCombateTrasEscudo;
        //    }
        //    print("dañooo");
        //    GetComponent<VidaPlayer>().RecibirDaño(0, (new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 1)), (new Vector2(this.transform.gameObject.transform.position.x, this.transform.gameObject.transform.position.y - 1)));
        //}
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
