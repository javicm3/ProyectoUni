using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaTemporizador : MonoBehaviour
{
    public Transform puntoArriba;
    public Vector3 originalPos;
    public bool subiendo;

    public float velocidadSubida = 10f;
    public float velocidadBajada;
    public bool abiertaPermanente = false;




    public float tiempoTrasPulsar;
    public float auxTiempoTrasPulsar;
    int puntoRandom;
    public bool pulsado = false;
    bool estoyArriba;

    bool unavez;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;




    }
    public void Activar()
    {
        pulsado = true;
        auxTiempoTrasPulsar = tiempoTrasPulsar;
        unavez = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (abiertaPermanente == true)
        {
            if (Vector2.Distance(this.transform.position, puntoArriba.transform.position) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, puntoArriba.transform.position, velocidadSubida * Time.deltaTime);
            }
        }
        else
        {


            if (pulsado == true)
            {
                if (!unavez)
                {
                    unavez = true;
                    subiendo = true;
                }
                auxTiempoTrasPulsar -= Time.deltaTime;

                if (auxTiempoTrasPulsar > 0 && subiendo)
                {
                    if (Vector2.Distance(this.transform.position, puntoArriba.transform.position) > 0.1f)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, puntoArriba.transform.position, velocidadSubida * Time.deltaTime);
                    }
                    else
                    {
                        subiendo = false;
                        velocidadBajada = Vector2.Distance(this.transform.position, originalPos) / auxTiempoTrasPulsar;
                    }

                }

            }
            if (subiendo == false && pulsado == true)
            {
                if (Vector2.Distance(this.transform.position, originalPos) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, originalPos, velocidadBajada * Time.deltaTime);
                }
                else
                {
                    pulsado = false;
                }
            }
        }
    }
}
