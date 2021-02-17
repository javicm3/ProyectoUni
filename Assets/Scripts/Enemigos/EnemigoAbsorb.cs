using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAbsorb : EnemigoPadre
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

    ManagerEnergia playerEnergy;
    [SerializeField] float energiaPorSegundo = 10;
    [SerializeField] float tiempoReactivar = 15;
    float tiempoRestante;

    float energiaAbsorbida;
    [SerializeField] float energiaNecesaria = 30;
    [SerializeField] float radioReactivarEnem = 15;
    [SerializeField] GameObject proyectil;
    [SerializeField] float velProyectil;

    void Awake()
    {
        coll = GetComponent<CircleCollider2D>();        
        lineRend = GetComponent<LineRenderer>();
        lineRend.SetPosition(0, transform.position);
        lineRend.enabled = false;

        estado = States.Idle;
    }


    protected override void Update()
    {
        switch (estado)
        {
            case States.Absorber:
                if (playerEnergy.actualEnergy > 0)
                {
                    AbsorberEnergia();
                    lineRend.enabled = true;
                    lineRend.SetPosition(1, playerEnergy.transform.position);
                }
                else { lineRend.enabled = false; }
                 
                break;

            case States.Desactivado:
                tiempoRestante -= Time.deltaTime;
                if (tiempoRestante<=0)
                {
                    Reactivar();
                }
                break;

            default:
                break;

        }
    }


    public override void Reactivar()
    {
        stun = false;

        if (Vector3.Distance(transform.position, playerEnergy.transform.position) < coll.radius)
        {
            estado = States.Absorber;
            lineRend.enabled = true;
            lineRend.SetPosition(1, playerEnergy.transform.position);
        }
        else { estado = States.Idle; }
    }

    public override void Stun() //Hay que ver por donde se llama al metodo desactivar :3
    {
        stun = true;
        estado = States.Desactivado;
        tiempoRestante = tiempoReactivar;
        lineRend.enabled = false;
    }


    void AbsorberEnergia()
    {
        playerEnergy.RestarEnergia(energiaPorSegundo * Time.deltaTime);
        energiaAbsorbida += energiaPorSegundo * Time.deltaTime;
        
        if (energiaAbsorbida>=energiaNecesaria)
        {
            energiaAbsorbida -= energiaNecesaria;

            // Buscar a los enemigos que hay en el area y reactivar el más cercano

            bool enemigoActivado = false;
            RaycastHit2D[] hit = Physics2D.CircleCastAll(new Vector2(transform.position.x,transform.position.y), radioReactivarEnem, Vector2.left, 0.05f);
            foreach (RaycastHit2D item in hit)
            {
                print(item.transform.tag);
                if (item.transform.GetComponent<EnemigoPadre>()!=null && !item.transform.GetComponent<EnemigoPadre>().seActivaPorCercania)
                {
                    item.transform.GetComponent<EnemigoPadre>().seActivaPorCercania = true;
                    print("enemigo encontrado");

                    //D I S P A R A R   R A Y O   D E   E N E R G Í A   A L   E N E M I G O   E N   C U E S T I O N
                    enemigoActivado = true;
                    break;
                }
            }            
            

            if (!enemigoActivado)//Disparar al jugador
            {
                GameObject bala= Instantiate(proyectil, transform.position, transform.rotation);
                bala.GetComponent<Rigidbody2D>().AddForce(playerEnergy.transform.position-transform.position, ForceMode2D.Impulse); //Esto me hace cero de caso asique crearé una bala
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (estado==States.Idle && collision.tag=="Player")
        {
            if (playerEnergy==null)
            { playerEnergy = collision.transform.GetComponent<ManagerEnergia>(); }

            estado = States.Absorber;
            lineRend.enabled = true;
            lineRend.SetPosition(1, playerEnergy.transform.position);
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
