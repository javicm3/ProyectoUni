﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InControl;
using UnityEngine.SceneManagement;

public class FuncionalidadPausa : MonoBehaviour
{

    [SerializeField] GameObject menuPausa;
    [SerializeField] GameObject menuOpciones;
    Image fondo;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    InputDevice joystick;

    [Header("Controles")]
    [SerializeField] Image imagenControles;
    [SerializeField] Sprite mandoEsp;
    [SerializeField] Sprite mandoIng;
    [SerializeField] Sprite tecladoEsp;
    [SerializeField] Sprite tecladoIng;

    [Header("Textos")]
    [SerializeField] TextMeshProUGUI pauseT;
    [SerializeField] TextMeshProUGUI resumeT;
    [SerializeField] TextMeshProUGUI optionsT;
    //[SerializeField] TextMeshProUGUI lobbyT;
    [SerializeField] TextMeshProUGUI exitT;
    [SerializeField] TextMeshProUGUI opcionesT;
    [SerializeField] TextMeshProUGUI volumenT;
    [SerializeField] TextMeshProUGUI musicaT;
    [SerializeField] TextMeshProUGUI sonidoT;
    [SerializeField] TextMeshProUGUI controlsT;
    [SerializeField] TextMeshProUGUI tecladoT;
    [SerializeField] TextMeshProUGUI mandoT;
    [SerializeField] TextMeshProUGUI volverT;

    void ActualizarIdiomas()
    {
        int i = SistemaGuardado.indiceIdioma;

        pauseT.text = Idiomas.pause[i];
        resumeT.text = Idiomas.resume[i];
        optionsT.text = Idiomas.opciones[i];
        exitT.text = Idiomas.salir[i];
        opcionesT.text = Idiomas.opciones[i];
        volumenT.text = Idiomas.volumen[i];
        musicaT.text = Idiomas.musica[i];
        sonidoT.text = Idiomas.sonido[i];
        controlsT.text = Idiomas.controles[i];
        tecladoT.text = Idiomas.teclado[i];
        mandoT.text = Idiomas.mando[i];
        volverT.text = Idiomas.volver[i];
    }

    // Start is called before the first frame update
    void Start()
    {
        fondo=GetComponent<Image>();
        ActualizarIdiomas();
        musicSlider.value = GameManager.Instance.MusicVolume;
        sfxSlider.value = GameManager.Instance.SfxVolume;
        StartCoroutine(FindJoystick());
    }

    IEnumerator FindJoystick()
    {
        yield return new WaitForEndOfFrame();
        joystick = FindObjectOfType<ControllerPersonaje>().joystick;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (joystick!=null && joystick.Name != "NullInputDevice" && joystick.MenuWasPressed))
        {
            if (Time.timeScale == 1)
            {
                FindObjectOfType<NewAudioManager>().Play("Pausa");
                OnPause(true);                
            }
            else
            {
                FindObjectOfType<NewAudioManager>().Play("SalirPausa");
                OnPause(false);
                menuOpciones.SetActive(false);
            }
        }
    }

    public void OnPause(bool menu)
    {
        if (menu) { Time.timeScale = 0; }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            foreach (HUDController hud in GetComponentsInChildren<HUDController>())
            {
                hud.DiselectAllItems();
            }
        }
        menuPausa.SetActive(menu);
        fondo.enabled = menu;
    }

    public void OnOptions(bool options)
    {
        menuOpciones.SetActive(options);
        menuPausa.SetActive(!options);
    }

    public void VolverLobby(string scene)
    {
        if (scene == "PantallaInicio")
        {
            Destroy(GameManager.Instance);
        }
        else
        {
            GameManager.Instance.NivelActual.actualColeccionablesCogidos.Clear();
            GameManager.Instance.NivelActual.actualColeccionablesCogidos.AddRange(GameManager.Instance.NivelActual.coleccionablesCogidos);
        }
        if(GhostData.Instance!=null) GhostData.Instance.activado = false;
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }

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


        imagenControles.gameObject.SetActive(true);
        menuOpciones.SetActive(false);
    }

    public void CerrarControles()
    {
        imagenControles.gameObject.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void OnExit()
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
}
