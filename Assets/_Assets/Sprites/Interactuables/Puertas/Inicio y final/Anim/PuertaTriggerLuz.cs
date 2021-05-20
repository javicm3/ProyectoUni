using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaTriggerLuz : MonoBehaviour
{
    Animator animCC;


    // Start is called before the first frame update
    void Start()
    {
        animCC = GetComponent<Animator>();
        animCC.SetBool("Player", false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animCC.SetBool("Player", true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animCC.SetBool("Player", false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animCC.SetBool("Player", true);
        }
    }

}
