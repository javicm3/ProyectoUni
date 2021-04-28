using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporteZona : MonoBehaviour
{

    public GameObject Player;
    bool yendo = false;
    public float tiempoReutilizar = 1f;
    float auxtiempoRe;
    public GameObject Ir;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if ((tiempoReutilizar<=0)&&(Player.GetComponent<ControllerPersonaje>().movimientoBloqueado==false))
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick != null)
                {
                    if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action2.WasPressed)
                    {

                        FadeInOut fade = FindObjectOfType<FadeInOut>();
                        if (fade != null)
                        {
                            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
                            PlayerInput plInput = playerGO.GetComponentInChildren<PlayerInput>();
                            plInput.inputHorizBlock = true;
                            plInput.inputVerticBlock = true;

                            ControllerPersonaje per = playerGO.GetComponentInChildren<ControllerPersonaje>();
                            per.dashBloqueado = true;
                            per.saltoBloqueado = true;
                            per.dashCaidaBloqueado = true;
                            per.movimientoBloqueado = true;
                            per.rb.velocity = Vector2.zero;
                            StartCoroutine(fade.FadeOut());
                            Invoke("IrZona", 1.5f);

                        }
                      
                        auxtiempoRe = tiempoReutilizar;





                    }
                }
                else
                {
                    if (Input.GetButtonDown("Interact"))
                    {



                        FadeInOut fade = FindObjectOfType<FadeInOut>();
                        if (fade != null)
                        {
                            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
                            PlayerInput plInput = playerGO.GetComponentInChildren<PlayerInput>();
                            plInput.inputHorizBlock = true;
                            plInput.inputVerticBlock = true;

                            ControllerPersonaje per = playerGO.GetComponentInChildren<ControllerPersonaje>();
                            per.dashBloqueado = true;
                            per.saltoBloqueado = true;
                            per.dashCaidaBloqueado = true;
                            per.movimientoBloqueado = true;
                            per.rb.velocity = Vector2.zero;

                            StartCoroutine(fade.FadeOut());
                            Invoke("IrZona", 1.5f);

                        }
                  
                        auxtiempoRe = tiempoReutilizar;


                    }
                }
            }
        }
    }
    void IrZona()
    {
        Player.transform.position = Ir.transform.position;
        Invoke("QuitarFade", 1f);

    }
   
    void QuitarFade()
    {
        FadeInOut fade = FindObjectOfType<FadeInOut>();
        if (fade != null)
        {

            StartCoroutine(fade.FadeIn());


        }
    }
    private void Update()
    {
      

            if (tiempoReutilizar > 0)
            {
                tiempoReutilizar -= Time.deltaTime;
            }
         
    }
}
