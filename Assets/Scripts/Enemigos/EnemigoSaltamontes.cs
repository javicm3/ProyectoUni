﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSaltamontes : EnemigoPadre
{
    [Header("Tocar")]
    public GameObject controlador;
    public float tiempoHastaSpawn = 0.5f;
    public float auxTiempoHastaSpawn = 0.5f;

    public float fVerticalMov = 5;
    public float fHorizMov = 5;
    public float tiempoEntreSaltosAndar = 1.5f;
    [Space(5)] public float tiempoEntreSaltosPerseguir = 1.2f;
    public float fVerticalPerseg = 5;
    public float fHorizPerseg = 5;
    [Space(5)] public float fVerticalAtaque = 7;
    public float fHorizAtaqueMin = 2;
    public float fHorizAtaqueMax = 7;
    public float tiempoEntreSaltosAtacar = 1f;



    public float distanciaAlSuelo = 1f;

    public float distanciaAtacar = 15f;
    public float distanciaPerseguir = 27f;[Space(5)]
    public GameObject pIzq;
    public GameObject pMaxIzq;
    public GameObject pDer;
    public GameObject pMaxDer;
    public LayerMask capasSuelo;[Space(5)]
    public bool mirandoDerecha = true;
    public bool direccionDerecha = true;[Space(5)]
    [Header("No tocar")]
    [SerializeField] bool grounded;
    public GameObject prefabEnemigoSpawneado;
    public float fSalidaPequeños = 15f;
    GameObject puntoSpawn1;
    GameObject puntoSpawn2;
    public float faseActual = 0;
    public enum States
    {
        Desactivado,
        Movimiento,
        Perseguir,
        Atacar,
        Stun
    }
    public States estado;
    float auxtiempoEntreSaltosAndar;
    float auxtiempoEntreSaltosPerseguir;
    float auxtiempoEntreSaltosAtacar;
    public GameObject parte1;
    public GameObject parte2;
    public float fuerzaDivision = 50f;
    Animator animCC;

    Rigidbody2D rb;
    //GameObject player;
    private Vector3 velocityBeforePhysicsUpdate;
    Color colororiginal;
    void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = rb.velocity;
    }
    // Start is called before the first frame update
    void Start()
    {
        estado = States.Desactivado;
        auxtiempoEntreSaltosAndar = tiempoEntreSaltosAndar;
        auxtiempoEntreSaltosAtacar = tiempoEntreSaltosAtacar;
        auxtiempoEntreSaltosPerseguir = tiempoEntreSaltosPerseguir;
        //player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
        rb = this.GetComponent<Rigidbody2D>();
        animCC = GetComponent<Animator>();
        if (this.GetComponent<SpriteRenderer>() != null)
        {
            colororiginal = this.GetComponent<SpriteRenderer>().color;
        }

    }
    public override void Stun()
    {if (stun == false)
        {
            stun = true;
            ManagerLogros.Instance.AddStat("EnemigosStun");
        }
     
        animCC.SetBool("Estuneado", true);
        animCC.SetBool("Moviendose", false);

    }
    public override void Reactivar()
    {
        //controlador.GetComponent<>
    }
    public override void ActivarPrimeraVez()
    {

        if (Vector2.Distance(this.transform.position, player.transform.position) < distanciaActivacion)
        {
            activado = true;
            estado = States.Movimiento;
            //reseter posicion
        }
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();



        //if (activado == false)
        //{

        //    if (Vector2.Distance(this.transform.position, player.transform.position) < distanciaActivacion)
        //    {
        //        activado = true;
        //        estado = States.Movimiento;
        //        //reseter posicion
        //    }
        //}
        if (activado)
        {
            if ((faseActual == 2) && (estado != States.Stun && estado != States.Desactivado))
            {
                if (this.GetComponent<SpriteRenderer>() != null) this.GetComponent<SpriteRenderer>().color = colororiginal;


            }
            else if (faseActual == 2)
            {
                if (controlador.GetComponent<ControladorSaltamontesRespawn>().pequeñosStun == 4)
                {
                    Destroy(this.gameObject);
                }
            }
            if (stun)
            {
                estado = States.Stun;
            }
            else
            {
                ComprobarSuelo();
                ComprobarDistancia();
                if (velocityBeforePhysicsUpdate.x > 0)
                {
                    this.transform.localScale = new Vector3(1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                }
                else if (velocityBeforePhysicsUpdate.x < 0)
                {

                    this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                }
            }



            if (estado == States.Desactivado)
            {
                auxtiempoEntreSaltosAndar = tiempoEntreSaltosAndar;
                auxtiempoEntreSaltosAtacar = tiempoEntreSaltosAtacar;
                auxtiempoEntreSaltosPerseguir = tiempoEntreSaltosPerseguir;
            }
            else if (estado == States.Movimiento)
            {
                stun = false;
                animCC.SetBool("Estuneado", false);

                auxtiempoEntreSaltosAtacar = tiempoEntreSaltosAtacar;
                auxtiempoEntreSaltosPerseguir = tiempoEntreSaltosPerseguir;

                if (grounded)
                {
                    if (auxtiempoEntreSaltosAndar > 0)
                    {

                        auxtiempoEntreSaltosAndar -= Time.deltaTime;
                        animCC.SetBool("Moviendose", false);

                    }
                    else
                    {
                        auxtiempoEntreSaltosAndar = tiempoEntreSaltosAndar;
                        Saltar(fHorizMov, fVerticalMov);
                        animCC.SetBool("Moviendose", true);
                    }
                }
            }
            else if (estado == States.Perseguir)
            {
                if (player.transform.position.x > this.transform.position.x)
                {
                    mirandoDerecha = true;
                    direccionDerecha = true;

                }
                else
                {
                    mirandoDerecha = false;
                    direccionDerecha = false;
                }
                auxtiempoEntreSaltosAndar = tiempoEntreSaltosAndar;
                auxtiempoEntreSaltosAtacar = tiempoEntreSaltosAtacar;
                if (grounded)
                {
                    if (auxtiempoEntreSaltosPerseguir > 0)
                    {

                        auxtiempoEntreSaltosPerseguir -= Time.deltaTime;
                        animCC.SetBool("Moviendose", false);


                    }
                    else
                    {
                        auxtiempoEntreSaltosPerseguir = tiempoEntreSaltosPerseguir;
                        Saltar(fHorizPerseg, fVerticalPerseg);
                        animCC.SetBool("Moviendose", true);
                    }
                }

            }
            else if (estado == States.Atacar)
            {
                if (player.transform.position.x > this.transform.position.x)
                {
                    mirandoDerecha = true;
                    direccionDerecha = true;

                }
                else
                {
                    mirandoDerecha = false;
                    direccionDerecha = false;
                }
                auxtiempoEntreSaltosAndar = tiempoEntreSaltosAndar;
                auxtiempoEntreSaltosPerseguir = tiempoEntreSaltosPerseguir;
                if (grounded)
                {
                    if (auxtiempoEntreSaltosAtacar > 0)
                    {

                        auxtiempoEntreSaltosAtacar -= Time.deltaTime;
                        animCC.SetBool("Moviendose", false);
                    }
                    else
                    {
                        auxtiempoEntreSaltosAtacar = tiempoEntreSaltosAtacar;
                        animCC.SetBool("Moviendose", true);

                        Saltar(Mathf.Clamp(Vector2.Distance(this.transform.position, player.transform.position), fHorizAtaqueMin, fHorizAtaqueMax), fVerticalAtaque);
                    }
                }
            }
            else if (estado == States.Stun)
            {
                if (faseActual != 2)
                {


                }
                else
                {
                    if (this.GetComponent<SpriteRenderer>() != null) this.GetComponent<SpriteRenderer>().color = Color.black;

                }
                auxtiempoEntreSaltosAndar = tiempoEntreSaltosAndar;
                auxtiempoEntreSaltosAtacar = tiempoEntreSaltosAtacar;
                auxtiempoEntreSaltosPerseguir = tiempoEntreSaltosPerseguir;

                if (auxTiempoHastaSpawn < 0)
                {
                    auxTiempoHastaSpawn = 0;
                    if (faseActual != 2)
                    {
                        estado = States.Desactivado;
                        auxTiempoHastaSpawn = tiempoHastaSpawn;
                        SpawnEnemigos();
                        this.gameObject.SetActive(false);
                        stun = false;
                    }
                    else
                    {

                        if (this.GetComponent<SpriteRenderer>() != null) this.GetComponent<SpriteRenderer>().color = Color.black;
                        controlador.GetComponent<ControladorSaltamontesRespawn>().SumarPeq();
                        GetComponentInChildren<DañoEnemigos>().gameObject.GetComponent<BoxCollider2D>().enabled = false;

                    }

                    //if(faseActual!=2)



                }
                else if (auxTiempoHastaSpawn > 0)
                {


                    if (parte1.layer!=11)
                    {
                        Destroy(parte1, 2f);
                        parte1.transform.parent = null;
                        parte1.GetComponent<Rigidbody2D>().isKinematic = false;
                        parte1.GetComponent<BoxCollider2D>().enabled = true;
                        parte1.layer = 11;
                        parte1.GetComponent<Rigidbody2D>().AddForce(fuerzaDivision * -this.transform.right, ForceMode2D.Impulse);
                        Destroy(parte2, 2f);
                        parte2.transform.parent = null;
                        parte2.GetComponent<Rigidbody2D>().isKinematic = false;
                        parte2.GetComponent<BoxCollider2D>().enabled = true;
                        parte2.layer = 11;
                        parte2.GetComponent<Rigidbody2D>().AddForce(fuerzaDivision * this.transform.right, ForceMode2D.Impulse);
                    }

                    auxTiempoHastaSpawn -= Time.deltaTime;
                }

            }
        }
    }
    void SpawnEnemigos()
    {
        NewAudioManager.Instance.Play("EnemigoDivisión");
        GameObject enemigoSpawneado = Instantiate(prefabEnemigoSpawneado, this.transform.Find("PuntoSpawn1").transform.position, Quaternion.identity);
        enemigoSpawneado.GetComponent<Rigidbody2D>().velocity = (transform.Find("PuntoSpawn1").transform.position - this.transform.position).normalized * fSalidaPequeños;
        enemigoSpawneado.GetComponent<EnemigoSaltamontes>().pDer = pDer;
        enemigoSpawneado.GetComponent<EnemigoSaltamontes>().pMaxDer = pMaxDer;
        enemigoSpawneado.GetComponent<EnemigoSaltamontes>().pIzq = pIzq;
        enemigoSpawneado.GetComponent<EnemigoSaltamontes>().pMaxIzq = pMaxIzq;
        enemigoSpawneado.GetComponent<EnemigoSaltamontes>().controlador = controlador;
        //enemigoSpawneado.name = (string)("hijo" + Random.Range(0, 10)+"fase"+faseActual+1);

        enemigoSpawneado = Instantiate(prefabEnemigoSpawneado, this.transform.Find("PuntoSpawn2").transform.position, Quaternion.identity);
        enemigoSpawneado.GetComponent<Rigidbody2D>().velocity = (transform.Find("PuntoSpawn2").transform.position - this.transform.position).normalized * fSalidaPequeños;
        enemigoSpawneado.GetComponent<EnemigoSaltamontes>().pDer = pDer;
        enemigoSpawneado.GetComponent<EnemigoSaltamontes>().pMaxDer = pMaxDer;
        enemigoSpawneado.GetComponent<EnemigoSaltamontes>().pIzq = pIzq;
        enemigoSpawneado.GetComponent<EnemigoSaltamontes>().pMaxIzq = pMaxIzq;
        enemigoSpawneado.GetComponent<EnemigoSaltamontes>().controlador = controlador;
        //enemigoSpawneado.name = (string)("hijo" + Random.Range(10, 20) + "fase" + faseActual + 1);


    }
    public void Saltar(float fX, float fY)
    {
        NewAudioManager.Instance.Play("EnemigoDivisionSalto");
        if (mirandoDerecha == true)
        {
            //print((((((new Vector2(fX, fY).magnitude* new Vector2(fX, fY).magnitude* Mathf.Sin(2*Vector2.Angle(new Vector2(fX, 0), new Vector2(fX, fY)))) / Physics.gravity.magnitude  + "formulon"+ Vector2.Angle(new Vector2(fX, 0), new Vector2(fX, fY)) +"angulo"+ Mathf.Sin(2*Vector2.Angle(new Vector2(fX, 0), new Vector2(fX, fY))))))));
            if (this.transform.position.x + fX > pMaxDer.transform.position.x)
            {
                mirandoDerecha = false;
                direccionDerecha = false;

                //rb.AddForce(new Vector2(-fX, fY), ForceMode2D.Impulse);
                rb.velocity = new Vector2(-fX, fY);
            }
            else if (this.transform.position.x + fX * 0.5f > pDer.transform.position.x)
            {
                mirandoDerecha = false;
                direccionDerecha = false;

                //rb.AddForce(new Vector2(fX, fY), ForceMode2D.Impulse);
                rb.velocity = new Vector2(-fX, fY);
            }
            else
            {
                rb.velocity = new Vector2(fX, fY);
                //rb.AddForce(new Vector2(fX, fY), ForceMode2D.Impulse);
                //print(new Vector2(fX, fY).magnitude + "velocidad");
                //print(rb.velocity + "velocideead");
            }

        }
        else
        {
            if (this.transform.position.x - fX < pMaxIzq.transform.position.x)
            {
                mirandoDerecha = true;
                direccionDerecha = true;

                rb.velocity = new Vector2(fX, fY);
            }
            else if (this.transform.position.x - fX * 0.5f < pIzq.transform.position.x)
            {
                mirandoDerecha = true;
                direccionDerecha = true;

                rb.velocity = new Vector2(fX, fY);
            }
            else
            {
                rb.velocity = new Vector2(-fX, fY);
            }
        }

    }
    void ComprobarSuelo()
    {
        Debug.DrawRay(transform.position, -this.transform.up * (distanciaAlSuelo + 0.2f), Color.green);

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, -this.transform.up, distanciaAlSuelo + 0.3f, capasSuelo);
        if (hit.collider != null)
        {
            if (grounded == false)
            {
                NewAudioManager.Instance.Play("EnemigoDivisiónCaída");
            }
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
    void ComprobarDistancia()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) < distanciaAtacar)
        {
            if ((player.transform.position.x > pDer.transform.position.x) || (player.transform.position.x < pIzq.transform.position.x))
            {
                estado = States.Movimiento;
            }
            else
            {
                estado = States.Atacar;
            }


        }
        else if (Vector2.Distance(this.transform.position, player.transform.position) < distanciaPerseguir)
        {
            if ((player.transform.position.x > pDer.transform.position.x) || (player.transform.position.x < pIzq.transform.position.x))
            {
                estado = States.Movimiento;
            }
            else
            {
                estado = States.Perseguir;
            }

        }
        if (Vector2.Distance(this.transform.position, player.transform.position) > distanciaPerseguir)
        {

            estado = States.Movimiento;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 8)
        {

            if (collision.contacts[0].normal.x > 0.5f)
            {
                rb.velocity = new Vector2(-velocityBeforePhysicsUpdate.x, rb.velocity.y);
                if (rb.velocity.x < 0)
                {
                    mirandoDerecha = false;
                    direccionDerecha = false;

                }
                else
                {
                    mirandoDerecha = true;
                    direccionDerecha = false;
                }
            }
        }
    }
}
