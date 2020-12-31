using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadaGoo : MonoBehaviour
{

    public ControllerPersonaje CP;

    void Start()
    {
        
    }

    void Update()
    {
        if (CP.estoyDasheando)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
