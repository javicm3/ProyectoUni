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
                   
                    FadeInOut fade = FindObjectOfType<FadeInOut>();
                    if (fade != null)
                    {
                       
                        StartCoroutine(fade.FadeOut());
                        Invoke("IrZona", 1.0f);

                    }

                }
            }
            else
            {
                if (Input.GetButtonDown("Interact"))
                {
                  
                    FadeInOut fade = FindObjectOfType<FadeInOut>();
                    if (fade != null)
                    {
                       
                        StartCoroutine(fade.FadeOut());
                        Invoke("IrZona", 1.0f);

                    }

                }
            }
        }
            
    }
    void IrZona()
    {
        Player.transform.position = Ir.transform.position;
        Invoke("QuitarFade", 0.6f);

    }
    void QuitarFade()
    {
        FadeInOut fade = FindObjectOfType<FadeInOut>();
        if (fade != null)
        {
          
            StartCoroutine(fade.FadeIn());


        }
    }
}
