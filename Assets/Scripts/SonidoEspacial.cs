using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoEspacial : MonoBehaviour
{[Header("Si no es horizontal es vertical")]
    public bool esHorizontal = false;
    public GameObject limiteAbajooDerecha;
    public GameObject limiteArribaoIzquierda;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (esHorizontal == true)
        {
            if (player.transform.position.x < limiteAbajooDerecha.transform.position.x && player.transform.position.x > limiteArribaoIzquierda.transform.position.x)
            {
                this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y);
            }
            else
            {
                if (player.transform.position.x > limiteAbajooDerecha.transform.position.x)
                {
                    this.transform.position = new Vector3(limiteAbajooDerecha.transform.position.x, this.transform.position.y);
                }
                if (player.transform.position.x < limiteArribaoIzquierda.transform.position.x)
                {
                    this.transform.position = new Vector3(limiteArribaoIzquierda.transform.position.x, this.transform.position.y);
                }
            }
        }
        else
        {
            if (player.transform.position.y > limiteAbajooDerecha.transform.position.y && player.transform.position.y < limiteArribaoIzquierda.transform.position.y)
            {
                this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y);
            }
            else
            {
                if (player.transform.position.y < limiteAbajooDerecha.transform.position.y)
                {
                    this.transform.position = new Vector3(this.transform.position.x, limiteAbajooDerecha.transform.position.y);
                }
                if (player.transform.position.y > limiteArribaoIzquierda.transform.position.y)
                {
                    this.transform.position = new Vector3(this.transform.position.x, limiteArribaoIzquierda.transform.position.y);
                }
            }
        }
       
    }
}
