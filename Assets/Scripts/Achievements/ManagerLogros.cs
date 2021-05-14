using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerLogros : MonoBehaviour
{
    private static ManagerLogros _instance;

    public static ManagerLogros Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ManagerLogros>();
            }

            return _instance;
        }
    }

    [System.Serializable]
    public struct logroID
    {
        public string steamID;

    }
    [SerializeField] public List<logroID> logrosIDs = new List<logroID>();
    public bool[] desbloqueadosInfo;
    bool estaDesbloqueadoLogro;
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


    }

    void Start()
    {
        desbloqueadosInfo = new bool[logrosIDs.Count];
        ComprobarDesbloqueados();
    }
    void ComprobarDesbloqueados()
    {
        for (int i = 0; i < logrosIDs.Count; i++)
        {
            SteamUserStats.GetAchievement(logrosIDs[i].steamID, out desbloqueadosInfo[i]);
        }
    }
    public void DesbloquearLogro(int id)
    {
        estaDesbloqueadoLogro = false;
        ComprobarLogro(logrosIDs[id].steamID);
      
        if (!estaDesbloqueadoLogro)
        {  Debug.Log("Logro desb: " + logrosIDs[id].steamID);
            SteamUserStats.SetAchievement(logrosIDs[id].steamID);
            SteamUserStats.StoreStats();
            desbloqueadosInfo[logrosIDs.IndexOf(logrosIDs[id])] = true;
        }
    }
    public void AddStat(string id)
    {
        int value;
        SteamUserStats.GetStat(id, out value);
        value++;
        SteamUserStats.SetStat(id, value);
        Debug.Log("stat added : " + id+ value);

        if (id == "NivelesZona1")
        {
            if (value >= 3)
            {
                DesbloquearLogro(1);
            }
        }
        else if (id == "NivelesZona2")
        {
            if (value >= 4)
            {
                DesbloquearLogro(3);
            }
        }
        else if (id == "NivelesGhost")
        {
            if (value >= 12)
            {
                DesbloquearLogro(6);
            }
        }
        else if (id == "NivelesSinMuerte")
        {
            if (value >= 12)
            {
                DesbloquearLogro(16);
            }
        }
        else if (id == "FantasmasGanados")
        {
            if (value >= 5)
            {
                DesbloquearLogro(5);
            }
        }
        else if (id == "FantasmasPerdidos")
        {
            if (value >= 5)
            {
                DesbloquearLogro(7);
            }
        }
        else if (id == "EnemigosStun")
        {
            if (value >= 20)
            {
                DesbloquearLogro(9);
            }
        }
        else if (id == "NivelesTodosColeccionables")
        {
            if (value >= 12)
            {
                DesbloquearLogro(14);
            }
        }
        else if (id == "TotalMuertes")
        {
            if (value >= 30)
            {
                DesbloquearLogro(18);
            }
        }
        SteamUserStats.StoreStats();

    }
    void ComprobarLogro(string idLogro)
    {
        SteamUserStats.GetAchievement(idLogro, out estaDesbloqueadoLogro);
    }
    void Update()
    {
      
        if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.M))
        {
            SteamUserStats.ResetAllStats(true);
            SteamUserStats.StoreStats();
        }
    }
}
