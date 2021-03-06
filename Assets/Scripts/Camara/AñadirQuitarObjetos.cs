﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AñadirQuitarObjetos : MonoBehaviour
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
    public bool bloqueaMovimiento = false;
    public bool limitaDistanciaMax = false;
    public float distanciaMaxLimite = 50f;

    public bool ignorarSiVieneXDerecha = false;
    public bool ignorarSiVieneXIzquierda = false;

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
    public float[] pesoObjAñadir;
    public float[] radioObjAñadir;
    public GameObject[] objQuitar;
    GameObject player;
    bool timerSoloInicio = false;
    bool timerVariosInicio = false;
    bool timerSecuencialInicio = false;
    public int coeficienteInicialSecuencia = 0;
    // Start is called before the first frame update
    void Start()
    {
        cinemakina = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>();
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
    }

    void AñadirObj()
    {
        if (FindObjectOfType<CameraZoom>() != null && limitaDistanciaMax)
        {
            FindObjectOfType<CameraZoom>().limitarDistancia = true;
            FindObjectOfType<CameraZoom>().tempMaxDistance = distanciaMaxLimite;
        }
        foreach (GameObject go in objAñadir)
        {
            int num = 0;
            for (int i = 0; i < targetGroup.m_Targets.Length; i++)
            {
                if (i<= 1) { if (targetGroup.m_Targets[i].target != null) num++; }
                else
                {
                    if (targetGroup.m_Targets[i].target != null)
                    {
                        num++;
                        if (targetGroup.m_Targets[i].target == go.transform)
                        {
                            
                            break;
                        }
                    }
                    else
                    {
                        targetGroup.m_Targets[i].target = go.transform;

                        if (pesoObjAñadir.Length > 0)
                        {
                            if (pesoObjAñadir[i - num] != 0)
                            {
                                targetGroup.m_Targets[i].weight = pesoObjAñadir[i - num];

                            }
                        }
                        if (radioObjAñadir.Length > 0) if (radioObjAñadir[i - num] != 0) targetGroup.m_Targets[i].radius = radioObjAñadir[i - num];
                        break;

                    }
                }
            }
        }
    }
    void QuitarObj()
    {
        if (FindObjectOfType<CameraZoom>() != null && limitaDistanciaMax)
        {
            FindObjectOfType<CameraZoom>().limitarDistancia = true;
            FindObjectOfType<CameraZoom>().tempMaxDistance = distanciaMaxLimite;
        }
        foreach (GameObject go in objQuitar)
        {
            for (int i = 0; i < targetGroup.m_Targets.Length; i++)
            {
                if (i == 0) { }
                else if (i == 1) { }
                else
                {
                    if (targetGroup.m_Targets[i].target != null)
                    {
                        if (targetGroup.m_Targets[i].target == go.transform)
                        {
                            targetGroup.m_Targets[i].target = null;
                            targetGroup.m_Targets[i].weight = 1;
                            targetGroup.m_Targets[i].radius = 0;

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

        player.GetComponent<ControllerPersonaje>().movimientoBloqueado = false;
        FindObjectOfType<CameraZoom>().soloplayer = true;
        FindObjectOfType<CameraZoom>().limitarDistancia = false;
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
        if (FindObjectOfType<CameraZoom>() != null && limitaDistanciaMax)
        {
            FindObjectOfType<CameraZoom>().limitarDistancia = true;
            FindObjectOfType<CameraZoom>().tempMaxDistance = distanciaMaxLimite;
        }
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
        if (FindObjectOfType<CameraZoom>() != null && limitaDistanciaMax)
        {
            FindObjectOfType<CameraZoom>().limitarDistancia = true;
            FindObjectOfType<CameraZoom>().tempMaxDistance = distanciaMaxLimite;
        }
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
        if (FindObjectOfType<CameraZoom>() != null && limitaDistanciaMax)
        {
            FindObjectOfType<CameraZoom>().limitarDistancia = true;
            FindObjectOfType<CameraZoom>().tempMaxDistance = distanciaMaxLimite;
        }
        targetGroup.m_Targets[0].target = objetosEnfocadosSecuencia[(int)coeficienteInicialSecuencia].transform;
        auxTiempoSecuenciaEnfoques = tiempoEntreEnfoquesSecuencia;
        coeficienteInicialSecuencia += 1;
        timerSecuencialInicio = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          
            Vector3 offset = this.GetComponent<Collider2D>().offset;
            if (collision.gameObject.transform.position.x > (this.transform.position + offset).x)
            {
                if (!ignorarSiVieneXDerecha)
                {
                    if (dejarSoloPlayer)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        DejarSoloPlayer();
                    }
                    if (quitarObjetos)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        QuitarObj();

                    }
                    if (añadirObjetos)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        AñadirObj();
                    }


                    if (enfocarObjetoSolo && timerSoloInicio == false)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        EnfocarObjSolo(); ;
                    }
                    if (enfocarVariosObjetos && timerVariosInicio == false)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        EnfocarVariosObj();
                    }
                    if (enfocarSecuenciaObjetos && timerSecuencialInicio == false)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        EnfocarSecuenciaObj();
                    }
                    if (bloqueaMovimiento)
                    {
                        player.GetComponent<ControllerPersonaje>().movimientoBloqueado = true;
                        player.GetComponent<ControllerPersonaje>().rb.velocity = Vector3.zero;
                    }
                    if (seDestruyeTrasTocarlo) Destroy(this.gameObject);
                }

            }
            else if (collision.gameObject.transform.position.x < (this.transform.position+ offset).x)
            {
                if (!ignorarSiVieneXIzquierda)
                {
                    if (dejarSoloPlayer)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        DejarSoloPlayer();
                    }
                    if (quitarObjetos)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        QuitarObj();

                    }
                    if (añadirObjetos)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        AñadirObj();
                    }


                    if (enfocarObjetoSolo && timerSoloInicio == false)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        EnfocarObjSolo(); ;
                    }
                    if (enfocarVariosObjetos && timerVariosInicio == false)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        EnfocarVariosObj();
                    }
                    if (enfocarSecuenciaObjetos && timerSecuencialInicio == false)
                    {
                        FindObjectOfType<CameraZoom>().InicioTimerTrans();
                        EnfocarSecuenciaObj();
                    }
                    if (bloqueaMovimiento)
                    {
                        player.GetComponent<ControllerPersonaje>().movimientoBloqueado = true;
                        player.GetComponent<ControllerPersonaje>().rb.velocity = Vector3.zero;
                    }
                    if (seDestruyeTrasTocarlo) Destroy(this.gameObject);
                }
            }


        }
    }
}
