using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorEnemigos : MonoBehaviour
{
    private ControllerPersonaje padre;

    void Start()
    {
        padre = transform.parent.GetComponent<ControllerPersonaje>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "EnemigoDetectar")
        {

            padre.enemigoCerca = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "EnemigoDetectar")
        {

            padre.enemigoCerca = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "EnemigoDetectar")
        {
            padre.enemigoCerca = false;
        }
    }
}
