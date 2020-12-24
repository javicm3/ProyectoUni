using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViajeCables : MonoBehaviour
{
    public float originalGravity;
    public bool viajando;
    bool unavez = false;
    public Vector3 ordenDireccion;
    public bool inputEnabled = false;
    public float inputPreferido;
    Vector2 targetVelocity;
    public float speedMov = 10;
    public Rigidbody2D m_Rigidbody2D;
    //public CharacterController2D char2d;
    public Movimiento mov;
    public SpriteRenderer rendererCuerpo;
    public SpriteRenderer rendererViaje;
    public Collider2D colliderBueno;
    public Collider2D colliderViaje;
    public Vector3 m_Velocity = Vector3.zero;
    float originalspeed;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    // Start is called before the first frame update
    void Start()
    {
        originalspeed = speedMov;
        originalGravity = m_Rigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (viajando)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                speedMov = originalspeed * 2f;
            }
            else
            {
                speedMov = originalspeed;
            }
            m_Rigidbody2D.gravityScale = 0;
            if (unavez == false)
            {
                print("Deeeeeact");
                m_Rigidbody2D.velocity = Vector2.zero;
                unavez = true;
                //rendererCuerpo.enabled = false;
                rendererCuerpo.gameObject.SetActive(false);
                rendererViaje.enabled = true;
                //char2d.enabled = false;
                mov.enabled = false;
                colliderBueno.enabled = false;
                colliderViaje.enabled = true;
            }
            print(ordenDireccion);
            m_Rigidbody2D.velocity = ordenDireccion.normalized * speedMov;
        }
        else
        {
            m_Rigidbody2D.gravityScale = originalGravity;
            if (unavez == true)
            {
                print("Desact");
                unavez = false;
                rendererCuerpo.enabled = true;
                rendererCuerpo.gameObject.SetActive(true);
                rendererViaje.enabled = false;
                //char2d.enabled = true;
                mov.enabled = true; colliderViaje.enabled = false;
                colliderBueno.enabled = true;

            }
        }
        if (inputEnabled)
        {
            if (Input.GetKey(KeyCode.W))
            {
                inputPreferido = 0;
                //inputPreferido =new Vector2(0, 1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputPreferido = 1;
                //inputPreferido = new Vector2(1,0 );
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputPreferido = 2;
                //inputPreferido = new Vector2(0, -1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputPreferido = 3;
                //inputPreferido = new Vector2(-1,0);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Nodo")
        {
            Nodo node = collision.gameObject.GetComponent<Nodo>();
            if (node.salida == false && viajando == true)
            {
                this.transform.position = node.transform.position;
            }
            else if (node.salida == false)
            {


                if (node.numeroDirecciones > 0)
                {
                    if (node.puedeAbajo && inputPreferido == 1)
                    {
                        ordenDireccion = new Vector2(0, -1);
                    }
                    else if (node.puedeArriba && inputPreferido == 0)
                    {
                        ordenDireccion = new Vector2(0, 1);
                    }

                    else if (node.puedeDerecha && inputPreferido == 2)
                    {
                        ordenDireccion = new Vector2(1, 0);
                    }

                    else if (node.puedeIzquierda && inputPreferido == 3)
                    {
                        ordenDireccion = new Vector2(-1, 0);
                    }

                    else if (node.puedeAbajo && inputPreferido != 1)
                    {
                        if (node.direccionDefault == 0)
                        {
                            ordenDireccion = new Vector2(0, 1);
                        }
                        else if (node.direccionDefault == 1)
                        {
                            ordenDireccion = new Vector2(0, -1);
                        }
                        else if (node.direccionDefault == 2)
                        {
                            ordenDireccion = new Vector2(1, 0);
                        }
                        else if (node.direccionDefault == 3)
                        {
                            ordenDireccion = new Vector2(-1, 0);
                        }

                    }
                    else if (node.puedeArriba && inputPreferido != 0)
                    {
                        if (node.direccionDefault == 0)
                        {
                            ordenDireccion = new Vector2(0, 1);
                        }
                        else if (node.direccionDefault == 1)
                        {
                            ordenDireccion = new Vector2(0, -1);
                        }
                        else if (node.direccionDefault == 2)
                        {
                            ordenDireccion = new Vector2(1, 0);
                        }
                        else if (node.direccionDefault == 3)
                        {
                            ordenDireccion = new Vector2(-1, 0);
                        }
                    }
                    else if (node.puedeDerecha && inputPreferido != 2)
                    {
                        if (node.direccionDefault == 0)
                        {
                            ordenDireccion = new Vector2(0, 1);
                        }
                        else if (node.direccionDefault == 1)
                        {
                            ordenDireccion = new Vector2(0, -1);
                        }
                        else if (node.direccionDefault == 2)
                        {
                            ordenDireccion = new Vector2(1, 0);
                        }
                        else if (node.direccionDefault == 3)
                        {
                            ordenDireccion = new Vector2(-1, 0);
                        }
                    }
                    else if (node.puedeIzquierda && inputPreferido != 3)
                    {
                        if (node.direccionDefault == 0)
                        {
                            ordenDireccion = new Vector2(0, 1);
                        }
                        else if (node.direccionDefault == 1)
                        {
                            ordenDireccion = new Vector2(0, -1);
                        }
                        else if (node.direccionDefault == 2)
                        {
                            ordenDireccion = new Vector2(1, 0);
                        }
                        else if (node.direccionDefault == 3)
                        {
                            ordenDireccion = new Vector2(-1, 0);
                        }
                    }
                }
                else
                {
                    if (node.puedeAbajo)
                    {
                        ordenDireccion = new Vector2(0, -1);
                    }
                    else if (node.puedeArriba)
                    {
                        ordenDireccion = new Vector2(0, 1);
                    }
                    else if (node.puedeDerecha)
                    {
                        ordenDireccion = new Vector2(1, 0);
                    }
                    else if (node.puedeIzquierda)
                    {
                        ordenDireccion = new Vector2(-1, 0);
                    }
                }
            }
            else
            {
                if (viajando == true)
                {
                    if (node.salidaAbajo == true)
                    {
                        this.transform.position = node.transform.position + new Vector3(0, -2);
                        viajando = false;
                    }
                    else if (node.salidaArriba == true)
                    {
                        this.transform.position = node.transform.position + new Vector3(0, +2);
                        viajando = false;
                    }
                    else if (node.salidaDerecha == true)
                    {
                        this.transform.position = node.transform.position + new Vector3(2, 0);
                        viajando = false;
                    }
                    else if (node.salidaIzquierda == true)
                    {
                        this.transform.position = node.transform.position + new Vector3(-2, 0);
                        viajando = false;
                    }
                    viajando = false;
                }

            }

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InputEnabledCables")
        {
            inputEnabled = true;
        }
        else
        {
            inputEnabled = false;
        }
        if (collision.gameObject.tag == "Nodo")
        {
            Nodo node = collision.gameObject.GetComponent<Nodo>();
            if (node.salida == false)
            {


                if (node.numeroDirecciones > 0)
                {
                    if (node.puedeAbajo && inputPreferido == 1)
                    {
                        ordenDireccion = new Vector2(0, -1);
                    }
                    else if (node.puedeArriba && inputPreferido == 0)
                    {
                        ordenDireccion = new Vector2(0, 1);
                    }

                    else if (node.puedeDerecha && inputPreferido == 2)
                    {
                        ordenDireccion = new Vector2(1, 0);
                    }

                    else if (node.puedeIzquierda && inputPreferido == 3)
                    {
                        ordenDireccion = new Vector2(-1, 0);
                    }

                    else if (node.puedeAbajo && inputPreferido != 1)
                    {
                        if (node.direccionDefault == 0)
                        {
                            ordenDireccion = new Vector2(0, 1);
                        }
                        else if (node.direccionDefault == 1)
                        {
                            ordenDireccion = new Vector2(0, -1);
                        }
                        else if (node.direccionDefault == 2)
                        {
                            ordenDireccion = new Vector2(1, 0);
                        }
                        else if (node.direccionDefault == 3)
                        {
                            ordenDireccion = new Vector2(-1, 0);
                        }

                    }
                    else if (node.puedeArriba && inputPreferido != 0)
                    {
                        if (node.direccionDefault == 0)
                        {
                            ordenDireccion = new Vector2(0, 1);
                        }
                        else if (node.direccionDefault == 1)
                        {
                            ordenDireccion = new Vector2(0, -1);
                        }
                        else if (node.direccionDefault == 2)
                        {
                            ordenDireccion = new Vector2(1, 0);
                        }
                        else if (node.direccionDefault == 3)
                        {
                            ordenDireccion = new Vector2(-1, 0);
                        }
                    }
                    else if (node.puedeDerecha && inputPreferido != 2)
                    {
                        if (node.direccionDefault == 0)
                        {
                            ordenDireccion = new Vector2(0, 1);
                        }
                        else if (node.direccionDefault == 1)
                        {
                            ordenDireccion = new Vector2(0, -1);
                        }
                        else if (node.direccionDefault == 2)
                        {
                            ordenDireccion = new Vector2(1, 0);
                        }
                        else if (node.direccionDefault == 3)
                        {
                            ordenDireccion = new Vector2(-1, 0);
                        }
                    }
                    else if (node.puedeIzquierda && inputPreferido != 3)
                    {
                        if (node.direccionDefault == 0)
                        {
                            ordenDireccion = new Vector2(0, 1);
                        }
                        else if (node.direccionDefault == 1)
                        {
                            ordenDireccion = new Vector2(0, -1);
                        }
                        else if (node.direccionDefault == 2)
                        {
                            ordenDireccion = new Vector2(1, 0);
                        }
                        else if (node.direccionDefault == 3)
                        {
                            ordenDireccion = new Vector2(-1, 0);
                        }
                    }
                }
                else
                {
                    if (node.puedeAbajo)
                    {
                        ordenDireccion = new Vector2(0, -1);
                    }
                    else if (node.puedeArriba)
                    {
                        ordenDireccion = new Vector2(0, 1);
                    }
                    else if (node.puedeDerecha)
                    {
                        ordenDireccion = new Vector2(1, 0);
                    }
                    else if (node.puedeIzquierda)
                    {
                        ordenDireccion = new Vector2(-1, 0);
                    }
                }
            }
            else
            {
                if (viajando == true)
                {
                    if (node.salidaAbajo == true)
                    {
                        this.transform.position = node.transform.position + new Vector3(0, -2);
                        viajando = false;
                    }
                    else if (node.salidaArriba == true)
                    {
                        this.transform.position = node.transform.position + new Vector3(0, +2);
                        viajando = false;
                    }
                    else if (node.salidaDerecha == true)
                    {
                        this.transform.position = node.transform.position + new Vector3(2, 0);
                        viajando = false;
                    }
                    else if (node.salidaIzquierda == true)
                    {
                        this.transform.position = node.transform.position + new Vector3(-2, 0);
                        viajando = false;
                    }
                }

            }
        }

    }
}
