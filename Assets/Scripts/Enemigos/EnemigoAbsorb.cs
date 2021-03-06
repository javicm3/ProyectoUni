﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemigoAbsorb : EnemigoPadre
{
    enum States
    {
        Desactivado,
        Idle,
        Absorber,
        Cooldown,
    }

    States estado;

    LineRenderer lineRend;
    CircleCollider2D coll;
    Animator cmpAnim;
    [SerializeField] Animator cmpAnimInt;

    ManagerEnergia playerEnergy;
    [SerializeField] float tiempoAbsorcion = 3;
    [SerializeField] float tiempoReactivar = 15;
    float tiempoRestante;

    float actTiempoAbsorcion;
    [SerializeField] float energiaRobar = 30;
    [SerializeField] float radioReactivarEnem = 15;
    [SerializeField] GameObject proyectil;
    [SerializeField] float velProyectil;
    [SerializeField] float cooldownAbsorber = 5;
    float cooldownRestante;

    public LayerMask layerMask;

    void Awake()
    {
        cmpAnim = GetComponentInChildren<Animator>();
        coll = GetComponentInChildren<CircleCollider2D>();
        lineRend = GetComponentInChildren<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.SetPosition(0, this.transform.position);
        lineRend.enabled = false;

        estado = States.Idle;
    }

    private void Start()
    {
        playerEnergy = FindObjectOfType<ManagerEnergia>();
    }

    protected override void Update()
    {
        switch (estado)
        {
            case States.Absorber:
                if (playerEnergy.actualEnergy > 0)
                {
                    //NewAudioManager.Instance.Play("EnemigoChupadita");
                    lineRend.enabled = true;
                    lineRend.SetPosition(1, playerEnergy.transform.position);
                    lineRend.SetPosition(0, this.transform.position);
                    AbsorberEnergia();
                }
                else
                {
                    //NewAudioManager.Instance.Stop("EnemigoChupadita");
                    lineRend.enabled = false;
                }

                break;

            case States.Desactivado:
                tiempoRestante -= Time.deltaTime;
                if (tiempoRestante <= 0)
                {
                    Reactivar();
                }
                break;

            case States.Cooldown:
                cooldownRestante -= Time.deltaTime;
                if (cooldownRestante <= 0)
                { Reactivar(); }
                break;

            default:
                break;

        }
    }



    void ActiveAnim(string boolName)
    {
        cmpAnim.SetBool("Cargando", false);
        cmpAnim.SetBool("Estuneado", false);
        cmpAnim.SetBool("Ataque", false);

        cmpAnim.SetBool(boolName, true);
        if (boolName == "Cargando")
        {
            cmpAnimInt.SetBool("True", true);
        }
        else if (boolName != "Ataque")
        {
            cmpAnimInt.SetBool("True", false);
        }

    }

    public override void Reactivar()
    {
        stun = false;

        if (Vector3.Distance(transform.position, playerEnergy.transform.position) < coll.radius && playerEnergy.actualEnergy > energiaRobar)
        {
            estado = States.Absorber; ActiveAnim("Cargando");
            lineRend.enabled = true;
            lineRend.SetPosition(1, playerEnergy.transform.position);
        }
        else { estado = States.Idle; ActiveAnim("None"); }
    }

    public override void Stun() //Hay que ver por donde se llama al metodo desactivar :3
    {
        if (stun == false) { stun = true; ManagerLogros.Instance.AddStat("EnemigosStun"); }
        estado = States.Desactivado; ActiveAnim("Estuneado");
        tiempoRestante = tiempoReactivar;
        lineRend.enabled = false;
    }


    void AbsorberEnergia()
    {
        //playerEnergy.RestarEnergia(energiaPorSegundo * Time.deltaTime);
        actTiempoAbsorcion += Time.deltaTime;

        if (actTiempoAbsorcion > tiempoAbsorcion)
        {
            actTiempoAbsorcion = 0;
            playerEnergy.RestarEnergia(energiaRobar);
            if (playerEnergy.actualEnergy < 2)
            {
                ManagerLogros.Instance.DesbloquearLogro(12);
            }
            lineRend.enabled = false;
            cooldownRestante = cooldownAbsorber;
            estado = States.Cooldown;
            ActiveAnim("Ataque");
        }
    }

    public void EjecutarAtaque()
    {
        cmpAnimInt.SetBool("True", false);
        // Buscar a los enemigos que hay en el area y reactivar el más cercano
        bool enemigoActivado = false;
        RaycastHit2D[] hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radioReactivarEnem, Vector2.left, 0.05f);
        foreach (RaycastHit2D item in hit)
        {
            if (item.transform.GetComponent<EnemigoPadre>() != null && !item.transform.GetComponent<EnemigoPadre>().seActivaPorCercania)
            {
                item.transform.GetComponent<EnemigoPadre>().seActivaPorCercania = true;
                print("enemigo encontrado");

                //D I S P A R A R   R A Y O   D E   E N E R G Í A   A L   E N E M I G O   E N   C U E S T I O N   (por hacer)
                enemigoActivado = true;
                break;
            }
        }


        if (!enemigoActivado)//Disparar al jugador
        {
            GameObject bala = Instantiate(proyectil, transform.position, transform.rotation);
            bala.GetComponent<Rigidbody2D>().AddForce((playerEnergy.transform.position - transform.position).normalized * velProyectil, ForceMode2D.Impulse);
        }
    }

    bool ComprobarParedes()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerEnergy.transform.position - transform.position, 1000, layerMask);
        if (hit && hit.transform.tag=="Player")
        {
            return true;
        }

        return false;
    }

    public void TriggerEnter2D(Collider2D collision)
    {
        if (estado == States.Idle && collision.tag == "Player" && ComprobarParedes())
        {
            if (playerEnergy == null)
            { playerEnergy = collision.transform.GetComponent<ManagerEnergia>(); }

            if (playerEnergy.actualEnergy > 0)
            {
                estado = States.Absorber; ActiveAnim("Cargando");
                lineRend.enabled = true;
                lineRend.SetPosition(1, playerEnergy.transform.position);
            }
            if (playerEnergy.actualEnergy < 0)
            {
                playerEnergy.actualEnergy = 0;
            }
        }
    }

    public void TriggerStay2D(Collider2D collision)
    {
        if (estado == States.Idle && collision.tag == "Player" && playerEnergy.actualEnergy > energiaRobar && ComprobarParedes())
        {
            estado = States.Absorber; ActiveAnim("Cargando");
            lineRend.enabled = true;
            lineRend.SetPosition(1, playerEnergy.transform.position);
        }
        else if(estado == States.Absorber && !ComprobarParedes())
        {
            estado = States.Idle; ActiveAnim("None");
            lineRend.enabled = false;
            actTiempoAbsorcion = 0;
        }
        
    }

    public void TriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (estado == States.Absorber)
            {
                estado = States.Idle; ActiveAnim("None");
                lineRend.enabled = false;
                actTiempoAbsorcion = 0;
            }
            /*else if(estado==States.Cooldown)
            { estado = States.Idle; ActiveAnim("None"); }*/
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioReactivarEnem);
        //UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, radioReactivarEnem );
    }
}
