﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronBoss : MonoBehaviour
{
    public Transform cabeza;
    public float velocidadMov;
    public GameObject pareja;
    LineRenderer lr;
    public int rangoProbabilidadLaser;
    public float tiempoLaser = 1;
    public float tiempoAviso = 1;
    float tmp = 0;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(cabeza.position, transform.forward, velocidadMov * Time.deltaTime);
        tmp += Time.deltaTime;
        if(tmp >= 1)
        {
            int activar = Random.Range(0, rangoProbabilidadLaser);
            print(activar);
            if(activar == 0)
            {
                print("dale");
                StartCoroutine(LaserDron());
            }
            tmp = 0;
        }
        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, pareja.transform.position);

    }
    IEnumerator LaserDron()
    {
        lr.enabled = true;
        lr.material = GetComponentInParent<AtaquesBoss>().materialHorzOff;
        yield return new WaitForSeconds(tiempoAviso);
        lr.material = GetComponentInParent<AtaquesBoss>().materialHorzOn;
        yield return new WaitForSeconds(tiempoLaser);
        lr.enabled = false;
    }
}