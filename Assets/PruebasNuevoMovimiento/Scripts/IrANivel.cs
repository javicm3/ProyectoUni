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
        if (collision.gameObject.tag == "Player")
        {

            if (nivelDestino == "ND-1")
            { textoEstrellasCogidas.text = "Estrellas"+ GameManager.Instance.estrellasCogidosNivel0.Count.ToString()+"/"+ GameManager.Instance.estrellasMaxNv[0];
                textoColeccionablesCogidos.text = "Coleccionables" + GameManager.Instance.coleccionablesCogidosNivel0.Count.ToString() + "/" + GameManager.Instance.coleccionablesMaxNv[0];
            }
            if (nivelDestino == "ND-2")
            {
                textoEstrellasCogidas.text = "Estrellas" + GameManager.Instance.estrellasCogidosNivel1.Count.ToString() + "/" + GameManager.Instance.estrellasMaxNv[1];
                textoColeccionablesCogidos.text = "Coleccionables" + GameManager.Instance.coleccionablesCogidosNivel1.Count.ToString() + "/" + GameManager.Instance.coleccionablesMaxNv[1];
            }
            if (nivelDestino == "ND-3")
            {
                textoEstrellasCogidas.text = "Estrellas" + GameManager.Instance.estrellasCogidosNivel2.Count.ToString() + "/" + GameManager.Instance.estrellasMaxNv[2];
                textoColeccionablesCogidos.text = "Coleccionables" + GameManager.Instance.coleccionablesCogidosNivel2.Count.ToString() + "/" + GameManager.Instance.coleccionablesMaxNv[2];
            }
            if (nivelDestino == "NivelSemana26")
            {
                textoEstrellasCogidas.text = "Estrellas" + GameManager.Instance.estrellasCogidosNivel2.Count.ToString() + "/" + GameManager.Instance.estrellasMaxNv[2];
                textoColeccionablesCogidos.text = "Coleccionables" + GameManager.Instance.coleccionablesCogidosNivel2.Count.ToString() + "/" + GameManager.Instance.coleccionablesMaxNv[2];
            }

            cartel.enabled = true;
            textoEstrellas.text = "Necesitas"+requisitoEstrellas.ToString();
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
