using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaTriggerLuz : MonoBehaviour
{
    Animator animCC;
    [SerializeField] bool inicio;

    private void Awake()
    {
        animCC = GetComponent<Animator>();
        animCC.SetBool("Player", false);

        if (!inicio)
        {
            animCC.SetBool("Intro", false);

        }
        else
        {
            animCC.SetBool("Intro", true);
        }
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
