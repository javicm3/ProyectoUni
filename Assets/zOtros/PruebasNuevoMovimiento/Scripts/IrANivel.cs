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

    // Update is called once per frame
    void Update()
    {

    }
    void Activar(string nivel)
    {
        SceneManager.LoadScene(nivel, LoadSceneMode.Single);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//CAMBIAR SI DECIDO BUSCAR LOS COLECCIONABLES AL INICIO DEL NIVEL
        {
            int estrellasCogidas=0, coleccionablesCogidos=0;
            if (GameManager.Instance.ListaNiveles!=null)
            {
                foreach (LevelInfo level in GameManager.Instance.ListaNiveles)
                {
                    if (nivelDestino == level.nombreNivel)
                    {
                        estrellasCogidas = level.estrellasCogidas.Count;
                        coleccionablesCogidos = level.coleccionablesCogidos.Count;
                    }
                }
            }


            //----------------------------------------------------------------------------------
            if (nivelDestino == "ND-1")
            {
                textoEstrellasCogidas.text = "Estrellas" + estrellasCogidas.ToString() + "/" + GameManager.Instance.estrellasMaxNv[0];
                textoColeccionablesCogidos.text = "Coleccionables" + coleccionablesCogidos.ToString() + "/" + GameManager.Instance.coleccionablesMaxNv[0];
            }
            if (nivelDestino == "ND-2")
            {
                textoEstrellasCogidas.text = "Estrellas" + estrellasCogidas.ToString() + "/" + GameManager.Instance.estrellasMaxNv[1];
                textoColeccionablesCogidos.text = "Coleccionables" + coleccionablesCogidos.ToString() + "/" + GameManager.Instance.coleccionablesMaxNv[1];
            }
            if (nivelDestino == "ND-3")
            {
                textoEstrellasCogidas.text = "Estrellas" + estrellasCogidas.ToString() + "/" + GameManager.Instance.estrellasMaxNv[2];
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
                    if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action3.WasPressed)
                    {
                        if (GameManager.Instance.totalEstrellas.Count >= requisitoEstrellas)
                        {
                            Activar(nivelDestino);
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        if (GameManager.Instance.totalEstrellas.Count >= requisitoEstrellas)
                        {
                            Activar(nivelDestino);
                        }
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
