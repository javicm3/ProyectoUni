using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explodable))]
[RequireComponent(typeof(BoxCollider2D))]
public class DestructiblePorDash : MonoBehaviour
{
    public bool soloDashNormal = false;
    public bool soloDashAbajo = false;
    public bool ambosDashes = true;
    Explodable expl;
    [SerializeField] float duracionFrag = 5;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        expl = GetComponent<Explodable>();
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
        BoxCollider2D coll = GetComponent<BoxCollider2D>();
        coll.isTrigger = true;
        coll.size *= 1.2f;
    }
    private void FixedUpdate()
    {
        if (ambosDashes)
        {
            if ((Vector2.Distance(player.transform.position, this.transform.position) < 5) && (player.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.0 || player.GetComponent<ControllerPersonaje>().dashEnCaida))
            {
                GetComponent<Collider2D>().isTrigger = true;


                expl.explode(duracionFrag);
            }
        }
        else if (soloDashAbajo)
        {
            if ((Vector2.Distance(player.transform.position, this.transform.position) < 5) && ( player.GetComponent<ControllerPersonaje>().dashEnCaida))
            {
                GetComponent<Collider2D>().isTrigger = true;


                expl.explode(duracionFrag);
            }

        }
        else if (soloDashNormal)
        {
            if ((Vector2.Distance(player.transform.position, this.transform.position) < 5) && (player.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.0 ))
            {
                GetComponent<Collider2D>().isTrigger = true;


                expl.explode(duracionFrag);
            }
        }
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (ambosDashes)
            {
                if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.0f || collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida)
                {
                    GetComponent<Collider2D>().isTrigger = true;
                    expl.explode(duracionFrag);
                }
            }
            else if (soloDashAbajo)
            {
                if (collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida)
                {
                    GetComponent<Collider2D>().isTrigger = true;
                    expl.explode(duracionFrag);
                }

            }
            else if (soloDashNormal)
            {
                if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.0f)
                {
                    GetComponent<Collider2D>().isTrigger = true;
                    expl.explode(duracionFrag);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (ambosDashes)
            {
                if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.0f || collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida)
                {
                    GetComponent<Collider2D>().isTrigger = true;
                    expl.explode(duracionFrag);
                }
            }
            else if (soloDashAbajo)
            {
                if (collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida)
                {
                    GetComponent<Collider2D>().isTrigger = true;
                    expl.explode(duracionFrag);
                }

            }
            else if (soloDashNormal)
            {
                if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.0f)
                {
                    GetComponent<Collider2D>().isTrigger = true;
                    expl.explode(duracionFrag);
                }
            }
        }
    }       
}
