using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaND1 : MonoBehaviour
{

    public float speed;
    public Transform pos1, pos2;
    public Transform startPos;
    public float tiempoParada = 2;
    public float auxtiempoParada;
    public  bool vuelta = false;
    public bool verticales = false;
    public Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
        auxtiempoParada = tiempoParada;
    }

    // Update is called once per frame
    void Update()
    {
        if (verticales)
        {
            auxtiempoParada -= Time.deltaTime;
            if ((auxtiempoParada <= 0)&& (vuelta = false))
            {
                nextPos = pos1.position;
                vuelta = true;
            }
        }
        if (FindObjectOfType<VidaPlayer>().reiniciando)
        {




            vuelta = false;
            this.transform.position = startPos.position;
            transform.gameObject.SetActive(false);
        }
        if (transform.position == startPos.position)
        {
            if (!verticales)
            {
 auxtiempoParada -= Time.deltaTime;
            }
           
            if (auxtiempoParada <= 0)
            {
                nextPos = pos1.position;
                vuelta = true;
            }
        }

        if (this.transform.position == pos1.position)
        {
            if (!verticales)
            {

          
            //nextPos = pos2.position;
            if (vuelta == true)
            {
                nextPos = pos2.position;
                //if (FindObjectOfType<ControllerPersonaje>().gameObject.transform.parent != null) FindObjectOfType<ControllerPersonaje>().gameObject.transform.parent = null;

                //vuelta = false;
                //this.transform.position = startPos.position;
                //transform.gameObject.SetActive(false);

            }
            }
            else
            {
                this.transform.position = pos1.position;
                transform.gameObject.SetActive(false);
            }
        }
        if (this.transform.position == pos2.position)
        {
            //nextPos = pos2.position;
            if (!verticales)
            {
                if (vuelta == true)
                {
                    nextPos = pos2.position;
                    if (FindObjectOfType<ControllerPersonaje>().gameObject.transform.parent != null) FindObjectOfType<ControllerPersonaje>().gameObject.transform.parent = null;

                    vuelta = false;
                    this.transform.position = startPos.position;
                    transform.gameObject.SetActive(false);

                }
            }
            else
            {
                if (vuelta == true)
                {
                    nextPos = pos1.position;
                   

                    vuelta = false;
                    this.transform.position = startPos.position;
                   

                }
            }

        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OndDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    public IEnumerator Scuttle()
    {
        yield return new WaitForSeconds(tiempoParada);

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
