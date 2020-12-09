using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { float numerocoll = GameObject.FindGameObjectsWithTag("Coleccionable").Length;
        float numeroest = GameObject.FindGameObjectsWithTag("Estrella").Length;
        //print(numerocoll + "colecc" + numeroest + "estrella");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
