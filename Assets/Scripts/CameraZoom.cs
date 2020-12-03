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

    // Start is called before the first frame update
    void Start()
    {
        tiempoSinInput = 0;
        cinemakina = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        cc = GameObject.FindObjectOfType<ControllerPersonaje>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.personajevivo == true)
        //{

        if (Mathf.Abs(cc.rb.velocity.x) > 7)
        {
            tiempoSinInput = 0;
            if (cinemakina.m_Lens.OrthographicSize < finalsize)
            {
                cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * Mathf.Clamp(cc.rb.velocity.x, 1, 1.3f) * Time.deltaTime, 0, 7);
                
            }

        }
        
        else  if (Mathf.Abs(cc.rb.velocity.x) > 12)
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
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * Mathf.Clamp(cc.rb.velocity.x, 1.5f,2.2f) * Time.deltaTime, 0, 7);
                }

            }
            else if (Mathf.Abs(cc.rb.velocity.y) > 23f)
        {
            tiempoSinInput = 0;
            if (cinemakina.m_Lens.OrthographicSize < finalsize)
                {
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador *1f * Time.deltaTime, 0, 7);
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
                tiempoSinInput += Time.deltaTime*0.5f;
                    cinemakina.m_Lens.OrthographicSize -= (Time.deltaTime + tiempoSinInput);
            }
                else
                {
                    if (cinemakina.m_Lens.OrthographicSize > startsize)
                {
                    tiempoSinInput += Time.deltaTime*0.5f;
                    cinemakina.m_Lens.OrthographicSize -=(Time.deltaTime+tiempoSinInput) ;
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
