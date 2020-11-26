using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatDesaparece : MonoBehaviour
{
    public float tiempoAntesDesaparecer = 1f;
    public float auxAntes;
    public float auxDespues;
    public float tiempoAparecer = 3f;
    Collider2D bcollider;
    SpriteRenderer srenderer;
    public bool tocando = false;
    public bool timerinicialEmpezado = false;
    public GameObject OffsetIzq;
    public GameObject OffsetDer;
    public GameObject OffsetY;
    public AudioClip sonidoDesaparecer;
    public AudioClip sonidoAparecer;
    // Start is called before the first frame update
    void Start()
    {
        bcollider = this.GetComponent<Collider2D>();
        srenderer = this.GetComponent<SpriteRenderer>();
        auxAntes = tiempoAntesDesaparecer;
        auxDespues = tiempoAparecer;
    }

    // Update is called once per frame
    void Update()
    {

        if ((bcollider.enabled == true) && (timerinicialEmpezado))
        {
            auxAntes -= Time.deltaTime;
            if (auxAntes < tiempoAntesDesaparecer * 0.4f)
            {
                srenderer.color = new Color(srenderer.color.r, srenderer.color.g, srenderer.color.b, 0.25f);
            }
            if (auxAntes < 0)
            {
                this.GetComponent<AudioSource>().PlayOneShot(sonidoDesaparecer);
               
                auxAntes = tiempoAntesDesaparecer;
                bcollider.enabled = false;
                srenderer.enabled = false;
                timerinicialEmpezado = false;
            }
        }
        if (bcollider.enabled == false)
        {
            auxDespues -= Time.deltaTime;
            if (auxDespues < 0)
            {

                srenderer.color = new Color(srenderer.color.r, srenderer.color.g, srenderer.color.b, 1f);
                auxDespues = tiempoAparecer;
                bcollider.enabled = true;
             
                this.GetComponent<AudioSource>().PlayOneShot(sonidoAparecer);

            }
            else
            {
                if (auxDespues > 0f && auxDespues < 0.5f)
                {

                    srenderer.enabled = true;
                    srenderer.color = new Color(srenderer.color.r, srenderer.color.g, srenderer.color.b, 0.25f);
                }

            }
        }

    }
    void IniciarTimer()
    {
        if (timerinicialEmpezado == false)
        {
            timerinicialEmpezado = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if ((collision.gameObject.transform.position.x > OffsetIzq.transform.position.x) && (collision.gameObject.transform.position.x < OffsetDer.transform.position.x) && (collision.gameObject.transform.position.y > OffsetY.gameObject.transform.position.y))
            {
                IniciarTimer();
                print("colisionado");
            }

        }
        else
        {
            tocando = false;
        }
    }
}
