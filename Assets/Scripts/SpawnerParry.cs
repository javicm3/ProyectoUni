using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerParry : MonoBehaviour
{
    public float cdDesactivacion = 4f;
    public GameObject bouncerPrefab;
    bool activado = false;
    float auxtimer;
    // Start is called before the first frame update
    void Start()
    {
        Desactivar();
        auxtimer = cdDesactivacion;
    }

    // Update is called once per frame
    void Update()
    {
        if (activado == true)
        {
            auxtimer -= Time.deltaTime;
            if (auxtimer <= 0)
            {
                
                activado = false;
                Desactivar();
            }
        }
    }
    void Desactivar()
    {
       bouncerPrefab.GetComponent<SpriteRenderer>().enabled = false;
        bouncerPrefab.GetComponent<CircleCollider2D>().enabled = false;
       
    }
    void Activar()
    {
        bouncerPrefab.GetComponent<SpriteRenderer>().enabled = true;
        bouncerPrefab.GetComponent<CircleCollider2D>().enabled = true;
        auxtimer = cdDesactivacion;
        activado = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.tag == "AreaRebote")
        {
            
           Activar();
        }
    }
}
