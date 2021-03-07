using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaAbsorber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("-.-");
        if (collision.gameObject.tag != "Enemigo" && collision.gameObject.tag !="EnemigoDetectar")
        {
            print(collision.gameObject);
            Destroy(this.gameObject,0.05f);
        }     

    }
}
