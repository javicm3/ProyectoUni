using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Posiciones
{
    public Vector3 posicion;
    public Vector3 velocidad;
    public float tiempo;
    public Posiciones(Vector3 pos, Vector3 velocity, float time)
    {
        tiempo = time;
        posicion = pos;
        velocidad = velocity;
    }

}
public class GhostData : MonoBehaviour
{
    private static GhostData _instance;

    public static GhostData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GhostData>();
            }

            return _instance;
        }
    }
    public enum States
    {
        Grabar,
        Reproducir
    }

    public States estado;
    public GameObject fantasmaPrefab;
    GameObject fantasma;
    public int actualLevel;
    public int actualpos = 0;
    public int actualposRepro = 0;

    public bool nivelCompletado1 = false;
    public List<Posiciones> posN1 = new List<Posiciones>();
    public List<Posiciones> posN1Temp = new List<Posiciones>();
    public float actualTiempoNivel1;
    public float mejorTiempoNivel1 = 999999999999999999;

    public bool nivelCompletado2 = false;
    public List<Posiciones> posN2 = new List<Posiciones>();
    public List<Posiciones> posN2Temp = new List<Posiciones>();
    public float actualTiempoNivel2;
    public float mejorTiempoNivel2 = 999999999999999999;
    public bool nivelCompletado3 = false;
    public List<Posiciones> posN3 = new List<Posiciones>();
    public List<Posiciones> posN3Temp = new List<Posiciones>();
    public float actualTiempoNivel3;
    public float mejorTiempoNivel3 = 999999999999999999;

    public List<Posiciones> posN4;
    public List<Posiciones> posN5;
    public List<Posiciones> posN6;
    public List<Posiciones> posN7;
    public List<Posiciones> posN8;
    public List<Posiciones> posN9;
    public List<Posiciones> posN10;
    public List<Posiciones> posN11;
    float tiempoEntreGrabaciones = 0.02f;
    float tiempoFuturaGrabacion = 0;
    public GameObject player;
    public Rigidbody2D rb;
    public float tiempoNivel;
    // Start is called before the first frame update
    void Start()
    {

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
        tiempoNivel = 0;
        actualLevel = 0;
        actualTiempoNivel1 = 0;
        actualTiempoNivel2 = 0;
        actualTiempoNivel3 = 0;
        actualpos = 0;
        actualposRepro = 0;
        tiempoFuturaGrabacion = 0;
        if (GameObject.FindObjectOfType<ControllerPersonaje>() != null)
        {
            player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
            rb = player.GetComponent<Rigidbody2D>();
        }
        if (scene.name == "Nivel_1")
        {
            actualLevel = 1;
            if (nivelCompletado1 == false)
            {
                posN1Temp.Clear();
            }
            else
            {
                fantasma = GameObject.Instantiate(fantasmaPrefab, player.transform.position, Quaternion.identity);
            }

        }
        if (scene.name == "Nivel_2")
        {
            actualLevel = 2;
            if (nivelCompletado2 == false)
            {
                posN1Temp.Clear();
            }
            else
            {
                fantasma = GameObject.Instantiate(fantasmaPrefab, player.transform.position, Quaternion.identity);
            }

        }
        if (scene.name == "Nivel_3")
        {
            actualLevel = 3;
            if (nivelCompletado3 == false)
            {
                posN1Temp.Clear();
            }
            else
            {
                fantasma = GameObject.Instantiate(fantasmaPrefab, player.transform.position, Quaternion.identity);
            }

        }

    }
    public void TerminarNivel(string nivel)
    {
        if (nivel == "Nivel_1")
        {
            actualTiempoNivel1 = tiempoNivel;
            nivelCompletado1 = true;
            if (actualTiempoNivel1 < mejorTiempoNivel1)
            {
                posN1.Clear();
                for (int i = 0; i < posN1Temp.Count; i++)
                {
                    posN1.Add(posN1Temp[i]);


                }
                posN1Temp.Clear();
                mejorTiempoNivel1 = actualTiempoNivel1;
                print(mejorTiempoNivel1 + "besttime");
            }
            else
            {
                posN1Temp.Clear();
            }
        }
        if (nivel == "Nivel_2")
        {
            actualTiempoNivel2 = tiempoNivel;
            nivelCompletado2 = true;
            if (actualTiempoNivel2 < mejorTiempoNivel2)
            {
                posN2.Clear();
                for (int i = 0; i < posN2Temp.Count; i++)
                {
                    posN2.Add(posN2Temp[i]);


                }
                posN2Temp.Clear();
                mejorTiempoNivel2 = actualTiempoNivel2;
                print(mejorTiempoNivel2 + "besttime");
            }
            else
            {
                posN2Temp.Clear();
            }
        }
        if (nivel == "Nivel_3")
        {
            actualTiempoNivel3 = tiempoNivel;
            nivelCompletado3 = true;
            if (actualTiempoNivel3 < mejorTiempoNivel1)
            {
                posN3.Clear();
                for (int i = 0; i < posN3Temp.Count; i++)
                {
                    posN3.Add(posN3Temp[i]);


                }
                posN3Temp.Clear();
                mejorTiempoNivel3 = actualTiempoNivel3;
                print(mejorTiempoNivel3 + "besttime");
            }
            else
            {
                posN3Temp.Clear();
            }
        }
    }
    public void HacerGrabacion(int actualLevel)
    {

        if (actualLevel == 1)
        {
            posN1Temp.Add(new Posiciones(Vector3.zero, Vector3.zero, 0));
            posN1Temp[actualpos] = new Posiciones(player.transform.position, rb.velocity, tiempoNivel);

            actualpos++;

        }
        if (actualLevel == 2)
        {
            posN2Temp.Add(new Posiciones(Vector3.zero, Vector3.zero, 0));
            posN2Temp[actualpos] = new Posiciones(player.transform.position, rb.velocity, tiempoNivel);

            actualpos++;

        }
        if (actualLevel == 3)
        {
            posN3Temp.Add(new Posiciones(Vector3.zero, Vector3.zero, 0));
            posN3Temp[actualpos] = new Posiciones(player.transform.position, rb.velocity, tiempoNivel);

            actualpos++;

        }

    }
    // Update is called once per frame
    void Update()
    {
        if (actualLevel == 1)
        {
            tiempoNivel += Time.deltaTime;
            if (nivelCompletado1)
            {
                estado = States.Reproducir;
            }
            else
            {
                estado = States.Grabar;
            }
            if (estado == States.Grabar)
            {
                if (tiempoFuturaGrabacion == 0)
                {
                    tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                    HacerGrabacion(actualLevel);

                }
                else
                {
                    if (tiempoFuturaGrabacion < tiempoNivel)
                    {
                        tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                        HacerGrabacion(actualLevel);
                    }
                }

            }
            else
            {
                if (fantasma != null) MoverFantasma();

                if (tiempoFuturaGrabacion == 0)
                {
                    tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                    HacerGrabacion(actualLevel);

                }
                else
                {
                    if (tiempoFuturaGrabacion < tiempoNivel)
                    {
                        tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                        HacerGrabacion(actualLevel);
                    }
                }
            }


        }
        if (actualLevel == 2)
        {
            tiempoNivel += Time.deltaTime;
            if (nivelCompletado2)
            {
                estado = States.Reproducir;
            }
            else
            {
                estado = States.Grabar;
            }
            if (estado == States.Grabar)
            {
                if (tiempoFuturaGrabacion == 0)
                {
                    tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                    HacerGrabacion(actualLevel);

                }
                else
                {
                    if (tiempoFuturaGrabacion < tiempoNivel)
                    {
                        tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                        HacerGrabacion(actualLevel);
                    }
                }

            }
            else
            {
                if (fantasma != null) MoverFantasma();

                if (tiempoFuturaGrabacion == 0)
                {
                    tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                    HacerGrabacion(actualLevel);

                }
                else
                {
                    if (tiempoFuturaGrabacion < tiempoNivel)
                    {
                        tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                        HacerGrabacion(actualLevel);
                    }
                }
            }


        }
        if (actualLevel == 3)
        {
            tiempoNivel += Time.deltaTime;
            if (nivelCompletado3)
            {
                estado = States.Reproducir;
            }
            else
            {
                estado = States.Grabar;
            }
            if (estado == States.Grabar)
            {
                if (tiempoFuturaGrabacion == 0)
                {
                    tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                    HacerGrabacion(actualLevel);

                }
                else
                {
                    if (tiempoFuturaGrabacion < tiempoNivel)
                    {
                        tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                        HacerGrabacion(actualLevel);
                    }
                }

            }
            else
            {
                if (fantasma != null) MoverFantasma();

                if (tiempoFuturaGrabacion == 0)
                {
                    tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                    HacerGrabacion(actualLevel);

                }
                else
                {
                    if (tiempoFuturaGrabacion < tiempoNivel)
                    {
                        tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                        HacerGrabacion(actualLevel);
                    }
                }
            }


        }
    }
    public void MoverFantasma()
    {
        if (actualLevel == 1)
        {
            if (actualpos < posN1.Count)
            {

                fantasma.transform.position = Vector3.MoveTowards(fantasma.transform.position, posN1[actualpos].posicion, /*(Vector2.Distance(posN1[actualposRepro].posicion, posN1[actualposRepro + 1].posicion) / (posN1[actualposRepro + 1].tiempo - posN1[actualposRepro].tiempo)*/5000 * Time.deltaTime);
            }
        }
        if (actualLevel == 2)
        {
            if (actualpos < posN2.Count)
            {

                fantasma.transform.position = Vector3.MoveTowards(fantasma.transform.position, posN2[actualpos].posicion, /*(Vector2.Distance(posN1[actualposRepro].posicion, posN1[actualposRepro + 1].posicion) / (posN1[actualposRepro + 1].tiempo - posN1[actualposRepro].tiempo)*/5000 * Time.deltaTime);
            }
        }
        if (actualLevel == 3)
        {
            if (actualpos < posN3.Count)
            {

                fantasma.transform.position = Vector3.MoveTowards(fantasma.transform.position, posN3[actualpos].posicion, /*(Vector2.Distance(posN1[actualposRepro].posicion, posN1[actualposRepro + 1].posicion) / (posN1[actualposRepro + 1].tiempo - posN1[actualposRepro].tiempo)*/5000 * Time.deltaTime);
            }
        }

    }
}
