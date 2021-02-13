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

    ManagerEnergia player;
    [SerializeField] float energiaPorSegundo = 10;
    [SerializeField] float tiempoReactivar = 15;
    float tiempoRestante;

    void Start()
    {
        
    }


    void Update()
    {
        switch (estado)
        {
            case States.Absorber:
                player.RestarEnergia(energiaPorSegundo * Time.deltaTime); //Instanciar un rayo hasta el personaje para dar feedback de que se le está absorbiendo
                print("absorber"); 
                break;

            case States.Desactivado:
                tiempoRestante -= Time.deltaTime;
                if (tiempoRestante<=0)
                {
                    estado = States.Idle; //tendría que comprobar si el jugador está en el trigger
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estado==States.Idle && collision.tag=="Player")
        {
            if (player==null)
            { player = collision.transform.GetComponent<ManagerEnergia>(); }

            estado = States.Absorber;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (estado == States.Absorber && collision.tag == "Player")
        {
            estado = States.Idle;
        }
    }
}
