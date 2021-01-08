using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaSube : MonoBehaviour
{
    public float[] velocidades;
    public GameObject[] alturas;
    public bool iniciado = false;
    public Transform posicionInicial;
    public Transform posicionFuera;
    public int posicionActual = 0;
    GameObject player;

    public Transform VerdaderaPos;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ControllerPersonaje>().gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        print(velocidades[posicionActual] + "uwu" + posicionActual);
        if (player.GetComponent<VidaPlayer>().reiniciando)
        {
            if (this.transform.position != posicionFuera.transform.position)
            {
                Reiniciar();
            }

        }
        if (iniciado == true)
        {
            if (this.transform.position == posicionFuera.transform.position)
            {
                this.transform.position = posicionInicial.transform.position;
            }
            this.transform.position = new Vector2(player.transform.position.x, this.transform.position.y);
            if (posicionActual == 0)
            {
                if (VerdaderaPos.position.y < alturas[posicionActual].transform.position.y)
                {
                    this.transform.position = this.transform.position + velocidades[posicionActual] * Vector3.up* Time.deltaTime;
                }
                else
                {
                    if (posicionActual < alturas.Length) posicionActual++;

                }
            }
            else
            {
                if (VerdaderaPos.position.y < alturas[posicionActual].transform.position.y)
                {
                    this.transform.position = this.transform.position + velocidades[posicionActual] * Vector3.up*Time.deltaTime;
                }
                else
                {
                    if (posicionActual < alturas.Length) posicionActual++;

                }
            }




        }
        else
        {
            this.transform.position = posicionFuera.transform.position;
            posicionActual = 0;
        }
    }
    public void Reiniciar()
    {
        iniciado = false;
    }
}
