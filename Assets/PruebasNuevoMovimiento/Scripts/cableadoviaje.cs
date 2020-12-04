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
   public  GameObject nodoElegido;
    float elapsed;
    PlayerInput Pinput;
    public bool conVelocidad = false;
    // Start is called before the first frame update
    void Start()
    {
        originalspeed = speedMov;
        originalGravity = m_Rigidbody2D.gravityScale;
        nodos = GameObject.FindGameObjectsWithTag("Nodo");
        rendererViaje.gameObject.SetActive(false);
        Pinput = this.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (viajando)
        {


            controllerPersonaje.saltoBloqueado = true;
            controllerPersonaje.movimientoBloqueado = true;
            m_Rigidbody2D.isKinematic = true;
            m_Rigidbody2D.gravityScale = 0;
            if (unavez == false)
            {
                this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidoLoop, this.GetComponent<AudioManager>().MoverseCables);
                m_Rigidbody2D.velocity = Vector2.zero;
                unavez = true;
               if(rendererCuerpo!=null) rendererCuerpo.gameObject.SetActive(false);
                //rendererViaje.enabled = true;
                rendererViaje.gameObject.SetActive(true);
                colliderNormal.enabled = false;
                GetComponent<VidaPlayer>().enabled = false;
                colliderViaje.enabled = true;
                controllerPersonaje.dashBloqueado = true;
                controllerPersonaje.dashCaidaBloqueado = true;
            }
            print(this.GetComponent<Rigidbody2D>().velocity);
            if (this.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                conVelocidad = true;
            }
            else
            {
                conVelocidad = false;
            }

            if (Pinput.inputVertical>0&&conVelocidad==false)
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
            if (Pinput.inputVertical < 0 && conVelocidad == false)
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
            if( Pinput.inputHorizontal > 0 && conVelocidad == false)
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
            if (Pinput.inputHorizontal < 0 && conVelocidad == false)
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
          
            if ((nodoElegido != null))
            {
                controllerPersonaje.rb.velocity = (transform.position - nodoElegido.transform.position).normalized;
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
                this.GetComponent<AudioManager>().Stop(this.GetComponent<AudioManager>().sonidoLoop);
                GetComponent<VidaPlayer>().enabled = true;
                m_Rigidbody2D.gravityScale = originalGravity;
                unavez = false;
               if(rendererCuerpo!=null) rendererCuerpo.enabled = true;
                if (rendererCuerpo != null) rendererCuerpo.gameObject.SetActive(true);
                //rendererViaje.enabled = false;
                rendererViaje.gameObject.SetActive(false);
                colliderViaje.enabled = false;
                colliderNormal.enabled = true;
                controllerPersonaje.movimientoBloqueado = false;
                controllerPersonaje.dashBloqueado = false;
                controllerPersonaje.saltoBloqueado = false;
                controllerPersonaje.dashCaidaBloqueado = false;
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Nodo")
        {
           
            nodoActual = collision.gameObject;
            inputEnabled = true;
            controllerPersonaje.rb.velocity = Vector2.zero;
            //speedMov = 0;
            Nodo node = collision.gameObject.GetComponent<Nodo>();
            if (node.salida == false && viajando == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().pasarPorNodo);
                this.transform.position = new Vector3(node.transform.position.x,node.transform.position.y,this.transform.position.z);
            }
            if (node.salidaAbajo == true && viajando == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().salidaCables);
                this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) +new Vector3(0, -2);
                viajando = false;
            }
            else if (node.salidaArriba == true && viajando == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().salidaCables);
                this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) + new Vector3(0, +2);
                viajando = false;
            }
            else if (node.salidaDerecha == true && viajando == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().salidaCables);
                this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) + new Vector3(2, 0);
                viajando = false;
            }
            else if (node.salidaIzquierda == true && viajando == true)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().salidaCables);
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
            controllerPersonaje.rb.velocity = Vector2.zero;
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
