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
    public GameObject slotsMenu;
    public GameObject confirmacion;
    public GameObject noArchivo;
    public Button[] slots;
    bool newGame = false;
    
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

    [Header("Textos Menu Slots")]
    [SerializeField] TextMeshProUGUI sobreescribirT;
    [SerializeField] TextMeshProUGUI noArchivoT;
    [SerializeField] TextMeshProUGUI siT;
    //[SerializeField] TextMeshProUGUI noT; Añadir si se ponen más idiomas

    [SerializeField] TextMeshProUGUI aceptarT;
    [SerializeField] TextMeshProUGUI[] slotT;
    [SerializeField] TextMeshProUGUI volverT3;

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
        ActualizarIdiomas();
        ChargeSlotInfo(); //Igual poner esto cuando se abra el menu de slots
        musicSlider.value = GameManager.Instance.MusicVolume;
        sfxSlider.value = GameManager.Instance.SfxVolume;        
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

    public void NewGame(bool newGame)
    {
        this.newGame = newGame;
    }

    public void OpenSlots(bool open )
    {        
        slotsMenu.SetActive(open);
        botonesPrincipales.SetActive(!open);

    }

    void ChargeSlotInfo()
    {
        for (int i = 0; i < 3; i++)
        {
            if (SistemaGuardado.slotExists[i]==0)
            {
                //Lo que sea que haga cuando no existe
            }
            else
            {
                //Lo que sea que haga cuando si existe
                //Placeholder
                slots[i].transform.Find("Empty").gameObject.SetActive(false);
            }
        }
    }


    public void ClickSlot(int slot) 
    {
        SistemaGuardado.indiceSlot = slot;
        if (newGame)
        {
            //Comprobar si el archivo ya existe, si es así pantalla sobreescribir
            if (SistemaGuardado.slotExists[slot] != 0)
            {
                confirmacion.SetActive(true);
                slotsMenu.SetActive(false);
            }
            else { SceneManager.LoadScene("NL-0"); }
        }
        else
        {
            //Comprobar si el archivo existe, si es asi decir que no se puede cargar el archivo porque está vacio
            if (SistemaGuardado.slotExists[slot] != 0)
            {
                SistemaGuardado.Cargar();
                SceneManager.LoadScene("NL-0");
            }
            else
            {
                noArchivo.SetActive(true);
                slotsMenu.SetActive(false);
            }            
        }
    }

    public void Confirmar()
    {
        SceneManager.LoadScene("NL-0");
    }

    public void CerrarPanel(GameObject panel)
    {
        panel.SetActive(false);
        slotsMenu.SetActive(true);
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
        volverT3.text = Idiomas.volver[i];

        verControlesT.text = Idiomas.controles[i];
        mandoT.text = Idiomas.mando[i];
        tecladoT.text = Idiomas.teclado[i];

        volumenT.text = Idiomas.volumen[i];
        musicaT.text = Idiomas.musica[i];
        sonidoT.text = Idiomas.sonido[i];

        idiomasT.text = Idiomas.idioma[i];
        españolT.text = Idiomas.español[i];
        inglesT.text = Idiomas.ingles[i];

        sobreescribirT.text = Idiomas.overwrite[i];
        noArchivoT.text = Idiomas.emptyFile[i];
        aceptarT.text = Idiomas.acept[i];
        siT.text = Idiomas.yes[i];
        for (int j = 0; j < slotT.Length; j++)
        {
            slotT[j].text = Idiomas.slot[i] + (j + 1);
        }

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
