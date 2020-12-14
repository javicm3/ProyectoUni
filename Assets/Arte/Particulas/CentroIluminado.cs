using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentroIluminado : MonoBehaviour
{
    public GameObject prefabCentro;
    float tiempoSpawn = 0.7f;
    float tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = tiempoSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoSpawn -= Time.deltaTime;
        if(tiempoSpawn <= 0)
        {
            Instantiate(prefabCentro, transform);
            tiempoSpawn = tmp;
        }
        
    }
}
