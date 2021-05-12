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
    [SerializeField] List<logroID> logrosIDs=new List<logroID>();
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
        Debug.Log("Logro desb: " + logrosIDs[id].steamID);
        if (!estaDesbloqueadoLogro)
        {
            SteamUserStats.SetAchievement(logrosIDs[id].steamID);
            SteamUserStats.StoreStats();
            desbloqueadosInfo[logrosIDs.IndexOf(logrosIDs[id])] = true;
        }
    }
    void ComprobarLogro(string idLogro)
    {
        SteamUserStats.GetAchievement(idLogro, out estaDesbloqueadoLogro);
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.J)&& Input.GetKey(KeyCode.P)&& Input.GetKey(KeyCode.M))
        {
            SteamUserStats.ResetAllStats(true);
        }
    }
}
