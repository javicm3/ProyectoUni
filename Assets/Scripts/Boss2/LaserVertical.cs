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
        if(this.GetComponent<BoxCollider2D>() != null)
        {
            if (player.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.2f)
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                this.GetComponent<BoxCollider2D>().enabled = true;
            }
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.GetComponent<BoxCollider2D>() != null)
        {
            if (collision.tag == "LaseresBossFinal")
            {
                eb.acumulacion++;
                eb.ataqueTerminado = true;
                Destroy(this.gameObject);
            }
        }
    }
}
