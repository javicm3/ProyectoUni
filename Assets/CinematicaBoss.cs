using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaBoss : MonoBehaviour
{
    public float tiempoNiebla;
    public float segundosLaseres;
    public float tiempoMensajes;
    public float tiempoOpacidad;
    public GameObject camara1;
    //public GameObject camara2;
    public GameObject holograma;
    public GameObject[] laseres1;
    public GameObject[] laseres2;
    public GameObject[] laseres3;
    public GameObject particulasNiebla;
    public SpriteRenderer[] partesBoss;
    public Sprite mensaje1;
    public Sprite mensaje2;
    public bool primeravez;
    public Transform puntoNiebla;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        if(primeravez == true)
        {
            for (int i = 0; i < laseres1.Length; i++)
            {
                laseres1[i].SetActive(false);
            }
            for (int i = 0; i < laseres2.Length; i++)
            {
                laseres1[i].SetActive(false);
            }
            for (int i = 0; i < laseres3.Length; i++)
            {
                laseres1[i].SetActive(false);
            }
        }
        camara1.SetActive(false);
        for (int i = 0; i < partesBoss.Length; i++)
        {
            //float newAlpha = Mathf.Lerp(0, partesBoss[i].material.color.a, Time.deltaTime * t);
            //partesBoss[i].material.color = new Color(partesBoss[i].material.color.r, partesBoss[i].material.color.g, partesBoss[i].material.color.b, 0);
            partesBoss[i].enabled = false;
        }
        holograma.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Encendiendo()
    {
        camara1.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInput>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(tiempoNiebla);
        
        //camara2.SetActive(true);
        Instantiate(particulasNiebla, puntoNiebla.transform.position, Quaternion.identity);
        for(int i = 0; i < partesBoss.Length; i++)
        {
            //float newAlpha = Mathf.Lerp(0, partesBoss[i].material.color.a, Time.deltaTime * t);
            //partesBoss[i].material.color = new Color(partesBoss[i].material.color.r, partesBoss[i].material.color.g, partesBoss[i].material.color.b, newAlpha);
            partesBoss[i].enabled = true;
        }
        holograma.SetActive(true);
        yield return new WaitForSeconds(tiempoOpacidad);

        holograma.GetComponent<SpriteRenderer>().sprite = mensaje1;
        yield return new WaitForSeconds(tiempoMensajes);
        holograma.GetComponent<SpriteRenderer>().sprite = mensaje2;
        yield return new WaitForSeconds(tiempoMensajes);
        StartCoroutine(Laseres());

    }
    public IEnumerator Laseres()
    {
        for(int i = 0; i < laseres1.Length; i++)
        {
            laseres1[i].SetActive(true);
        }
        yield return new WaitForSeconds(segundosLaseres);
        for (int i = 0; i < laseres2.Length; i++)
        {
            laseres2[i].SetActive(true);
        }
        yield return new WaitForSeconds(segundosLaseres);
        for (int i = 0; i < laseres3.Length; i++)
        {
            laseres3[i].SetActive(true);
        }
        yield return new WaitForSeconds(segundosLaseres);
        camara1.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInput>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //camara2.SetActive(false);
    }
}
