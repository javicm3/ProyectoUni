using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    //V A R I A B L E S   N U E V A S   P A U L A

    //COMENTARIO: Por cada nivel que se añade, para setear los coleccionables max hay que añadir un case en el swich del void CheckLevelList() (abajo del todo) (ahora está puesto hasta el 7 por si acaso)

    List<LevelInfo> listaNiveles = new List<LevelInfo>();
    public List<LevelInfo> ListaNiveles { get => listaNiveles; }
    LevelInfo nivelActual;
    public LevelInfo NivelActual { get => nivelActual; }
    public string[] escenasSinColeccionables = new string[] { "PantallaInicio", "NL-0" };
    Checkpoint ultimoCheck;
    public Checkpoint UltimoCheck { get => ultimoCheck; set => ultimoCheck = value; }

    bool mostrarSaveIcon;
    public bool MostrarSaveIcon { get => mostrarSaveIcon; set => mostrarSaveIcon = value; }
    ListaHabilidades habilidades = new ListaHabilidades(); 
    public ListaHabilidades Habilidades { get => habilidades; set => habilidades = value; }

    NewAudioManager NAM;
    public bool animDesbloquear;

    //  V O L U M E N   S O N I D O 
    public AudioMixer audioMixer;

    [Range(0, 1)]
    float musicVolume = 0.5f;

    [Range(0, 1)]
    float sfxVolume = 0.5f;

    public float MusicVolume
    {
        set
        {
            value = Mathf.Clamp(value, 0.0001f, 1);
            musicVolume = value;
          if(audioMixer!=null)  audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume)*20);
        }
    } 

    public float SfxVolume
    {
        set
        {
            value = Mathf.Clamp(value, 0.0001f, 1);
            sfxVolume = value;
           if(audioMixer!=null) audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        }
    }

    public float MusicVolumeSave
    {
        set
        {
            value = Mathf.Clamp(value, 0.0001f, 1);
            PlayerPrefs.SetFloat("music", value);
            if (PlayerPrefs.HasKey("music"))
            { print(PlayerPrefs.GetFloat("music")); }
        } 
    }

    public float SfxVolumeSave
    {
        set
        {
            value = Mathf.Clamp(value, 0.0001f, 1);
            PlayerPrefs.SetFloat("effects", value);
        }
    }

    public void CargarVolumenGuardado()
    {
        if (audioMixer != null) audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        if (audioMixer != null) audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20); 
    }

    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    [Header("FINGuardadasNivelPrueba")]
    public bool personajevivo = true;
    public Vector3 futureSpawn;
    [HideInInspector] public float maxScore1;
    [HideInInspector] public float maxScore2;
    [HideInInspector] public float maxScore3;
    [HideInInspector] public float maxScore4;
    [HideInInspector] public float maxScore5;
    public float nivelescompletados = 0;
    Image estrella1;
    Image estrella2;
    Image estrella3;
    Color colorestrellaapagada = new Color(0.075f, 0.072f, 0.072f);
    int nivelaux = 0;
    public GameObject mainPanel;
    Camera camarita;
    public float monedasNecesariasDisparo = 3;
    public float coleccionablesneed = 3;
    float actualcoleccionables = 0;
    public bool puedoDisparar = false;
    [Header("NUEVAS VARIABLES")]
    //public float actualLevel = 0;
    public float[] coleccionablesMaxNv;
    public List<string> totalColeccionables;
    public GameObject PanelColeccionables;
    public Text textoActualColecc;
    public Text textoMaxColecc;

    //Variable para el tiempo de muerte
    public float tiempoMuerte;

    //public bool desbloqueadoDash=true;

    [Header(" FIN COSAS ANTERIORES IGNORAR")]

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }



    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
      
        if (GameObject.FindGameObjectsWithTag("InicioNivel") != null)
        {
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("InicioNivel"))
            {
                go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
            }
        }
        if (GameObject.FindObjectsOfType<Checkpoint>() != null)
        {
            Checkpoint[] checks= FindObjectsOfType<Checkpoint>();
            foreach (Checkpoint go in checks)
            {
                go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////
             //- - - - - - - - E S T O Y   T R A B A J A N D O   A Q U Í  - - - - - - - - 
        //////////////////////////////////////////////////////////////////////////////////////////


        bool tieneColeccionables = true;
        foreach (string nombreEscena in escenasSinColeccionables)
        {
            if (nombreEscena == scene.name)
            {
                tieneColeccionables = false;
            }
        }
        if (tieneColeccionables)
        {
            CheckLevelList(scene.name);//Dentro de este método se seteea el nivel actual
            UltimoCheck = null;

            if (personajevivo == false)//Esto no se por que lo hacia Julio
            {
                nivelActual.actualColeccionablesCogidos.Clear();
            }

            foreach (string go in nivelActual.coleccionablesCogidos)
            {
                Destroy(GameObject.Find(go.ToString()));
                print("destroy " + go);
            }


            textoActualColecc = GameObject.Find("Actual").GetComponent<Text>();
            textoActualColecc.text = nivelActual.coleccionablesCogidos.Count.ToString();
            GameObject.Find("Maximo").GetComponent<Text>().text = nivelActual.maxColeccionables.ToString();

            personajevivo = true;

            if (GameObject.FindGameObjectWithTag("Player") != null) //Julio hacia este if antes de buscar el audio manager asique lo haré igual
            {
                NAM = FindObjectOfType<NewAudioManager>();
                return;
            }
        }
        else if(scene.name=="NL-0") 
        {
            //actualLevel = -1;
            personajevivo = true;

            GameObject.Find("Actual").GetComponent<Text>().text = totalColeccionables.Count.ToString();
            //textoMaxColecc = GameObject.Find("Maximo").GetComponent<Text>();
            //textoMaxColecc.text = coleccionablesMaxNv[(int)actualLevel].ToString();

            if (animDesbloquear)
            {
                Invoke("HacerAnim", 1);
                animDesbloquear = false;
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInput>().enabled = false;
            }

        }
    }

    void HacerAnim()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetTrigger("Habilidad");
        Invoke("DevolverInput", 7.45f);
    }
    void DevolverInput()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInput>().enabled = true;
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);

        SistemaGuardado.Incializar();
        SfxVolume = PlayerPrefs.GetFloat("effects");
        MusicVolume = PlayerPrefs.GetFloat("music");
        CargarVolumenGuardado();
    }


    public void ReiniciarEscena()
    {
        personajevivo = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene(string nextlvl)
    { 
        SceneManager.LoadScene(nextlvl, LoadSceneMode.Single);
    
    }

    public void MuertePJ() //- - - - - - MIRAR SI TIENE ALGO QUE VER CON LOS CHECKPOINTS
    {
        if (personajevivo == true)
        {
            //print("muerto");
            personajevivo = false;
            if (GameObject.Find("Player") != null) GameObject.Find("Player").GetComponent<ControllerPersonaje>().movimientoBloqueado = true;
            if (GameObject.Find("Player") != null) GameObject.Find("Player").GetComponent<ControllerPersonaje>().rb.velocity = Vector2.zero;
            //Frenar();
            //Time.timeScale = 0.3f;
            //GameObject.Find("Player").GetComponent<Animator>().SetTrigger("Die");
            if (GameObject.Find("Player") != null) GameObject.Find("Player").GetComponent<ControllerPersonaje>().enabled = false;
            //GameObject.Find("Player").GetComponent<SpriteRenderer>().color = Color.black;
            StartCoroutine(Tiemporeiniciar(tiempoMuerte));
        }

    }

    public IEnumerator Tiemporeiniciar(float tiempo)
    {
        SpriteRenderer[] renderers;
        yield return new WaitForSeconds(tiempo);
        if (GameObject.Find("Player") != null)
        {
            GameObject.Find("Player").GetComponent<Particulas>().SpawnParticulas(GameObject.FindObjectOfType<Particulas>().particulasMuerte, GameObject.Find("Player").transform.position, GameObject.Find("Player").transform);
            renderers = GameObject.Find("Player").GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sp in renderers)
            {
                sp.enabled = false;
            }
        }



        //if (GameObject.Find("Player") != null) Destroy(GameObject.Find("Player").GetComponentInChildren<Animator>());
        StartCoroutine(TrueReinicio(1.3f));
    }
    public IEnumerator TrueReinicio(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);

        Time.timeScale = 1f;
        ReiniciarEscena();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.R))
        {
            ReiniciarEscena();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            habilidades.dash = true;
            habilidades.chispazo = true;
            habilidades.movCables = true;
            habilidades.movParedes = true;

            ControllerPersonaje p = FindObjectOfType<ControllerPersonaje>();
            if (p!=null)
            { p.CargarHabilidadesGM(); }
        }
    }


    public void CogerColeccionableNivel(GameObject coleccionable)
    {
        if (!nivelActual.actualColeccionablesCogidos.Contains(coleccionable.name))
        {
            nivelActual.actualColeccionablesCogidos.Add(coleccionable.name);
            textoActualColecc.text = nivelActual.actualColeccionablesCogidos.Count.ToString();

            coleccionable.AddComponent<Moneda>().Desactivar();           

            NAM.Play("Points");
        }
    }


    void CheckLevelList(string nombreEscena)
    {

        bool nivelEnLista = false;

        if (listaNiveles!=null)
        {
            foreach (LevelInfo level in listaNiveles)
            {
                if (level.nombreNivel == nombreEscena)
                {
                    nivelEnLista = true;
                    nivelActual = level;

                    int i = 0;
                    switch (nombreEscena)
                    {
                        case "ND-1":
                            i = 0;
                            break;
                        case "ND-2":
                            i = 1;
                            break;
                        case "ND-3":
                            i = 2;
                            break;
                        case "ND-5":
                            i = 3;
                            break;
                        case "ND-6":
                            i = 4;
                            break;
                        case "ND-7":
                            i = 5;
                            break;
                    }
                    nivelActual.maxColeccionables = (int)coleccionablesMaxNv[i];
                    break;
                }
            }
        }


        if (!nivelEnLista)//Añadir el nivel a la lista
        {
            LevelInfo nuevoNivel = new LevelInfo();
            nuevoNivel.nombreNivel = nombreEscena;
            //- - - - - - - - - - - - - - - - - - - Decidir si busco todos los coleccionables del nivel o no :3
            listaNiveles.Add(nuevoNivel);
            nivelActual = nuevoNivel;
        }
    }

}

[System.Serializable]
public class LevelInfo
{
    public string nombreNivel;
    public bool completado = false;

    public List<string> coleccionablesCogidos = new List<string>();
    public List<string> actualColeccionablesCogidos = new List<string>(); 

    public int maxColeccionables;
}

[System.Serializable]
public class ListaHabilidades
{
    public bool dash = false;
    public bool chispazo = false;
    public bool movParedes = false;
    public bool movCables = false;
}

