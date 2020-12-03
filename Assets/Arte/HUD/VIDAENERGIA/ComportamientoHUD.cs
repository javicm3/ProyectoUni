﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoHUD : MonoBehaviour
{
    LineRenderer line;
    public GameObject player;
    public Transform punto1;
    Vector2 posicion2;
    Vector2 posicion1;
    bool movido;
    bool calcularPosicion;
    public float movimiento = 100;
    public float altura = 4;
    float alturaPlayer;
    public float responsividad = 2;
    public float velocidad = 1;
    float vAux;
    public float velocidadMax = 50;
    bool vCambioSentido;
    
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
        //player = GameObject.FindGameObjectWithTag("Player");
        calcularPosicion = false;
        movido = false;
        posicion1 = new Vector2(player.transform.position.x, 5 + player.transform.position.y);
        posicion2 = new Vector2(player.transform.position.x, 5 + player.transform.position.y);
        vAux = velocidad;
    }

    // Update is called once per frame
    void Update()
    {
        //print(points);
        //line.SetPosition(1, new Vector3(line.gameObject.transform.position.x, line.gameObject.transform.position.y, player.transform.position.z));
        line.SetPosition(0, punto1.position);
        line.SetPosition(1, player.transform.position);


        //posicion2 = new Vector2(player.transform.position.x + movimiento + 10, altura + player.transform.position.y);

        //posicion1 = new Vector2(player.transform.position.x - movimiento, altura + player.transform.position.y);
        if (calcularPosicion == false)
        {
            alturaPlayer = player.transform.position.y;
            calcularPosicion = true;
        }

        if (player.transform.position.y <= alturaPlayer - responsividad )
        {
            posicion2 = new Vector2(player.transform.position.x + movimiento + 10, altura + player.transform.position.y);
            posicion1 = new Vector2(player.transform.position.x - movimiento, altura + player.transform.position.y);
            posicion1 += new Vector2(0, 2);
            posicion2 += new Vector2(0, 2);
            calcularPosicion = false;
        }
        else if (player.transform.position.y >= alturaPlayer + responsividad)
        {
            posicion2 = new Vector2(player.transform.position.x + movimiento + 10, altura + player.transform.position.y);
            posicion1 = new Vector2(player.transform.position.x - movimiento, altura + player.transform.position.y);
            posicion1 -= new Vector2(0, 4);
            posicion2 -= new Vector2(0, 4);
            calcularPosicion = false;
        }
        else 
        {
            //posicion2.x = player.transform.position.x + movimiento + 10;
            posicion1 = new Vector2(player.transform.position.x - movimiento, altura + player.transform.position.y);

            posicion2 = new Vector2(player.transform.position.x + movimiento , altura + player.transform.position.y);
            //posicion1.x = player.transform.position.x - movimiento;

        }

        if (GetComponentInParent<PlayerInput>().personajeInvertido)
        {
            
            if(vCambioSentido == true)
            {
                velocidad = velocidadMax;
                vCambioSentido = false;
            }
            else if(transform.position.x >= posicion2.x)
            {
                velocidad = vAux;
            }
            transform.position = Vector2.MoveTowards(transform.position, posicion2, velocidad * Time.deltaTime);
            //if(transform.position.x >= posicion2.x)
            //{
            //    calcularPosicion = true;
            //    movido = true;
            //}
            vCambioSentido = false;
        }
        else if(GetComponentInParent<PlayerInput>().personajeInvertido == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, posicion1, velocidad * Time.deltaTime);
            if (vCambioSentido == false)
            {
                velocidad = velocidadMax;
                vCambioSentido = true;
            }
            else if (transform.position.x <= posicion1.x)
            {
                velocidad = vAux; 
            }
            //if (transform.position.x <= posicion1.x)
            //{
            //    calcularPosicion = false;
            //    movido = false;
            //}
            //movido = false;
            vCambioSentido = true;
        }
     
    }
    
}
