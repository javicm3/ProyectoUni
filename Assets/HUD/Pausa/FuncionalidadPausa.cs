using System.Collections;
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
        fondo = GetComponent<Image>();
        ActualizarIdiomas();
        musicSlider.value = GameManager.Instance.MusicVolume;
        sfxSlider.value = GameManager.Instance.SfxVolume;

        joystick = InputManager.ActiveDevice;
        mandoprev = joystick != null && joystick.Name != "NullInputDevice";
        StartCoroutine(FindJoystick());
    }

    PlayerInput pInput;
    ControllerPersonaje cp;
    IEnumerator FindJoystick()
    {
        yield return new WaitForEndOfFrame();
        cp = FindObjectOfType<ControllerPersonaje>();
        pInput = FindObjectOfType<PlayerInput>();

        //if (cp.joystick != null && cp.joystick.Name != "NullInputDevice") mandoprev = true;
    }

    bool gatito = false;
    bool mandoprev = false;
    InputDevice joystick;
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Cinematica")
        {
            if (Input.GetKeyDown(KeyCode.Escape) || (cp!=null &&cp.joystick != null && cp.joystick.Name != "NullInputDevice" && cp.joystick.Name != "Unknown Device" && cp.joystick.MenuWasPressed))
            {
                if (Time.timeScale == 1)
                {
                    FindObjectOfType<NewAudioManager>().Play("Pausa");
                    OnPause(true);
                    pInput.enabled = false;
                    cp.saltoBloqueado = true;
                }
                else
                {
                    FindObjectOfType<NewAudioManager>().Play("SalirPausa");
                    OnPause(false);
                    menuOpciones.SetActive(false);
                    imagenControles.gameObject.SetActive(false);
                    pInput.enabled = true;
                    cp.saltoBloqueado = false;
                }
            }


            //if (cp.joystick != null && cp.joystick.Name != "NullInputDevice" && cp.joystick.Name != "Unknown Device") mandonow = true;
            
            joystick = InputManager.ActiveDevice;
            //print(mandoprev + " " + (joystick != null && joystick.Name != "NullInputDevice" && joystick.Name != "Unknown Device") + " " + joystick.Name);
            if (gatito)
            {
                if (mandoprev != (joystick != null && joystick.Name != "NullInputDevice" && joystick.Name != "Unknown Device")) //(mandoprev!=mandonow)
                {
                    mandoprev = !mandoprev;
                    if (Time.timeScale == 1)
                    {
                        FindObjectOfType<NewAudioManager>().Play("Pausa");

                        OnPause(true);
                        if (!mandoprev)
                        {
                            foreach (HUDController hud in GetComponentsInChildren<HUDController>())
                            {
                                hud.isController = mandoprev;
                            }

                            Cursor.visible = true;
                        }

                        pInput.enabled = false;
                        cp.saltoBloqueado = true;
                    }

                }
            }
            else { gatito = true; mandoprev = (joystick != null && joystick.Name != "NullInputDevice" && joystick.Name != "Unknown Device"); } //siento esta basura pero al inicio no me lo seteaba bien asique aqui
            
        }
    }

    public void OnPause(bool menu)
    {
        if (menu) { Time.timeScale = 0; }
        else
        {
            pInput.enabled = true;
            cp.saltoBloqueado = false;
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
            Destroy(GameManager.Instance.gameObject);
        }
        else
        {
            GameManager.Instance.NivelActual.actualColeccionablesCogidos.Clear();
            GameManager.Instance.NivelActual.actualColeccionablesCogidos.AddRange(GameManager.Instance.NivelActual.coleccionablesCogidos);
        }
        if (GhostData.Instance != null) GhostData.Instance.activado = false;
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
