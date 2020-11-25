using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cableadoviaje : MonoBehaviour
{
    public GameObject[] nodos;
    GameObject nodoActual;
    bool unavez = false;
    public bool inputEnabled = false;
    Vector2 targetVelocity;
    public float speedMov = 40;
    public Rigidbody2D m_Rigidbody2D;
    public ControllerPersonaje controllerPersonaje;
    public SpriteRenderer rendererCuerpo;
    public SpriteRenderer rendererViaje;
    public Collider2D colliderNormal;
    public Collider2D colliderViaje;
    public Vector3 m_Velocity = Vector3.zero;
    float originalspeed;
    public float originalGravity;
    public bool viajando;
    GameObject nodoElegido;
    float elapsed;
    // Start is called before the first frame update
    void Start()
    {
        originalspeed = speedMov;
        originalGravity = m_Rigidbody2D.gravityScale;
        nodos = GameObject.FindGameObjectsWithTag("Nodo");
        rendererViaje.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (viajando)
        {
            controllerPersonaje.movimientoBloqueado = true;
            m_Rigidbody2D.isKinematic = true;
            m_Rigidbody2D.gravityScale = 0;
            if (unavez == false)
            {
                m_Rigidbody2D.velocity = Vector2.zero;
                unavez = true;
                //rendererCuerpo.gameObject.SetActive(false);
                //rendererViaje.enabled = true;
                rendererViaje.gameObject.SetActive(true);
                colliderNormal.enabled = false;
                colliderViaje.enabled = true;
            }


            if (Input.GetKeyDown(KeyCode.W))
            {
                /*foreach (GameObject nodo in nodos)
                {
                    if (nodo.transform.position.x == nodoActual.transform.position.x && nodo.transform.position.y > nodoActual.transform.position.y)
                    {
                        speedMov = originalspeed;
                        nodoElegido = nodo;
                    }
                }*/


                rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector2.up, Vector2.left);
                for (int i = 0; i < nodos.Length; i++)
                {
                    if (nodos[i].transform.position.x == nodoActual.transform.position.x && nodos[i].transform.position.y > nodoActual.transform.position.y)
                    {
                        speedMov = originalspeed;
                        if (nodoElegido == nodoActual)
                        {
                            nodoElegido = null;
                        }
                        if ((nodoElegido == null || nodos[i].transform.position.y < nodoElegido.transform.position.y) && nodos[i].transform.position.y != nodoActual.transform.position.y)
                        {
                            nodoElegido = nodos[i];
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                //foreach (GameObject nodo in nodos)
                //{
                //    if (nodo.transform.position.x == nodoActual.transform.position.x && nodo.transform.position.y < nodoActual.transform.position.y && nodo != nodoActual)
                //    {
                //        speedMov = originalspeed;
                //        nodoElegido = nodo;
                //    }
                //}


                rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector2.down, Vector2.left);
                for (int i = 0; i < nodos.Length; i++)
                {
                    if (nodos[i].transform.position.x == nodoActual.transform.position.x && nodos[i].transform.position.y < nodoActual.transform.position.y)
                    {
                        speedMov = originalspeed;
                        if (nodoElegido == nodoActual)
                        {
                            nodoElegido = null;
                        }
                        if ((nodoElegido == null || nodos[i].transform.position.y > nodoElegido.transform.position.y) && nodos[i].transform.position.y != nodoActual.transform.position.y)
                        {
                            nodoElegido = nodos[i];
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                //foreach (GameObject nodo in nodos)
                //{
                //    if (nodo.transform.position.y == nodoActual.transform.position.y && nodo.transform.position.x > nodoActual.transform.position.x)
                //    {
                //        speedMov = originalspeed;
                //        nodoElegido = nodo;
                //    }
                //}


                rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector2.right, Vector2.up);
                for (int i = 0; i < nodos.Length; i++)
                {
                    if (nodos[i].transform.position.x > nodoActual.transform.position.x && nodos[i].transform.position.y == nodoActual.transform.position.y)
                    {
                        speedMov = originalspeed;
                        if (nodoElegido == nodoActual)
                        {
                            nodoElegido = null;
                        }
                        if ((nodoElegido == null || nodos[i].transform.position.x < nodoElegido.transform.position.x) && nodos[i].transform.position.x != nodoActual.transform.position.x)
                        {
                            nodoElegido = nodos[i];
                        }

                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                //foreach (GameObject nodo in nodos)
                //{
                //    if (nodo.transform.position.y == nodoActual.transform.position.y && nodo.transform.position.x < nodoActual.transform.position.x)
                //    {

                //        speedMov = originalspeed;
                //        nodoElegido = nodo;
                //    }
                //}


                rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector2.left, Vector2.up);
                for (int i = 0; i < nodos.Length; i++)
                {
                    if (nodos[i].transform.position.x < nodoActual.transform.position.x && nodos[i].transform.position.y == nodoActual.transform.position.y)
                    {
                        speedMov = originalspeed;
                        if (nodoElegido == nodoActual)
                        {
                            nodoElegido = null;
                        }
                        if ((nodoElegido == null || nodos[i].transform.position.x > nodoElegido.transform.position.y) && nodos[i].transform.position.x != nodoActual.transform.position.x)
                        {
                            nodoElegido = nodos[i];
                        }

                    }
                }
            }

            if (nodoElegido != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, nodoElegido.transform.position, Time.deltaTime * speedMov);
                GetComponent<Particulas>().particulasViajeCables.SetActive(true);
                VelocidadViaje();
            }
        }
        else
        {
            nodoElegido = null;
            m_Rigidbody2D.isKinematic = false;
            //
            if (unavez == true)
            {

                m_Rigidbody2D.gravityScale = originalGravity;
                unavez = false;
                rendererCuerpo.enabled = true;
                //rendererCuerpo.gameObject.SetActive(true);
                //rendererViaje.enabled = false;
                rendererViaje.gameObject.SetActive(false);
                colliderViaje.enabled = false;
                colliderNormal.enabled = true;
                controllerPersonaje.movimientoBloqueado = false;
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Nodo")
        {

            nodoActual = collision.gameObject;
            inputEnabled = true;
            //speedMov = 0;
            Nodo node = collision.gameObject.GetComponent<Nodo>();
            if (node.salida == false && viajando == true)
            {
                this.transform.position = new Vector3(node.transform.position.x,node.transform.position.y,this.transform.position.z);
            }
            if (node.salidaAbajo == true && viajando == true)
            {
                this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) +new Vector3(0, -2);
                viajando = false;
            }
            else if (node.salidaArriba == true && viajando == true)
            {
                this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) + new Vector3(0, +2);
                viajando = false;
            }
            else if (node.salidaDerecha == true && viajando == true)
            {
                this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) + new Vector3(2, 0);
                viajando = false;
            }
            else if (node.salidaIzquierda == true && viajando == true)
            {
                this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) + new Vector3(-2, 0);
                viajando = false;
            }
          

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nodo")
        {
            //speedMov = originalspeed;

            //inputEnabled = false;
        }
    }
    void VelocidadViaje()
    {
        float distanciaEntreNodos = Vector2.Distance(nodoActual.transform.position, nodoElegido.transform.position);
        float distanciaAlObjetivo = Vector2.Distance(transform.position, nodoElegido.transform.position);
        if (distanciaEntreNodos * 0.8f < distanciaAlObjetivo)
        {
            speedMov = originalspeed * 6f;

        }
        else if (distanciaEntreNodos * 0.2 > distanciaAlObjetivo)
        {
            speedMov = originalspeed * 0.4f;

        }
        else
        {
            speedMov = originalspeed;
        }
        if (distanciaAlObjetivo == 0)
        {
            GetComponent<Particulas>().particulasViajeCables.SetActive(false);
        }
        else
        {
            GetComponent<Particulas>().particulasViajeCables.SetActive(true);

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Nodo")
        {
            Nodo node = collision.gameObject.GetComponent<Nodo>();
            nodoActual = collision.gameObject;
            if (node.salida == true)
            {
                if (viajando == true)
                {
                    if (node.salida == true)
                    {
                        viajando = false;
                    }
                }
            }
        }
    }
}
