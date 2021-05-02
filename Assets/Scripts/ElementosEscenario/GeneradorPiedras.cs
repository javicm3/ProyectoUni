using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorPiedras : MonoBehaviour
{
    public GameObject[] posicionesSpawn;
    public float[] tiemposPiedras;
    public int[] posicionesPiedras;
    public GameObject escombro;
    public float numPiedras;
   public  int actualNumber = 0;
   public  bool activado = false;
    GameObject player;
  public  float tiempoInicio;
    public float auxtime;
    // Start is called before the first frame update
    void Start()
    {
        actualNumber = 0;
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<VidaPlayer>().reiniciando)
        {
            actualNumber = 0;
            activado = false;
        }
        if (activado)
        {
            for (int i = tiemposPiedras.Length-1; i >=0; i--)
            {
                print(tiemposPiedras[i] + "e");
                if (Time.time >= tiempoInicio + tiemposPiedras[i])
                {
                    print(tiemposPiedras[i] + "tiempoPiedra");
                    if (actualNumber == i)
                    {
                        print(tiemposPiedras[i] + "act");
                        Instantiate(escombro, posicionesSpawn[posicionesPiedras[actualNumber]].transform.position, this.transform.rotation);
                        actualNumber++;
                    }
                }
            }



        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<ControllerPersonaje>() != null)
        {
            if (activado == false)
            {
                activado = true;
                tiempoInicio = Time.time;

            }

        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<ControllerPersonaje>() != null)
        {
            if (activado == false)
            {
                activado = true;
                tiempoInicio = Time.time;
            }

        }
    }


}
