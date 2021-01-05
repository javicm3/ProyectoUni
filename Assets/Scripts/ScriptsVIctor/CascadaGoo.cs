using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadaGoo : MonoBehaviour
{

   
    public float distancia = 20f;
    public float tiempoEntreComprobaciones = 2f;
    float auxTiempoEntreComprobaciones;
    GameObject player;
    [Range(0.3f, 1.0f)]
    public float tiempoTrasDash = 0.55f;
   
    bool activado = true;

    void Start()
    {
        auxTiempoEntreComprobaciones = tiempoEntreComprobaciones;
        player = FindObjectOfType<ControllerPersonaje>().gameObject;
    }

    void Update()
    {
        auxTiempoEntreComprobaciones -= Time.deltaTime;
        if (auxTiempoEntreComprobaciones < 0)
        {

       
        if (Vector2.Distance(player.transform.position, this.transform.position) < distancia)
            {
                activado = true;
            }
            else
            {
                activado = false;
            }
           

        }
        else
        {
            auxTiempoEntreComprobaciones = tiempoEntreComprobaciones;
        }
        if (activado == true)
        {
            if (player.GetComponent<ControllerPersonaje>().auxCdDash > (1-tiempoTrasDash))
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                this.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
