using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{

    public GameObject m_camara;
    public GameObject m_nuevaCamara;

    public bool m_cambio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ControllerPersonaje>() != null)
        {
            if(m_cambio == true)
            {
                m_camara.SetActive(false);
                m_nuevaCamara.SetActive(true);
            }
            if (m_cambio == false)
            {
                m_camara.SetActive(true);
                m_nuevaCamara.SetActive(false);
            }
        }
    }
}
