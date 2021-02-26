using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDeEscombros1 : MonoBehaviour
{
    public GameObject escombroDestruible;
    float tmp;
    public float tiempoSpawn = 5;
    public float tiempoSpawnMin = 5;
    public float tiempoSpawnMax = 5;
    float posicion;
    public float rango;
    bool plataformaActiva = false;


    // Start is called befor e the first frame update
    void Start()
    {
        tmp = tiempoSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (plataformaActiva)
        {
            posicion = Random.Range(this.transform.position.x + rango, this.transform.position.x - rango);
            tiempoSpawn -= Time.deltaTime;
            if (tiempoSpawn <= 0)
            {
                Instantiate(escombroDestruible, new Vector3(posicion, transform.position.y), this.transform.rotation);

                tiempoSpawn = Random.Range(tiempoSpawnMin, tiempoSpawnMax);

            }
        }        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            plataformaActiva = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            plataformaActiva = false;
        }
    }
}
