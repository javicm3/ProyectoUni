using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AñadirTargetGroupSolo : MonoBehaviour
{
    CinemachineVirtualCamera cinemakina;
    CinemachineTargetGroup targetGroup;

    public float pesoObjAñadir = 1;
    public float radioObjAñadir = 5f;
    public float distanciaAñadir = 35f;
    public float distanciaQuitar = 40f;
    public bool añadido = false;
    public bool limitaDistanciaMax = false;
    public float distanciaMaxLimite = 50;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        cinemakina = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>();
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, distanciaAñadir);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, distanciaQuitar);
    }
    // Update is called once per frame
    void Update()
    {
        if (añadido == false)
        {
            if (Vector2.Distance(player.transform.position, this.transform.position) < distanciaAñadir)
            {
                AñadirObj();
                añadido = true;
            }
        }
        else
        {
            if (Vector2.Distance(player.transform.position, this.transform.position) > distanciaQuitar)
            {
                QuitarObj();
                añadido = false;
            }
        }
    }
    void AñadirObj()
    {
        if (FindObjectOfType<CameraZoom>() != null && limitaDistanciaMax)
        {
            FindObjectOfType<CameraZoom>().limitarDistancia = true;
            FindObjectOfType<CameraZoom>().maxDistance = distanciaMaxLimite;
        }
       
            for (int i = 0; i < targetGroup.m_Targets.Length; i++)
            {
                if (i == 0) { }
                else
                {
                    if (targetGroup.m_Targets[i].target != null)
                    {

                        if (targetGroup.m_Targets[i].target == this.transform)
                        {
                            break;
                        }
                    }
                    else
                    {
                        targetGroup.m_Targets[i].target = this.transform;

                       
                                targetGroup.m_Targets[i].weight = pesoObjAñadir;

                            
                        
                        targetGroup.m_Targets[i].radius = radioObjAñadir;
                        break;

                    }
                }
            
        }
    }
    void QuitarObj()
    {
        if (FindObjectOfType<CameraZoom>() != null && limitaDistanciaMax)
        {
            FindObjectOfType<CameraZoom>().limitarDistancia = true;
            FindObjectOfType<CameraZoom>().maxDistance = distanciaMaxLimite;
        }
       
            for (int i = 0; i < targetGroup.m_Targets.Length; i++)
            {
                if (i == 0) { }
                else
                {
                    if (targetGroup.m_Targets[i].target != null)
                    {
                        if (targetGroup.m_Targets[i].target == this.transform)
                        {
                            targetGroup.m_Targets[i].target = null;
                            targetGroup.m_Targets[i].weight = 1;
                            targetGroup.m_Targets[i].radius = 0;

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
