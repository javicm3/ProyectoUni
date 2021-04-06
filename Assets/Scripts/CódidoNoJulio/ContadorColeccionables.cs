using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorColeccionables : MonoBehaviour
{
    public float nColeccionables;
    // Start is called before the first frame update
    void Start()
    {
        nColeccionables = GameObject.FindGameObjectsWithTag("Coleccionable").Length;
        print(nColeccionables);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
