using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLoop : MonoBehaviour
{ public float fuerzaImpulso = 100;
    GameObject direccionHijo;

    // Start is called before the first frame update
    void Start()
    {
        direccionHijo = transform.GetChild(0).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("collidede");
            other.GetComponent<PlayerInput>().ultimoInputHorizontal = other.GetComponent<PlayerInput>().ultimoInputHorizontal *- 1;
            other.GetComponent<Rigidbody2D>().AddForce(fuerzaImpulso * (direccionHijo.transform.position - this.transform.position).normalized, ForceMode2D.Impulse);
           
        }
    }
}
