using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    CinemachineVirtualCamera cinemakina;
    public float startsize = 13;
    public float finalsize = 19;
    ControllerPersonaje cc;
    public GameObject segundorb;
    public float indiceMultiplicador = 0.05f;
    float tiempoSinInput;
    public GameObject hud;
    float indiceHUD;
    public GameObject targetGroup;
    public float maxZoom;
    public float minZoom;
    float maxDistance;
    public float indiceMultiplicadorZoom = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        tiempoSinInput = 0;
        cinemakina = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        cc = GameObject.FindObjectOfType<ControllerPersonaje>();
    }
    float DistanciaMaxima()
    {
        var bounds = new Bounds(targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target.position, Vector3.zero);

        for (int i = 0; i < (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets.Length); i++)
        {
            if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target != null) bounds.Encapsulate(targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target.position);
        }
        if (bounds.size.x > bounds.size.y)
        {
 return bounds.size.x;
        }
        else
        {
            return bounds.size.y;
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.personajevivo == true)
        //{
        bool soloplayer = false;
        for (int i = 0; i < targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets.Length; i++)
        {
            if (i == 0)
            {
                if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target == this.gameObject.transform)
                {
                    soloplayer = true;
                }
                else
                {
                    soloplayer = false;
                }
            }
            else
            {
                if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target !=null)
                {
                    soloplayer = false;

                }
            }
        }
        if (soloplayer==true)
        {


            if (Mathf.Abs(cc.rb.velocity.x) > 7)
            {
                tiempoSinInput = 0;
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * Mathf.Clamp(cc.rb.velocity.x, 1, 1.3f) * Time.deltaTime, 0, 7);

                }

            }

            else if (Mathf.Abs(cc.rb.velocity.x) > 12)
            {
                tiempoSinInput = 0;
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * Mathf.Clamp(cc.rb.velocity.x, 1, 2f) * Time.deltaTime, 0, 7);
                }

            }
            else if (Mathf.Abs(cc.rb.velocity.x) > 25)
            {
                tiempoSinInput = 0;
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * Mathf.Clamp(cc.rb.velocity.x, 1.5f, 2.2f) * Time.deltaTime, 0, 7);
                }

            }
            else if (Mathf.Abs(cc.rb.velocity.y) > 23f)
            {
                tiempoSinInput = 0;
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 1f * Time.deltaTime, 0, 7);
                }

            }

            else if (Mathf.Abs(cc.rb.velocity.y) > 16f)
            {
                tiempoSinInput = 0;
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 0.4f * Time.deltaTime, 0, 7);
                }

            }
            else if (Mathf.Abs(cc.rb.velocity.y) > 8f)
            {
                tiempoSinInput = 0;
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 0.2f * Time.deltaTime, 0, 7);
                }

            }

            else
            {
                if ((cinemakina.m_Lens.OrthographicSize > startsize) && (Mathf.Abs(cc.rb.velocity.y) < 8f) && (Mathf.Abs(cc.rb.velocity.y) > 0.0f))
                {
                    tiempoSinInput += Time.deltaTime * 0.5f;
                    cinemakina.m_Lens.OrthographicSize -= (Time.deltaTime + tiempoSinInput);
                }
                else
                {
                    if (cinemakina.m_Lens.OrthographicSize > startsize)
                    {
                        tiempoSinInput += Time.deltaTime * 0.5f;
                        cinemakina.m_Lens.OrthographicSize -= (Time.deltaTime + tiempoSinInput);
                    }
                }
            }
        }
        else
        {
            maxDistance = DistanciaMaxima();
            if (cinemakina.m_Lens.OrthographicSize < maxDistance * 0.6f && cinemakina.m_Lens.OrthographicSize < maxZoom)
            {
                cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + indiceMultiplicadorZoom * Time.deltaTime;
            }
        }
        //      }
        //      else
        //      {
        //          if (cinemakina.m_Lens.OrthographicSize > 2)
        //          {
        //cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize -= Time.deltaTime * 10;
        //          }

        //      }
    }
}
