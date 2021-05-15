using Steamworks;
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


    bool reproducir;
    public bool activado;
    public GameObject fantasmaPrefab;
    GameObject fantasma;
    public int actualLevel;
    public int actualpos = 0;

    public List<Posiciones> posTemp = new List<Posiciones>();

    LevelInfo nivelActual;

    float tiempoEntreGrabaciones = 0.02f;
    float tiempoFuturaGrabacion = 0;
    public GameObject player;
    public Rigidbody2D rb;
    public float tiempoNivel;


    public void IniciarFantasma(ref LevelInfo nivel)
    {
       
        activado = true;
        nivelActual = nivel;
        tiempoNivel = 0;
        actualpos = 0;
        tiempoFuturaGrabacion = 0;
        posTemp.Clear();

        if (GameObject.FindObjectOfType<ControllerPersonaje>() != null)
        {
            player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
            rb = player.GetComponent<Rigidbody2D>();
        }

        if (!nivelActual.completado) 
        {
            reproducir = false;
        }
        else
        {
            fantasma = GameObject.Instantiate(fantasmaPrefab, player.transform.position, Quaternion.identity);
            reproducir = true;
            bool desbloq;
            SteamUserStats.GetAchievement(ManagerLogros.Instance.logrosIDs[4].steamID, out desbloq);
            if (!desbloq)
            {
                ManagerLogros.Instance.DesbloquearLogro(4);
            }
        }   
    }

    public void TerminarNivel(string nivel)
    {
        if (tiempoNivel<nivelActual.mejorTiempo || nivelActual.mejorTiempo==0)
        {
            if (fantasma != null)
            {
                ManagerLogros.Instance.AddStat("FantasmasGanados");
            
            if (!GameManager.Instance.NivelActual.fantasmaGanado)
            {
                GameManager.Instance.NivelActual.fantasmaGanado = true;
                ManagerLogros.Instance.AddStat("NivelesGhost");
            }}
            nivelActual.pos.Clear();
            nivelActual.pos.AddRange(posTemp);
        }
        else
        {
            if (fantasma != null)
            {
                ManagerLogros.Instance.AddStat("FantasmasPerdidos");
            }
           
        }
        posTemp.Clear();
        activado = false;
    }

    public void HacerGrabacion() 
    {
        posTemp.Add(new Posiciones(Vector3.zero, Vector3.zero, 0));
        posTemp[actualpos] = new Posiciones(player.transform.position, rb.velocity, tiempoNivel);
        actualpos++;
    }

    void Update() 
    {
        if (activado)
        {
            tiempoNivel += Time.deltaTime;

            if (tiempoFuturaGrabacion == 0)
            {
                tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                HacerGrabacion();

            }
            else
            {
                if (tiempoFuturaGrabacion < tiempoNivel)
                {
                    tiempoFuturaGrabacion = tiempoNivel + tiempoEntreGrabaciones;
                    HacerGrabacion();
                }
            }

            if (reproducir)
            {
                if (fantasma != null) MoverFantasma();
            }
        }
    }

    public void MoverFantasma()
    {
        if (actualpos < nivelActual.pos.Count)
        {
            fantasma.transform.position = Vector3.MoveTowards(fantasma.transform.position, nivelActual.pos[actualpos].posicion, /*(Vector2.Distance(posN1[actualposRepro].posicion, posN1[actualposRepro + 1].posicion) / (posN1[actualposRepro + 1].tiempo - posN1[actualposRepro].tiempo)*/5000 * Time.deltaTime);
        }
    }
}
