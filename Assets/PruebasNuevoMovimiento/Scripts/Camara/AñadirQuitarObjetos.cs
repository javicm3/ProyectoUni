using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AñadirQuitarObjetos : MonoBehaviour
{
    CinemachineVirtualCamera cinemakina;
     CinemachineTargetGroup targetGroup;
    public bool entrada;
    public bool salida;
    public bool dejarSoloPlayer;
    public GameObject[] objAñadir;
    public GameObject[] objQuitar;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        cinemakina = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void AñadirObj()
    {
      foreach(GameObject go in objAñadir)
        {
            for (int i = 0; i < targetGroup.m_Targets.Length; i++)
            {
                if (i == 0) {  }
                else
                {
                    if (targetGroup.m_Targets[i].target != null)
                    {

                        if (targetGroup.m_Targets[i].target == go.transform)
                        {
                            break;
                        }
                    }
                    else
                    {
                        targetGroup.m_Targets[i].target = go.transform;
                        break;
                    }
                }
            }
        }
    }
    void QuitarObj()
    {
        foreach (GameObject go in objQuitar)
        {
            for (int i = 0; i < targetGroup.m_Targets.Length; i++)
            {
                if (i == 0) { }
                else
                {
                    if (targetGroup.m_Targets[i].target != null)
                    {
                        if ( targetGroup.m_Targets[i].target == go.transform)
                        {
                            targetGroup.m_Targets[i].target = null;
                            break;
                        }

                    }
                    else
                    {
                      
                    }
                }
            }
        }
    }
    void DejarSoloPlayer()
    {
        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            if (i == 0) { targetGroup.m_Targets[0].target = player.transform; }
            else
            {if (targetGroup.m_Targets[i].target != null)
                {
                targetGroup.m_Targets[i].target = null;

                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (entrada)
            {
                AñadirObj();
            }
            if (salida)
            {
                QuitarObj();

            }
            if (dejarSoloPlayer) DejarSoloPlayer();
        }
    }
}
