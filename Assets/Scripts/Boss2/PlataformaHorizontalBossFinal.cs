using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaHorizontalBossFinal : MonoBehaviour
{
    public float speed;
    public Transform pos2;
    public Transform startPos;
    public float tiempoParada = 2;
    public float auxtiempoParada;
    bool der;
    bool izq;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = startPos.position;
        auxtiempoParada = tiempoParada;
        izq = true;
        der = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position != pos2.position && der == true)
        {
            //nextPos = pos2.position;
            Irder();

        }
        if (this.transform.position != startPos.position && izq == true)
        {
            //nextPos = pos2.position;
            
            Irizq();

        }
        if (Vector3.Distance(this.transform.position, pos2.position) < 2)
        {
            der = false;
            izq = true;
        }
        if(Vector3.Distance(this.transform.position, startPos.position) < 2)
        {
            izq = false;
            der = true;
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
    void Irder()
    {
        transform.position = Vector3.MoveTowards(transform.position, pos2.position, speed * Time.deltaTime);
    }
    void Irizq()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);
    }
}
