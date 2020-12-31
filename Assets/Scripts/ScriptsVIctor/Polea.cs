using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polea : MonoBehaviour
{

    ControllerPersonaje CP;

    public GameObject CoSube;
    public GameObject CoCae;

    float impulse = 1000f;

    void Update()
    {
        if(CoSube == null)
            return;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ControllerPersonaje>().estoyDasheando)
        {
            CoCae.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            CoSube.GetComponent<BoxCollider2D>().enabled = false;
            CoSube.GetComponent<Rigidbody2D>().AddForce(transform.up * impulse);
        }
    }
}
