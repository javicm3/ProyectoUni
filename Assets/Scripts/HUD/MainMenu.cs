using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject controles;
    public GameObject botonesPrincipales;
    public GameObject opciones; 
    
    [Header("Textos Menu Principal")]
    [SerializeField] TextMeshProUGUI jugarT;
    [SerializeField] TextMeshProUGUI cargarT;
    [SerializeField] TextMeshProUGUI opcionesT1;
    
    [SerializeField] TextMeshProUGUI salirT;
    [SerializeField] TextMeshProUGUI volverT1;
    

    [Header("Textos Menu Opciones")]
    [SerializeField] TextMeshProUGUI opcionesT2;

    [SerializeField] TextMeshProUGUI idiomasT;
    [SerializeField] TextMeshProUGUI españolT;
    [SerializeField] TextMeshProUGUI inglesT;

    [SerializeField] TextMeshProUGUI volumenT;
    [SerializeField] TextMeshProUGUI musicaT;
    [SerializeField] TextMeshProUGUI sonidoT;

    [SerializeField] TextMeshProUGUI verControlesT;
    [SerializeField] TextMeshProUGUI mandoT;
    [SerializeField] TextMeshProUGUI tecladoT;
    [SerializeField] TextMeshProUGUI volverT2;

    [Header("Controles")]
    [SerializeField] Image imagenControles;
    [SerializeField] Sprite controlesEsp;
    [SerializeField] Sprite controlesIng;

    [Header("Tic Idiomas")]
    [SerializeField] Image ticEspañol;
    [SerializeField] Image ticIngles;

    [Header("Sliders")]
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    // Start is called before the first frame update
    void Start()
    {
        //Esto de momento da error porque no hay gm en la escena
        musicSlider.value = GameManager.Instance.MusicVolume;
        sfxSlider.value = GameManager.Instance.SfxVolume;
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

    public void OnMusicChange()
    {
        GameManager.Instance.MusicVolume = musicSlider.value;
    }

    public void OnEffectsChange()
    {
        GameManager.Instance.SfxVolume = sfxSlider.value;
    }


    public void SaveVolumeSettings()
    {
        GameManager.Instance.MusicVolumeSave = musicSlider.value;
        GameManager.Instance.SfxVolumeSave = sfxSlider.value;
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
        //volverT1.text = Idiomas.volver[i];
        volverT2.text = Idiomas.volver[i];

        verControlesT.text = Idiomas.controles[i];
        mandoT.text = Idiomas.mando[i];
        tecladoT.text = Idiomas.teclado[i];

        volumenT.text = Idiomas.volumen[i];
        musicaT.text = Idiomas.musica[i];
        sonidoT.text = Idiomas.sonido[i];

        idiomasT.text = Idiomas.idioma[i];
        españolT.text = Idiomas.español[i];
        inglesT.text = Idiomas.ingles[i];

        switch (i) //Lo pongo con un switch por si queremos poner más idiomas
        {
            case 0:
                //imagenControles.sprite = controlesEsp;
                ticEspañol.enabled=true; ticIngles.enabled = false;
                break;

            case 1:
                //imagenControles.sprite = controlesIng;
                ticEspañol.enabled = false; ticIngles.enabled = true;
                break;

            default:
                break;
        }
    }
}
