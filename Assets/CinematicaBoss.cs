using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaBoss : MonoBehaviour
{
    public float tiempoNiebla;
    public float segundosLaseres;
    public float tiempoMensajes;
    public float velocidadOpacidad;
    public GameObject camara1;
    //public GameObject camara2;
    public GameObject holograma;
    public GameObject textoHolograma;
    public GameObject[] laseres1;
    public GameObject[] laseres2;
    public GameObject[] laseres3;
    public GameObject particulasNiebla;
    public SpriteRenderer[] partesBoss;
    public Texture2D mensaje1;
    public Texture2D mensaje2;
    public bool primeravez;
    public Transform puntoNiebla;
    public GameObject lucesFondo;
    bool luces =  false;
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
        yield return new WaitForSeconds(0.7f);

        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(1.7f);

        holograma.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Laseres());
        lucesFondo.GetComponent<Animator>().SetTrigger("On");
        textoHolograma.GetComponent<SpriteRenderer>().material.SetTexture("_Letras", mensaje1);
        yield return new WaitForSeconds(tiempoMensajes);

        textoHolograma.GetComponent<SpriteRenderer>().material.SetTexture("_Letras", mensaje2);
        yield return new WaitForSeconds(tiempoMensajes);

        camara1.SetActive(false);
        
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInput>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

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
        
        //camara2.SetActive(false);
    }
    public IEnumerator FadeIn()
    {
        
        //float newAlpha = Mathf.Lerp(0, partesBoss[i].material.color.a, Time.deltaTime * t);
        //partesBoss[i].material.color = new Color(partesBoss[i].material.color.r, partesBoss[i].material.color.g, partesBoss[i].material.color.b, newAlpha);
        for (float f = 0.05f; f <= 1; f += velocidadOpacidad)
        {
            for (int i = 0; i < partesBoss.Length; i++)
            {
                partesBoss[i].enabled = true;
                Color c = partesBoss[i].GetComponent<SpriteRenderer>().color;
                c.a = f;
                partesBoss[i].GetComponent<SpriteRenderer>().color = c;
                yield return new WaitForSeconds(0.02f);
            }

        }
    }
}
