using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaSubeBoss : MonoBehaviour
{
    public float speed;
    public Transform pos2;
    public Transform startPos;
    public float tiempoParada = 2;
    public float auxtiempoParada;


    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = startPos.position;
        auxtiempoParada = tiempoParada;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position != pos2.position)
        {
            //nextPos = pos2.position;
            transform.position = Vector3.MoveTowards(transform.position, pos2.position, speed * Time.deltaTime);

        }
    }
  
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.parent = FindObjectOfType<ControllerPersonaje>().gameObject.transform)
            {
                collision.gameObject.transform.parent = this.transform;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.parent == this.transform)
            {
                collision.gameObject.transform.parent = null;
            }
        }
    }
}

