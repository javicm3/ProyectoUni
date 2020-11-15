using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
     Vector2 direccion;
    public float dañoparticula = 20f;

    Vector2 direccionMov;

    public float fuerzaInicial = 25f;

    // Use this for initialization
    private void Awake()
    {

    }
    void Start()
    {


        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        direccion = (pz - GameObject.FindGameObjectWithTag("Player").transform.position).normalized;
        this.transform.parent = null;
      
        this.transform.up = direccion;
        this.GetComponent<Rigidbody2D>().AddForce(direccion.normalized * fuerzaInicial, ForceMode2D.Impulse);
        print(direccion * fuerzaInicial);
        GetComponent<BoxCollider2D>().enabled = true;
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            Destroy(this.gameObject);
        
    }
}
