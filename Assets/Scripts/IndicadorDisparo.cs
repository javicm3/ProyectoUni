using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicadorDisparo : MonoBehaviour
{
    Vector3 camPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        camPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        camPos.z = -10;
        Vector3 dir = camPos - this.transform.position;
        Vector3 aux = (camPos - this.transform.position).normalized;
        aux.z = 0;
        this.transform.up = aux;
    }
}

