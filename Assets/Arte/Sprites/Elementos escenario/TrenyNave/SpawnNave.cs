using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNave : MonoBehaviour
{
    public GameObject nave;
    float tmp;
    public float tiempoSpawn = 5;
    float posicion;
    public float rango;
    // Start is called befor e the first frame update
    void Start()
    {
        tmp = tiempoSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        posicion = Random.Range(this.transform.position.y + rango, this.transform.position.y - rango);
        print(posicion);
        tiempoSpawn -= Time.deltaTime;
        if(tiempoSpawn <= 0)
        {
            Instantiate(nave, new Vector3(transform.position.x, posicion), this.transform.rotation);

            tiempoSpawn = tmp;

        }
    }
}
