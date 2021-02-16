using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pruebadestructibl : MonoBehaviour
{
    Explodable scriptDestruir;
    // Start is called before the first frame update
    void Start()
    {
        scriptDestruir = GetComponentInChildren<Explodable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f)
            {
                scriptDestruir.explode();
            }
        }
    }
}
