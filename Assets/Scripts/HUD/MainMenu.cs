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
    [SerializeField] TextMeshProUGUI noArchivoT;
    [SerializeField] TextMeshProUGUI sobreescribirT;
    [SerializeField] TextMeshProUGUI siT;
    //[SerializeField] TextMeshProUGUI noT; Añadir si se ponen más idiomas

    [SerializeField] TextMeshProUGUI aceptarT;
    [SerializeField] TextMeshProUGUI[] slotT;
    [SerializeField] TextMeshProUGUI volverT3;

    [Header("Controles")]
    [SerializeField] Image imagenControles;
    [SerializeField] Sprite mandoEsp;
    [SerializeField] Sprite mandoIng;
    [SerializeField] Sprite tecladoEsp;
    [SerializeField] Sprite tecladoIng;

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
    public void Controles(bool mando)
    {
        if (mando)
        {
            if (SistemaGuardado.indiceIdioma == 1)
            {
                imagenControles.sprite = mandoIng;
            }
            else { imagenControles.sprite = mandoEsp; }
        }
        else
        {
            if (SistemaGuardado.indiceIdioma == 1)
            {
                imagenControles.sprite = tecladoIng;
            }
            else { imagenControles.sprite = tecladoEsp; }
        }


        controles.SetActive(true);
        opciones.SetActive(false);
    }

    public void CerrarControles()
    {
        controles.SetActive(false);
        opciones.SetActive(true);
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
            if (SistemaGuardado.dataSlots.slotArray[i].exists)
            {
                Transform d = slots[i].transform.Find("Slot/Datos");
                d.gameObject.SetActive(true);
                d.Find("Coleccionables").GetComponent<TextMeshProUGUI>().text = ""+SistemaGuardado.dataSlots.slotArray[i].coleccionables;

                float t = SistemaGuardado.dataSlots.slotArray[i].gameTime/60;
                string hours = Mathf.Floor(t/60).ToString("00");
                string minutes = Mathf.Floor(t % 60).ToString("00");

                d.Find("Tiempo").GetComponent<TextMeshProUGUI>().text = hours+" : " +minutes;

                slots[i].transform.Find("Slot/Vacio").gameObject.SetActive(false);
            }
            else
            {
                slots[i].transform.Find("Slot/Vacio").gameObject.SetActive(true);
            }
        }
    }


    public void ClickSlot(int slot) 
    {
        SistemaGuardado.indiceSlot = slot;
        if (newGame)
        {
            //Comprobar si el archivo ya existe, si es así pantalla sobreescribir
            if (SistemaGuardado.dataSlots.slotArray[slot].exists)
            {
                confirmacion.SetActive(true);
                slotsMenu.SetActive(false);
            }
            else
            {                
                SistemaGuardado.timeToIgnore = Time.time;
                Cursor.visible = false;
                GameManager.Instance.PlayCinematica(1, "Lobby");
            }
        }
        else
        {
            //Comprobar si el archivo existe, si es asi decir que no se puede cargar el archivo porque está vacio
            if (SistemaGuardado.dataSlots.slotArray[slot].exists)
            {
                SistemaGuardado.timeToIgnore = Time.time;
                SistemaGuardado.Cargar();
                SceneManager.LoadScene("Lobby");
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
        SistemaGuardado.timeToIgnore = Time.time;
        Cursor.visible = false;
        GameManager.Instance.PlayCinematica(1,"Lobby");
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
            slotT[j].transform.Find("Datos/t").GetComponent<TextMeshProUGUI>().text = Idiomas.time[i];
            slotT[j].transform.Find("Datos/c").GetComponent<TextMeshProUGUI>().text = Idiomas.collectibles[i];
            slotT[j].transform.Find("Vacio").GetComponent<TextMeshProUGUI>().text = Idiomas.empty[i];
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
