using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPlataformas : MonoBehaviour
{
    public Sprite activado;
    public Sprite apagado;

    public GameObject[] objetosActivados;
    public float tiempoEntrePlataformas = 0.4f;

    public AudioClip clip;
    public AudioSource source;
    public Canvas cartel;
    // Start is called before the first frame update
    void Start()
    {
        cartel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((FindObjectOfType<VidaPlayer>().reiniciando))
        {
            if (this.GetComponent<SpriteRenderer>().sprite == activado)
            {
                this.GetComponent<SpriteRenderer>().sprite = apagado;
            }
        }
    }
    public void Activar()
    {
        //PlataformaND1.vuelta = false;
       
        StartCoroutine(ScuttleV2());
        foreach (GameObject go in objetosActivados)
        {
            go.SetActive(true);
            if (go.GetComponent<PlataformaND1>() != null) {

                go.GetComponent<PlataformaND1>().transform.position = go.GetComponent<PlataformaND1>().startPos.position;

                go.GetComponent<PlataformaND1>().nextPos = go.GetComponent<PlataformaND1>().startPos.transform.position;
                go.GetComponent<PlataformaND1>().auxtiempoParada = go.GetComponent<PlataformaND1>().tiempoParada;
            }

        }
        source.PlayOneShot(clip);
        this.GetComponent<SpriteRenderer>().sprite = activado;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.GetComponent<SpriteRenderer>().sprite == apagado)
            {
                //cartel.enabled = true;
                //if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action3.WasPressed)
                //{
                Activar();
                //}
            }
            else
            {
                cartel.enabled = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cartel.enabled = false;
        }
    }

    public IEnumerator ScuttleV2()
    {
        foreach (GameObject go in objetosActivados)
        {
            go.SetActive(true);

            yield return new WaitForSeconds(tiempoEntrePlataformas);
        }
    }
}
