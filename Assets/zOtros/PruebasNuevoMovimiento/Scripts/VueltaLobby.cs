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
            }
            if (!GameManager.Instance.totalColeccionables.Contains(go))
            {
                GameManager.Instance.totalColeccionables.Add(go);
            }
        }

        GameManager.Instance.NivelActual.completado = true;


        GameManager.Instance.UltimoCheck = null;
    }

    bool done = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {                       
            cartel.enabled = true;

            if (!done && (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick!=null&& GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action2.WasPressed))
            {
                done = true;
                GuardarDatos();
                if (GetComponent<DesbloquearHabilidades>()!=null)
                { GetComponent<DesbloquearHabilidades>().DesbloquearHabilidad(); }

                SistemaGuardado.Guardar();
                FadeInOut fade = FindObjectOfType<FadeInOut>();
                if (fade != null)
                {
                    StartCoroutine(fade.FadeOut());
                    Invoke("IrLobby", 1.2f);

                }
                else { IrLobby();  }
            }
        }
    }

    void IrLobby()
    { SceneManager.LoadScene("NL-0", LoadSceneMode.Single); }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cartel.enabled = false;
        }
    }
}
