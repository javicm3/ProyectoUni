using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject cartel;
    public bool activado = false;
    public GameObject partActivado;
    public AudioClip sonidoActivar;
    public AudioClip sonidoVoces;
    public AudioClip sonidoFondo;

    public AudioSource source;
    public AudioSource source2;
    public AudioSource sourceFondo;

    // Start is called before the first frame update
    void Start()
    {
        if (activado == true)
        {
            sourceFondo.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activado == true)
        {
            sourceFondo.enabled = true;
            //this.GetComponent<SpriteRenderer>().color = Color.cyan;
            partActivado.SetActive(true);
        }
        else
        {
            sourceFondo.enabled = false;
            //this.GetComponent<SpriteRenderer>().color = Color.black;
            partActivado.SetActive(false);
        }
        if (cartel.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Activar();
            }
        }
    }
    void Activar()
    {
        source.PlayOneShot(sonidoActivar);
        source2.PlayOneShot(sonidoVoces);
        cartel.SetActive(false);
        GameObject[] restoRespawns = GameObject.FindGameObjectsWithTag("respawn");
        foreach (GameObject go in restoRespawns)
        {
            go.GetComponent<Respawn>().activado = false;
        }
        activado = true;
      
        GuardarDatos();
      
    }
    void GuardarDatos()
    {
        //if (GameManager.Instance.tempMoneda1 == true) GameManager.Instance.pickMoneda1 = true;
        //if (GameManager.Instance.tempMoneda2 == true) GameManager.Instance.pickMoneda2 = true;
        //if (GameManager.Instance.tempMoneda3 == true) GameManager.Instance.pickMoneda3 = true;
        GameManager.Instance.futureSpawn = this.transform.position;
        //GameManager.Instance.currentRespawn = this.gameObject.transform.position;
        //GameManager.Instance.lastcoleccionablesneed = GameManager.Instance.coleccionablesneed;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (activado == false)
            {
                cartel.SetActive(true);
            }

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (activado == false)
            {
                cartel.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (activado == false)
            {
                cartel.SetActive(false);
            }
        }
    }
}
