using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("COSAS ANTERIORES IGNORAR")]
    public float tiemponivel1;
    public float tiemponivel2;
    public float tiemponivel3;
    public float tiemponivel4;
    public float tiemponivel5;

    bool sepuede = true;
    public float actualMonedas = 0;
    public GameObject[] monedasImage;
    [Header("CosasGuardadasNivelPrueba")]
    public Vector3 currentRespawn;

    public GameObject moneda1;
    public GameObject moneda2;
    public GameObject moneda3;
    public float lastcoleccionablesneed = 3;
    public bool tempMoneda1 = false;
    public bool tempMoneda2 = false;
    public bool tempMoneda3 = false;
    public bool pickMoneda1 = false;
    public bool pickMoneda2 = false;
    public bool pickMoneda3 = false;
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
    public float actualLevel = 0;
    public float[] coleccionablesMaxNv;
    public float[] coleccionablesActNv;
    public float[] estrellasMaxNv;
    public float[] estrellasActNv;
    public List<string> coleccionablesCogidosNivel0;
    public List<string> actualColeccionablesCogidosNivel0;
    public List<string> estrellasCogidosNivel0;
    public List<string> actualEstrellasCogidosNivel0;
    public List<string> coleccionablesCogidosNivel1;
    public List<string> actualColeccionablesCogidosNivel1;
    public List<string> estrellasCogidosNivel1;
    public List<string> actualEstrellasCogidosNivel1;
    public List<string> coleccionablesCogidosNivel2;
    public List<string> actualColeccionablesCogidosNivel2;
    public List<string> estrellasCogidosNivel2;
    public List<string> actualEstrellasCogidosNivel2;
    public List<string> totalColeccionables;
    public List<string> totalEstrellas;
    public GameObject PanelColeccionables;
    public Text textoActualColecc;
    public Text textoMaxColecc;
    public Text textoActualEstrellas;
    public Text textoMaxEstrellas;


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
        Debug.Log("Level Loaded");
        if (scene.name == "PlayTesting")
        {
            actualLevel = 0;
            personajevivo = true;
            coleccionablesActNv[(int)actualLevel] = 0;
            coleccionablesCogidosNivel0.Clear();
            estrellasActNv[(int)actualLevel] = 0;
            estrellasCogidosNivel0.Clear();

            textoActualColecc = GameObject.Find("Actual").GetComponent<Text>();
            textoMaxColecc = GameObject.Find("Maximo").GetComponent<Text>();
            textoActualColecc.text = coleccionablesActNv[(int)actualLevel].ToString();
            textoMaxColecc.text = coleccionablesMaxNv[(int)actualLevel].ToString();


            textoActualEstrellas = GameObject.Find("ActualEstrella").GetComponent<Text>();
            textoMaxEstrellas = GameObject.Find("MaximoEstrella").GetComponent<Text>();
            textoActualEstrellas.text = estrellasActNv[(int)actualLevel].ToString();
            textoMaxEstrellas.text = estrellasMaxNv[(int)actualLevel].ToString();

        }
        if (scene.name == "UltimoPlayTestin")
        {
            actualLevel = 0;
            personajevivo = true;
            coleccionablesActNv[(int)actualLevel] = 0;
            coleccionablesCogidosNivel0.Clear();
            estrellasActNv[(int)actualLevel] = 0;
            estrellasCogidosNivel0.Clear();

            textoActualColecc = GameObject.Find("Actual").GetComponent<Text>();
            textoMaxColecc = GameObject.Find("Maximo").GetComponent<Text>();
            textoActualColecc.text = coleccionablesActNv[(int)actualLevel].ToString();
            textoMaxColecc.text = coleccionablesMaxNv[(int)actualLevel].ToString();


            textoActualEstrellas = GameObject.Find("ActualEstrella").GetComponent<Text>();
            textoMaxEstrellas = GameObject.Find("MaximoEstrella").GetComponent<Text>();
            textoActualEstrellas.text = estrellasActNv[(int)actualLevel].ToString();
            textoMaxEstrellas.text = estrellasMaxNv[(int)actualLevel].ToString();

        }
        if (scene.name == "UltimoPlaytestin")
        {
            actualLevel = 0;
            personajevivo = true;
            coleccionablesActNv[(int)actualLevel] = 0;
            coleccionablesCogidosNivel0.Clear();
            estrellasActNv[(int)actualLevel] = 0;
            estrellasCogidosNivel0.Clear();

            textoActualColecc = GameObject.Find("Actual").GetComponent<Text>();
            textoMaxColecc = GameObject.Find("Maximo").GetComponent<Text>();
            textoActualColecc.text = coleccionablesActNv[(int)actualLevel].ToString();
            textoMaxColecc.text = coleccionablesMaxNv[(int)actualLevel].ToString();


            textoActualEstrellas = GameObject.Find("ActualEstrella").GetComponent<Text>();
            textoMaxEstrellas = GameObject.Find("MaximoEstrella").GetComponent<Text>();
            textoActualEstrellas.text = estrellasActNv[(int)actualLevel].ToString();
            textoMaxEstrellas.text = estrellasMaxNv[(int)actualLevel].ToString();

        }
        if (scene.name == "Lobby")
        {
            actualLevel = -1;
            personajevivo = true;
            //coleccionablesActNv[(int)actualLevel] = 0;
            //coleccionablesCogidosNivel0.Clear();
            //estrellasActNv[(int)actualLevel] = 0;
            //estrellasCogidosNivel0.Clear();

            textoActualColecc = GameObject.Find("Actual").GetComponent<Text>();
            //textoMaxColecc = GameObject.Find("Maximo").GetComponent<Text>();
            textoActualColecc.text = totalColeccionables.Count.ToString();
            //textoMaxColecc.text = coleccionablesMaxNv[(int)actualLevel].ToString();


            textoActualEstrellas = GameObject.Find("ActualEstrella").GetComponent<Text>();
            //textoMaxEstrellas = GameObject.Find("MaximoEstrella").GetComponent<Text>();
            textoActualEstrellas.text =  totalEstrellas.Count.ToString();
            //textoMaxEstrellas.text = estrellasMaxNv[(int)actualLevel].ToString();

        }
        if (scene.name == "PlayGround")
        {
            actualLevel = 0;

            coleccionablesActNv[(int)actualLevel] = coleccionablesCogidosNivel0.Count;
            if (personajevivo == false)
            {
                actualColeccionablesCogidosNivel0.Clear();
                foreach (string go in coleccionablesCogidosNivel0)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            else
            {
                foreach (string go in coleccionablesCogidosNivel0)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            estrellasActNv[(int)actualLevel] = estrellasCogidosNivel0.Count;
            if (personajevivo == false)
            {
                actualEstrellasCogidosNivel0.Clear();
                foreach (string go in estrellasCogidosNivel0)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            else
            {
                foreach (string go in estrellasCogidosNivel0)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }

            textoActualColecc = GameObject.Find("Actual").GetComponent<Text>();
            textoMaxColecc = GameObject.Find("Maximo").GetComponent<Text>();
            textoActualColecc.text = coleccionablesActNv[(int)actualLevel].ToString();
            textoMaxColecc.text = coleccionablesMaxNv[(int)actualLevel].ToString();


            textoActualEstrellas = GameObject.Find("ActualEstrella").GetComponent<Text>();
            textoMaxEstrellas = GameObject.Find("MaximoEstrella").GetComponent<Text>();
            textoActualEstrellas.text = estrellasActNv[(int)actualLevel].ToString();
            textoMaxEstrellas.text = estrellasMaxNv[(int)actualLevel].ToString();
            personajevivo = true;
        }
        if (scene.name == "Nivel0")
        {
            actualLevel = 0;

            coleccionablesActNv[(int)actualLevel] = coleccionablesCogidosNivel0.Count;
            if (personajevivo == false)
            {
                actualColeccionablesCogidosNivel0.Clear();
                foreach (string go in coleccionablesCogidosNivel0)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            else
            {
                foreach (string go in coleccionablesCogidosNivel0)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            estrellasActNv[(int)actualLevel] = estrellasCogidosNivel0.Count;
            if (personajevivo == false)
            {
                actualEstrellasCogidosNivel0.Clear();
                foreach (string go in estrellasCogidosNivel0)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            else
            {
                foreach(string go in estrellasCogidosNivel0)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            
            textoActualColecc = GameObject.Find("Actual").GetComponent<Text>();
            textoMaxColecc = GameObject.Find("Maximo").GetComponent<Text>();
            textoActualColecc.text = coleccionablesActNv[(int)actualLevel].ToString();
            textoMaxColecc.text = coleccionablesMaxNv[(int)actualLevel].ToString();


            textoActualEstrellas = GameObject.Find("ActualEstrella").GetComponent<Text>();
            textoMaxEstrellas = GameObject.Find("MaximoEstrella").GetComponent<Text>();
            textoActualEstrellas.text = estrellasActNv[(int)actualLevel].ToString();
            textoMaxEstrellas.text = estrellasMaxNv[(int)actualLevel].ToString();
            personajevivo = true;
        }
        if (scene.name == "Nivel1")
        {
            actualLevel = 1;

            coleccionablesActNv[(int)actualLevel] = coleccionablesCogidosNivel1.Count;
            if (personajevivo == false)
            {
                actualColeccionablesCogidosNivel1.Clear();
                foreach (string go in coleccionablesCogidosNivel1)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            else
            {
                foreach (string go in coleccionablesCogidosNivel1)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            estrellasActNv[(int)actualLevel] = estrellasCogidosNivel1.Count;
            if (personajevivo == false)
            {
                actualEstrellasCogidosNivel1.Clear();
                foreach (string go in estrellasCogidosNivel0)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            else
            {
                foreach (string go in estrellasCogidosNivel1)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }

            textoActualColecc = GameObject.Find("Actual").GetComponent<Text>();
            textoMaxColecc = GameObject.Find("Maximo").GetComponent<Text>();
            textoActualColecc.text = coleccionablesActNv[(int)actualLevel].ToString();
            textoMaxColecc.text = coleccionablesMaxNv[(int)actualLevel].ToString();


            textoActualEstrellas = GameObject.Find("ActualEstrella").GetComponent<Text>();
            textoMaxEstrellas = GameObject.Find("MaximoEstrella").GetComponent<Text>();
            textoActualEstrellas.text = estrellasActNv[(int)actualLevel].ToString();
            textoMaxEstrellas.text = estrellasMaxNv[(int)actualLevel].ToString();
            personajevivo = true;
        }
        if (scene.name == "NIVEL 1")
        {
            actualLevel = 1;

            coleccionablesActNv[(int)actualLevel] = coleccionablesCogidosNivel1.Count;
            if (personajevivo == false)
            {
                actualColeccionablesCogidosNivel1.Clear();
                foreach (string go in coleccionablesCogidosNivel1)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            else
            {
                foreach (string go in coleccionablesCogidosNivel1)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            estrellasActNv[(int)actualLevel] = estrellasCogidosNivel1.Count;
            if (personajevivo == false)
            {
                actualEstrellasCogidosNivel1.Clear();
                foreach (string go in estrellasCogidosNivel0)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            else
            {
                foreach (string go in estrellasCogidosNivel1)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }

            textoActualColecc = GameObject.Find("Actual").GetComponent<Text>();
            textoMaxColecc = GameObject.Find("Maximo").GetComponent<Text>();
            textoActualColecc.text = coleccionablesActNv[(int)actualLevel].ToString();
            textoMaxColecc.text = coleccionablesMaxNv[(int)actualLevel].ToString();


            textoActualEstrellas = GameObject.Find("ActualEstrella").GetComponent<Text>();
            textoMaxEstrellas = GameObject.Find("MaximoEstrella").GetComponent<Text>();
            textoActualEstrellas.text = estrellasActNv[(int)actualLevel].ToString();
            textoMaxEstrellas.text = estrellasMaxNv[(int)actualLevel].ToString();
            personajevivo = true;
        }
        if (scene.name == "Nivel2")
        {
            actualLevel = 2;

            coleccionablesActNv[(int)actualLevel] = coleccionablesCogidosNivel2.Count;
            if (personajevivo == false)
            {
                actualColeccionablesCogidosNivel2.Clear();
                foreach (string go in coleccionablesCogidosNivel2)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            else
            {
                foreach (string go in coleccionablesCogidosNivel2)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            estrellasActNv[(int)actualLevel] = estrellasCogidosNivel2.Count;
            if (personajevivo == false)
            {
                actualEstrellasCogidosNivel2.Clear();
                foreach (string go in estrellasCogidosNivel2)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }
            else
            {
                foreach (string go in estrellasCogidosNivel2)
                {
                    Destroy(GameObject.Find(go.ToString()));
                }
            }

            textoActualColecc = GameObject.Find("Actual").GetComponent<Text>();
            textoMaxColecc = GameObject.Find("Maximo").GetComponent<Text>();
            textoActualColecc.text = coleccionablesActNv[(int)actualLevel].ToString();
            textoMaxColecc.text = coleccionablesMaxNv[(int)actualLevel].ToString();


            textoActualEstrellas = GameObject.Find("ActualEstrella").GetComponent<Text>();
            textoMaxEstrellas = GameObject.Find("MaximoEstrella").GetComponent<Text>();
            textoActualEstrellas.text = estrellasActNv[(int)actualLevel].ToString();
            textoMaxEstrellas.text = estrellasMaxNv[(int)actualLevel].ToString();
            personajevivo = true;
        }
        Debug.Log(scene.name);
        Debug.Log(mode);
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
    }
    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void NextScene(string nextlvl)
    {
        SceneManager.LoadScene(nextlvl, LoadSceneMode.Single);
    }

    public void MuertePJ()
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
            GameObject.Find("Player").GetComponent<Particulas>().SpawnParticulas(GameObject.FindObjectOfType<Particulas>().particulasExplosion, GameObject.Find("Player").transform.position);
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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
    }
    public void CogerEstrellaNivel(GameObject estrellaCogida)
    {

      
        if (actualLevel == 0)
        {
            if (!actualEstrellasCogidosNivel0.Contains(estrellaCogida.name))
            {

                actualEstrellasCogidosNivel0.Add(estrellaCogida.name);
                estrellasActNv[(int)actualLevel] += 1;
                ActualizarActualEstrellas(actualLevel);
                estrellaCogida.GetComponent<SpriteRenderer>().enabled = false;
                estrellaCogida.GetComponent<Collider2D>().enabled = false;
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().estrella);
                }
            }

        }
        if (actualLevel == 1)
        {
            if (!actualEstrellasCogidosNivel1.Contains(estrellaCogida.name))
            {

                actualEstrellasCogidosNivel1.Add(estrellaCogida.name);
                estrellasActNv[(int)actualLevel] += 1;
                ActualizarActualEstrellas(actualLevel);
                estrellaCogida.GetComponent<SpriteRenderer>().enabled = false;
                estrellaCogida.GetComponent<Collider2D>().enabled = false;
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().estrella);
                }
            }

        }
        if (actualLevel == 2)
        {
            if (!actualEstrellasCogidosNivel2.Contains(estrellaCogida.name))
            {

                actualEstrellasCogidosNivel2.Add(estrellaCogida.name);
                estrellasActNv[(int)actualLevel] += 1;
                ActualizarActualEstrellas(actualLevel);
                estrellaCogida.GetComponent<SpriteRenderer>().enabled = false;
                estrellaCogida.GetComponent<Collider2D>().enabled = false;
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().estrella);
                }
            }

        }

    }
    public void ActualizarActualEstrellas(float lvl)
    {



        textoActualEstrellas.text = estrellasActNv[(int)actualLevel].ToString();


    }
    public void CogerColeccionableNivel(GameObject coleccionableCogido)
    {


        if (actualLevel == 0)
        {
            if (!actualColeccionablesCogidosNivel0.Contains(coleccionableCogido.name))
            {

                actualColeccionablesCogidosNivel0.Add(coleccionableCogido.name);
                coleccionablesActNv[(int)actualLevel] += 1;
                ActualizarActualColeccionables(actualLevel);
                coleccionableCogido.GetComponent<SpriteRenderer>().enabled = false;
                coleccionableCogido.GetComponent<Collider2D>().enabled = false;
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().coleccionable);
                }
            }

        }
        if (actualLevel == 1)
        {
            if (!actualColeccionablesCogidosNivel1.Contains(coleccionableCogido.name))
            {

                actualColeccionablesCogidosNivel1.Add(coleccionableCogido.name);
                coleccionablesActNv[(int)actualLevel] += 1;
                ActualizarActualColeccionables(actualLevel);
                coleccionableCogido.GetComponent<SpriteRenderer>().enabled = false;
                coleccionableCogido.GetComponent<Collider2D>().enabled = false;
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().coleccionable);
                }
            }

        }
        if (actualLevel == 2)
        {
            if (!actualColeccionablesCogidosNivel2.Contains(coleccionableCogido.name))
            {

                actualColeccionablesCogidosNivel2.Add(coleccionableCogido.name);
                coleccionablesActNv[(int)actualLevel] += 1;
                ActualizarActualColeccionables(actualLevel);
                coleccionableCogido.GetComponent<SpriteRenderer>().enabled = false;
                coleccionableCogido.GetComponent<Collider2D>().enabled = false;
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().coleccionable);
                  
                }
            }

        }

    }
    public void ActualizarActualColeccionables(float lvl)
    {



        textoActualColecc.text = coleccionablesActNv[(int)actualLevel].ToString();


    }
}
