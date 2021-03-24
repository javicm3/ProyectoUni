using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject controles;
    public GameObject botonesPrincipales;
    public GameObject opciones;

    [Header("Textos")]
    [SerializeField] Text jugarT;
    [SerializeField] Text cargarT;
    [SerializeField] Text opcionesT1;
    [SerializeField] Text opcionesT2;
    [SerializeField] Text salirT;
    [SerializeField] Text volverT1;
    [SerializeField] Text volverT2;
    [SerializeField] Text controlesT;
    [SerializeField] Text españolT;
    [SerializeField] Text inglesT;

    [Header("Controles")]
    [SerializeField] Image imagenControles;
    [SerializeField] Sprite controlesEsp;
    [SerializeField] Sprite controlesIng;


    // Start is called before the first frame update
    void Start()
    {
        ActualizarIdiomas();
    }


    public void Opciones(bool op)
    {
        opciones.SetActive(op);
        botonesPrincipales.SetActive(!op);
    }

    //Cambiar que cosas abre o cierra
    public void Controles(bool cont)
    {
        controles.SetActive(cont);
        opciones.SetActive(!cont);

    }
    public void Play()
    {
        SceneManager.LoadScene("NL-0");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void CambiarIdioma(int index)
    {
        SistemaGuardado.CambiarIndiceIdioma(index);
        ActualizarIdiomas();
    }

    void ActualizarIdiomas()
    {
        int i = SistemaGuardado.indiceIdioma;

        jugarT.text = Idiomas.jugar[i];
        cargarT.text = Idiomas.cargar[i];
        opcionesT1.text = Idiomas.opciones[i];
        opcionesT2.text = Idiomas.opciones[i];
        salirT.text = Idiomas.salir[i];
        volverT1.text = Idiomas.volver[i];
        volverT2.text = Idiomas.volver[i];
        controlesT.text = Idiomas.controles[i];
        españolT.text = Idiomas.español[i];
        inglesT.text = Idiomas.ingles[i];

        switch (i) //Lo pongo con un switch por si queremos poner más idiomas
        {
            case 0:
                imagenControles.sprite = controlesEsp;
                break;

            case 1:
                imagenControles.sprite = controlesIng;
                break;

            default:
                break;
        }
    }
}
