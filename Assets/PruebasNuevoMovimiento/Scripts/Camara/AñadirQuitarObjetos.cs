﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AñadirQuitarObjetos : MonoBehaviour
{
    CinemachineVirtualCamera cinemakina;
    CinemachineTargetGroup targetGroup;
    [Header("TIPOS")]
    public bool añadirObjetos;
    public bool quitarObjetos;
    public bool dejarSoloPlayer;
    public bool enfocarObjetoSolo;
    public bool enfocarVariosObjetos;
    public bool enfocarSecuenciaObjetos;

    [Header("Timers y Objetos")]
    [Header("Un objeto enfocado x tiempo")]
    public GameObject objetoEnfocadoSolo;
    public float tiempoVolverEnfoqueSolo = 5f;
    //public float radioEnfoqueObjSolo = 3f;
    float auxTiempoEnfoqueSolo;
    [Header("Varios a la vez x tiempo")]
    public GameObject[] objetosEnfocados;
    public float tiempoVolverVariosEnfocados = 5f;
    float auxTiempoVariosEnfoques;
    [Header("Varios a la vez uno tras otro")]
    public GameObject[] objetosEnfocadosSecuencia;
    public float tiempoEntreEnfoquesSecuencia = 3f;
    float auxTiempoSecuenciaEnfoques;


    public GameObject[] objAñadir;
    public GameObject[] objQuitar;
    GameObject player;
    bool timerSoloInicio = false;
    bool timerVariosInicio = false;
    bool timerSecuencialInicio = false;
   public float coeficienteInicialSecuencia = 0;
    // Start is called before the first frame update
    void Start()
    {
        cinemakina = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>();
        player = GameObject.FindGameObjectWithTag("Player");
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
            if (i == 0) { targetGroup.m_Targets[0].target = player.transform; }
            else
            {
                if (targetGroup.m_Targets[i].target != null)
                {
                    targetGroup.m_Targets[i].target = null;

                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (timerSoloInicio)
        {
            auxTiempoEnfoqueSolo -= Time.deltaTime;
            if (auxTiempoEnfoqueSolo <= 0)
            {
                auxTiempoEnfoqueSolo = 0;
                DejarSoloPlayer();
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
            if (i == 0) { targetGroup.m_Targets[0].target = objetoEnfocadoSolo.transform;

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
        print("PUTAMADRE");
        targetGroup.m_Targets[0].target = objetosEnfocadosSecuencia[0].transform;
        auxTiempoSecuenciaEnfoques = tiempoEntreEnfoquesSecuencia;
        coeficienteInicialSecuencia += 1;
        timerSecuencialInicio = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { if (dejarSoloPlayer) DejarSoloPlayer();

            if (añadirObjetos)
            {
                AñadirObj();
            }
            if (quitarObjetos)
            {
                QuitarObj();

            }
           
            if (enfocarObjetoSolo&&timerSoloInicio==false)
            {
                EnfocarObjSolo(); ;
            }
            if (enfocarVariosObjetos&&timerVariosInicio==false)
            {
              EnfocarVariosObj() ;
            }
            if (enfocarSecuenciaObjetos&&timerSecuencialInicio==false)
            {
                EnfocarSecuenciaObj();
            }
        }
    }
}
