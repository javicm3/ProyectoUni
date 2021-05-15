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
        if (GameManager.Instance.NivelActual.actualColeccionablesCogidos.Count == 0)
        {
            if (SceneManager.GetActiveScene().name != "Nivel_4_Boss1" && SceneManager.GetActiveScene().name != "Nivel_12_BossFinal")
            {
                ManagerLogros.Instance.DesbloquearLogro(25);
            }

        }
        foreach (string go in GameManager.Instance.NivelActual.actualColeccionablesCogidos)
        {
            if (!GameManager.Instance.NivelActual.coleccionablesCogidos.Contains(go))
            {
                GameManager.Instance.NivelActual.coleccionablesCogidos.Add(go);


                GameManager.Instance.totalColeccionables.Add(go);
            }
        }
        if (GameManager.Instance.NivelActual.coleccionablesCogidos.Count == GameManager.Instance.NivelActual.maxColeccionables)
        {
            ManagerLogros.Instance.DesbloquearLogro(13);
            if (!GameManager.Instance.NivelActual.todosColeccionablesCogidos)
            {
                GameManager.Instance.NivelActual.todosColeccionablesCogidos = true;
                ManagerLogros.Instance.AddStat("NivelesTodosColeccionables");
            }

        }
        GameManager.Instance.NivelActual.completado = true;


        GameManager.Instance.UltimoCheck = null;
    }

    bool done = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameObject.FindObjectOfType<ControllerPersonaje>().gameObject != null)
        {
            cartel.enabled = true;

            if ((!done && (Input.GetButtonDown("Interact")) || (!done && GameObject.FindObjectOfType<ControllerPersonaje>().gameObject != null && GameObject.FindObjectOfType<ControllerPersonaje>().joystick != null && GameObject.FindObjectOfType<ControllerPersonaje>().joystick.Action2.WasPressed)))
            {
                if (SceneManager.GetActiveScene().name == "Nivel_1")
                {
                    if (!GameManager.Instance.NivelActual.completado)
                    {
                        ManagerLogros.Instance.AddStat("NivelesZona1");
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Nivel_2")
                {
                    if (!GameManager.Instance.NivelActual.completado)
                    {
                        ManagerLogros.Instance.AddStat("NivelesZona1");
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Nivel_3")
                {
                    if (GhostData.Instance.tiempoNivel < 120f)
                    {
                        ManagerLogros.Instance.DesbloquearLogro(26);
                    }
                    if (!GameManager.Instance.NivelActual.completado)
                    {
                        ManagerLogros.Instance.AddStat("NivelesZona1");
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Nivel_5")
                {
                    if (!GameManager.Instance.NivelActual.completado)
                    {
                        ManagerLogros.Instance.AddStat("NivelesZona2");
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Nivel_6")
                {
                    if (!GameManager.Instance.NivelActual.completado)
                    {
                        ManagerLogros.Instance.AddStat("NivelesZona2");
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Nivel_7")
                {
                    if (!GameManager.Instance.NivelActual.completado)
                    {
                        ManagerLogros.Instance.AddStat("NivelesZona2");
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Nivel_8")
                {
                    if (GhostData.Instance.tiempoNivel < 180f)
                    {
                        ManagerLogros.Instance.DesbloquearLogro(27);
                    }
                    if (!GameManager.Instance.NivelActual.completado)
                    {
                        ManagerLogros.Instance.AddStat("NivelesZona2");
                    }
                }
                else if (SceneManager.GetActiveScene().name == "Nivel_11")
                {
                    if (GhostData.Instance.tiempoNivel < 240f)
                    {
                        ManagerLogros.Instance.DesbloquearLogro(28);
                    }
                   
                }
                done = true;
                GuardarDatos();
                if (GhostData.Instance != null)
                {
                    GhostData.Instance.TerminarNivel(SceneManager.GetActiveScene().name);
                }



                if (GetComponent<DesbloquearHabilidades>() != null)
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
                else { IrLobby(); }
            }
        }
    }

    void IrLobby()
    {
        if (FindObjectOfType<VidaPlayer>().heMuertoEnEsteNivel == false)
        {
            ManagerLogros.Instance.DesbloquearLogro(15);
            if (!GameManager.Instance.NivelActual.pasadoSinMorir)
            {
                GameManager.Instance.NivelActual.pasadoSinMorir = true;
                ManagerLogros.Instance.AddStat("NivelesSinMuerte");
            }


        }
        if (SceneManager.GetActiveScene().name == "Nivel_1")
        {
            ManagerLogros.Instance.DesbloquearLogro(0);

        }
        else if (SceneManager.GetActiveScene().name == "Nivel_4_Boss1")
        {
            ManagerLogros.Instance.DesbloquearLogro(2);
        }
        else if (SceneManager.GetActiveScene().name == "Nivel_12_BossFinal")
        {
            ManagerLogros.Instance.DesbloquearLogro(8);
        }
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
