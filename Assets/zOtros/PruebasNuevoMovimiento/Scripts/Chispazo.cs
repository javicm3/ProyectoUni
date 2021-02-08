using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chispazo : MonoBehaviour
{
    private Rigidbody2D rb;
    private ControllerPersonaje controlCC;
    GameObject reboteObj;
    Collider2D areaDeteccion;
    public bool rebotePerdido = false;
    public float tiempoMaxRebote = 1f;
    public float auxtiempoRebote;
    float realFuerzaAtraccionRebote;
    public float fuerzaAtraccionRebote = 300;
    public bool puedeRebotar;
    public float saltoBouncerFuerza = 300;
    bool unavezSaltoLibre = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        //areaDeteccion = GetComponent<CircleCollider2D>();
        controlCC = GetComponentInParent<ControllerPersonaje>();
        auxtiempoRebote = tiempoMaxRebote;
        realFuerzaAtraccionRebote = fuerzaAtraccionRebote;

    }

    // Update is called once per frame
    void Update()
    {
        if (puedeRebotar)
        {
            auxtiempoRebote -= Time.deltaTime;
            controlCC.movimientoBloqueado = true;
            controlCC.saltoBloqueado = true;
            unavezSaltoLibre = true;
            Vector3 pz2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz2.z = 0;

            Vector2 direccion2 = (pz2 - this.transform.position).normalized;

            if (Input.GetButtonDown("Jump"))
            {
                print("reboto");
                puedeRebotar = false;


                rb.AddForce(new Vector2(direccion2.normalized.x * 1.5f, direccion2.normalized.y) * saltoBouncerFuerza);
                controlCC.PermitirGravedad();
                rebotePerdido = true;
                controlCC.dashBloqueado = false;
                controlCC.dashCaidaBloqueado = false;
            }


        }
        else
        {
            if (unavezSaltoLibre == true)
            {
                unavezSaltoLibre = false;
                controlCC.saltoBloqueado = false;
                controlCC.movimientoBloqueado = false;
            }

        }
    }
    private void OnTriggerStay2D(Collider2D rebote)
    {
        if (rebote.gameObject.tag == "Enemigo")
        {

            controlCC.tocandoRebote = true;
            reboteObj = rebote.gameObject;
            print("deteccion");

            Vector2 direccion = transform.position - rebote.transform.position;

            float distancia = Vector2.Distance(transform.position, rebote.transform.position);
            realFuerzaAtraccionRebote = fuerzaAtraccionRebote + distancia * 10;

            if (rebotePerdido == false)
            {
                controlCC.AnularGravedad();
                if (distancia < 1.1f)
                {
                    print(auxtiempoRebote);
                    print(distancia + "dist");
                    controlCC.constantegravedad = 1;
                    controlCC.dashEnCaida = false;
                    controlCC.animCC.SetBool("cayendo", controlCC.dashEnCaida);
                    controlCC.dashBloqueado = true;
                    controlCC.dashCaidaBloqueado = true;


                    if (auxtiempoRebote > 0)
                    {
                        controlCC.saltoDobleHecho = false;
                        rb.velocity = Vector2.zero;
                        puedeRebotar = true;
                        this.transform.position = rebote.transform.position;

                    }
                    else
                    {
                        auxtiempoRebote = tiempoMaxRebote;
                        puedeRebotar = false;
                        rebotePerdido = true;
                        controlCC.PermitirGravedad();
                    }




                }
                else if (distancia > 1.1f)
                {
                    puedeRebotar = false;

                    rb.AddForce(-direccion.normalized * fuerzaAtraccionRebote * Time.deltaTime);
                }
            }


            //if (distancia <= 0)
            //{
            //    controlCC.tieneGravedad = true;
            //    rebotePerdido = true;
            //}

            //if (distancia > 0 && distancia <= 2.5f)
            //{
            //    puedeRebotar = true;
            //}

        }
    }

    private void OnTriggerExit2D(Collider2D rebote)
    {
        if (rebote.gameObject.tag == "Enemigo")
        {
            auxtiempoRebote = tiempoMaxRebote;
            controlCC.saltoBloqueado = false;
            controlCC.PermitirGravedad();
            rebotePerdido = false;
            controlCC.tocandoRebote = false;
        }
    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FuncionalidadRebote : MonoBehaviour
//{
//    private Rigidbody2D rb;
//    private ControllerPersonaje controlCC;
//    GameObject reboteObj;
//    Collider2D areaDeteccion;
//    public bool rebotePerdido = false;
//    public float tiempoMaxRebote = 1f;
//    public float auxtiempoRebote;
//    float realFuerzaAtraccionRebote;
//    public float fuerzaAtraccionRebote = 300;
//    public bool puedeRebotar;
//    public float saltoBouncerFuerza = 300;
//    public bool unaVezSalirSalto = false;
//    bool unavezSaltoLibre = false;
//    // Start is called before the first frame update
//    void Start()
//    {
//        rb = this.GetComponent<Rigidbody2D>();
//        //areaDeteccion = GetComponent<CircleCollider2D>();
//        controlCC = GetComponentInParent<ControllerPersonaje>();
//        auxtiempoRebote = tiempoMaxRebote;
//        realFuerzaAtraccionRebote = fuerzaAtraccionRebote;

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (puedeRebotar)
//        {
//            controlCC.movimientoBloqueado = true;
//            controlCC.saltoBloqueado = true;
//            unavezSaltoLibre = true;
//            Vector3 pz2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            pz2.z = 0;

//            Vector2 direccion2 = (pz2 - this.transform.position).normalized;

//            if (Input.GetButtonDown("Jump"))
//            {
//                print("reboto");
//                puedeRebotar = false;


//                rb.AddForce(new Vector2(direccion2.normalized.x * 1.5f, direccion2.normalized.y) * saltoBouncerFuerza);
//                controlCC.PermitirGravedad();
//                rebotePerdido = true;
//                controlCC.dashBloqueado = false;
//                controlCC.dashCaidaBloqueado = false;
//            }


//        }
//        else
//        {
//            if (unavezSaltoLibre == true)
//            {
//                unavezSaltoLibre = false;
//                controlCC.saltoBloqueado = false;
//                controlCC.movimientoBloqueado = false;
//            }

//        }

//        if (controlCC.tocandoRebote)
//        {
//            print("deteccion");

//            Vector2 direccion = transform.position - reboteObj.transform.position;

//            float distancia = Vector2.Distance(transform.position, reboteObj.transform.position);
//            realFuerzaAtraccionRebote = fuerzaAtraccionRebote + distancia * 20;

//            if (rebotePerdido == false)
//            {
//                controlCC.AnularGravedad();
//                if (distancia < 1.1f)
//                {
//                    auxtiempoRebote -= Time.deltaTime;
//                    print(distancia + "dist");
//                    controlCC.constantegravedad = 1;
//                    controlCC.dashEnCaida = false;
//                    controlCC.dashBloqueado = true;
//                    controlCC.dashCaidaBloqueado = true;


//                    if (auxtiempoRebote > 0)
//                    {
//                        rb.velocity = Vector2.zero;
//                        puedeRebotar = true;
//                        this.transform.position = reboteObj.transform.position;
//                    }
//                    else
//                    {
//                        auxtiempoRebote = tiempoMaxRebote;
//                        puedeRebotar = false;
//                        rebotePerdido = true;
//                        controlCC.PermitirGravedad();
//                    }




//                }
//                else if (distancia > 1.1f)
//                {
//                    puedeRebotar = false;

//                    rb.AddForce(-direccion.normalized * fuerzaAtraccionRebote * Time.deltaTime);
//                }
//            }
//        }
//        else
//        {
//            if (unaVezSalirSalto == false)
//            {
//                unaVezSalirSalto = true;

//                auxtiempoRebote = tiempoMaxRebote;
//                controlCC.saltoBloqueado = false;
//                controlCC.PermitirGravedad();
//                rebotePerdido = false;
//            }
//        }

//    }
//    private void OnTriggerStay2D(Collider2D rebote)
//    {
//        if (rebote.gameObject.tag == "Bouncer")
//        {
//            unaVezSalirSalto = false;
//            controlCC.tocandoRebote = true;
//            reboteObj = rebote.gameObject;
//            print(reboteObj.name);


//            //if (distancia <= 0)
//            //{
//            //    controlCC.tieneGravedad = true;
//            //    rebotePerdido = true;
//            //}

//            //if (distancia > 0 && distancia <= 2.5f)
//            //{
//            //    puedeRebotar = true;
//            //}

//        }
//    }

//    private void OnTriggerExit2D(Collider2D rebote)
//    {
//        if (rebote.gameObject.tag == "Bouncer")
//        {

//            controlCC.tocandoRebote = false;
//        }
//    }
//}
