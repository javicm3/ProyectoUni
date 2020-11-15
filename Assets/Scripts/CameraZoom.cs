using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    CinemachineVirtualCamera cinemakina;
    float startsize = 11;
    float finalsize = 16;
    ControllerPersonaje cc;
    public GameObject segundorb;
    public float indiceMultiplicador = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        cinemakina = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        cc = GameObject.FindObjectOfType<ControllerPersonaje>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.personajevivo == true)
        //{


            if (Mathf.Abs(cc.rb.velocity.x) > 12)
            {
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * 1f * Time.deltaTime, 0, 5);
                }

            }
            else if (Mathf.Abs(cc.rb.velocity.x) > 18)
            {
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * 1.8f * Time.deltaTime, 0, 5);
                }

            }
            else if (Mathf.Abs(cc.rb.velocity.y) > 23f)
            {
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 1f * Time.deltaTime, 0, 5);
                }

            }

            else if (Mathf.Abs(cc.rb.velocity.y) > 16f)
            {
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 0.4f * Time.deltaTime, 0, 5);
                }

            }
            else if (Mathf.Abs(cc.rb.velocity.y) > 8f)
            {
                if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 0.2f * Time.deltaTime, 0, 5);
                }

            }

            else
            {
                if ((cinemakina.m_Lens.OrthographicSize > startsize) && (Mathf.Abs(cc.rb.velocity.y) < 8f) && (Mathf.Abs(cc.rb.velocity.y) > 0.0f))
                {
                    cinemakina.m_Lens.OrthographicSize -= Time.deltaTime;
                }
                else
                {
                    if (cinemakina.m_Lens.OrthographicSize > startsize)
                    {
                        cinemakina.m_Lens.OrthographicSize -= Time.deltaTime ;
                    }
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
