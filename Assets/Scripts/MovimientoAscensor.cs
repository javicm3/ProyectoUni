﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAscensor : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] Transform[] puntosPersonaje;
    int indexArray;

    public float tEspera;
    bool esperado;
    // Start is called before the first frame update
    void Start()
    {
        indexArray = 0;
        esperado = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, puntosPersonaje[indexArray].position) > 1)
        {
            this.transform.Translate((puntosPersonaje[indexArray].position - this.transform.position).normalized * Time.deltaTime * speed);
            esperado = false;
        }
        else if(Vector3.Distance(this.transform.position, puntosPersonaje[indexArray].position) <= 1 && esperado == false)
        {
                StartCoroutine(EsperarSegundos());
        }
        else
        {
            if (indexArray < puntosPersonaje.Length - 1)
            {
                indexArray++;
            }
            else
            {
                indexArray = 0;
            }
        }
    }
    IEnumerator EsperarSegundos()
    {
        yield return new WaitForSeconds(tEspera);
        esperado = true;
    }

}
