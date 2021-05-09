using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VueltaLobby : MonoBehaviour
{
    public Canvas cartel;
   
    // Start is called before the first frame update
    void Start()
    {
        cartel.enabled = false;
    }


    public void GuardarDatos()
    {
        foreach (string go in GameManager.Instance.NivelActual.actualColeccionablesCogidos)
        {
            if (!GameManager.Instance.NivelActual.coleccionablesCogidos.Contains(go))
            {
                GameManager.Instance.NivelActual.coleccionablesCogidos.Add(go);
                GameManager.Instance.totalColeccionables.Add(go);
            }
        }

        GameManager.Instance.NivelActual.completado = true;


        GameManager.Instance.UltimoCheck = null;
    }

    bool done = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameObject.FindObjectOfType<ControllerPersonaje>().gameObject!=null)
        {                       
            cartel.enabled = true;

            if ((!done && (Input.GetButtonDown("Interact")) || (!done&& GameObject.FindObjectOfType<ControllerPersonaje>().gameObject!=null&& GameObject.FindObjectOfType<ControllerPersonaje>().joystick!=null&& GameObject.FindObjectOfType<ControllerPersonaje>().joystick.Action2.WasPressed)))
            {
                done = true;
                if (GhostData.Instance != null)
                {
                    GhostData.Instance.TerminarNivel(SceneManager.GetActiveScene().name);
                }
                GuardarDatos();
                if (GetComponent<DesbloquearHabilidades>()!=null)
                { GetComponent<DesbloquearHabilidades>().DesbloquearHabilidad(); }

                SistemaGuardado.Guardar();
                FadeInOut fade = FindObjectOfType<FadeInOut>();
                if (fade != null)
                {
                    StartCoroutine(fade.FadeOut());

                    GameObject playerGO = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
                        PlayerInput plInput = playerGO.GetComponent<PlayerInput>();
                        plInput.inputHorizBlock = true;
                        plInput.inputVerticBlock = true;

                        ControllerPersonaje per = playerGO.GetComponent<ControllerPersonaje>();
                        per.dashBloqueado = true;
                        per.saltoBloqueado = true;
                        per.dashCaidaBloqueado = true;
                        per.movimientoBloqueado = true;
                        per.rb.velocity = Vector2.zero;
                        Invoke("IrLobby", 1.2f);

                }
                else { IrLobby();  }
            }
        }
    }

    void IrLobby()
    {
      

        FindObjectOfType<PantallaFinal>().ActivarPantalla();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cartel.enabled = false;
        }
    }
}
