using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreadorPuntos : MonoBehaviour
{
    public GameObject punto;
    public float cantidad;
    
    // Start is called before the first frame update
    void Start()
    {
        Collider2D c = GetComponent<Collider2D>();
        for (int i = 0; i < cantidad; i++)
        {
            float posicionX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            float posicionY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            Vector2 pos = new Vector2(posicionX, posicionY);
            Instantiate(punto, pos, punto.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
