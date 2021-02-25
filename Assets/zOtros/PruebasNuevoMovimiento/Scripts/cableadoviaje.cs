using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cableadoviaje : MonoBehaviour
{
    public bool conVelocidad = false;
    public GameObject[] nodos;
    GameObject nodoActual;
    bool unavez = false;
    public bool inputEnabled = false;
    Vector2 targetVelocity;
    public float speedMov = 40;
    public Rigidbody2D m_Rigidbody2D;
    public ControllerPersonaje controllerPersonaje;
    public GameObject rendererCuerpo;
    public SpriteRenderer rendererViaje;
    public Collider2D colliderNormal;
    public Collider2D colliderViaje;
    public Vector3 m_Velocity = Vector3.zero;
    float originalspeed;
    public float originalGravity;
    public bool viajando;
    public GameObject nodoElegido;
    float elapsed;
    PlayerInput Pinput;
    ParticleSystem ps;
    public float fuerzaSalida = 200;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<Particulas>().particulasBolaViajeCables.gameObject.GetComponent<ParticleSystem>();
        ps.gameObject.SetActive(false);
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
            controllerPersonaje.dashBloqueado = true;
            m_Rigidbody2D.isKinematic = true;
            m_Rigidbody2D.gravityScale = 0;
            if (unavez == false)
            {
                //if (this.GetComponent<AudioManager>().sonidoLoop.isPlaying == false) this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidoLoop, this.GetComponent<AudioManager>().MoverseCables);
                m_Rigidbody2D.velocity = Vector2.zero;
                unavez = true;
                if (rendererCuerpo.gameObject != null) rendererCuerpo.gameObject.SetActive(false);
                rendererViaje.enabled = true;
                rendererViaje.gameObject.SetActive(true);
                colliderNormal.enabled = false;
                GetComponent<VidaPlayer>().enabled = false;
                colliderViaje.enabled = true;
                controllerPersonaje.dashBloqueado = true;
                controllerPersonaje.dashCaidaBloqueado = true;
            }
            if (this.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                conVelocidad = true;
            }
            else
            {
                conVelocidad = false;
            }
            if (nodoActual!=null&&!nodoActual.GetComponent<Nodo>().entrada)
            {

            
            if (Pinput.inputVertical > 0 && conVelocidad == false)
            {
                /*foreach (GameObject nodo in nodos)
                {
                    if (nodo.transform.position.x == nodoActual.transform.position.x && nodo.transform.position.y > nodoActual.transform.position.y)
                    {
                        speedMov = originalspeed;
                        nodoElegido = nodo;
                    }
                }*/


                rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.left);
                var em = ps.velocityOverLifetime;
                em.enabled = true;
                em.x = -8;
                em.y = 0;
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


                rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
                var em = ps.velocityOverLifetime;
                em.enabled = true;
                em.x = -8;
                em.y = 0;
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
            if (Pinput.inputHorizontal > 0 && conVelocidad == false)
            {
                //foreach (GameObject nodo in nodos)
                //{
                //    if (nodo.transform.position.y == nodoActual.transform.position.y && nodo.transform.position.x > nodoActual.transform.position.x)
                //    {
                //        speedMov = originalspeed;
                //        nodoElegido = nodo;
                //    }
                //}


                rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

                var em = ps.velocityOverLifetime;
                em.enabled = true;
                em.y = 0;
                em.x = -8;

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


                rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
                var em = ps.velocityOverLifetime;
                em.enabled = true;
                em.x = -8;
                em.y = 0;
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
            }
            if (nodoElegido != null)
            {
                //transform.position = Vector3.MoveTowards(transform.position, nodoElegido.transform.position, Time.deltaTime * speedMov);
                controllerPersonaje.rb.velocity = (transform.position - nodoElegido.transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(nodoElegido.transform.position.x, nodoElegido.transform.position.y, 0), Time.deltaTime * speedMov);
                //transform.rotation = Quaternion.Euler(0, 0, 0);
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
                //this.GetComponent<AudioManager>().Stop(this.GetComponent<AudioManager>().sonidoLoop);
                GetComponent<VidaPlayer>().enabled = true;
                m_Rigidbody2D.gravityScale = originalGravity;
                unavez = false;
                //if (rendererCuerpo != null) rendererCuerpo.enabled = true;
                if (rendererCuerpo != null) rendererCuerpo.gameObject.SetActive(true);
                //rendererCuerpo.gameObject.SetActive(true);
                rendererViaje.enabled = false;
                rendererViaje.gameObject.SetActive(false);
                colliderViaje.enabled = false;
                colliderNormal.enabled = true;
                controllerPersonaje.movimientoBloqueado = false;
                controllerPersonaje.dashBloqueado = false;
                controllerPersonaje.dashCaidaBloqueado = false;
                controllerPersonaje.saltoBloqueado = false;
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
            if (node.entrada)
            {

                if (node.entradaArriba)
                {
                    /*foreach (GameObject nodo in nodos)
                    {
                        if (nodo.transform.position.x == nodoActual.transform.position.x && nodo.transform.position.y > nodoActual.transform.position.y)
                        {
                            speedMov = originalspeed;
                            nodoElegido = nodo;
                        }
                    }*/


                    rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.left);
                    var em = ps.velocityOverLifetime;
                    em.enabled = true;
                    em.x = -8;
                    em.y = 0;
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
                if (node.entradaAbajo)
                {
                    //foreach (GameObject nodo in nodos)
                    //{
                    //    if (nodo.transform.position.x == nodoActual.transform.position.x && nodo.transform.position.y < nodoActual.transform.position.y && nodo != nodoActual)
                    //    {
                    //        speedMov = originalspeed;
                    //        nodoElegido = nodo;
                    //    }
                    //}


                    rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
                    var em = ps.velocityOverLifetime;
                    em.enabled = true;
                    em.x = -8;
                    em.y = 0;
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
                if (node.entradaDerecha)
                {
                    //foreach (GameObject nodo in nodos)
                    //{
                    //    if (nodo.transform.position.y == nodoActual.transform.position.y && nodo.transform.position.x > nodoActual.transform.position.x)
                    //    {
                    //        speedMov = originalspeed;
                    //        nodoElegido = nodo;
                    //    }
                    //}


                    rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

                    var em = ps.velocityOverLifetime;
                    em.enabled = true;
                    em.y = 0;
                    em.x = -8;

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
                if (node.entradaIzquierda)
                {
                    //foreach (GameObject nodo in nodos)
                    //{
                    //    if (nodo.transform.position.y == nodoActual.transform.position.y && nodo.transform.position.x < nodoActual.transform.position.x)
                    //    {

                    //        speedMov = originalspeed;
                    //        nodoElegido = nodo;
                    //    }
                    //}


                    rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
                    var em = ps.velocityOverLifetime;
                    em.enabled = true;
                    em.x = -8;
                    em.y = 0;
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

            }

            if (!node.entrada && !node.salida) controllerPersonaje.rb.velocity = Vector2.zero;
            if (node.salida)
            {if (GetComponent<ControllerPersonaje>().saltoDobleHecho == true) GetComponent<ControllerPersonaje>().saltoDobleHecho =false;
                if (node.salida == false && viajando == true)
                {
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().pasarPorNodo);
                    this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z);

                }
                if (node.salidaAbajo == true && viajando == true)
                {
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().salidaCables);
                    this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) + new Vector3(0, -2);
                    viajando = false;

                    m_Rigidbody2D.isKinematic = false;
                    //
                    if (unavez == true)
                    {
                        //this.GetComponent<AudioManager>().Stop(this.GetComponent<AudioManager>().sonidoLoop);
                        GetComponent<VidaPlayer>().enabled = true;
                        m_Rigidbody2D.gravityScale = originalGravity;
                        unavez = false;
                        //if (rendererCuerpo != null) rendererCuerpo.enabled = true;
                        if (rendererCuerpo != null) rendererCuerpo.gameObject.SetActive(true);
                        //rendererCuerpo.gameObject.SetActive(true);
                        rendererViaje.enabled = false;
                        rendererViaje.gameObject.SetActive(false);
                        colliderViaje.enabled = false;
                        colliderNormal.enabled = true;
                        controllerPersonaje.movimientoBloqueado = false;
                        controllerPersonaje.dashBloqueado = false;
                        controllerPersonaje.dashCaidaBloqueado = false;
                        controllerPersonaje.saltoBloqueado = false;
                    }
                    this.GetComponent<ControllerPersonaje>().rb.AddForce(new Vector2(0, -1) * fuerzaSalida, ForceMode2D.Impulse); nodoElegido = null;
                }
                else if (node.salidaArriba == true && viajando == true)
                {
                    // GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().salidaCables);
                    this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) + new Vector3(0, +2);
                    viajando = false;
                    m_Rigidbody2D.isKinematic = false;
                    //
                    if (unavez == true)
                    {
                        //this.GetComponent<AudioManager>().Stop(this.GetComponent<AudioManager>().sonidoLoop);
                        GetComponent<VidaPlayer>().enabled = true;
                        m_Rigidbody2D.gravityScale = originalGravity;
                        unavez = false;
                        //if (rendererCuerpo != null) rendererCuerpo.enabled = true;
                        if (rendererCuerpo != null) rendererCuerpo.gameObject.SetActive(true);
                        //rendererCuerpo.gameObject.SetActive(true);
                        rendererViaje.enabled = false;
                        rendererViaje.gameObject.SetActive(false);
                        colliderViaje.enabled = false;
                        colliderNormal.enabled = true;
                        controllerPersonaje.movimientoBloqueado = false;
                        controllerPersonaje.dashBloqueado = false;
                        controllerPersonaje.dashCaidaBloqueado = false;
                        controllerPersonaje.saltoBloqueado = false;
                    }
                    this.GetComponent<ControllerPersonaje>().rb.AddForce(new Vector2(0, 1) * fuerzaSalida, ForceMode2D.Impulse); nodoElegido = null;

                }
                else if (node.salidaDerecha == true && viajando == true)
                {
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().salidaCables);
                    this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) + new Vector3(2, 0);
                    viajando = false;
                    m_Rigidbody2D.isKinematic = false;
                    //
                    if (unavez == true)
                    {
                        //this.GetComponent<AudioManager>().Stop(this.GetComponent<AudioManager>().sonidoLoop);
                        GetComponent<VidaPlayer>().enabled = true;
                        m_Rigidbody2D.gravityScale = originalGravity;
                        unavez = false;
                        //if (rendererCuerpo != null) rendererCuerpo.enabled = true;
                        if (rendererCuerpo != null) rendererCuerpo.gameObject.SetActive(true);
                        //rendererCuerpo.gameObject.SetActive(true);
                        rendererViaje.enabled = false;
                        rendererViaje.gameObject.SetActive(false);
                        colliderViaje.enabled = false;
                        colliderNormal.enabled = true;
                        controllerPersonaje.movimientoBloqueado = false;
                        controllerPersonaje.dashBloqueado = false;
                        controllerPersonaje.dashCaidaBloqueado = false;
                        controllerPersonaje.saltoBloqueado = false;
                    }
                    this.GetComponent<ControllerPersonaje>().rb.AddForce(new Vector2(1, 0) * fuerzaSalida, ForceMode2D.Impulse); nodoElegido = null;

                }
                else if (node.salidaIzquierda == true && viajando == true)
                {
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidosUnaVez, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().salidaCables);
                    this.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, this.transform.position.z) + new Vector3(-2, 0);
                    viajando = false;
                    nodoElegido = null;
                    m_Rigidbody2D.isKinematic = false;
                    //
                    if (unavez == true)
                    {
                        //this.GetComponent<AudioManager>().Stop(this.GetComponent<AudioManager>().sonidoLoop);
                        GetComponent<VidaPlayer>().enabled = true;
                        m_Rigidbody2D.gravityScale = originalGravity;
                        unavez = false;
                        //if (rendererCuerpo != null) rendererCuerpo.enabled = true;
                        if (rendererCuerpo != null) rendererCuerpo.gameObject.SetActive(true);
                        //rendererCuerpo.gameObject.SetActive(true);
                        rendererViaje.enabled = false;
                        rendererViaje.gameObject.SetActive(false);
                        colliderViaje.enabled = false;
                        colliderNormal.enabled = true;
                        controllerPersonaje.movimientoBloqueado = false;
                        controllerPersonaje.dashBloqueado = false;
                        controllerPersonaje.dashCaidaBloqueado = false;
                        controllerPersonaje.saltoBloqueado = false;
                    }
                    this.GetComponent<ControllerPersonaje>().rb.AddForce(new Vector2(-1, 0) * fuerzaSalida, ForceMode2D.Impulse);

                }
                //FindObjectOfType<NewAudioManager>().Play("PlayerCable");
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
        else if (distanciaEntreNodos * 0.1 > distanciaAlObjetivo)
        {
            speedMov = originalspeed * 0.8f * Mathf.Clamp(distanciaAlObjetivo, 0.9f, 1.1f);

        }
        else
        {
            speedMov = originalspeed;
        }
        if (distanciaAlObjetivo == 0)
        {
            GetComponent<Particulas>().particulasViajeCables.SetActive(false);

            GetComponent<Particulas>().particulasBolaViajeCables.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            var em = ps.velocityOverLifetime;
            em.x = 0;
            em.y = 0;
            em.z = 0;
            em.enabled = false;

        }
        else
        {
            GetComponent<Particulas>().particulasViajeCables.SetActive(true);

            GetComponent<Particulas>().particulasBolaViajeCables.gameObject.transform.localScale = new Vector3(1, 1, 1);
            var em = ps.velocityOverLifetime;
            em.enabled = true;

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
                if (GetComponent<ControllerPersonaje>().saltoDobleHecho == true) GetComponent<ControllerPersonaje>().saltoDobleHecho = false;
                if (viajando == true)
                {
                    if (node.salida == true)
                    {
                        viajando = false;
                    }
                }
            }
            else
            {
                if (node.entrada)
                {
                  
                    if (node.entradaArriba)
                    {
                        /*foreach (GameObject nodo in nodos)
                        {
                            if (nodo.transform.position.x == nodoActual.transform.position.x && nodo.transform.position.y > nodoActual.transform.position.y)
                            {
                                speedMov = originalspeed;
                                nodoElegido = nodo;
                            }
                        }*/


                        rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.left);
                        var em = ps.velocityOverLifetime;
                        em.enabled = true;
                        em.x = -8;
                        em.y = 0;
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
                    if (node.entradaAbajo)
                    {
                        //foreach (GameObject nodo in nodos)
                        //{
                        //    if (nodo.transform.position.x == nodoActual.transform.position.x && nodo.transform.position.y < nodoActual.transform.position.y && nodo != nodoActual)
                        //    {
                        //        speedMov = originalspeed;
                        //        nodoElegido = nodo;
                        //    }
                        //}


                        rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
                        var em = ps.velocityOverLifetime;
                        em.enabled = true;
                        em.x = -8;
                        em.y = 0;
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
                    if (node.entradaDerecha)
                    {
                        //foreach (GameObject nodo in nodos)
                        //{
                        //    if (nodo.transform.position.y == nodoActual.transform.position.y && nodo.transform.position.x > nodoActual.transform.position.x)
                        //    {
                        //        speedMov = originalspeed;
                        //        nodoElegido = nodo;
                        //    }
                        //}


                        rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

                        var em = ps.velocityOverLifetime;
                        em.enabled = true;
                        em.y = 0;
                        em.x = -8;

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
                    if (node.entradaIzquierda)
                    {
                        //foreach (GameObject nodo in nodos)
                        //{
                        //    if (nodo.transform.position.y == nodoActual.transform.position.y && nodo.transform.position.x < nodoActual.transform.position.x)
                        //    {

                        //        speedMov = originalspeed;
                        //        nodoElegido = nodo;
                        //    }
                        //}


                        rendererViaje.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
                        var em = ps.velocityOverLifetime;
                        em.enabled = true;
                        em.x = -8;
                        em.y = 0;
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

                }
                if (node.entrada == false && node.salida == false)
                {
                    controllerPersonaje.rb.velocity = Vector2.zero;
                }

            }
        }
    }
}
