using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VueltaLobby : MonoBehaviour
{
    public string nivelDestino;
    public Canvas cartel;
    public string nombreEsteNivel;
   
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
    public void GuardarDatos()
    {
        if (nombreEsteNivel == "Nivel0")
        {
            foreach (string go in GameManager.Instance.actualColeccionablesCogidosNivel0)
            {
                if (!GameManager.Instance.coleccionablesCogidosNivel0.Contains(go))
                {
                    GameManager.Instance.coleccionablesCogidosNivel0.Add(go);
                }
                if (!GameManager.Instance.totalColeccionables.Contains(go))
                {
                    GameManager.Instance.totalColeccionables.Add(go);
                }

            }
            foreach (string go in GameManager.Instance.actualEstrellasCogidosNivel0)
            {
                if (!GameManager.Instance.estrellasCogidosNivel0.Contains(go))
                {
                    GameManager.Instance.estrellasCogidosNivel0.Add(go);
                }
                if (!GameManager.Instance.totalEstrellas.Contains(go))
                {
                    GameManager.Instance.totalEstrellas.Add(go);
                }
            }
           
        }
        else if (nombreEsteNivel == "Nivel1")
        {

            foreach (string go in GameManager.Instance.actualColeccionablesCogidosNivel1)
            {
                if (!GameManager.Instance.coleccionablesCogidosNivel1.Contains(go))
                {
                    GameManager.Instance.coleccionablesCogidosNivel1.Add(go);
                }
                if (!GameManager.Instance.totalColeccionables.Contains(go))
                {
                    GameManager.Instance.totalColeccionables.Add(go);
                }
            }
            foreach (string go in GameManager.Instance.actualEstrellasCogidosNivel1)
            {
                if (!GameManager.Instance.estrellasCogidosNivel1.Contains(go))
                {
                    GameManager.Instance.estrellasCogidosNivel1.Add(go);
                }
                if (!GameManager.Instance.totalEstrellas.Contains(go))
                {
                    GameManager.Instance.totalEstrellas.Add(go);
                }
            }
        }
        else if (nombreEsteNivel == "Nivel2")
        {

            foreach (string go in GameManager.Instance.actualColeccionablesCogidosNivel2)
            {
                if (!GameManager.Instance.coleccionablesCogidosNivel2.Contains(go))
                {
                    GameManager.Instance.coleccionablesCogidosNivel2.Add(go);
                }
                if (!GameManager.Instance.totalColeccionables.Contains(go))
                {
                    GameManager.Instance.totalColeccionables.Add(go);
                }
            }
            foreach (string go in GameManager.Instance.actualEstrellasCogidosNivel2)
            {
                if (!GameManager.Instance.estrellasCogidosNivel2.Contains(go))
                {
                    GameManager.Instance.estrellasCogidosNivel2.Add(go);
                }
                if (!GameManager.Instance.totalEstrellas.Contains(go))
                {
                    GameManager.Instance.totalEstrellas.Add(go);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {



            cartel.enabled = true;

            if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action2.WasPressed)
            {
                GuardarDatos();
                Activar(nivelDestino);


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
