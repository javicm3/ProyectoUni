using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPlataformas : MonoBehaviour
{
    public Sprite activado;
    public Sprite apagado;

    public GameObject[] objetosActivados;
    
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

    }
    public void Activar()
    {
        StartCoroutine(ScuttleV2());
        source.PlayOneShot(clip);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.GetComponent<SpriteRenderer>().sprite == apagado)
            {


                cartel.enabled = true;
                if (Input.GetButtonDown("Interact") || GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerPersonaje>().joystick.Action3.WasPressed)
                {
                    Activar();
                }
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
            print("squalo");
            yield return new WaitForSeconds(0.4f);
        }      
    }
}
