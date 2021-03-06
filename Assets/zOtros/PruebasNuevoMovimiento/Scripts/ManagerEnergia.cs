﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerEnergia : MonoBehaviour
{
    public float maxEnergy = 100;
    public float actualEnergy = 0;
    public float energiaDash = 50;
    public float energiaSumadaWallJump = 5;
    public float energiaSumadaBouncer = 10;
    public float energiaDashAbajo = 15f;
    public float energiaDisparo = 25f;
    public float energiaxEnemigoCombate = 10f;
    public float tiempoAux = -1;
    GameObject barraEnergia;
    ControllerPersonaje cc;
    public float indiceMultiplicador = 2;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("EnergyBar").gameObject != null) barraEnergia = GameObject.Find("EnergyBar").gameObject;
        actualEnergy = 0;
        cc = GetComponent<ControllerPersonaje>();
    }
    public void RestarEnergia(float valor)
    {
        if (tiempoAux <= 0)
        {
            actualEnergy -= valor;
            if (actualEnergy <= 0) { actualEnergy = 0; NewAudioManager.Instance.Play("PlayerNoEnergy"); }

        }



    }
    public void SumarEnergia(float valor)
    {
        actualEnergy += valor;
        if (actualEnergy > maxEnergy)
        {
            actualEnergy = maxEnergy;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (tiempoAux >= 0)
        {
            tiempoAux -= Time.deltaTime;
        }
        //if (cc.haciendoCombate == true)
        //{
        //    actualEnergy -= energiaxSegCombate*Time.deltaTime;
        //    if (actualEnergy <= 0) actualEnergy = 0;
        //}
        if (Input.GetKeyDown(KeyCode.K))
        {
            maxEnergy = 10000;
            actualEnergy = 10000;
        }
        if (barraEnergia != null) barraEnergia.GetComponent<Image>().fillAmount = actualEnergy / maxEnergy;
        if (cc.grounded)
        {
            if (actualEnergy < maxEnergy)
            {
                if (cc.rb.velocity.x != 0)
                {
                    if ((cc.pegadoPared == false))
                    {
                        if (cc.auxCdDashReal < 0.6 * cc.auxCdDashReal)
                        {
                            actualEnergy += Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * Time.deltaTime;
                        }
                    }


                }
            }
            else actualEnergy = maxEnergy;

        }

    }
}
