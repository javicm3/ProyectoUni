using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoversePlat : MonoBehaviour
{
    CharacterController2D cac;
    // Start is called before the first frame update
    void Start()
    {
        cac = this.gameObject.GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (cac.m_Grounded)
        {
            if ((collision.gameObject.tag == "Plataforma")&&(collision.gameObject!=null))
            {
                print("plata");
                transform.parent = collision.transform;

            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        
            if (collision.gameObject.tag == "Plataforma")
            {
                transform.parent = null;

            }
        
    }
}
