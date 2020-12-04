using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTren : MonoBehaviour
{
    public GameObject tren;
    float tmp;
    public float tiempoSpawn = 5;
    // Start is called befor e the first frame update
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
            Instantiate(tren, this.transform);

            tiempoSpawn = tmp;

        }
    }
}
