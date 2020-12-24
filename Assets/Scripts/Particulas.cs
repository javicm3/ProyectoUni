﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particulas : MonoBehaviour
{
    public GameObject particulasBounce;
    public GameObject particulasSalto;
    public GameObject particulasWallJump;
    public GameObject particulasDash;
    public GameObject particulasvelMax;
    public GameObject particulasvelMax2;
    public GameObject particulasExplosion;
    public GameObject particulasViajeCables;
    public GameObject particulasCaida;
    public GameObject particulasDaño;
    public GameObject particulasDobleSalto;
    public GameObject particulasDobleSaltoInvertido;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SpawnParticulas(GameObject particulas,Vector3 posicion, Transform parent)
    {
        if (particulas == particulasDash)
        {
            if (this.GetComponent<PlayerInput>().personajeInvertido == false)
            {
                GameObject part = Instantiate(particulas, posicion, Quaternion.Euler(0, 0, 90), this.GetComponentInChildren<AnimAux>().gameObject.transform);
            }
            else
            {
                GameObject part = Instantiate(particulas, posicion, Quaternion.Euler(0, 0, -90), this.GetComponentInChildren<AnimAux>().gameObject.transform);
            }

        }
        else if (particulas == particulasDobleSalto)
        {
            if (this.GetComponent<PlayerInput>().personajeInvertido == false)
            {
                GameObject part = Instantiate(particulasDobleSalto, posicion, Quaternion.identity, parent);
            }
            else
            {
                GameObject part = Instantiate(particulasDobleSaltoInvertido, posicion, Quaternion.identity, parent);
            }

        }
        else if(parent == null)
        {
            Instantiate(particulas, posicion, Quaternion.identity);
        }
        else
        {
            Instantiate(particulas, posicion, Quaternion.identity, parent);
        }

    }
    public void SpawnParticulasSinTransform(GameObject particulas, Vector3 posicion)
    {
       
            Instantiate(particulas, posicion, Quaternion.identity);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
