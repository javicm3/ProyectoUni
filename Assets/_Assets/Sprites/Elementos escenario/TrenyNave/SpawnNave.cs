using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNave : MonoBehaviour
{
    public GameObject nave;
    public float tmpMin;
    public float tmpMax;
    float tiempoSpawn = 5;
    float posicion;
    public float rango;

    void Update()
    {
        posicion = Random.Range(this.transform.position.y + rango, this.transform.position.y - rango);

        tiempoSpawn -= Time.deltaTime;
        if(tiempoSpawn <= 0)
        {
            Instantiate(nave, new Vector3(transform.position.x, posicion), this.transform.rotation);

            tiempoSpawn = Random.Range(tmpMin, tmpMax);

        }
    }
}
