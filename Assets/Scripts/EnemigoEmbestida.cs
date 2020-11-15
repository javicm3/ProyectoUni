using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoEmbestida : MonoBehaviour
{
    public enum States
    {
        Andar,
        Perseguir,
        Embestir,
        Idle,
        Muerte
    }
    public bool reciengolpeado = false;
    bool grounded = true;
    public float maxtiempoIdle = 2.5f;
    public float mintiempoIdle = 0.7f;
    float aux;
    public float rangoDeteccion = 5;
    public float rangoAtaque = 3f;
    public float speedembestida = 6f;
    float speedactualembestida;
    public States estado;
    int lastpos;
    public Transform[] puntosPatrulla;
    Vector3 posicionDestino;
    public GameObject groundCheck;
    public float groundcheckRadio = 0.3f;
    public LayerMask Suelo;
    public bool mirandoDerecha = true;
    public SpriteRenderer cuerpo;
    public float speed = 2;
    GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        speedactualembestida = speedembestida;
        estado = States.Andar;
        SetRandomPoint();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position.x < puntosPatrulla[0].transform.position.x) || (this.transform.position.x > puntosPatrulla[1].transform.position.x))
        {
            SigPunto();
            speedactualembestida = speedembestida;
            estado = States.Andar;
            
        }
        if (mirandoDerecha == true)
        {
            cuerpo.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            cuerpo.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (estado == States.Andar)
        {
            anim.SetBool("Andar", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Atacar", false);
            anim.SetBool("Chase", false);
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
            ComprobarSiEmbestir();
        }
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
            ComprobarDistPunto();
            if ((player.transform.position.x > puntosPatrulla[0].transform.position.x) && (player.transform.position.x < puntosPatrulla[1].transform.position.x))
            {
                ComprobarSiEmbestir();
            }
        }
        if (estado == States.Idle)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Andar", false);
            anim.SetBool("Atacar", false);
            anim.SetBool("Chase", false);
            if (aux > 0)
            {
                aux -= Time.deltaTime;
            }
            else aux = 0;
            if (aux == 0)
            {
                estado = States.Andar;
            }
            ComprobarSiEmbestir();
        }
        if (estado == States.Embestir)
        {
            anim.SetBool("Atacar", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Andar", false);
            anim.SetBool("Chase", false);
            speedactualembestida -= Time.deltaTime;
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
    void ComprobarSiEmbestir()
    {
        //if (estado != States.Idle)
        //{
        if (Vector3.Distance(player.transform.position, this.transform.position) < rangoAtaque)
        {
            if ((player.transform.position.y > this.transform.position.y - 1) && (player.transform.position.y < this.transform.position.y + 1))
            {
                if ((player.transform.position.x > puntosPatrulla[0].transform.position.x) && (player.transform.position.x < puntosPatrulla[1].transform.position.x))
                {
                    if (reciengolpeado == false)
                    {
                        Embestir();
                        estado = States.Embestir;
                    }

                }
            }


        }
        else if (Vector3.Distance(player.transform.position, this.transform.position) < rangoDeteccion)
        {
          
            if ((player.transform.position.y > this.transform.position.y - 1) && (player.transform.position.y < this.transform.position.y + 1))
            {
                if ((player.transform.position.x > puntosPatrulla[0].transform.position.x) && (player.transform.position.x < puntosPatrulla[1].transform.position.x))
                {  estado = States.Perseguir;
                    posicionDestino = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
                    this.transform.Translate((posicionDestino - this.transform.position).normalized * Time.deltaTime * speed);
                    if (posicionDestino.x < this.transform.position.x)
                    {
                        mirandoDerecha = false;
                    }
                    else
                    {
                        mirandoDerecha = true;
                    }
                }




            }
        }



    }
    public void Embestir()
    {
        if (reciengolpeado == false)
        {


            if ((player.transform.position.x > puntosPatrulla[0].transform.position.x) && (player.transform.position.x < puntosPatrulla[1].transform.position.x))
            {
                if (mirandoDerecha == true)
                {
                    posicionDestino = new Vector3(player.transform.position.x + 3.5f, this.transform.position.y, this.transform.position.z);
                }
                else
                {
                    posicionDestino = new Vector3(player.transform.position.x - 3.5f, this.transform.position.y, this.transform.position.z);
                }

            }
            else
            {
                if ((player.transform.position.x > puntosPatrulla[0].transform.position.x) && (player.transform.position.x > puntosPatrulla[1].transform.position.x))
                {
                    posicionDestino = new Vector3(puntosPatrulla[1].transform.position.x - 0.1f, this.transform.position.y, this.transform.position.z);
                }
                else
                {
                    if ((player.transform.position.x < puntosPatrulla[0].transform.position.x) && (player.transform.position.x < puntosPatrulla[1].transform.position.x))
                    {
                        posicionDestino = new Vector3(puntosPatrulla[0].transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);
                    }
                }
            }
            ComprobarDistPunto();
        }
        else
        {
          
        }
    }
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

        if ((lastpos + 1) >= puntosPatrulla.Length)
        {
            lastpos = -1;
        }
        posicionDestino = new Vector3(puntosPatrulla[lastpos + 1].transform.position.x, this.transform.position.y, this.transform.position.z);
        lastpos += 1;
        aux = Random.Range(mintiempoIdle, maxtiempoIdle);
        estado = States.Idle;

    }
    public void ComprobarDistPunto()
    {
        if ((estado == States.Andar) || (estado == States.Idle) || (estado == States.Perseguir))
        {
            if (Vector3.Distance(posicionDestino, this.transform.position) <= 0.7f)
            {
                SigPunto();
            }
        }
        else
        {
            if ((this.transform.position.x < puntosPatrulla[0].transform.position.x) || (this.transform.position.x > puntosPatrulla[1].transform.position.x))
            {
                SigPunto();
                speedactualembestida = speedembestida;
                estado = States.Idle;
            }
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

            if (Vector3.Distance(posicionDestino, this.transform.position) <= 0.2f)
            {
                SigPunto();
                speedactualembestida = speedembestida;
                estado = States.Idle;

            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("colision"); reciengolpeado = true; Invoke("ResetGolpeo", 1f);
            SigPunto(); speedactualembestida = speedembestida;
            estado = States.Idle;
        }
    }

}
