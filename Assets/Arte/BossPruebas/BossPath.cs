﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPath : MonoBehaviour
{
    [SerializeField] private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector2 catPosition;

    //public float[] speedModifier;

    Ruta cmpRuta;

    bool tienePausa = false;

    private bool coroutineAllowed;
    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        
        //speedModifier = 0.5f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        cmpRuta = routes[routeToGo].gameObject.GetComponent<Ruta>();

        //if ()
            
        if(coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        if(cmpRuta.pausa && cmpRuta.pausaTerminada == false)
        {
            
            StartCoroutine(EsperarPausa());
        }
        else
        {
            coroutineAllowed = false;

            Vector2 p0 = routes[routeNumber].GetChild(0).position;
            Vector2 p1 = routes[routeNumber].GetChild(1).position;
            Vector2 p2 = routes[routeNumber].GetChild(2).position;
            Vector2 p3 = routes[routeNumber].GetChild(3).position;

            while (tParam < 1)
            {
                tParam += Time.deltaTime * cmpRuta.speedModifier;

                catPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

                transform.position = catPosition;
                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;

            routeToGo += 1;

            if (routeToGo > routes.Length - 1)
                routeToGo = 0; ;

            coroutineAllowed = true;
        }
        
    }
    private IEnumerator EsperarPausa()
    {
        //print("dejame trankilo");
        yield return new WaitForSeconds(cmpRuta.segundosPausa);
        //cmpRuta.pausa = false;
        cmpRuta.pausaTerminada = true;
        //coroutineAllowed = false;
    }
}
