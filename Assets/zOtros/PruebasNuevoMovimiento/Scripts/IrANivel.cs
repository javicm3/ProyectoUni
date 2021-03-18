using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IrANivel : MonoBehaviour
{
    public string nivelDestino;
    public Canvas cartel;
    public float requisitoEstrellas;
    public Text textoEstrellas;
    public Text textoEstrellasCogidas;
    public Text textoColeccionablesCogidos;
    // Start is called before the first frame update
    void Start()
    {
        cartel.enabled = false;
    }

    bool done = false;
    void Activar(string nivel)
    {
        print("._.");
        if (!done)
        {
            done = true;
            FadeInOut fade = FindObjectOfType<FadeInOut>();
            if (fade != null)
            {
                StartCoroutine(fade.FadeOut());
                StartCoroutine(CargarNivel(nivel));
            }
            else { SceneManager.LoadScene(nivel, LoadSceneMode.Single); }
        }
    }
   
    
    IEnumerator CargarNivel(string nivel)
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(nivel, LoadSceneMode.Single);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//CAMBIAR SI DECIDO BUSCAR LOS COLECCIONABLES AL INICIO DEL NIVEL
        {
            int coleccionablesCogidos=0;
            if (GameManager.Instance.ListaNiveles!=null)
            {
                foreach (LevelInfo level in GameManager.Instance.ListaNiveles)
                {
                    if (nivelDestino == level.nombreNivel)
                    {
                        coleccionablesCogidos = level.coleccionablesCogidos.Count;
                    }
                }
            }


            //----------------------------------------------------------------------------------
            if (nivelDestino == "ND-1")
            {
                textoColeccionablesCogidos.text = "Coleccionables" + coleccionablesCogidos.ToString() + "/" + GameManager.Instance.coleccionablesMaxNv[0];
            }
            if (nivelDestino == "ND-2")
            {
                textoColeccionablesCogidos.text = "Coleccionables" + coleccionablesCogidos.ToString() + "/" + GameManager.Instance.coleccionablesMaxNv[1];
            }
            if (nivelDestino == "ND-3")
            {
                textoColeccionablesCogidos.text = "Coleccionables" + coleccionablesCogidos.ToString() + "/" + GameManager.Instance.coleccionablesMaxNv[2];
            }
            //-------------------------------------------------------------------------------------

            cartel.enabled = true;
            textoEstrellas.text = "Necesitas" + requisitoEstrellas.ToString();
            if (nivelDestino == "NL-0")
            {
                Activar(nivelDestino);
            }
            else
            {if (GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick != null)
                {
                    if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action2.WasPressed)
                    {
                        Activar(nivelDestino);
                        /*if (GameManager.Instance.totalEstrellas.Count >= requisitoEstrellas)
                        {
                            Activar(nivelDestino);
                        }*/
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        Activar(nivelDestino);
                        /*if (GameManager.Instance.totalEstrellas.Count >= requisitoEstrellas)
                        {
                            Activar(nivelDestino);
                        }*/
                    }
                }               
            }
        }
        else
        {

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cartel.enabled = false;
        }
    }
}
