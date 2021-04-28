using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPuerta : MonoBehaviour
{
    Animator animCC;
    // Start is called before the first frame update
    void Start()
    {
        animCC = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animCC.SetBool("Abierto", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animCC.SetBool("Abierto", false);

        }
    }


}
