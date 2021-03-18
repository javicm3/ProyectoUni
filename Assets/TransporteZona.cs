using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporteZona : MonoBehaviour
{

    public GameObject Player;

    public GameObject Ir;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick != null)
            {
                if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action2.WasPressed)
                {
                    Player.transform.position = Ir.transform.position;

                }
            }
            else
            {
                if (Input.GetButtonDown("Interact"))
                {
                    Player.transform.position = Ir.transform.position;

                }
            }
        }
            
    }
}
