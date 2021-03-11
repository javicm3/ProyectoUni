using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explodable))]
[RequireComponent(typeof(BoxCollider2D))]
public class DestructiblePorDash : MonoBehaviour
{
    Explodable expl;
    [SerializeField] float duracionFrag = 5;

    // Start is called before the first frame update
    void Start()
    {
        expl = GetComponent<Explodable>();

        BoxCollider2D coll = GetComponent<BoxCollider2D>();
        coll.isTrigger = true;
        coll.size *= 1.2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.2f || collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida)
            {
                GetComponent<Collider2D>().isTrigger = true;
                expl.explode(duracionFrag);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.2f || collision.gameObject.GetComponent<ControllerPersonaje>().dashEnCaida)
            {
                GetComponent<Collider2D>().isTrigger = true;
                expl.explode(duracionFrag);
            }
        }
    }
}
