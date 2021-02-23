﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquesBoss : MonoBehaviour
{
   
    public GameObject rayoVert;
    public GameObject rayoHoriz;
    public GameObject disparoLaser;
    public GameObject rayoDiagonal1;
    public GameObject rayoDiagonal2;

    public Transform posRayoVert1;
    public Transform posRayoVert2;
    public Transform[] posicionesHorizontales;
    public Transform puntoDisparo;

    public float speedRayosVert;
    public float tiempoSpawnRayoHorizontal;
    public float duracionRayoHorizontal;
    public float tiempoDisparos;
    public float desviacionDisparos;
    public float desviacionDiagonal = 30;
    public float velocidadDiagonal;
    public LayerMask layerM;
    GameObject vert1;
    GameObject vert2;
    GameObject dia1;
    GameObject horizontal1;

    public Material materialHorzOff;
    public Material materialHorzOn;
    GameObject player;
    public float numeroDisparos;
    float numDisparosAux = 0;
    Quaternion rotationLaser1;
    Quaternion rotationLaser2;
    float duracionDiagonales;
    bool diagonales;
    bool pillarDireccionDiagonal;
    Vector3 direccionLaser;
    Vector3 rot1;
    Vector3 rot2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        duracionDiagonales = 0;
        pillarDireccionDiagonal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            vert1 = Instantiate(rayoVert, posRayoVert1.position, Quaternion.identity);
            vert2 = Instantiate(rayoVert, posRayoVert2.position, Quaternion.identity);
            
        }
        if(vert1 != null || vert2 != null)
        {
            vert1.transform.position = Vector3.MoveTowards(vert1.transform.position, posRayoVert2.position, speedRayosVert);
            vert2.transform.position = Vector3.MoveTowards(vert2.transform.position, posRayoVert1.position, speedRayosVert);
            if (vert1.transform.position == posRayoVert2.position)
            {
                Destroy(vert1);
            }
            if (vert2.transform.position == posRayoVert1.position)
            {
                Destroy(vert2);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            StartCoroutine(RayoHorizontal());
           
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartCoroutine(DisparosBoss());
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StartCoroutine(RayoDiagonal());
        }


        UpdateDiagonalLaser();
        //if (diagonales)
        //{
        //    Vector3 direccionLaser = player.transform.position - puntoDisparo.position;
        //    float angle = Mathf.Atan2(direccionLaser.y, direccionLaser.x) * Mathf.Rad2Deg;
        //    rotationLaser1.eulerAngles = Vector3.Lerp(rotationLaser1.eulerAngles, new Vector3(0, 0, angle - desviacionDiagonal), Time.deltaTime * velocidadDiagonal);
        //    rotationLaser2.eulerAngles = Vector3.Lerp(rotationLaser2.eulerAngles, new Vector3(0, 0, angle + desviacionDiagonal), Time.deltaTime * velocidadDiagonal);
        //    float t = velocidadDiagonal * Time.deltaTime
        //}


    }
    IEnumerator RayoHorizontal()
    {
        horizontal1 = Instantiate(rayoHoriz, posicionesHorizontales[Random.Range(0, posicionesHorizontales.Length - 1)]);
        horizontal1.GetComponent<LineRenderer>().material = materialHorzOff;
        yield return new WaitForSeconds(tiempoSpawnRayoHorizontal);
        horizontal1.GetComponent<LineRenderer>().material = materialHorzOn;
        yield return new WaitForSeconds(duracionRayoHorizontal);
        Destroy(horizontal1);
       
    }
    IEnumerator DisparosBoss()
    {
        if (numDisparosAux < numeroDisparos)
        {
            GameObject disparo1;
            GameObject disparo2;

            //primer rayo dirigido
            disparo1 = Instantiate(disparoLaser, puntoDisparo.position, Quaternion.identity);
            disparo1.transform.parent = null;
            Vector3 direccionLaser = (player.transform.position - puntoDisparo.position).normalized;
            float angle = Mathf.Atan2(direccionLaser.y, direccionLaser.x) * Mathf.Rad2Deg;
            disparo1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return new WaitForSeconds(tiempoDisparos);

            //segundo rayo random
            disparo2 = Instantiate(disparoLaser, puntoDisparo);
            disparo2.transform.parent = null;
            direccionLaser = (player.transform.position - puntoDisparo.position).normalized;
            
            angle = Mathf.Atan2(direccionLaser.y, direccionLaser.x) * Mathf.Rad2Deg;
            angle = angle + Random.Range(-desviacionDisparos, desviacionDisparos);
            disparo2.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return new WaitForSeconds(tiempoDisparos);
            numDisparosAux++;
            StartCoroutine(DisparosBoss());
        }
        else
        {
            numDisparosAux = 0;
        }
        
    }
    IEnumerator RayoDiagonal()
    {
        rayoDiagonal1.SetActive(true);
        rayoDiagonal2.SetActive(true);
        diagonales = false;

        direccionLaser = player.transform.position - puntoDisparo.position;
        float angle = Mathf.Atan2(direccionLaser.y, direccionLaser.x) * Mathf.Rad2Deg;

        rotationLaser1.eulerAngles = new Vector3(0, 0, angle + desviacionDiagonal);
        rotationLaser2.eulerAngles = new Vector3(0, 0, angle - desviacionDiagonal);

        yield return new WaitForSeconds(1);
        duracionDiagonales = 0;
        diagonales = true;
        //rotationLaser1.eulerAngles = Vector3.Lerp(new Vector3(0, 0, angle + desviacionDiagonal), new Vector3(0, 0, angle - desviacionDiagonal), Time.deltaTime * velocidadDiagonal);
        //rotationLaser2.eulerAngles = Vector3.Lerp(new Vector3(0, 0, angle - desviacionDiagonal), new Vector3(0, 0, angle + desviacionDiagonal), Time.deltaTime * velocidadDiagonal);

        yield return new WaitForSeconds(3);
        diagonales = false;
        rayoDiagonal1.SetActive(false);
        rayoDiagonal2.SetActive(false);
        pillarDireccionDiagonal = true;
    }
    void UpdateDiagonalLaser()
    {
        if (rayoDiagonal1.gameObject.activeSelf)
        {
            RaycastHit2D hit = Physics2D.Raycast(puntoDisparo.position, rayoDiagonal1.transform.right, Mathf.Infinity, layerM);
            rayoDiagonal1.GetComponent<LineRenderer>().SetPosition(0, puntoDisparo.position);
            if (hit)
            {
                rayoDiagonal1.GetComponent<LineRenderer>().SetPosition(1, hit.point);
            }

            RaycastHit2D hit2 = Physics2D.Raycast(puntoDisparo.position, rayoDiagonal2.transform.right, Mathf.Infinity, layerM);
            rayoDiagonal2.GetComponent<LineRenderer>().SetPosition(0, puntoDisparo.position);
            if (hit2)
            {
                rayoDiagonal2.GetComponent<LineRenderer>().SetPosition(1, hit2.point);
            }

            rayoDiagonal1.transform.rotation = rotationLaser1;
            rayoDiagonal2.transform.rotation = rotationLaser2;
            if (diagonales)
            {
                MoverDiagonales();
            }
            
        }
    }
    void MoverDiagonales()
    {
        
        float angle = Mathf.Atan2(direccionLaser.y, direccionLaser.x) * Mathf.Rad2Deg;
        print(angle);
       
        if (pillarDireccionDiagonal)
        {
            rot1 = rotationLaser1.eulerAngles;
            rot2 = rotationLaser2.eulerAngles;
            pillarDireccionDiagonal = false;
        }
        print("1: " + rot1);
        print("2: " + rot2);
        
        
        if(angle >= -90 && angle <= 90)
        {
            
            rotationLaser1.eulerAngles = Vector3.Lerp(rotationLaser1.eulerAngles, rot2, Time.deltaTime * velocidadDiagonal);
            rotationLaser2.eulerAngles = Vector3.Lerp(rotationLaser2.eulerAngles, rot1, Time.deltaTime * velocidadDiagonal);
        }
        else
        {
            rotationLaser1.eulerAngles = Vector3.Lerp(rotationLaser1.eulerAngles, rot2, Time.deltaTime * velocidadDiagonal);
            rotationLaser2.eulerAngles = Vector3.Lerp(rotationLaser2.eulerAngles, rot1, Time.deltaTime * velocidadDiagonal);
        }
        

    }
}
