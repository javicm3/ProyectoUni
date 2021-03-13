using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    GameObject meshRendererGO;
    GameObject particlesGO;

    public void Desactivar()
    {
        meshRendererGO = GetComponentInChildren<MeshRenderer>().gameObject;
        particlesGO = GetComponentInChildren<ParticleSystem>().gameObject;
        GetComponent<CircleCollider2D>().enabled = false;

        meshRendererGO.SetActive(false);
        particlesGO.SetActive(false);
    }

    public void Activar()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        meshRendererGO.SetActive(true);
        particlesGO.SetActive(true);
    }
}
