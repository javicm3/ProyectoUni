using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class GameManager : MonoBehaviour
{
    //V A R I A B L E S   N U E V A S   P A U L A

    //COMENTARIO: Por cada nivel que se añade, para setear los coleccionables max hay que añadir un case en el swich del void CheckLevelList() (abajo del todo) (ahora está puesto hasta el 7 por si acaso)

    List<LevelInfo> listaNiveles = new List<LevelInfo>();
    public List<LevelInfo> ListaNiveles { get => listaNiveles; }
    LevelInfo nivelActual;
    public LevelInfo NivelActual { get => nivelActual; }
    public string[] escenasSinColeccionables = new string[] { "PantallaInicio", "Lobby" };
    Checkpoint ultimoCheck;
    public Checkpoint UltimoCheck { get => ultimoCheck; set => ultimoCheck = value; }

    bool mostrarSaveIcon;
    public bool MostrarSaveIcon { get => mostrarSaveIcon; set => mostrarSaveIcon = value; }
    ListaHabilidades habilidades = new ListaHabilidades(); 
    public ListaHabilidades Habilidades { get => habilidades; set => habilidades = value; }

    NewAudioManager NAM;
    public bool animDesbloquear;
    public DesbloquearHabilidades.habilidad habilidad;

    GameObject playerGO;

    //Cinemática
    public int cinematicaIndex = 1;
    public string cinematicaScene;

    public void PlayCinematica(int index, string scene)
    {
        cinematicaIndex = index;
        cinematicaScene = scene;
        SceneManager.LoadScene("Cinematica");
    }

    //  V O L U M E N   S O N I D O 
    public AudioMixer audioMixer;
    public bool haciendoAnim;
    [Range(0, 1)]
    float musicVolume = 0.5f;

    [Range(0, 1)]
    float sfxVolume = 0.5f;

    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            value = Mathf.Clamp(value, 0.0001f, 1);
            musicVolume = value;
            if(audioMixer!=null)  audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume)*20);
        }
    } 

    public float SfxVolume
    {
        get { return sfxVolume; }
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
    public float maxColeccionablesTotal;
    public float[] coleccionablesMaxNv;
    public List<string> totalColeccionables;
    public GameObject PanelColeccionables;
    public TextMeshProUGUI textoColecc;


    

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
            GameObject[] coleccionablesMonedas = GameObject.FindGameObjectsWithTag("Coleccionable");
            for (int i = 0; i < coleccionablesMonedas.Length; i++)
            {
                if (coleccionablesMonedas[i].GetComponentInChildren<ParticleSystem>() != null)
                {
                    coleccionablesMonedas[i].gameObject.name = (string)("Moneda" + i);
                }
            }
            Cursor.visible = false;
            CheckLevelList(scene.name);//Dentro de este método se seteea el nivel actual
            if(GhostData.Instance!=null)GhostData.Instance.IniciarFantasma(ref nivelActual);
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

            nivelActual.maxColeccionables = GameObject.FindGameObjectsWithTag("Coleccionable").Length;
            nivelActual.tiempoEmpezar = Time.time;
            
      if (GameObject.Find("TextoColecc")!=null && GameObject.Find("TextoColecc").GetComponent<TextMeshProUGUI>()!=null)      textoColecc = GameObject.Find("TextoColecc").GetComponent<TextMeshProUGUI>();
            if (GameObject.Find("TextoColecc") != null && GameObject.Find("TextoColecc").GetComponent<TextMeshProUGUI>() != null) textoColecc.text = nivelActual.coleccionablesCogidos.Count + "  /  " + nivelActual.maxColeccionables; 

            personajevivo = true;

            if (FindObjectOfType<ControllerPersonaje>()!=null&& FindObjectOfType<ControllerPersonaje>().gameObject != null) //Julio hacia este if antes de buscar el audio manager asique lo haré igual
            {
                NAM = FindObjectOfType<NewAudioManager>();
                return;
            }
        }
        else if(scene.name=="Lobby") 
        {
            Cursor.visible = false;
            personajevivo = true;
            print(totalColeccionables.Count+"totalcolecc");
            GameObject.Find("TextoColecc").GetComponent<TextMeshProUGUI>().text = totalColeccionables.Count.ToString();

            if (animDesbloquear)
            {
                haciendoAnim = true;
                Invoke("HacerAnim", 1);
                animDesbloquear = false;

                playerGO = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
                PlayerInput plInput = playerGO.GetComponent<PlayerInput>();
                plInput.inputHorizBlock = true;
                plInput.inputVerticBlock = true;

                ControllerPersonaje per = playerGO.GetComponent<ControllerPersonaje>();
                per.dashBloqueado = true;
                per.saltoBloqueado = true;
                per.dashCaidaBloqueado = true;
                per.movimientoBloqueado = true;
            }

        }
    }

    void HacerAnim()
    {
        playerGO.GetComponentInChildren<Animator>().SetTrigger("Habilidad");
        if (GameObject.Find("Maquina_Coleccionable Prefab") != null)
        {
            GameObject.Find("Maquina_Coleccionable Prefab").GetComponent<Animator>().SetTrigger("Coleccionable");
        }

        FindObjectOfType<Pantalla>().ChangeScreen(habilidad);
        if (habilidad == DesbloquearHabilidades.habilidad.movimientoCable)
        {
            ManagerLogros.Instance.DesbloquearLogro(17);
        }
        Invoke("DevolverInput", 7.45f);
        
        
    }
    public void DevolverInput()
    {
        FindObjectOfType<VideosTutorial>().AbrirTutorial(habilidad);

        if (playerGO.gameObject == null) playerGO = GameObject.FindObjectOfType<CharacterController>().gameObject;
        PlayerInput plInput = playerGO.GetComponent<PlayerInput>();
        ControllerPersonaje per = playerGO.GetComponent<ControllerPersonaje>();
        haciendoAnim = false;
        plInput.inputHorizBlock = false;
        plInput.inputVerticBlock = false;
        per.dashBloqueado = false;
        per.saltoBloqueado = false;
        per.dashCaidaBloqueado = false;
        per.movimientoBloqueado = false;        
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

            SistemaGuardado.Incializar();
            if (PlayerPrefs.HasKey("effects")) { SfxVolume = PlayerPrefs.GetFloat("effects"); }
            if (PlayerPrefs.HasKey("music")) { MusicVolume = PlayerPrefs.GetFloat("music"); }

            CargarVolumenGuardado();
            DontDestroyOnLoad(gameObject);
        }
      


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
            print("muerto");
            personajevivo = false;
            if (GameObject.Find("Player") != null) GameObject.Find("Player").GetComponent<ControllerPersonaje>().movimientoBloqueado = true;
            if (GameObject.Find("Player") != null) GameObject.Find("Player").GetComponent<ControllerPersonaje>().rb.velocity = Vector2.zero;
            //Frenar();
            //Time.timeScale = 0.3f;
            //GameObject.Find("Player").GetComponent<Animator>().SetTrigger("Die");
            if (GameObject.Find("Player") != null) GameObject.Find("Player").GetComponent<ControllerPersonaje>().enabled = false;
            //GameObject.Find("Player").GetComponent<SpriteRenderer>().color = Color.black;
            StartCoroutine(Tiemporeiniciar(0.6f));
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


    //-------------------------------------------------------------------------------------------------------------------------------
    //                      Q U I T A R   T E C L A S   D E S A R R O L L A D O R   P A R A   F I N A L
    //-------------------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        if (Input.GetKey(KeyCode.R )&& Input.GetKey(KeyCode.LeftControl))
        {
            ReiniciarEscena();
        }
        if (Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftControl))
        {
            habilidades.dash = true;
            habilidades.chispazo = true;
            habilidades.movCables = true;
            habilidades.movParedes = true;

            ControllerPersonaje p = FindObjectOfType<ControllerPersonaje>();
            if (p!=null)
            { p.CargarHabilidadesGM(); }
        }
        if (Input.GetKeyDown(KeyCode.U) && Input.GetKey(KeyCode.LeftControl))
        {
            int count = totalColeccionables.Count;
            for (int i = count; i < 1000; i++)
            {
                totalColeccionables.Add("fake"+i);
                GameObject.Find("TextoColecc").GetComponent<TextMeshProUGUI>().text = totalColeccionables.Count.ToString() + "  /  " + maxColeccionablesTotal;
            }
        }
    }


    public void CogerColeccionableNivel(GameObject coleccionable)
    {
        if (!nivelActual.actualColeccionablesCogidos.Contains(coleccionable.name))
        {
            nivelActual.actualColeccionablesCogidos.Add(coleccionable.name);
            textoColecc.text = nivelActual.actualColeccionablesCogidos.Count + "  /  " + nivelActual.maxColeccionables;

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
                    break;
                }
            }
        }


        if (!nivelEnLista)//Añadir el nivel a la lista
        {
            LevelInfo nuevoNivel = new LevelInfo();
            nuevoNivel.nombreNivel = nombreEscena;
            listaNiveles.Add(nuevoNivel);
            nivelActual = nuevoNivel;
        }

    }

}

[System.Serializable]
public class LevelInfo
{
    public float tiempoCorriendoTotal = 0;
    public string nombreNivel;
    public bool completado = false;
    public bool fantasmaGanado = false;
    public List<string> coleccionablesCogidos = new List<string>();
    public List<string> actualColeccionablesCogidos = new List<string>();
    public bool todosColeccionablesCogidos = false;
    public bool pasadoSinMorir = false;

    public int maxColeccionables;

    public float tiempoEmpezar;
    public float mejorTiempo = 0;

    //GHOST DATA
    public List<Posiciones> pos = new List<Posiciones>();
}

[System.Serializable]
public class ListaHabilidades
{
    public bool dash = false;
    public bool chispazo = false;
    public bool movParedes = false;
    public bool movCables = false;
}

