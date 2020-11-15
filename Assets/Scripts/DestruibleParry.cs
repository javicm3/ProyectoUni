using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruibleParry : MonoBehaviour{
public float cdReactivacion=2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Desactivar()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        Invoke("Activar", cdReactivacion);
    }
    void Activar()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject+"AAAAAAAAAAA"+ collision.gameObject.tag);
        if (collision.gameObject.tag==  "AreaRebote")
        {
            print("AAAAAAAAAAA");
            Desactivar();
        }
    }
}
