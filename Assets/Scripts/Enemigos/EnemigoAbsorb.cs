using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAbsorb : MonoBehaviour
{
    enum States
    {
        Desactivado,
        Idle,
        Absorber,
    }
    
    States estado;

    LineRenderer lineRend;
    CircleCollider2D coll;

    ManagerEnergia player;
    [SerializeField] float energiaPorSegundo = 10;
    [SerializeField] float tiempoReactivar = 15;
    float tiempoRestante;

    void Awake()
    {
        coll = GetComponent<CircleCollider2D>();        
        lineRend = GetComponent<LineRenderer>();
        lineRend.SetPosition(0, transform.position);
        lineRend.enabled = false;

        estado = States.Idle;
    }


    void Update()
    {
        switch (estado)
        {
            case States.Absorber:
                player.RestarEnergia(energiaPorSegundo * Time.deltaTime);
                lineRend.SetPosition(1, player.transform.position); 
                break;

            case States.Desactivado:
                tiempoRestante -= Time.deltaTime;
                if (tiempoRestante<=0)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < coll.radius)
                    {
                        estado = States.Absorber;
                        lineRend.enabled = true;
                        lineRend.SetPosition(1, player.transform.position);
                    }
                    else { estado = States.Idle; }                    
                }
                break;

            default:
                break;

        }
    }

    public void Desactivar() //Hay que ver por donde se llama al metodo desactivar :3
    {
        estado = States.Desactivado;
        tiempoRestante = tiempoReactivar;
        lineRend.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estado==States.Idle && collision.tag=="Player")
        {
            if (player==null)
            { player = collision.transform.GetComponent<ManagerEnergia>(); }

            estado = States.Absorber;
            lineRend.enabled = true;
            lineRend.SetPosition(1, player.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (estado == States.Absorber && collision.tag == "Player")
        {
            estado = States.Idle;
            lineRend.enabled = false;
        }
    }
}
