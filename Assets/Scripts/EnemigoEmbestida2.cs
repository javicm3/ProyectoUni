using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemigoEmbestida2 : EnemigoPadre
{
    public enum States
    {
        Andar,
        Perseguir,
        Embestir,
        //Idle,
        Muerte,
        Stun
    }
    public bool reciengolpeado = false;
    bool grounded = true;
    public float maxtiempoIdle = 2.5f;
    public float mintiempoIdle = 0.7f;
    float aux;
    public float rangoDeteccion = 5;
    public float rangoAtaque = 3f;
    public float speedembestida = 6f;
    public float speedactualembestida;
    public float cdEmbestida = 2f;
    [SerializeField] float auxCdEmbestida;
    public States estado;
    int lastpos;
    public Transform[] puntosPatrulla;
    public Transform maxPuntoPatrullaIzq;
    public Transform maxPuntoPatrullaDer;
    public Vector3 posicionDestino;
    public GameObject groundCheck;
    public float groundcheckRadio = 0.3f;
    public LayerMask Suelo;
    public bool mirandoDerecha = true;
    public SpriteRenderer cuerpo;
    public float speed = 2;

    Animator anim;
    public float tiempoStun = 2f;
    public float auxTiempoStun;

    public GameObject escudo;

    public List<Light2D> luces;
    public override void ActivarPrimeraVez()
    {

        if (Vector2.Distance(this.transform.position, player.transform.position) < distanciaActivacion)
        {
            activado = true;
            estado = States.Andar;
            //reseter posicion
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        speedactualembestida = speedembestida;
        estado = States.Andar;
        SetRandomPoint();
        player = FindObjectOfType<ControllerPersonaje>().gameObject;
        anim = GetComponentInChildren<Animator>();
        Light2D[] hijos = this.GetComponentsInChildren<Light2D>();
        foreach (Light2D go in hijos)
        {
            if (luces != null) luces.Add(go);
        }
    }
    public override void Stun()
    {
        escudo.SetActive(false);
        stun = true;

        foreach (Light2D go in luces)
        {
            go.gameObject.SetActive(false);

        }
        auxTiempoStun = tiempoStun;

    }
    public override void Reactivar()
    {
        estado = States.Andar;
        stun = false;
        foreach (Light2D go in luces)
        {
            go.gameObject.SetActive(true);

        }
    }
    // Update is called once per frame
    protected override void Update()
    {

        base.Update();
        if (auxTiempoStun > 0)
        {
            auxTiempoStun -= Time.deltaTime;
            if (auxTiempoStun <= 0)
            {
                auxTiempoStun = 0;
                Reactivar();
                
            }
        }
        if (activado == true)
        {
            if (auxCdEmbestida > 0)
            {
                auxCdEmbestida -= Time.deltaTime;
                if (auxCdEmbestida <= 0)
                {
                    auxCdEmbestida = 0;
                }
            }


            if (stun == true)
            {

                estado = States.Stun;


            }
            else
            {
                escudo.SetActive(true);
            }
            if (reciengolpeado == false)
            {


                if (this.transform.position.x < maxPuntoPatrullaIzq.transform.position.x)
                {
                    SetRandomPoint();
                    speedactualembestida = speedembestida;
                    estado = States.Andar;
                    reciengolpeado = true; Invoke("ResetGolpeo", 5f);
                }
                else if (this.transform.position.x > maxPuntoPatrullaDer.transform.position.x)
                {
                    SetRandomPoint();
                    speedactualembestida = speedembestida;
                    estado = States.Andar;
                    reciengolpeado = true; Invoke("ResetGolpeo", 5f);
                }
            }
            //if ((this.transform.position.x < puntosPatrulla[0].transform.position.x) || (this.transform.position.x > puntosPatrulla[1].transform.position.x))
            //{
            //    SigPunto();
            //    speedactualembestida = speedembestida;
            //    estado = States.Andar;

            //}
            if (mirandoDerecha == true)
            {
                cuerpo.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                cuerpo.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (estado == States.Stun)
            {

                anim.SetBool("Idle", true);
                anim.SetBool("Andar", false);
                anim.SetBool("Atacar", false);
                anim.SetBool("Chase", false);
                
            }
            if (estado == States.Andar)
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Atacar", false);
                anim.SetBool("Chase", false);
                anim.SetBool("Andar", true);

                this.transform.Translate((posicionDestino - this.transform.position).normalized * Time.deltaTime * speed);
                if (posicionDestino.x < this.transform.position.x)
                {
                    mirandoDerecha = false;
                }
                else
                {
                    mirandoDerecha = true;
                }
                ComprobarDistPunto();
                ComprobarDistPlayer();
            }
            if (reciengolpeado == false)
            {
                if (estado == States.Perseguir)
                {
                    anim.SetBool("Idle", false);
                    anim.SetBool("Andar", false);
                    anim.SetBool("Atacar", false);
                    anim.SetBool("Chase", true);
                    this.transform.Translate((posicionDestino - this.transform.position).normalized * Time.deltaTime * speed * 1.1f);
                    if (posicionDestino.x < this.transform.position.x)
                    {
                        mirandoDerecha = false;
                    }
                    else
                    {
                        mirandoDerecha = true;
                    }
                    ComprobarDistPlayer();

                }
                //if (estado == States.Idle)
                //{
                //    anim.SetBool("Idle", true);
                //    anim.SetBool("Andar", false);
                //    anim.SetBool("Atacar", false);
                //    anim.SetBool("Chase", false);
                //    if (aux > 0)
                //    {
                //        aux -= Time.deltaTime;
                //    }
                //    else aux = 0;
                //    if (aux == 0)
                //    {
                //        estado = States.Andar;
                //    }
                //    ComprobarSiEmbestir();
                //}
                if (estado == States.Embestir)
                {
                    anim.SetBool("Atacar", true);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Andar", false);
                    anim.SetBool("Chase", false);
                    speedactualembestida -= Time.deltaTime;
                    if (speedactualembestida <= 0) speedactualembestida = 0;
                    this.transform.Translate((posicionDestino - this.transform.position).normalized * Time.deltaTime * speedactualembestida);
                    if (posicionDestino.x < this.transform.position.x)
                    {
                        mirandoDerecha = false;
                    }
                    else
                    {
                        mirandoDerecha = true;
                    }
                    ComprobarDistPunto();

                }

            }
            else if (estado != States.Andar)
            {
                anim.SetBool("Idle", true);
                anim.SetBool("Andar", false);
                anim.SetBool("Atacar", false);
                anim.SetBool("Chase", false);
            }
            else
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Andar", true);
                anim.SetBool("Atacar", false);
                anim.SetBool("Chase", false);

            }
        }
    }
    void ComprobarDistPlayer()
    {
        if (player != null)
        {


            if ((player.transform.position.y > this.transform.position.y - 3) && (player.transform.position.y < this.transform.position.y + 3))
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) < rangoAtaque)
                {
                    if (reciengolpeado == false)
                    {
                        if (Mathf.Sign((this.transform.position - player.transform.position).x) <= 0)
                        {
                            posicionDestino = new Vector3(player.transform.position.x + 8f, this.transform.position.y, this.transform.position.z);
                        }
                        else
                        {
                            posicionDestino = new Vector3(player.transform.position.x - 8f, this.transform.position.y, this.transform.position.z);
                        }
                        if ((player.transform.position.x < maxPuntoPatrullaIzq.transform.position.x) || (player.transform.position.x > maxPuntoPatrullaDer.transform.position.x))
                        {
                            //SetRandomPoint();
                            speedactualembestida = speedembestida;
                            estado = States.Andar;
                            reciengolpeado = true; Invoke("ResetGolpeo", 2f);
                        }
                        else
                        {
                            if (auxCdEmbestida <= 0)
                            {

                                auxCdEmbestida = cdEmbestida;
                                estado = States.Embestir;
                            }
                        }

                    }

                }
                else if (Vector3.Distance(player.transform.position, this.transform.position) < rangoDeteccion)
                {
                    if (reciengolpeado == false)
                    {


                        if (Mathf.Sign((this.transform.position - player.transform.position).x) <= 0)
                        {
                            posicionDestino = new Vector3(player.transform.position.x + 4f, this.transform.position.y, this.transform.position.z);
                        }
                        else
                        {
                            posicionDestino = new Vector3(player.transform.position.x - 4f, this.transform.position.y, this.transform.position.z);
                        }
                        if ((player.transform.position.x < maxPuntoPatrullaIzq.transform.position.x) || (player.transform.position.x > maxPuntoPatrullaDer.transform.position.x))
                        {
                            SetRandomPoint();
                            speedactualembestida = speedembestida;
                            estado = States.Andar;
                            reciengolpeado = true; Invoke("ResetGolpeo", 2f);
                        }
                        else
                        {

                            if (auxCdEmbestida <= 0)
                            {
                                estado = States.Perseguir;
                            }

                        }
                    }

                }
                else
                {


                    estado = States.Andar;

                }
            }
        }
    }
    //void ComprobarSiEmbestir()
    //{
    //    //if (estado != States.Idle)
    //    //{
    //    if (Vector3.Distance(player.transform.position, this.transform.position) < rangoAtaque)
    //    {
    //        if ((player.transform.position.y > this.transform.position.y - 1) && (player.transform.position.y < this.transform.position.y + 1))
    //        {
    //            if ((player.transform.position.x > puntosPatrulla[0].transform.position.x) && (player.transform.position.x < puntosPatrulla[1].transform.position.x))
    //            {
    //                if (reciengolpeado == false)
    //                {
    //                    Embestir();
    //                    estado = States.Embestir;
    //                }

    //            }
    //        }


    //    }
    //    else if (Vector3.Distance(player.transform.position, this.transform.position) < rangoDeteccion)
    //    {

    //        if ((player.transform.position.y > this.transform.position.y - 1) && (player.transform.position.y < this.transform.position.y + 1))
    //        {
    //            if ((player.transform.position.x > puntosPatrulla[0].transform.position.x) && (player.transform.position.x < puntosPatrulla[1].transform.position.x))
    //            {
    //                estado = States.Perseguir;
    //                posicionDestino = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
    //                this.transform.Translate((posicionDestino - this.transform.position).normalized * Time.deltaTime * speed);
    //                if (posicionDestino.x < this.transform.position.x)
    //                {
    //                    mirandoDerecha = false;
    //                }
    //                else
    //                {
    //                    mirandoDerecha = true;
    //                }
    //            }




    //        }
    //    }



    //}
    //public void Embestir()
    //{
    //    if (reciengolpeado == false)
    //    {


    //        if ((player.transform.position.x > puntosPatrulla[0].transform.position.x) && (player.transform.position.x < puntosPatrulla[1].transform.position.x))
    //        {
    //            if (mirandoDerecha == true)
    //            {
    //                posicionDestino = new Vector3(player.transform.position.x + 3.5f, this.transform.position.y, this.transform.position.z);
    //            }
    //            else
    //            {
    //                posicionDestino = new Vector3(player.transform.position.x - 3.5f, this.transform.position.y, this.transform.position.z);
    //            }

    //        }
    //        else
    //        {
    //            if ((player.transform.position.x > puntosPatrulla[0].transform.position.x) && (player.transform.position.x > puntosPatrulla[1].transform.position.x))
    //            {
    //                posicionDestino = new Vector3(puntosPatrulla[1].transform.position.x - 0.1f, this.transform.position.y, this.transform.position.z);
    //            }
    //            else
    //            {
    //                if ((player.transform.position.x < puntosPatrulla[0].transform.position.x) && (player.transform.position.x < puntosPatrulla[1].transform.position.x))
    //                {
    //                    posicionDestino = new Vector3(puntosPatrulla[0].transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);
    //                }
    //            }
    //        }
    //        ComprobarDistPunto();
    //    }
    //    else
    //    {

    //    }
    //}
    void ResetGolpeo()
    {
        reciengolpeado = false;
    }
    public void SetRandomPoint()
    {
        lastpos = Random.Range(0, puntosPatrulla.Length);
        posicionDestino = new Vector3(puntosPatrulla[lastpos].transform.position.x, this.transform.position.y, this.transform.position.z);
    }
    public void SigPunto()
    {
        //float dist = 0;
        //Vector2 dest;
        //dest = puntosPatrulla[1].position;
        //foreach (Transform go in puntosPatrulla)
        //{
        //    if (Vector2.Distance(this.transform.position, go.position) > dist)
        //    {
        //        dist = Vector2.Distance(this.transform.position, go.position);
        //        dest = go.position;
        //    }
        //    else
        //    {

        //    }
        //}
        //posicionDestino = dest;
        if ((lastpos + 1) >= puntosPatrulla.Length)
        {
            lastpos = -1;
        }
        posicionDestino = new Vector3(puntosPatrulla[lastpos + 1].transform.position.x, this.transform.position.y, this.transform.position.z);
        lastpos += 1;
        aux = Random.Range(mintiempoIdle, maxtiempoIdle);
        if (auxCdEmbestida > 0) estado = States.Andar;
        //estado = States.Idle;

    }
    public void ComprobarDistPunto()
    {
        if (estado == States.Andar /*|| (estado == States.Idle)|| (estado == States.Perseguir) */)
        {
            speedactualembestida = speedembestida;
            if (Vector3.Distance(posicionDestino, this.transform.position) <= 0.7f)
            {
                SigPunto();
            }
        }
        else
        {
            //if ((this.transform.position.x < puntosPatrulla[0].transform.position.x) || (this.transform.position.x > puntosPatrulla[1].transform.position.x))
            //{
            //    SigPunto();
            //    speedactualembestida = speedembestida;
            //    estado = States.Idle;
            //}
            //for (int i = 0; i < colliders.Length; i++)
            //{
            //    if (colliders[i].gameObject != gameObject)
            //    {
            //        grounded = true;

            //    }
            //   if(colliders)

            //}
            //if (grounded == false)
            //{
            //    SigPunto();
            //    speedactualembestida = speedembestida;
            //    estado = States.Idle;

            //}
            if (estado == States.Embestir)
            {
                if (Vector3.Distance(posicionDestino, this.transform.position) <= 1f)
                {
                    SigPunto();
                }

            }
            else if (Vector3.Distance(posicionDestino, this.transform.position) <= 0.3f)
            {
                SetRandomPoint();
                speedactualembestida = speedembestida;
                estado = States.Andar;

            }
            else if (Vector3.Distance(player.transform.position, this.transform.position) > rangoAtaque)
            {
                SetRandomPoint();
                speedactualembestida = speedembestida;
                estado = States.Andar;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            reciengolpeado = true; Invoke("ResetGolpeo", 2f);
            SetRandomPoint(); speedactualembestida = speedembestida;

        }
    }

}
