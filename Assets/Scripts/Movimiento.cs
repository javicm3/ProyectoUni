using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    //public float originalspeed;
    //public CharacterController2D controller;
    //public float fuerzaDashAbajo = 300f;
    //float movHorizontal = 0f;
    //public float speed = 20f;
    //public bool acelerando = false;
    //bool jump = false;
    //bool unavez = false;
    //Rigidbody2D rb;
    //public bool unavezDobleSalto = false;
    //public bool cayendoS = false;
    //float direccionSalto;
    //float speedpreSalto;
    //public float tiempocdDashAbajo = 0.5f;
    //float auxdashabajo;
    //public float speedMax = 2f;
    //public float multSpeed = 1f;
    //public bool haciendocambio = false;
    //public float lastdireccionmov;
    //public float primeraDireccionSalto;
    //public bool cambiadoaire = false;
    //float tiempoRecuerdoEspacio = 0.32f;
    //float tiempoRecuerdo;
    //public DetectorRebote detectorRebote;
    //EnergyManager em;
    //GameManager2 gm;
    //public GameObject SpriteCaida;
    //public bool maxSpeed = false;
    //public Material Shaderoriginal;
    //public Material ShadervelMax;
    //public Material ShaderoriginalCabolo;
    //public Material ShadervelMaxCabolo;
    //public Renderer Shaderaux;
    //public AudioClip dashabajo;
    //public Image cabolo;
    //public AudioSource source;
    //public bool parado = true;
    //public void Inertia()
    //{
    //    print("INERCIAAAAAAAAAAAAAAAAAAAAAAA");
    //    if (maxSpeed)
    //    {
    //        //this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, rb.velocity.y);
    //        this.GetComponent<Rigidbody2D>().AddForce(Input.GetAxisRaw("Horizontal") * -20 * new Vector2(1, 0), ForceMode2D.Force);
    //        this.GetComponent<CharacterController2D>().CancelInvoke("StopDash");
    //        this.GetComponent<CharacterController2D>().Invoke("StopDash", 0.06f);
    //        controller.puedomoverme = true;
    //    }
    //    else
    //    {


    //        //this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, rb.velocity.y);
    //        this.GetComponent<Rigidbody2D>().AddForce(Input.GetAxisRaw("Horizontal") * -8 * new Vector2(1, 0), ForceMode2D.Force);
    //        this.GetComponent<CharacterController2D>().CancelInvoke("StopDash");
    //        this.GetComponent<CharacterController2D>().Invoke("StopDash", 0.03f);
    //        controller.puedomoverme = true;
    //    }

    //}
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //    auxdashabajo = tiempocdDashAbajo;
    //    Shaderaux = GetComponentInChildren<Animator>().gameObject.GetComponent<Renderer>();
    //    em = this.GetComponent<EnergyManager>();
    //    rb = this.GetComponent<Rigidbody2D>();
    //    //gm = GameObject.FindObjectOfType<GameManager>();
    //    cabolo = GameObject.Find("cabolo").GetComponent<Image>();originalspeed = speed;
    //}
    //void CambiarDireccionAire()
    //{
    //    if (multSpeed > 0.9 * speedMax)
    //    {
    //        if (controller.m_Grounded)
    //        {
    //            haciendocambio = false;
    //        }
    //        else
    //        {

    //            multSpeed =/*-0.57f*/ 0.4f * speedMax;
    //            haciendocambio = false;
    //        }

    //    }
    //    else if (multSpeed > 0.8 * speedMax)
    //    {
    //        if (controller.m_Grounded)
    //        {
    //            haciendocambio = false;
    //        }
    //        else
    //        {

    //            multSpeed =/*-0.57f*/ 0.3f * speedMax;
    //            haciendocambio = false;
    //        }

    //    }
    //    else if (multSpeed > 0.57 * speedMax)
    //    {
    //        if (controller.m_Grounded)
    //        {
    //            haciendocambio = false;
    //        }
    //        else
    //        {

    //            multSpeed =/*-0.57f*/ 0.2f * speedMax;
    //            haciendocambio = false;
    //        }

    //    }
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    auxdashabajo -= Time.deltaTime;
    //    if (auxdashabajo < 0) auxdashabajo = 0;
    //    tiempoRecuerdo -= Time.deltaTime;
    //    if (controller.dashing == true) if (controller.dash2 == false) rb.velocity = new Vector2(rb.velocity.x, 0);

    //    if (!controller.Grounded)
    //    {
    //        if (cayendoS == true)
    //        {

    //            if (controller.pegadoPared == true) cayendoS = false;
    //            if (controller.puedoEscalar == false) cayendoS = false;
    //            multSpeed = 0;
    //            //controller.puedomoverme = false;
    //            rb.velocity = new Vector2(0, rb.velocity.y);
    //            SpriteCaida.GetComponent<SpriteRenderer>().enabled = false;

    //        }
    //        else if (cayendoS == false)
    //        {

    //            if (em.actualEnergy >= em.energiaDashAbajo)
    //            {
    //                SpriteCaida.GetComponent<SpriteRenderer>().enabled = true;
    //            }
    //            else
    //            {
    //                SpriteCaida.GetComponent<SpriteRenderer>().enabled = false;
    //            }


    //            if (controller.puedoEscalar == true)
    //            {
    //                if (Input.GetKeyDown(KeyCode.S))
    //                {
    //                    if (auxdashabajo <= 0)
    //                    {
    //                        if ((controller.pegadoPared != true) && (em.actualEnergy >= em.energiaDashAbajo))
    //                        {
    //                            if (controller.dashing == true)
    //                            {
    //                                controller.dashing = false;
    //                                controller.StopDash();
    //                            }
    //                            if (this.GetComponentInChildren<TriggerColliderPLayer>() != null) this.GetComponentInChildren<TriggerColliderPLayer>().IniciarAtaque("abajo");
    //                            em.RestarEnergia(em.energiaDashAbajo);
    //                            this.GetComponent<VidaPlayer>().recienAtacado = true;
    //                            cayendoS = true;
    //                            rb.velocity = new Vector2(0, 0);
    //                            rb.AddForce(new Vector2(0, -fuerzaDashAbajo));
    //                            auxdashabajo = tiempocdDashAbajo;
    //                            source.PlayOneShot(dashabajo);
    //                        }
    //                    }


    //                }
    //            }

    //        }
    //        if (gm.personajevivo == true)
    //        {
    //            float velocidad = movHorizontal;
    //            if (controller.dashing == false)
    //            {
    //                if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(velocidad * speedMax)) { }
    //                if (controller.puedomoverme != false)
    //                {
    //                    //if (rb.velocity.y < -0.5f)
    //                    //{
    //                    //    controller.Move(velocidad * multSpeed * 0.8f * Time.fixedDeltaTime, false, jump);
    //                    //}
    //                    //else
    //                    if (controller.puedoEscalar == true)
    //                    {
    //                        controller.Move(new Vector2(velocidad, 0) * multSpeed * 0.9f * Time.fixedDeltaTime, false, jump);
    //                    }
    //                    else
    //                    {


    //                        controller.Move(new Vector2(Vector2.Perpendicular(-controller.normal).x, Vector2.Perpendicular(controller.normal).y) * multSpeed * 0.9f * Time.fixedDeltaTime, false, jump);
    //                    }

    //                }

    //            }
    //            else
    //            {
    //                if (controller.impulsohecho == false)
    //                {
    //                    multSpeed = speedMax;
    //                }
    //                else
    //                {
    //                    multSpeed = 0.7f * speedMax;
    //                }

    //            }

    //        }

    //    }

    //    else// if (controller.Grounded == true)
    //    {
    //        SpriteCaida.GetComponent<SpriteRenderer>().enabled = false;
    //        if (controller.dashing == false)
    //        {
    //            if (cayendoS == false) this.GetComponent<CharacterController2D>().Invoke("StopInvulnerable", 1f);

    //        }
    //        if (controller.puedomoverme != true)
    //        {
    //            controller.puedomoverme = true;
    //            multSpeed = 0;
    //        }
    //        cayendoS = false;
    //        if (controller.dashing == false)
    //        {

    //            controller.Move(new Vector2(movHorizontal, 0) * multSpeed * Time.fixedDeltaTime, false, jump);


    //            cambiadoaire = false;
    //            primeraDireccionSalto = 0;
    //        }
    //        else
    //        {
    //            if (controller.dash2 == true)
    //            {


    //                primeraDireccionSalto = 0;
    //                if (Input.GetButtonDown("Jump"))
    //                {
    //                    tiempoRecuerdo = tiempoRecuerdoEspacio;

    //                }
    //                if ((tiempoRecuerdo > 0) && (controller.Grounded))
    //                {
    //                    tiempoRecuerdo = 0;
    //                    controller.SaltoPlayer();
    //                    speedpreSalto = Input.GetAxisRaw("Horizontal");
    //                    direccionSalto = speedpreSalto;
    //                }
    //                if (Input.GetButtonUp("Jump"))
    //                {
    //                    if (detectorRebote.recentbounce == false)
    //                    {
    //                        if (rb.velocity.y > 0)
    //                        {
    //                            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    //                        }

    //                    }
    //                }
    //            }
    //            //multSpeed = speedMax;
    //        }




    //    }

    //    jump = false;

    //    if (gm.personajevivo == true)
    //    {
    //        if (Input.GetAxisRaw("Horizontal") != 0)
    //        {
    //            //if (controller.puedoEscalar)
    //            //{
    //            if (Input.GetAxisRaw("Horizontal") != lastdireccionmov)
    //            {
    //                if (controller.m_Grounded)
    //                {
    //                    //this.GetComponent<Particulas>().SpawnParticulas(this.GetComponent<Particulas>().particulasBounce, new Vector2((controller.m_GroundCheck.transform.position.x + this.GetComponent<Movimiento>().lastdireccionmov), controller.m_GroundCheck.transform.position.y));
    //                    Instantiate(GetComponent<ChispasSuperficie>().particulaRayo, new Vector2((controller.m_GroundCheck.transform.position.x + this.GetComponent<Movimiento>().lastdireccionmov), controller.m_GroundCheck.transform.position.y + 1.3f), Quaternion.LookRotation(-Vector2.right, new Vector2(-1, -1)));

    //                }
    //                if (multSpeed > 0.9 * speedMax)
    //                {
    //                    if (controller.m_Grounded)
    //                    {





    //                        multSpeed *= 0.7f;
    //                    }
    //                    else
    //                    {

    //                        print("cambio");
    //                        if (controller.wallJumping == true)
    //                        {

    //                        }
    //                        else
    //                        {
    //                            multSpeed *= 0.8f;
    //                            if (controller.dash2 == false)
    //                            {

    //                                controller.dash2 = true;
    //                                controller.puedomoverme = false;
    //                                CancelInvoke("Inertia");
    //                                Invoke("Inertia", 0.04f);
    //                            }
    //                        }
    //                        multSpeed *= 0.3f;

    //                        //multSpeed *= 0.45f;
    //                        //if (haciendocambio == false)
    //                        //{
    //                        //    //multSpeed *= 0.8f;
    //                        //    Invoke("CambiarDireccionAire", 0.2f);
    //                        //    haciendocambio = true;
    //                        //}


    //                    }

    //                }
    //                else if (multSpeed > 0.57 * speedMax)
    //                {
    //                    if (controller.m_Grounded)
    //                    {


    //                        multSpeed *= 0.57f;
    //                    }
    //                    else
    //                    {
    //                        if (controller.wallJumping == true)
    //                        {

    //                        }
    //                        else
    //                        {
    //                            multSpeed *= 0.8f;
    //                            if (controller.dash2 == false)
    //                            {

    //                                controller.dash2 = true;
    //                                controller.puedomoverme = false;
    //                                CancelInvoke("Inertia");
    //                                Invoke("Inertia", 0.07f);
    //                            }

    //                        }
    //                        multSpeed *= 0.5f;
    //                        //multSpeed *= 0.4f;
    //                        //multSpeed =/*-0.57f*/ 0.4f * speedMax;
    //                        //if (haciendocambio == false)
    //                        //{
    //                        //    //multSpeed *= 0.8f;
    //                        //    Invoke("CambiarDireccionAire", 0.2f);
    //                        //    haciendocambio = true;
    //                        //}
    //                    }

    //                }


    //            }
    //            //}
    //            //else
    //            //{
    //            //}


    //            if (controller.dashing == false)
    //            {

    //                if ((controller.puedomoverme == true) && (controller.wallJumping == false) && (controller.dash2 == false))
    //                {
    //                    if (Input.GetAxisRaw("Horizontal") > 0)
    //                    {

    //                        lastdireccionmov = 1f;



    //                    }
    //                    else if (Input.GetAxisRaw("Horizontal") < 0)
    //                    {
    //                        lastdireccionmov = -1f;
    //                    }
    //                }


    //            }


    //            acelerando = true;
    //        }
    //        else
    //        {
    //            acelerando = false;
    //        }
    //        if (acelerando == true)
    //        {


    //            if (multSpeed < 0.5)
    //            {
    //                if (controller.m_Grounded)
    //                {
    //                    multSpeed = 0.5f;
    //                }
    //                else
    //                {
    //                    if (controller.puedomoverme == true) multSpeed += Time.deltaTime * 2.5f;
    //                }

    //            }
    //            else if (multSpeed < 0.65f)
    //            {
    //                parado = false;
    //                if (controller.m_Grounded)
    //                {
    //                    multSpeed += Time.deltaTime * 2.6f;
    //                }
    //                else
    //                {
    //                    if (controller.puedomoverme == true) multSpeed += Time.deltaTime * 2f;
    //                }

    //            }
    //            else if (multSpeed < 0.9f)
    //            {
    //                parado = false;
    //                if (controller.m_Grounded)
    //                {
    //                    multSpeed += Time.deltaTime * 2f;
    //                }
    //                else
    //                {
    //                    if (controller.puedomoverme == true) multSpeed += Time.deltaTime * 1.3f;
    //                }

    //            }
    //            else
    //            {
    //                parado = false;
    //                if (controller.m_Grounded)
    //                {
    //                    multSpeed += Time.deltaTime * 0.6f;
    //                }
    //                else
    //                {
    //                    if (controller.puedomoverme == true) multSpeed += Time.deltaTime * 0.3f;
    //                }


    //            }

    //            if (multSpeed >= speedMax)
    //            {

    //                multSpeed = speedMax;
    //            }
    //            if (multSpeed >= speedMax * 0.94f)
    //            {
    //                Shaderaux.material = ShadervelMax;
    //                cabolo.material = ShadervelMaxCabolo;
    //                maxSpeed = true;
    //                if (unavez == false)
    //                {

    //                    unavez = true;
    //                    //this.GetComponent<Particulas>().SpawnParticulas(this.GetComponent<Particulas>().particulasvelMax, controller.m_GroundCheck.position);
    //                }
    //            }
    //            else
    //            {
    //                unavez = false;
    //                Shaderaux.material = Shaderoriginal;
    //                cabolo.material = ShaderoriginalCabolo;
    //                maxSpeed = false;
    //            }

    //        }
    //        else
    //        {
    //            if (controller.dashing == false)
    //            {
    //                unavez = false;
    //                Shaderaux.material = Shaderoriginal;
    //                cabolo.material = ShaderoriginalCabolo;
    //                maxSpeed = false;
    //                parado = true;
    //                if (controller.dashgolpeo == false)
    //                {
    //                    if (controller.Grounded)
    //                    {
    //                        multSpeed -= Time.deltaTime * (15 / Mathf.Abs(rb.velocity.x));
    //                    }
    //                    else
    //                    {
    //                        multSpeed -= Time.deltaTime * (4 / Mathf.Abs(rb.velocity.x));
    //                    }
    //                    if (multSpeed < 0.0f) multSpeed = 0.0f;
    //                }
    //            }


    //        }
    //        if (Input.GetAxisRaw("Horizontal") != 0)
    //        {
    //            if (/*controller.Normal.x < 0 && */controller.Normal.y < 0.01f)
    //            {
    //                if (lastdireccionmov != 0/* && controller.targetVelocity.x > 0*/)
    //                {
    //                    speed = Mathf.Abs(speed) * -1f;
    //                }
    //                else
    //                {
    //                    speed = Mathf.Abs(speed);
    //                }
    //                //lastdireccionmov = -1;
    //                //multSpeed = Mathf.Abs(multSpeed) * -1;
    //                //controller.m_Rigidbody2D.velocity = new Vector2(Mathf.Abs(controller.m_Rigidbody2D.velocity.x) * -1, controller.m_Rigidbody2D.velocity.y);
    //            }
                
    //            //if (/*controller.Normal.x > 0 &&*/ controller.Normal.y < 0)
    //            //{
    //            //    if (lastdireccionmov < 0/* && controller.targetVelocity.x > 0*/)
    //            //    {
    //            //        speed = Mathf.Abs(speed) * -1;
    //            //    }
    //            //    else
    //            //    {
    //            //        speed = Mathf.Abs(speed);
    //            //    }
    //            //    //lastdireccionmov = -1;
    //            //    //   multSpeed = Mathf.Abs(multSpeed) * -1;
    //            //    //controller.m_Rigidbody2D.velocity = new Vector2(Mathf.Abs(controller.m_Rigidbody2D.velocity.x) * -1, controller.m_Rigidbody2D.velocity.y);
    //            //}
              
    //            if (/*controller.Normal.x < 0 &&*/ controller.Normal.y >= 0.01f)
    //            {
    //                //if (lastdireccionmov < 0)
    //                //{ speed = Mathf.Abs(speed) * -1; }
    //                //else
    //                //{
    //                speed = Mathf.Abs(speed);
    //                //}

    //                //multSpeed = Mathf.Abs(multSpeed) * -1;
    //                //controller.m_Rigidbody2D.velocity = new Vector2(Mathf.Abs(controller.m_Rigidbody2D.velocity.x) * -1, controller.m_Rigidbody2D.velocity.y);
    //            }
               
    //            //if (controller.Normal.x > 0 && controller.Normal.y > 0)
    //            //{
    //            //    //if (lastdireccionmov > 0)
    //            //    //{
    //            //    //    speed = Mathf.Abs(speed) * -1;
    //            //    //}
    //            //    //else
    //            //    //{
    //            //    speed = Mathf.Abs(speed);
    //            //    //}

    //            //    //multSpeed = Mathf.Abs(multSpeed) * -1;
    //            //    //controller.m_Rigidbody2D.velocity = new Vector2(Mathf.Abs(controller.m_Rigidbody2D.velocity.x) * -1, controller.m_Rigidbody2D.velocity.y);
    //            //}
    //            if (controller.puedomoverme == false)
    //            {

    //                movHorizontal = multSpeed * lastdireccionmov * speed;


    //            }
    //            else
    //            {
    //                if (this.GetComponent<CharacterController2D>().pegadoPared == true)
    //                {
    //                    movHorizontal = Input.GetAxisRaw("Horizontal") * 0.1f * speed;
    //                }
    //                else
    //                {
    //                    movHorizontal = Input.GetAxisRaw("Horizontal") * multSpeed * speed;
    //                }
    //            }

    //        }
    //        else movHorizontal = multSpeed * lastdireccionmov * speed;

    //        if (!controller.Grounded)
    //        {
    //            if (controller.puedoEscalar == true)
    //            {
    //                speed = Mathf.Abs(speed);
    //                rb.AddForce(new Vector2(0, -100f * Time.deltaTime));
    //                if (rb.velocity.y <= 6f)
    //                {
    //                    rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
    //                    /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/

    //                }
    //                if (rb.velocity.y < -12)
    //                {
    //                    rb.AddForce(new Vector2(0, -200f * Time.deltaTime));
    //                    /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/

    //                }
    //            }
    //            else
    //            {

    //            }

    //        }
    //        else if (controller.Grounded)
    //        {
    //            //si estoy encima de una plataforma bajar
    //            if (Input.GetKey(KeyCode.S))
    //            {
    //                multSpeed -= Time.deltaTime;
    //            }
    //        }
    //        if (!controller.Grounded && primeraDireccionSalto == 0)
    //        {
    //            primeraDireccionSalto = movHorizontal;
    //        }
    //        else if (!cambiadoaire)
    //        {
    //            float multDireccion = movHorizontal * primeraDireccionSalto;
    //            if (multDireccion < 0)
    //            {
    //                cambiadoaire = true;
    //            }
    //        }

    //        if (Input.GetButtonDown("Jump"))
    //        {
    //            tiempoRecuerdo = tiempoRecuerdoEspacio;
    //            if (controller.puedoSaltar == false)
    //            {
    //                if (/*controller.dashing != true && controller.dash2 != true && */controller.pegadoPared != true && cayendoS == false && controller.wallJumping != true)
    //                {
    //                    if (unavezDobleSalto == false)
    //                    {
    //                        unavezDobleSalto = true;
    //                        controller.DobleSaltoPlayer();
    //                        if (multSpeed > 0.7 && multSpeed < 1) multSpeed = 1;

    //                    }
    //                }

    //            }
    //        }


    //        if (controller.puedoSaltar)
    //        {
    //            //if (Input.GetButtonDown("Jump"))
    //            //{

    //            //    this.transform.parent = null;
    //            //    jump = true;
    //            //    speedpreSalto = Input.GetAxisRaw("Horizontal");
    //            //    direccionSalto = speedpreSalto;
    //            //}

    //            if ((tiempoRecuerdo > 0))
    //            {
    //                tiempoRecuerdo = 0;
    //                if (controller.dash2 == false) controller.SaltoPlayer();
    //                speedpreSalto = Input.GetAxisRaw("Horizontal");
    //                direccionSalto = speedpreSalto;
    //            }

    //        }
    //        if (Input.GetButtonUp("Jump"))
    //        {
    //            if (detectorRebote.recentbounce == false)
    //            {
    //                controller.puedoSaltar = false;
    //                if (rb.velocity.y > 0)
    //                {
    //                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    //                }

    //            }

    //        }

    //        //if (movHorizontal == 0)
    //        //{
    //        //    this.GetComponent<Animator>().SetFloat("speed", 0);
    //        //}
    //        //else if (movHorizontal > 0.2)
    //        //{
    //        //    this.GetComponent<Animator>().SetFloat("speed", 1);
    //        //}
    //        //else if (movHorizontal < -0.2)
    //        //{
    //        //    this.GetComponent<Animator>().SetFloat("speed", 2);
    //        //}

    //        //if (jump == true)
    //        //{
    //        //    this.GetComponent<Animator>().SetBool("jump", true);
    //        //}
    //        //else
    //        //{
    //        //    this.GetComponent<Animator>().SetBool("jump", false);
    //        //} void DashAbajo()
    //        //{
    //        //    cayendoS = true;
    //        //    rb.velocity = new Vector2(0, 0);
    //        //    rb.AddForce(new Vector2(0, -fuerzaDashAbajo));
    //        //}
    //        //// Update is called once per frame
    //        //void Update()
    //        //{
    //        //    if (Input.GetKeyDown(KeyCode.L))
    //        //    {
    //        //        tiempoantescaer = 0.5f;
    //        //        auxtiempoantescaer = tiempoantescaer;
    //        //    }
    //        //    if (antescaer == true)
    //        //    {

    //        //        if (tiempoantescaer == 0.5f)
    //        //        {
    //        //            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    //        //            auxtiempoantescaer -= Time.deltaTime;
    //        //            if (auxtiempoantescaer <= 0)
    //        //            {
    //        //                if (controller.pegadoPared != true)
    //        //                {
    //        //                    em.RestarEnergia(em.energiaDashAbajo);

    //        //                    DashAbajo();

    //        //                    auxtiempoantescaer = tiempoantescaer;
    //        //                    antescaer = false;
    //        //                }
    //        //            }
    //        //        }
    //        //        else
    //        //        {
    //        //            auxtiempoantescaer = tiempoantescaer;
    //        //        }
    //        //        tiempoRecuerdo -= Time.deltaTime;
    //        //        if (controller.dashing == true) if (controller.dash2 == false) rb.velocity = new Vector2(rb.velocity.x, 0);

    //        //        if (!controller.Grounded)
    //        //        {
    //        //            if (cayendoS == true)
    //        //            {
    //        //                this.GetComponent<VidaPlayer>().invulnerable = true;
    //        //                multSpeed = 0;
    //        //                controller.puedomoverme = false;
    //        //                rb.velocity = new Vector2(0, rb.velocity.y);
    //        //                print(rb.velocity);
    //        //            }
    //        //            else if (cayendoS == false)
    //        //            {
    //        //                if (controller.dashing == false)
    //        //                {
    //        //                    this.GetComponent<VidaPlayer>().invulnerable = false;
    //        //                }
    //        //                if (em.actualEnergy > em.energiaDashAbajo)
    //        //                {


    //        //                    if (Input.GetKeyDown(KeyCode.S))
    //        //                    {
    //        //                        antescaer = true;


    //        //                    }
    //        //                }
    //        //            }
    //        //        }

    //    }


    }





