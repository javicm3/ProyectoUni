using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControladorPlataformas : MonoBehaviour
{
    public Sprite activado;
    public Sprite apagado;
    public bool luz = false;
    public GameObject luzGO;
    public GameObject[] objetosActivados;
    public float tiempoEntrePlataformas = 0.4f;
    public bool secuenciaPlat = false;
    public bool seDesactivaConPlatf = false;
    //public float auxTiempoEntre;
    public AudioClip clip;
    public AudioSource source;
    public Canvas cartel;
    public bool activadoBool;
    public float tiempoReiniciar = 10;

    float tiempoRest;
    VidaPlayer vidaPl;


    void Start()
    {
        vidaPl = FindObjectOfType<VidaPlayer>();
        tiempoRest = tiempoReiniciar;
    }

    private void Update()
    {
        if (activadoBool)
        {
            tiempoRest -= Time.deltaTime;
            if (tiempoRest <= 0 || vidaPl.reiniciando)
            {
                tiempoRest = tiempoReiniciar;
                Reiniciar();
            }            
        }
    }

    void Reiniciar()
    {
        print("Controlador Reactivado");
        activadoBool = false;
        this.GetComponent<SpriteRenderer>().sprite = apagado;
        if (luz == true && luzGO != null)
        {
            luzGO.SetActive(false);
        }

        foreach (GameObject plat in objetosActivados)
        {
            PlataformaDron a = plat.GetComponent<PlataformaDron>();
            if (a!=null)
            {
                a.Desactivar();
            }

            plat.SetActive(false);
        }
        //Mirar si hago algo con las plataformas aquí
    }

    IEnumerator ActivarSecuencia()
    {
        WaitForSeconds w = new WaitForSeconds(tiempoEntrePlataformas);

        foreach (GameObject plat in objetosActivados)
        {
            plat.SetActive(true);
            PlataformaDron platDron = plat.GetComponent<PlataformaDron>();
            if (platDron != null)
            {
                platDron.Activar();
            }
            else
            {
                PlataformaND1 platND1 = plat.GetComponent<PlataformaND1>();
                if (platND1 != null)
                {
                    //METER UN METODO ACTIVAR EN LA PLATAFORMA
                    platND1.transform.position = platND1.GetComponent<PlataformaND1>().startPos.position;

                    platND1.GetComponent<PlataformaND1>().nextPos = platND1.GetComponent<PlataformaND1>().startPos.transform.position;
                    platND1.GetComponent<PlataformaND1>().auxtiempoParada = platND1.GetComponent<PlataformaND1>().tiempoParada;

                }
            }

            yield return w;
        }
    }

    void ActivarSinSecuencia()
    {
        foreach (GameObject plat in objetosActivados)
        {
            plat.SetActive(true);
            PlataformaDron platDron = plat.GetComponent<PlataformaDron>();
            if (platDron != null)
            {
                //METER UN METODO ACTIVAR EN LA PLATAFORMA
                platDron.Activar();
            }
            else
            {
                PlataformaND1 platND1 = plat.GetComponent<PlataformaND1>();
                if (platND1 != null)
                {
                    //METER UN METODO ACTIVAR EN LA PLATAFORMA
                    platND1.transform.position = platND1.GetComponent<PlataformaND1>().startPos.position;

                    platND1.GetComponent<PlataformaND1>().nextPos = platND1.GetComponent<PlataformaND1>().startPos.transform.position;
                    platND1.GetComponent<PlataformaND1>().auxtiempoParada = platND1.GetComponent<PlataformaND1>().tiempoParada;

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activadoBool && collision.gameObject.tag == "Player")
        {
            activadoBool=true;
            if (secuenciaPlat)
            {
                StartCoroutine(ActivarSecuencia());
            }
            else ActivarSinSecuencia();

            source.PlayOneShot(clip);
            this.GetComponent<SpriteRenderer>().sprite = activado;
            if (luz == true && luzGO != null)
            {
                luzGO.SetActive(true);
            }
        }
    }
}














