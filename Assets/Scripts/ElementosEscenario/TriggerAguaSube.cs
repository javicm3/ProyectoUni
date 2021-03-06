﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerAguaSube : MonoBehaviour
{
    CinemachineVirtualCamera cinemakina;
    CinemachineTargetGroup targetGroup;
    [Header("TIPOS")]
    public bool añadirObjetos = false;
    public bool quitarObjetos = false;
    public bool dejarSoloPlayer = false;
    public bool enfocarObjetoSolo = false;
    public bool enfocarVariosObjetos = false;
    public bool enfocarSecuenciaObjetos = false;
    public bool seDestruyeTrasTocarlo = false;
    public bool bloqueaMovimiento=true;
    [Header("Timers y Objetos")]
    [Header("Un objeto enfocado x tiempo")]
    public GameObject objetoEnfocadoSolo;
    public float tiempoVolverEnfoqueSolo = 5f;
    //public float radioEnfoqueObjSolo = 3f;
    public float auxTiempoEnfoqueSolo;
    [Header("Varios a la vez x tiempo")]
    public GameObject[] objetosEnfocados;
    public float tiempoVolverVariosEnfocados = 5f;
    float auxTiempoVariosEnfoques;
    [Header("Varios a la vez uno tras otro")]
    public GameObject[] objetosEnfocadosSecuencia;
    public float tiempoEntreEnfoquesSecuencia = 3f;
    float auxTiempoSecuenciaEnfoques;
    bool unavez = false;

    public GameObject[] objAñadir;
    public GameObject[] objQuitar;
    GameObject player;
    bool timerSoloInicio = false;
    bool timerVariosInicio = false;
    bool timerSecuencialInicio = false;
    public int coeficienteInicialSecuencia = 0;
    public GameObject aguaSube;
    // Start is called before the first frame update
    void Start()
    {
        cinemakina = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>();
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
    }

    void AñadirObj()
    {
        foreach (GameObject go in objAñadir)
        {
            for (int i = 0; i < targetGroup.m_Targets.Length; i++)
            {
                if (i == 0) { }
                else
                {
                    if (targetGroup.m_Targets[i].target != null)
                    {

                        if (targetGroup.m_Targets[i].target == go.transform)
                        {
                            break;
                        }
                    }
                    else
                    {
                        targetGroup.m_Targets[i].target = go.transform;
                        break;
                    }
                }
            }
        }
    }
    void QuitarObj()
    {
        foreach (GameObject go in objQuitar)
        {
            for (int i = 0; i < targetGroup.m_Targets.Length; i++)
            {
                if (i == 0) { }
                else
                {
                    if (targetGroup.m_Targets[i].target != null)
                    {
                        if (targetGroup.m_Targets[i].target == go.transform)
                        {
                            targetGroup.m_Targets[i].target = null;
                            break;
                        }

                    }
                    else
                    {

                    }
                }
            }
        }
    }
    void DejarSoloPlayer()
    {
        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            if (i == 0)
            {
                targetGroup.m_Targets[0].target = player.transform;

            }
            else if (i == 1)
            {
                targetGroup.m_Targets[1].target = player.GetComponent<CameraZoom>().ceboCamara.transform;
            }
            else
            {
                if (targetGroup.m_Targets[i].target != null)
                {
                    targetGroup.m_Targets[i].weight = 1;
                    targetGroup.m_Targets[i].radius = 0;
                    targetGroup.m_Targets[i].target = null;

                }
            }
        }
        print("soloplayea");
        player.GetComponent<ControllerPersonaje>().haciendoEnfoque = false;
        player.GetComponent<ControllerPersonaje>().movimientoBloqueado = false;
        player.GetComponent<ControllerPersonaje>().saltoBloqueado = false;
        player.GetComponent<ControllerPersonaje>().dashBloqueado = false;
        FindObjectOfType<CameraZoom>().soloplayer = true;
        player.GetComponentInChildren<ComportamientoHUD>().bloqueado = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) < 30f)
        {
            if (aguaSube.activeSelf == false) aguaSube.SetActive(true);
        }
        if (player.GetComponent<VidaPlayer>().reiniciando)
        {
            if (enfocarObjetoSolo == false) if (unavez == false)
                {
                    unavez = true;
                    aguaSube.GetComponent<AguaSube>().posicionInicial.transform.position = new Vector2(aguaSube.GetComponent<AguaSube>().posicionInicial.transform.position.x, aguaSube.GetComponent<AguaSube>().posicionInicial.transform.position.y + 3f);
                }
            if (seDestruyeTrasTocarlo==false) this.GetComponent<Collider2D>().enabled = true;
        }
        if (timerSoloInicio)
        {
            auxTiempoEnfoqueSolo -= Time.deltaTime;
            if (auxTiempoEnfoqueSolo <= 0)
            { DejarSoloPlayer();
                auxTiempoEnfoqueSolo = 0;
             
               
                timerSoloInicio = false;
            }
        }
        if (timerVariosInicio)
        {
            auxTiempoVariosEnfoques -= Time.deltaTime;
            if (auxTiempoVariosEnfoques <= 0)
            {
                auxTiempoVariosEnfoques = 0;
                DejarSoloPlayer();
                timerVariosInicio = false;
            }
        }
        if (timerSecuencialInicio)
        {
            auxTiempoSecuenciaEnfoques -= Time.deltaTime;
            if (auxTiempoSecuenciaEnfoques <= 0)
            {
                if (objetosEnfocadosSecuencia.Length > coeficienteInicialSecuencia)
                {
                    targetGroup.m_Targets[0].target = objetosEnfocadosSecuencia[(int)coeficienteInicialSecuencia].transform;
                    coeficienteInicialSecuencia += 1;
                    auxTiempoSecuenciaEnfoques += tiempoEntreEnfoquesSecuencia;
                }
                else
                {
                    DejarSoloPlayer();
                    coeficienteInicialSecuencia = 0;
                    auxTiempoSecuenciaEnfoques = 0;
                    timerSecuencialInicio = false;
                }
            }

        }
    }
    void EnfocarObjSolo()
    {
        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            if (i == 0)
            {
                targetGroup.m_Targets[0].target = objetoEnfocadoSolo.transform;
             
                //targetGroup.m_Targets[0].radius=radioEnfoqueObjSolo;
            }
            else
            {
                if (targetGroup.m_Targets[i].target != null)
                {
                    targetGroup.m_Targets[i].target = null;

                }
            }
        }
        auxTiempoEnfoqueSolo += tiempoVolverEnfoqueSolo;
        timerSoloInicio = true;
    }
    void EnfocarVariosObj()
    {
        targetGroup.m_Targets[0].target = null;
        foreach (GameObject go in objetosEnfocados)
        {
            for (int i = 0; i < targetGroup.m_Targets.Length; i++)
            {

                if (targetGroup.m_Targets[i].target != null)
                {

                    if (targetGroup.m_Targets[i].target == go.transform)
                    {
                        break;
                    }
                }
                else
                {
                    targetGroup.m_Targets[i].target = go.transform;
                    break;
                }

            }
        }

        auxTiempoVariosEnfoques += tiempoVolverVariosEnfocados;
        timerVariosInicio = true;
    }
    void EnfocarSecuenciaObj()
    {

        targetGroup.m_Targets[0].target = objetosEnfocadosSecuencia[(int)coeficienteInicialSecuencia].transform;
        auxTiempoSecuenciaEnfoques = tiempoEntreEnfoquesSecuencia;
        coeficienteInicialSecuencia += 1;
        timerSecuencialInicio = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (bloqueaMovimiento)
            {
   player.GetComponent<ControllerPersonaje>().movimientoBloqueado = true;
           
                player.GetComponent<ControllerPersonaje>().saltoBloqueado = true;
                player.GetComponent<ControllerPersonaje>().dashBloqueado = true;
                player.GetComponentInChildren<ComportamientoHUD>().bloqueado = true;
                player.GetComponent<ControllerPersonaje>().haciendoEnfoque = true;
                player.GetComponent<ControllerPersonaje>().rb.velocity = Vector3.zero;
            }
         
        
            if (aguaSube != null)
            { 
                aguaSube.GetComponent<AguaSube>().iniciado = true;
               
            }
            if (dejarSoloPlayer)
            {
                DejarSoloPlayer();
            }

            if (añadirObjetos)
            {
                AñadirObj();
            }
            if (quitarObjetos)
            {
                QuitarObj();

            }

            if (enfocarObjetoSolo && timerSoloInicio == false)
            {
                EnfocarObjSolo(); ;
            }
            if (enfocarVariosObjetos && timerVariosInicio == false)
            {
                EnfocarVariosObj();
            }
            if (enfocarSecuenciaObjetos && timerSecuencialInicio == false)
            {
                EnfocarSecuenciaObj();
            }

            if (seDestruyeTrasTocarlo) {
               
                /*this.GetComponent<Collider2D>().enabled = false*/
                enfocarObjetoSolo = false;
                bloqueaMovimiento = false;

            }
        }
    }
}


