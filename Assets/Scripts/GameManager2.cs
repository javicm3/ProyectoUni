using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
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

    private static GameManager2 _instance;

    public static GameManager2 Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager2>();
            }

            return _instance;
        }
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

    // Start is called before the first frame update
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
        if (scene.name == "Trailer")
        {
            tempMoneda1 = false;
            tempMoneda2 = false;
            tempMoneda3 = false;
            moneda1 = GameObject.Find("Moneda1"); if (pickMoneda1 == true) Destroy(moneda1);
            moneda2 = GameObject.Find("Moneda2"); if (pickMoneda2 == true) Destroy(moneda2);
            moneda3 = GameObject.Find("Moneda3"); if (pickMoneda3 == true) Destroy(moneda3);

            GameObject[] Respawns = GameObject.FindGameObjectsWithTag("respawn");
            float numactivos = 0;
            foreach (GameObject go in Respawns)
            {

                if (go.transform.position == currentRespawn)
                {
                    go.GetComponent<Respawn>().activado = true;
                    numactivos += 1;
                }
            }
            if (numactivos < 1)
            {
                if (GameObject.Find("StartPosition").gameObject != null) { GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("StartPosition").transform.position; }
            }
            else
            {
                GameObject.Find("firstrespawn").GetComponent<Respawn>().activado = false;
                GameObject.FindGameObjectWithTag("Player").transform.position = futureSpawn;
            }

            print(GameObject.FindGameObjectsWithTag("Habilidad").Length);
            //coleccionablesneed = monedasNecesariasDisparo;
            coleccionablesneed = lastcoleccionablesneed;
            if (coleccionablesneed == 0)
            {
                puedoDisparar = true;
            }
            else
            {
                puedoDisparar = false;
            }
        }
        if (scene.name == "NivelVictor")
        {
            tempMoneda1 = false;
            tempMoneda2 = false;
            tempMoneda3 = false;
            moneda1 = GameObject.Find("Moneda1"); if (pickMoneda1 == true) Destroy(moneda1);
            moneda2 = GameObject.Find("Moneda2"); if (pickMoneda2 == true) Destroy(moneda2);
            moneda3 = GameObject.Find("Moneda3"); if (pickMoneda3 == true) Destroy(moneda3);

            GameObject[] Respawns = GameObject.FindGameObjectsWithTag("respawn");
            float numactivos = 0;
            foreach (GameObject go in Respawns)
            {

                if (go.transform.position == currentRespawn)
                {
                    go.GetComponent<Respawn>().activado = true;
                    numactivos += 1;
                }
            }
            if (numactivos < 1)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("StartPosition").transform.position;
            }
            else
            {
                GameObject.Find("firstrespawn").GetComponent<Respawn>().activado = false;
                GameObject.FindGameObjectWithTag("Player").transform.position = futureSpawn;
            }

            print(GameObject.FindGameObjectsWithTag("Habilidad").Length);
            //coleccionablesneed = monedasNecesariasDisparo;
            coleccionablesneed = lastcoleccionablesneed;
            if (coleccionablesneed == 0)
            {
                puedoDisparar = true;
            }
            else
            {
                puedoDisparar = false;
            }
        }
        if (scene.name == "NivelJavi")
        {
            tempMoneda1 = false;
            tempMoneda2 = false;
            tempMoneda3 = false;
            moneda1 = GameObject.Find("Moneda1"); if (pickMoneda1 == true) Destroy(moneda1);
            moneda2 = GameObject.Find("Moneda2"); if (pickMoneda2 == true) Destroy(moneda2);
            moneda3 = GameObject.Find("Moneda3"); if (pickMoneda3 == true) Destroy(moneda3);

            GameObject[] Respawns = GameObject.FindGameObjectsWithTag("respawn");
            float numactivos = 0;
            foreach (GameObject go in Respawns)
            {

                if (go.transform.position == currentRespawn)
                {
                    go.GetComponent<Respawn>().activado = true;
                    numactivos += 1;
                }
            }
            if (numactivos < 1)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("StartPosition").transform.position;
            }
            else
            {
                GameObject.Find("firstrespawn").GetComponent<Respawn>().activado = false;
                GameObject.FindGameObjectWithTag("Player").transform.position = futureSpawn;
            }

            print(GameObject.FindGameObjectsWithTag("Habilidad").Length);
            //coleccionablesneed = monedasNecesariasDisparo;
            coleccionablesneed = lastcoleccionablesneed;
            if (coleccionablesneed == 0)
            {
                puedoDisparar = true;
            }
            else
            {
                puedoDisparar = false;
            }
        }
        if (scene.name == "CIUDAD")
        {
            tempMoneda1 = false;
            tempMoneda2 = false;
            tempMoneda3 = false;
            moneda1 = GameObject.Find("Moneda1"); if (pickMoneda1 == true) Destroy(moneda1);
            moneda2 = GameObject.Find("Moneda2"); if (pickMoneda2 == true) Destroy(moneda2);
            moneda3 = GameObject.Find("Moneda3"); if (pickMoneda3 == true) Destroy(moneda3);

            GameObject[] Respawns = GameObject.FindGameObjectsWithTag("respawn");
            float numactivos = 0;
            foreach (GameObject go in Respawns)
            {

                if (go.transform.position == currentRespawn)
                {
                    go.GetComponent<Respawn>().activado = true;
                    numactivos += 1;
                }
            }
            if (numactivos < 1)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("StartPosition").transform.position;
            }
            else
            {
                GameObject.Find("firstrespawn").GetComponent<Respawn>().activado = false;
                GameObject.FindGameObjectWithTag("Player").transform.position = futureSpawn;
            }

            print(GameObject.FindGameObjectsWithTag("Habilidad").Length);
            //coleccionablesneed = monedasNecesariasDisparo;
            coleccionablesneed = lastcoleccionablesneed;
            if (coleccionablesneed == 0)
            {
                puedoDisparar = true;
            }
            else
            {
                puedoDisparar = false;
            }
        }
        if (scene.name == "CIUDAD2")
        {

            tempMoneda1 = false;
            tempMoneda2 = false;
            tempMoneda3 = false;
            moneda1 = GameObject.Find("Moneda1"); if (pickMoneda1 == true) Destroy(moneda1);
            moneda2 = GameObject.Find("Moneda2"); if (pickMoneda2 == true) Destroy(moneda2);
            moneda3 = GameObject.Find("Moneda3"); if (pickMoneda3 == true) Destroy(moneda3);

            GameObject[] Respawns = GameObject.FindGameObjectsWithTag("respawn");
            float numactivos = 0;
            foreach (GameObject go in Respawns)
            {

                if (go.transform.position == currentRespawn)
                {
                    go.GetComponent<Respawn>().activado = true;
                    numactivos += 1;
                }
            }
            if (numactivos < 1)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("StartPosition").transform.position;
            }
            else
            {
                GameObject.Find("firstrespawn").GetComponent<Respawn>().activado = false;
                GameObject.FindGameObjectWithTag("Player").transform.position = futureSpawn;
            }

            print(GameObject.FindGameObjectsWithTag("Habilidad").Length);
            //coleccionablesneed = monedasNecesariasDisparo;
            coleccionablesneed = lastcoleccionablesneed;
            if (coleccionablesneed == 0)
            {
                puedoDisparar = true;
            }
            else
            {
                puedoDisparar = false;
            }
            //StartCoroutine("Ciudad", 2f);
        }
        if (scene.name == "CIUDAD3")
        {
            tempMoneda1 = false;
            tempMoneda2 = false;
            tempMoneda3 = false;
            moneda1 = GameObject.Find("Moneda1"); if (pickMoneda1 == true) Destroy(moneda1);
            moneda2 = GameObject.Find("Moneda2"); if (pickMoneda2 == true) Destroy(moneda2);
            moneda3 = GameObject.Find("Moneda3"); if (pickMoneda3 == true) Destroy(moneda3);

            GameObject[] Respawns = GameObject.FindGameObjectsWithTag("respawn");
            float numactivos = 0;
            foreach (GameObject go in Respawns)
            {

                if (go.transform.position == currentRespawn)
                {
                    go.GetComponent<Respawn>().activado = true;
                    numactivos += 1;
                }
            }
            if (numactivos < 1)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("StartPosition").transform.position;
            }
            else
            {
                GameObject.Find("firstrespawn").GetComponent<Respawn>().activado = false;
                GameObject.FindGameObjectWithTag("Player").transform.position = futureSpawn;
            }

            print(GameObject.FindGameObjectsWithTag("Habilidad").Length);
            //coleccionablesneed = monedasNecesariasDisparo;
            coleccionablesneed = lastcoleccionablesneed;
            if (coleccionablesneed == 0)
            {
                puedoDisparar = true;
            }
            else
            {
                puedoDisparar = false;
            }
        }
        Debug.Log(scene.name);
        Debug.Log(mode);
    }
    public IEnumerator Ciudad(float time)
    {
        yield return new WaitForSeconds(time);

    }
    private void Start()
    {
        //if (SceneManager.GetActiveScene().buildIndex == 0)
        //{
        //    mainPanel = GameObject.FindGameObjectWithTag("MainPanel");
        //    DesactPanel();
        //}
    }
    public void DesactPanel()
    {
        if (mainPanel != null) mainPanel.SetActive(false);

    }
    public void CogerColeccionable()
    {
        coleccionablesneed -= 1;
        if (coleccionablesneed <= 0)
        {
            puedoDisparar = true;
        }
        else
        {
            puedoDisparar = false;
        }
    }
    public void CambiarMonedas()
    {
        if (actualMonedas == 3)
        {
            foreach (GameObject a in monedasImage)
            {
                a.GetComponent<Image>().color = Color.white;
            }
        }
        else if (actualMonedas == 2)
        {
            monedasImage[0].GetComponent<Image>().color = Color.white;
            monedasImage[1].GetComponent<Image>().color = Color.white;
        }
        if (actualMonedas == 1)
        {
            monedasImage[0].GetComponent<Image>().color = Color.white;
        }
        if (actualMonedas == 0)
        {
            foreach (GameObject a in monedasImage)
            {
                a.GetComponent<Image>().color = Color.black;
            }
        }
    }
    public void CogerMonedas()
    {
        actualMonedas += 1;
        CambiarMonedas();
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (SceneManager.GetActiveScene().buildIndex != 0)
            //{
            //    SceneManager.LoadScene(0);
            //}
            //else
            //{
                Application.Quit();
            //}





        }
        if (GameObject.FindGameObjectWithTag("Mapa") == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReiniciarEscena();


            }
        }




    }
    // Update is called once per frame
    public void LoadMapa()
    {
        SceneManager.LoadScene(0);
    }
    public void EmpezarNivel()
    {
        SceneManager.LoadScene(nivelaux);
    }
    public void ActivarPanel(float nivel)
    {

        mainPanel.SetActive(true);
        nivelaux = (int)nivel;
        SetEstrellas(nivel);
    }
    public void DesactivarPanel()
    {
        mainPanel.SetActive(false);
    }
    public void SetEstrellas(float nivel)
    {
        estrella1 = GameObject.Find("PrimeraEstrella").GetComponent<Image>();
        estrella2 = GameObject.Find("SegundaEstrella").GetComponent<Image>();
        estrella3 = GameObject.Find("TerceraEstrella").GetComponent<Image>();
        float estrellasencendidas = 0;
        if (nivel == 1)
        {
            estrellasencendidas = maxScore1;
            GameObject.Find("TiempoNivel").GetComponent<Text>().text = tiemponivel1.ToString("0") + " s";
        }
        if (nivel == 2)
        {
            estrellasencendidas = maxScore2;
            GameObject.Find("TiempoNivel").GetComponent<Text>().text = tiemponivel2.ToString("0") + " s";
        }
        if (nivel == 3)
        {
            estrellasencendidas = maxScore3;
            GameObject.Find("TiempoNivel").GetComponent<Text>().text = tiemponivel3.ToString("0") + " s";

        }
        if (nivel == 4)
        {
            estrellasencendidas = maxScore4;
            GameObject.Find("TiempoNivel").GetComponent<Text>().text = tiemponivel4.ToString("0") + " s";
        }
        if (nivel == 5)
        {
            estrellasencendidas = maxScore5;
            GameObject.Find("TiempoNivel").GetComponent<Text>().text = tiemponivel5.ToString("0") + " s";
        }
        if (estrellasencendidas == 0)
        {
            print("hey");
            estrella1.color = colorestrellaapagada;
            estrella2.color = colorestrellaapagada;
            estrella3.color = colorestrellaapagada;
        }
        if (estrellasencendidas == 1)
        {
            estrella1.color = Color.white;
            estrella2.color = colorestrellaapagada;
            estrella3.color = colorestrellaapagada;
        }
        if (estrellasencendidas == 2)
        {
            estrella1.color = Color.white;
            estrella2.color = Color.white;
            estrella3.color = colorestrellaapagada;
        }
        if (estrellasencendidas == 3)
        {
            print("heey");
            estrella1.color = Color.white;
            estrella2.color = Color.white;
            estrella3.color = Color.white;
        }
    }
    public void SetMaxScore(float nivel, float score)
    {
        if (nivel == 1)
        {
            maxScore1 = score;
        }
        if (nivel == 2)
        {
            maxScore2 = score;
        }
        if (nivel == 3)
        {
            maxScore3 = score;
        }
        if (nivel == 4)
        {
            maxScore4 = score;
        }
        if (nivel == 5)
        {
            maxScore5 = score;
        }
    }
    public void SumarNivel()
    {
        nivelescompletados += 1;
    }
    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void NextScene(string nextlvl)
    {
        SceneManager.LoadScene(nextlvl, LoadSceneMode.Single);
        //if (SceneManager.GetActiveScene().buildIndex == 1)
        //{
        //    if (maxScore1 < actualMonedas)
        //    {
        //        SetMaxScore(1, actualMonedas);
        //    }
        //    if (nivelescompletados == 0)
        //    {
        //        SumarNivel();
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 2)
        //{
        //    if (maxScore2 < actualMonedas)
        //    {
        //        SetMaxScore(2, actualMonedas);
        //    }
        //    if (nivelescompletados == 1)
        //    {
        //        SumarNivel();
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 3)
        //{
        //    if (maxScore3 < actualMonedas)
        //    {
        //        SetMaxScore(3, actualMonedas);
        //    }
        //    if (nivelescompletados == 2)
        //    {
        //        SumarNivel();
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 4)
        //{
        //    if (maxScore4 < actualMonedas)
        //    {
        //        SetMaxScore(4, actualMonedas);
        //    }
        //    if (nivelescompletados == 3)
        //    {
        //        SumarNivel();
        //    }
        //}
        //if (SceneManager.GetActiveScene().buildIndex == 5)
        //{
        //    if (maxScore5 < actualMonedas)
        //    {
        //        SetMaxScore(5, actualMonedas);
        //    }
        //    if (nivelescompletados == 4)
        //    {
        //        SumarNivel();
        //    }
        //}

    }
    public void MuertePJ()
    {
        if (personajevivo == true)
        {
            print("muerto");
            personajevivo = false;
            Frenar();
            //Time.timeScale = 0.3f;
            //GameObject.Find("Player").GetComponent<Animator>().SetTrigger("Die");
            if (GameObject.Find("Player") != null) GameObject.Find("Player").GetComponent<CharacterController2D>().enabled = false;
            //GameObject.Find("Player").GetComponent<SpriteRenderer>().color = Color.black;
            StartCoroutine(Tiemporeiniciar(0.6f));
        }

    }
    public void Frenar()
    {
        if (GameObject.Find("Player") != null)
        {
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = new Vector3(0, -2);

            GameObject.Find("Player").GetComponent<Movimiento>().enabled = false;
            //GameObject.Find("Player").GetComponent<CharacterController2D>().anim.SetTrigger("Die");
            GameObject.Find("Player").GetComponent<CharacterController2D>().enabled = false;
        }

    }

    public IEnumerator Tiemporeiniciar(float tiempo)
    {
        SpriteRenderer[] renderers;
        yield return new WaitForSeconds(tiempo);
        if (GameObject.Find("Player") != null)
        {
            renderers = GameObject.Find("Player").GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sp in renderers)
            {
                sp.enabled = false;
            }
        }

        print(GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>().enabled);
        GameObject.FindObjectOfType<Particulas>().SpawnParticulas(GameObject.FindObjectOfType<Particulas>().particulasExplosion, GameObject.Find("Player").transform.position);
        //if (GameObject.Find("Player") != null) Destroy(GameObject.Find("Player").GetComponentInChildren<Animator>());
        StartCoroutine(TrueReinicio(1.3f));
    }
    public IEnumerator TrueReinicio(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);

        Time.timeScale = 1f;
        ReiniciarEscena();
    }



}
