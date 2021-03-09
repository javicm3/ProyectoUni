using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTren : MonoBehaviour
{
    public GameObject tren;
    public float tmpMin;
    public float tmpMax;
    float tiempoSpawn = 0;
    // Start is called befor e the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tiempoSpawn -= Time.deltaTime;
        if(tiempoSpawn <= 0)
        {
            Instantiate(tren, this.transform);

            tiempoSpawn = Random.Range(tmpMin, tmpMax);

        }
    }
}
