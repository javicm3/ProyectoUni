using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserVertical : MonoBehaviour
{

    public float tiempoEntreComprobaciones = 2f;
    public float tiempoTrasDash = 0.55f;
    
    GameObject player;
    GameObject boss;

    EstadosBoss2 eb;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ControllerPersonaje>().gameObject;
        boss = FindObjectOfType<AtaquesBoss>().gameObject;
        eb = boss.GetComponent<EstadosBoss2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.GetComponent<ControllerPersonaje>().auxCdDash - 0.1f > (player.GetComponent<ControllerPersonaje>().cooldownDash - tiempoTrasDash))
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boss")
        {
            eb.ataqueTerminado = true;
            eb.acumulacion++;
            Destroy(this.gameObject);
        }
    }
}
