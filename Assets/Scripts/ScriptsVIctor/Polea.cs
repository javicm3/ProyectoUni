using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polea : MonoBehaviour
{

    ControllerPersonaje CP;

    public GameObject CoSube;
    public GameObject CoCae;
    public bool seRompe;
    public float massCae = 10f;
    public float grav = 10f;

    float impulse = 1000f;

    void Update()
    {
        if (CoSube == null)
            return;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {


            if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f)
            {
                if (CoCae != null)
                {
                    CoCae.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    CoCae.GetComponent<Rigidbody2D>().mass = massCae;
                    CoCae.GetComponent<Rigidbody2D>().gravityScale = grav;
                }

                if (CoSube != null)
                {
                    CoSube.GetComponent<BoxCollider2D>().enabled = false;
                    CoSube.GetComponent<Rigidbody2D>().AddForce(transform.up * impulse);
                }
                if (seRompe)
                {
                    Destroy(this.gameObject);
                }

            }
        }
    }
}
