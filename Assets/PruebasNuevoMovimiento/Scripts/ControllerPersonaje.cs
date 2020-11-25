using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPersonaje : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] private PlayerInput pInput;


    public bool tocandoRebote = false;




    Vector2 ultimaParedPosicion;

    [Header("EscaladaYSaltoPared")]
    public float distanciaPared;
    public bool lastJumpPared = false;
    public bool deteccionParedes = true;
    public bool deteccionParedes2 = true;
    public bool pegadoPared = false;
    public float maxTiempoPared = 10f;
    public float tiempoPared;
    float auxpared;
    bool unavezsalirpared = false;
    public float fuerzaSaltoPared = 100;
    public float speedpared = 10;
    public float speed;
    public bool tengoMaxspeed = false;
    public float capSpeed;
    public bool capSpeedUnavez = false;

    [Header("Correr")]

    [Space(5)]

    [SerializeField] private float distanciaAlSuelo = 1.35f;

    [Space(5)]

    [SerializeField] private float velMinima = 2;

    [Space(5)]

    [SerializeField] private float velMaxima = 6;

    [Space(5)]

    [SerializeField] private float coefAceleracion = 1.5f;

    [Space(5)]

    [SerializeField] private float coefDeceleracion = 0.5f;

    [Space(5)]

    public float fuerzaAtraccionLoop = 2000f;

    [Header("CambiosSentido")]
    public float fuerzaCambioSentido = 30f;
    public float fuerzaCambioSentidoAire = 120f;

    [Header("Salto")]
    public float fuerzaSaltoMax = 11;
    public float fuerzaSaltoMin = 4;
    public float fSaltoInicial = 7;
    public float fSaltoMedio = 9;
    public float fSaltoAlto = 10;
    public bool saltoDobleHecho = false;
    public float fuerzaSaltoDoble = 100;
    public float tiempoSaltoCompleto = 1.2f;
    public float constantegravedad = 3;
    public float tiempoPreSalto = 0.5f;
    public float tiempoPulsadoEspacio;
    public float auxpresalto;


    [Header("Dash")]
    public float fuerzaDash = 100;
    public float fuerzaDashCaida = 100;
    public float tiempoDasheo = 1f;
    public bool estoyDasheando = true;
    public float auxdash;
    public float cooldownDash = 1.5f;
    public float auxCdDash;

    [Header("EscalarEsquinas")]
    public float distanciaBorde = 5;
    public Transform puntoCheckBorde;


    [Header("Chispazo")]
    public bool saltoInmediato = false;
    public GameObject ultimoEnemigoDetectado;
    public float distanciaChispazo = 10f;
    public Vector3 destinoChispazo;
    public bool haciendoChispazo = false;
    public bool puedeSalirChispazo = false;
    public float tiempoAntesChispazo = 1f;
    public float auxTiempoChispazo;
    public bool unavezSalirChispazo = false;
    public float fuerzaSalidaChispazo = 500f;
    public float fuerzaAcercarseChispazo = 500f;
    public bool chispazoPerdido = false;
    public float fuerzaChispazoPerdido = 200f;

    [Header("SaltoParedYOtrasVariables")]
    public bool tocando;
    public GameObject detectorPared;
    public float tiempoTrasSaltoPared = 0.5f;
    ManagerEnergia mEnergy;
    public float auxtiempoTrasSaltoPared = 0;
    public Transform posGround;
    public GameObject ultimoenemigo2;
    public Transform VFX;
    bool entradochispazo = false;
    bool unavezSaltoDobleTrasPared = false;
    public bool enemigoCerca = false;
    GameObject enemigoSeleccionado = null;

    [Header("Loops")]
    float tiempoTrasSaltoLoop = 0.3f;
    float auxTiempoTrasSaltoLoop;
    public bool looping = false;
    [SerializeField] public Vector2 normal;
    [SerializeField] public Vector2 ultimaNormal;
    [Header("VariablesQueNoSeDondeMeter")]
    public Vector2 puntoChoque;
    public bool pulsadoEspacio = false;
    public bool saltoIniciado = false;
    public bool grounded;
    public bool tieneGravedad;
    public bool dashEnCaida;
    public bool movimientoBloqueado;
    public bool dashBloqueado;
    public bool saltoBloqueado = true;
    public bool dashCaidaBloqueado;
    public bool subiendoUnavez;
    public bool movParedBloq = false;
    public bool tocandoderecha;
    public bool tocandoizquierda;
    public bool cambioSentidoReciente = false;
    public float tiempoTrasCambioSentido = 0.1f;
    public bool saltoDobleReciente = false;
    public float tiempoTrasSaltoDoble = 0.15f;
    public RaycastHit2D tocandoBorde;
    public RaycastHit2D tocandoBorde2;
    public RaycastHit2D derecha;
    public RaycastHit2D izquierda;
    public float tiempomaxTrasTocarsuelo = 0.2f;
    float originalgravity;
    bool invertirValores = false;
    float auxtiempoMaxSuelo;
    Vector2 speedAntes;
    Vector2 ledgePos1;
    Vector2 ledgePos2;
    Vector2 ledgePos3;
    Vector2 normal2;
    Vector2 normal3;
    public Animator animCC;

    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;
    public float ledgeClimbXOffset3 = 0f;
    public float ledgeClimbYOffset3 = 0f;

    [SerializeField] private LayerMask capasSuelo;
    [SerializeField] private LayerMask capasEnemigos;

    // Start is called before the first frame update

    void Start()
    {

        auxTiempoChispazo = tiempoAntesChispazo;
        auxpared = maxTiempoPared;
        auxtiempoMaxSuelo = tiempomaxTrasTocarsuelo;
        auxdash = tiempoDasheo;
        auxpresalto = tiempoPreSalto;
        rb = this.GetComponent<Rigidbody2D>();
        pInput = this.GetComponent<PlayerInput>();
        animCC = GetComponentInChildren<Animator>();
        mEnergy = GetComponent<ManagerEnergia>();
        originalgravity = rb.gravityScale;
        auxCdDash = cooldownDash;
    }

    // Update is called once per frame
    void Update()
    {
        //print(rb.velocity + "velocidad");
        if (auxCdDash > 0) auxCdDash -= Time.deltaTime;
        if (auxtiempoTrasSaltoPared > 0)
        {
            movParedBloq = true;
            auxtiempoTrasSaltoPared -= Time.deltaTime;
            if (unavezSaltoDobleTrasPared == false)
            {
                unavezSaltoDobleTrasPared = true;
                saltoDobleHecho = false;
            }
        }
        else
        {
            unavezSaltoDobleTrasPared = false;
            movParedBloq = false;
            auxtiempoTrasSaltoPared = 0;
        }

        if (grounded)
        {
            dashBloqueado = false;
        }

        if (!saltoBloqueado)
        {
            SaltoNormal();
        }

        if (deteccionParedes) if (deteccionParedes2) if (ultimaNormal.y > -0.9f) DetectarPared();
        /*    if (pegadoPared == false)*/
        if (!movParedBloq) ComprobarParedes();




        if (!dashCaidaBloqueado)
        {
            DashEnCaida();
        }

        animCC.SetFloat("SpeedY", rb.velocity.y);
        animCC.SetBool("Grounded", grounded);
        animCC.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        DetectarEnemigos();
        Chispazo();
    }
    private void FixedUpdate()
    {
        DetectarSuelo();

      
        if (!movimientoBloqueado)
        {
            MoverPersonaje();
        }
        GirarPersonaje();
        MovimientoPared();
        if (!dashBloqueado)
        {
            Dash();
        }




        if (tieneGravedad)
        {
            AplicarGravedad();
        }
    }
    public void DetectarEnemigos()
    {
        //DE TECTAR ENEMIGOS AL PASAR RATON
        Vector3 pz2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz2.z = 0;
        Vector3 direction = pz2 - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), direction, 10000, capasEnemigos);
        Debug.DrawRay(Input.mousePosition, direction);


        if (hit != false)
        {

            if (hit.collider.tag == "Enemigo")
            {
                if (haciendoChispazo == false) ultimoEnemigoDetectado = hit.collider.gameObject;
            }
            else
            {
                if (haciendoChispazo == false) ultimoEnemigoDetectado = null;
            }

        }
        else
        {
            if (haciendoChispazo == false) ultimoEnemigoDetectado = null;
        }
    }
    public void PonerCollider()
    {
        ultimoenemigo2.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
    }
    public void Chispazo()
    {
        if (ultimoEnemigoDetectado != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, ultimoEnemigoDetectado.transform.position - this.transform.position, Vector2.Distance(this.transform.position, ultimoEnemigoDetectado.transform.position) * 1.3f, capasEnemigos);

                Debug.DrawRay(this.transform.position, ultimoEnemigoDetectado.transform.position - this.transform.position);
                if (hit.collider != null)
                {
                    //print(hit.collider.name);
                    if (hit.collider.tag == "Enemigo")
                    {
                        //print("ENEMIGODETECTASR");
                        if (Vector2.Distance(this.transform.position, ultimoEnemigoDetectado.transform.position) < distanciaChispazo)
                        {
                            //print("Chispazo");

                            if (haciendoChispazo == false) destinoChispazo = ultimoEnemigoDetectado.transform.parent.GetChild(0).transform.position;
                            ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
                            //if (haciendoChispazo == false) 
                            haciendoChispazo = true;
                        }
                    }
                }

            }
        }
        else
        {
            if (enemigoCerca)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Collider2D[] resultados = new Collider2D[15];
                    if (pInput.personajeInvertido == false)
                    {
                        //RaycastHit2D[] hitCirculo= Physics2D.CircleCast(this.transform.position,distanciaChispazo,Vector2.zero,0,hitCirculo)

                        int enemigos = Physics2D.OverlapCircleNonAlloc(this.transform.position, distanciaChispazo, resultados);
                        float mejorDistancia = 900000f;

                        foreach (Collider2D col in resultados)
                        {

                            if (col != null)
                            {


                                if (col.gameObject.tag == "Enemigo")
                                {
                                    if (col.gameObject.transform.position.x > this.transform.position.x)
                                    {
                                        if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) < mejorDistancia)
                                        {
                                            enemigoSeleccionado = col.gameObject;
                                            mejorDistancia = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        if (enemigoSeleccionado == null)
                                        {

                                        }
                                        else
                                        {
                                            if (enemigoSeleccionado.transform.position.x > this.transform.position.x)
                                            {

                                            }
                                            else
                                            {
                                                if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) < mejorDistancia)
                                                {
                                                    enemigoSeleccionado = col.gameObject;
                                                    mejorDistancia = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                        int enemigos = Physics2D.OverlapCircleNonAlloc(this.transform.position, distanciaChispazo, resultados);
                        float mejorDistancia = 900000f;

                        foreach (Collider2D col in resultados)
                        {

                            if (col != null)
                            {


                                if (col.tag == "Enemigo")
                                {
                                    if (col.gameObject.transform.position.x < this.transform.position.x)
                                    {
                                        if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) < mejorDistancia)
                                        {
                                            enemigoSeleccionado = col.gameObject;
                                            mejorDistancia = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
                                        }
                                    }
                                    else
                                    {
                                        if (enemigoSeleccionado == null)
                                        {

                                        }
                                        else
                                        {
                                            if (enemigoSeleccionado.transform.position.x < this.transform.position.x)
                                            {

                                            }
                                            else
                                            {
                                                if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) < mejorDistancia)
                                                {
                                                    enemigoSeleccionado = col.gameObject;
                                                    mejorDistancia = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //print("Chispazo2");

                    ultimoEnemigoDetectado = enemigoSeleccionado;
                    if (ultimoEnemigoDetectado != null)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, ultimoEnemigoDetectado.transform.position - this.transform.position, Vector2.Distance(this.transform.position, ultimoEnemigoDetectado.transform.position) * 1.3f, capasEnemigos);
                        if (hit.collider != null)
                        {
                            //print(hit.collider.name);
                            if (hit.collider.tag == "Enemigo")
                            {
                                if (Vector2.Distance(this.transform.position, ultimoEnemigoDetectado.transform.position) < distanciaChispazo)
                                {
                                    if (haciendoChispazo == false) destinoChispazo = ultimoEnemigoDetectado.transform.parent.GetChild(0).transform.position;
                                    ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
                                    //if (haciendoChispazo == false) 
                                    haciendoChispazo = true;
                                }
                            }
                        }
                    }

                }
            }
        }

        if (haciendoChispazo)
        {
            if (!puedeSalirChispazo)
            {


                if (unavezSalirChispazo == true)
                {
                    unavezSalirChispazo = false;
                    saltoBloqueado = false;
                    movimientoBloqueado = false;
                }
                else
                {



                    Vector2 direccion = transform.position - destinoChispazo;

                    float distancia = Vector2.Distance(transform.position, destinoChispazo);

                    float fuerzaRealAtraccion;
                    fuerzaRealAtraccion = fuerzaAcercarseChispazo + distancia * 100;

                    if (chispazoPerdido == false)
                    {
                        AnularGravedad();
                        if (distancia <= 2f)
                        {
                            ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;

                            constantegravedad = 1;
                            dashEnCaida = false;
                            animCC.SetBool("cayendo", dashEnCaida);
                            dashBloqueado = true;
                            dashCaidaBloqueado = true;


                            if (tiempoAntesChispazo > 0)
                            {
                                ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
                                saltoDobleHecho = false;
                                rb.velocity = Vector2.zero;
                                puedeSalirChispazo = true;
                                this.transform.position = destinoChispazo;

                            }
                            else
                            {
                                ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
                                //print("EEEEEEEEEE");
                                tiempoAntesChispazo = auxTiempoChispazo;
                                puedeSalirChispazo = false;
                                chispazoPerdido = true;
                                haciendoChispazo = false;
                                PermitirGravedad();
                            }




                        }
                        else if (distancia > 2f)
                        {

                            if (chispazoPerdido == false)
                            {
                                if (entradochispazo == false)
                                {
                                    entradochispazo = true;
                                    rb.velocity = Vector2.zero;
                                }
                                ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
                                puedeSalirChispazo = false;

                                rb.AddForce(-direccion.normalized * fuerzaRealAtraccion * Time.deltaTime);
                            }
                            else
                            {
                                ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
                            }

                        }
                        else if (distancia > 3f)
                        {
                            ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
                        }
                    }
                }

            }
            else
            {
                tiempoAntesChispazo -= Time.deltaTime;
                movimientoBloqueado = true;
                saltoBloqueado = true;
                if (tiempoAntesChispazo > 0)
                {
                    if (ultimoEnemigoDetectado != null) ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
                    saltoDobleHecho = false;
                    rb.velocity = Vector2.zero;
                    puedeSalirChispazo = true;
                    this.transform.position = destinoChispazo;

                }
                else
                {
                    //print("EEEEEEEEEE");
                    tiempoAntesChispazo = auxTiempoChispazo;
                    puedeSalirChispazo = false;
                    chispazoPerdido = true;

                    ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
                    rb.AddForce(new Vector2(0, 1) * fuerzaChispazoPerdido); PermitirGravedad();

                    unavezSalirChispazo = true;
                    entradochispazo = false;
                    dashBloqueado = false;
                    dashCaidaBloqueado = false;

                    saltoBloqueado = false;
                    movimientoBloqueado = false;

                    haciendoChispazo = false;

                }



                Vector3 pz2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pz2.z = 0;

                Vector2 direccion2 = (pz2 - this.transform.position).normalized;

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //print("reboto");
                    puedeSalirChispazo = false;

                    entradochispazo = false;
                    rb.AddForce(new Vector2(direccion2.normalized.x, direccion2.normalized.y) * fuerzaSalidaChispazo);

                    if (new Vector2(direccion2.normalized.x, direccion2.normalized.y).x < 0)
                    {
                        //print("OEOEOEOEOE" + (new Vector2(direccion2.normalized.x, direccion2.normalized.y).x));
                        pInput.personajeInvertido = true;
                        transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                    }
                    else
                    {
                        //print("AAAAAAA" + (new Vector2(direccion2.normalized.x, direccion2.normalized.y).x));
                        pInput.personajeInvertido = false;
                        transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                    }
                    ultimoenemigo2 = ultimoEnemigoDetectado;
                    ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
                    Invoke("PonerCollider", 0.5f);
                    unavezSalirChispazo = true;

                    dashBloqueado = false;
                    dashCaidaBloqueado = false;
                    tiempoAntesChispazo = auxTiempoChispazo;
                    saltoBloqueado = false;
                    movimientoBloqueado = false;
                    PermitirGravedad();

                    Vector3 pz3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pz3.z = 0;
                    Vector3 direction = pz3 - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), direction, 10000, capasEnemigos);
                    Debug.DrawRay(Input.mousePosition, direction);


                    if (hit != false)
                    {
                        //print(hit.collider.name);
                        if (hit.collider.tag == "Enemigo")
                        {
                            ultimoEnemigoDetectado = hit.collider.gameObject;
                            destinoChispazo = ultimoEnemigoDetectado.transform.parent.GetChild(0).transform.position;
                        }
                        else
                        {
                            chispazoPerdido = true;
                            haciendoChispazo = false;
                        }
                    }
                    else
                    {
                        chispazoPerdido = true;
                        haciendoChispazo = false;
                    }


                }
            }

        }
        else
        {
            unavezSalirChispazo = false;
            chispazoPerdido = false;
        }
    }
    public void AnularGravedad()
    {
        rb.gravityScale = 0;

        tieneGravedad = false;
    }
    public void PermitirGravedad()
    {
        rb.gravityScale = originalgravity;

        tieneGravedad = true;
    }
    void DetectarPared()
    {


        if (pInput.personajeInvertido)
        {

            izquierda = Physics2D.Raycast(detectorPared.transform.position, -this.transform.right, distanciaPared, capasSuelo);
            derecha = Physics2D.Raycast(detectorPared.transform.position, new Vector2(1, 0), 0, capasSuelo);
            Debug.DrawRay(detectorPared.transform.position, -this.transform.right * distanciaPared, Color.green);
        }
        else
        {
            Debug.DrawRay(detectorPared.transform.position, this.transform.right * distanciaPared, Color.blue);
            izquierda = Physics2D.Raycast(detectorPared.transform.position, new Vector2(-1, 0), 0, capasSuelo);
            derecha = Physics2D.Raycast(detectorPared.transform.position, this.transform.right, distanciaPared, capasSuelo);
        }



        if (derecha.collider != null /*&& tocandoBorde*/)
        {

            if (derecha.collider.tag == "Pared")
            {

                rb.AddForce(new Vector2(1, 0) * 30 * Time.deltaTime);
                tocandoderecha = true;
                if ((!grounded) && (!looping)) pegadoPared = true; 
                dashBloqueado = false;
                ultimaParedPosicion = new Vector2(this.transform.position.x + 2, this.transform.position.y);
            }
            else
            {
                if (derecha.collider.tag != "Enemigo" && derecha.collider.tag != "Loop" && derecha.collider.tag != "NoClimb")
                {
                    tocandoderecha = true;
                }
                else
                {
                    tocandoderecha = false;
                }

            }



        }
        else
        {
            tocandoderecha = false;
        }
        if (izquierda.collider != null /*&& tocandoBorde*/)
        {

            if (izquierda.collider.tag == "Pared")
            {
                rb.AddForce(new Vector2(-1, 0) * 30 * Time.deltaTime);
                tocandoizquierda = true;
                if ((!grounded) && (!looping)) pegadoPared = true;

                dashBloqueado = false;
                ultimaParedPosicion = new Vector2(this.transform.position.x - 2, this.transform.position.y);
            }
            else
            {
                if (izquierda.collider.tag != "Enemigo" && izquierda.collider.tag != "Loop" && izquierda.collider.tag != "NoClimb")
                {
                    tocandoizquierda = true;
                }
                else
                {
                    tocandoizquierda = false;
                }

            }



        }
        else
        {
            tocandoizquierda = false;
        }
        if (derecha.collider == null && izquierda.collider == null && subiendoUnavez == false)
        {
            tocandoderecha = false;
            tocandoizquierda = false;
            pegadoPared = false;
        }
        if (pegadoPared)
        {
            saltoDobleHecho = false;
        }


    }
    void MovimientoPared()
    {
        //if ((!grounded) && (tiempomaxTrasTocarsuelo < 0))
        //{
        if (subiendoUnavez)
        {

            StartCoroutine(SubirBorde());
        }
        //}

        if (pegadoPared == true)
        {

            if (grounded == false)
            {
                animCC.SetBool("PegadoPared", true);
                //if (rb.velocity.y < -1)
                //{
                saltoBloqueado = true;
                estoyDasheando = false;
                dashEnCaida = false;
                //animCC.SetBool("cayendo", dashEnCaida);
                AnularGravedad();
                dashBloqueado = true;
                dashCaidaBloqueado = true;
                tieneGravedad = false;
                //}
                constantegravedad = 1;
                //print("    constantegravedad = 1;");
                maxTiempoPared -= Time.deltaTime;


                rb.velocity = new Vector2(0, rb.velocity.y);

                rb.velocity = new Vector2(0, pInput.inputVertical * speedpared * Time.deltaTime);
                animCC.SetFloat("MovimientoPared", Mathf.Abs(pInput.inputVertical));

                if (Input.GetButtonDown("Jump"))
                {
                    auxtiempoTrasSaltoPared += tiempoTrasSaltoPared;

                    lastJumpPared = true;
                    deteccionParedes2 = false;
                    pegadoPared = false;
                    //print("Saltopared" + ultimaParedPosicion + "piotencia" + fuerzaSaltoPared * new Vector2(1, 0.6f));
                    if (ultimaParedPosicion.x < this.transform.position.x)
                    {
                        pInput.inputHorizontal = 1;
                        pInput.personajeInvertido = false;
                        transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                        rb.velocity = Vector2.zero;
                        rb.AddForce(fuerzaSaltoPared * new Vector2(0.6f, 0.6f));
                        //print("ee" + fuerzaSaltoPared * new Vector2(0.6f, 0.6f));
                        maxTiempoPared = auxpared;

                    }
                    else
                    {
                        pInput.personajeInvertido = true;
                        pInput.inputHorizontal = -1;
                        transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                        rb.velocity = Vector2.zero;
                        rb.AddForce(fuerzaSaltoPared * new Vector2(-0.6f, 0.6f));
                        //print("oo" + fuerzaSaltoPared * new Vector2(-0.6f, 0.6f));
                        maxTiempoPared = auxpared;

                    }
                    animCC.SetTrigger("WallJump");
                    tocando = false;
                    dashBloqueado = false;
                    dashCaidaBloqueado = false;
                    unavezsalirpared = true;

                }
                else
                {
                    unavezsalirpared = true;
                }
                if (maxTiempoPared <= 0)
                {
                    pegadoPared = false;
                    animCC.SetBool("PegadoPared", false);
                    deteccionParedes = false;
                }
            }
            else
            {
                pegadoPared = false;
            }

        }
        else
        {
            animCC.SetBool("PegadoPared", false);
            if (tocandoRebote == false)
            {
                if ((!looping) && (!estoyDasheando))
                {
                    PermitirGravedad();
                }

            }

            if (unavezsalirpared == true)
            {
                unavezsalirpared = false;

                saltoBloqueado = false;
                //print("cambiogravedad");
                dashBloqueado = false;
                dashCaidaBloqueado = false;
                maxTiempoPared = auxpared;
                tieneGravedad = true;
                saltoDobleHecho = false;
            }
            if (deteccionParedes2 == false)
            {
                deteccionParedes2 = true;
            }



        }


    }
    void MoverPersonaje()
    {
        if (auxTiempoTrasSaltoLoop > 0) auxTiempoTrasSaltoLoop -= Time.deltaTime;
        if (grounded)
        {
            if (ultimaNormal.y > 0.8f)
            {
                if (Math.Sign(pInput.ultimoInputHorizontal) != Math.Sign(rb.velocity.x) && Mathf.Abs(rb.velocity.x) > 2)
                {

                    if (tengoMaxspeed)
                    {
                       

                        print("cambiosentido2");
                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                        this.rb.AddForce(direccion * fuerzaCambioSentido * 1.5f * Time.deltaTime);

                    }
                    else
                    {
                        //rb.velocity =new Vector2 (0, rb.velocity.y);
                        print("cambiosentido");

                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                        this.rb.AddForce(direccion * fuerzaCambioSentido * Time.deltaTime);


                    }
                    //rb.velocity = new Vector2(-rb.velocity.x * 0.7f, rb.velocity.y);
                    speed = 0;
                }
                //if (pInput.inputHorizontal < 0 && pInput.ultimoInputHorizontal>0&&  rb.velocity.x < 2)
                //{
                //    if (tengoMaxspeed)
                //    {

                //        print("cambiosentido2");
                //        Vector2 direccion = Vector2.Perpendicular(normal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                //        this.rb.AddForce(direccion * fuerzaCambioSentido * 1.5f * Time.deltaTime);

                //    }
                //    else
                //    {

                //        print("cambiosentido");

                //        Vector2 direccion = Vector2.Perpendicular(normal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                //        this.rb.AddForce(direccion * fuerzaCambioSentido * Time.deltaTime);


                //    }
                //    //rb.velocity = new Vector2(-rb.velocity.x * 0.7f, rb.velocity.y);
                //    speed = 0;
                //}
                //else if (pInput.inputHorizontal> 0 && pInput.ultimoInputHorizontal < 0 && rb.velocity.x > 2)
                //{
                //    if (tengoMaxspeed)
                //    {

                //        print("cambiosentido2");
                //        Vector2 direccion = Vector2.Perpendicular(normal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                //        this.rb.AddForce(direccion * fuerzaCambioSentido * 1.5f * Time.deltaTime);

                //    }
                //    else
                //    {

                //        print("cambiosentido");

                //        Vector2 direccion = Vector2.Perpendicular(normal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                //        this.rb.AddForce(direccion * fuerzaCambioSentido * Time.deltaTime);


                //    }
                //    speed = 0;
                //}
                //else if (pInput.inputHorizontal == 0 && Mathf.Abs(rb.velocity.x) > 2)
                //{

                //    //if (pInput.ultimoInputHorizontal > 0 && rb.velocity.x < 2)
                //    //{
                //    //    if (tengoMaxspeed)
                //    //    {

                //    //        print("cambiosentido2");
                //    //        Vector2 direccion = Vector2.Perpendicular(normal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                //    //        this.rb.AddForce(direccion * fuerzaCambioSentido * 1.5f * Time.deltaTime);

                //    //    }
                //    //    else
                //    //    {

                //    //        print("cambiosentido");

                //    //        Vector2 direccion = Vector2.Perpendicular(normal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                //    //        this.rb.AddForce(direccion * fuerzaCambioSentido * Time.deltaTime);


                //    //    }
                //    //    //rb.velocity = new Vector2(-rb.velocity.x * 0.7f, rb.velocity.y);
                //    //    speed = 0;
                //    //}
                //    //else if (pInput.ultimoInputHorizontal < 0 && rb.velocity.x > 2)
                //    //{
                //    //    if (tengoMaxspeed)
                //    //    {

                //    //        print("cambiosentido2");
                //    //        Vector2 direccion = Vector2.Perpendicular(normal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                //    //        this.rb.AddForce(direccion * fuerzaCambioSentido * 1.5f * Time.deltaTime);

                //    //    }
                //    //    else
                //    //    {

                //    //        print("cambiosentido");

                //    //        Vector2 direccion = Vector2.Perpendicular(normal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                //    //        this.rb.AddForce(direccion * fuerzaCambioSentido * Time.deltaTime);


                //    //    }
                //    //    speed = 0;
                //    //}
                //}


            }

            if (pInput.inputHorizontal != 0)
            {
                //FUERZA AUXILIAR PARA CAMBIO DE SENTIDO
                //if (normal.y >= 0)//Para que no se haga en loops (boca abajo);
                Collider2D[] hit = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 1.1f);
                foreach (Collider2D col in hit)
                {

                    if (col.tag == "Loop")
                    {

                        looping = true;

                    }



                }

                //else
                //{

                //}



                if (Mathf.Abs(rb.velocity.x) < (velMinima))
                {

                    speed = velMinima;

                    Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 80 * -pInput.inputHorizontal;
                    this.rb.AddForce(direccion * Time.deltaTime);
                    //rb.velocity += direccion * 1.1f * Time.deltaTime;

                    //if (pInput.inputHorizontal > 0)
                    //{

                    ////Vector2 velocidadWorld = new Vector2((((velMinima) + (Mathf.Abs(rb.velocity.x * coefAceleracion))) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(-ultimaNormal).x), (((velMinima) + (Mathf.Abs(rb.velocity.y * coefAceleracion))) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(ultimaNormal).y));
                    ////this.rb.AddForce(velocidadWorld * Time.fixedDeltaTime);
                    ///
                    //this.rb.velocity = new Vector2(velMinima, rb.velocity.y);
                    //}
                    //if (pInput.inputHorizontal < 0)
                    //{
                    //    this.rb.velocity = new Vector2(-velMinima, rb.velocity.y);
                    //}

                }
                if (Mathf.Abs(this.rb.velocity.x) < velMaxima)
                {
                    //if (pInput.inputHorizontal > 0)

                    if (capSpeedUnavez == false)
                    {
                        speed += coefAceleracion * Time.deltaTime;
                    }
                    else if (speed < capSpeed)
                    {
                        speed += coefAceleracion * Time.deltaTime;
                    }
                    else
                    {
                        speed = capSpeed;
                    }
                    Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 80 * -pInput.inputHorizontal;

                    this.rb.AddForce(direccion * Time.deltaTime);




                    //Vector2 velocidadWorld = new Vector2(((/*(Mathf.Abs(rb.velocity.x) + */(Mathf.Abs(rb.velocity.x * coefAceleracion))) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(-ultimaNormal).x), ((/*Mathf.Abs(rb.velocity.y) +*/ (Mathf.Abs(rb.velocity.y * coefAceleracion))) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(ultimaNormal).y));
                    //this.rb.AddForce(velocidadWorld * Time.fixedDeltaTime);


                    Debug.DrawRay(this.transform.position, direccion, Color.red);
                    //}
                    //else if (pInput.inputHorizontal < 0)
                    //{

                    //    Vector3 velocidadWorld = transform.TransformDirection(new Vector3(((Mathf.Abs(rb.velocity.x) + (Mathf.Abs(rb.velocity.x * coefAceleracion) * Time.fixedDeltaTime) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(ultimaNormal).x)), this.rb.velocity.y * Vector2.Perpendicular(-ultimaNormal).y, 0));
                    //    this.rb.velocity = new Vector2(-velocidadWorld.x,this.rb.velocity.y- velocidadWorld.y);
                    //}
                }
                if (Mathf.Abs(this.rb.velocity.x) >= velMaxima)
                {
                    if (capSpeedUnavez == false)
                    {
                        capSpeedUnavez = true;
                        capSpeed = speed;
                        print(capSpeed + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                    }
                    speed = capSpeed;
                    print("frenando"+Mathf.Sign(rb.velocity.x));
                   if(auxCdDash<1) rb.velocity = new Vector2((velMaxima-1) * Mathf.Sign(ultimaNormal.y) * pInput.ultimoInputHorizontal, rb.velocity.y);
                    //this.rb.AddForce(-this.rb.velocity * (coefDeceleracion * 0.1f) * Time.deltaTime);
                    //if (Mathf.Abs(speed) > 0)
                    //{
                    //    speed -= coefDeceleracion * Time.deltaTime;
                    //}
                    //if (Mathf.Abs(speed) <= 0)
                    //{
                    //    speed = 0;
                    //}
                    ////Vector2 direccion = Vector2.Perpendicular(normal) * speed * -pInput.inputHorizontal;

                    //this.rb.AddForce(-this.rb.velocity * coefDeceleracion * Time.deltaTime);
                    //this.rb.AddForce(-this.rb.velocity * Time.fixedDeltaTime);
                    //this.rb.AddForce(-direccion * Time.fixedDeltaTime);
                    //Vector2 velocidadWorld = new Vector2(((Mathf.Abs(rb.velocity.x) - (Mathf.Abs(rb.velocity.x * coefDeceleracion*0.05f) )) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(-ultimaNormal).x), this.rb.velocity.y * Vector2.Perpendicular(-ultimaNormal).y);
                    //this.rb.AddForce(velocidadWorld*Time.fixedDeltaTime);
                    //rb.velocity


                }
                //print("acelerando");
                animCC.SetBool("Corriendo", true);
            }
            else
            {
                if (Mathf.Abs(rb.velocity.x) > 0)
                {
                    if (Mathf.Abs(speed) > 0)
                    {
                        speed -= coefDeceleracion * Time.deltaTime;
                    }
                    if (speed <= 0)
                    {
                        speed = 0;
                    }


                    //Vector2 direccion = Vector2.Perpendicular(normal) * speed * -pInput.inputHorizontal;
                    //print("no acelerando");
                    //this.rb.AddForce(-direccion * Time.fixedDeltaTime);
                    this.rb.AddForce(-this.rb.velocity * coefDeceleracion * Time.deltaTime);
                    if (Mathf.Abs(rb.velocity.x) <= 0)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }

                }
                animCC.SetBool("Corriendo", false);
            }
            //print(rb.velocity);

        }
        else
        {

            if (pInput.inputHorizontal != 0)
            {
                //if (rb.velocity.y < 0)
                //{
                //    rb.velocity = new Vector2(rb.velocity.x * 0.99f, rb.velocity.y * 0.99f);
                //}



                Collider2D[] hit = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 1.1f);
                foreach (Collider2D col in hit)
                {

                    if (col.tag == "Loop")
                    {

                        looping = true;

                    }


                }

                if (Mathf.Sign(pInput.inputHorizontal) != Mathf.Sign(rb.velocity.x) && Mathf.Abs(rb.velocity.x) > 1)
                {
                    if (auxtiempoTrasSaltoPared < tiempoTrasSaltoPared * 0.8f)
                    {
                        if ((auxTiempoTrasSaltoLoop <= 0) && (Mathf.Sign(ultimaNormal.y) > 0))
                        {
                            if (tengoMaxspeed)
                            {
                                print("cambioaieeeeere");
                                //if (saltoDobleReciente)
                                //{
                                //    Vector2 direccion = new Vector2(1, 0) * coefDeceleracion * pInput.inputHorizontal;
                                //    this.rb.AddForce(direccion * fuerzaCambioSentido * 2.8f * Time.deltaTime);
                                //}
                                //else
                                {


                                    Vector2 direccion = new Vector2(1, 0) * coefDeceleracion * pInput.inputHorizontal;
                                    this.rb.AddForce(direccion * fuerzaCambioSentidoAire * 1.2f * Time.deltaTime);
                                }
                                //if (pInput.ultimoInputHorizontal == 1)
                                //    {
                                //        Vector2 direccion = new Vector2(1,0).normalized * coefDeceleracion * pInput.inputHorizontal;
                                //        this.rb.AddForce(direccion * fuerzaCambioSentido * 1.6f * Time.deltaTime);
                                //    }
                                //    else if (pInput.inputHorizontal == -1)
                                //    {
                                //        Vector2 direccion = new Vector2(-1, 0).normalized * coefDeceleracion * pInput.inputHorizontal;
                                //        this.rb.AddForce(direccion * fuerzaCambioSentido * 1.6f * Time.deltaTime);
                                //    }
                                //    //Vector2 direccion = new Vector2(-1, 0) * coefDeceleracion * -pInput.inputHorizontal;


                            }
                            else
                            {

                                //if (saltoDobleReciente)
                                //{
                                //    Vector2 direccion = new Vector2(1, 0) * coefDeceleracion * pInput.inputHorizontal;
                                //    this.rb.AddForce(direccion * fuerzaCambioSentido * 2f * Time.deltaTime);
                                //}
                                //else
                                {
                                    print("cambiorerer2air");


                                    Vector2 direccion = new Vector2(1, 0) * coefDeceleracion * pInput.inputHorizontal;
                                    this.rb.AddForce(direccion * fuerzaCambioSentidoAire * 0.8f * Time.deltaTime);
                                }
                                //if (pInput.ultimoInputHorizontal == 1)
                                //{
                                //    Vector2 direccion = new Vector2(1, 0).normalized * coefDeceleracion * pInput.inputHorizontal;
                                //    this.rb.AddForce(direccion * fuerzaCambioSentido * 0.8f * Time.deltaTime);
                                //}
                                //else if (pInput.inputHorizontal == -1)
                                //{
                                //    Vector2 direccion = new Vector2(-1, 0).normalized * coefDeceleracion * pInput.inputHorizontal;
                                //    this.rb.AddForce(direccion * fuerzaCambioSentido * 0.8f * Time.deltaTime);
                                //}


                            }
                            cambioSentidoReciente = true;
                            StopCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                            StartCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                        }
                    }

                }
                //FUERZA AUXILIAR PARA CAMBIO DE SENTIDO


                //rb.velocity = new Vector2(-rb.velocity.x * 0.7f, rb.velocity.y);






                if (Mathf.Abs(rb.velocity.x) < (velMinima))
                {
                    speed = velMinima;

                    Vector2 direccion = new Vector2(1/**Mathf.Sign(ultimaNormal.y)*/, 0) * speed * 80 * pInput.inputHorizontal;
                    //Vector2 direccion =new Vector2(Vector2.Perpendicular(ultimaNormal).y,0) * speed * 80 * -pInput.inputHorizontal;



                    this.rb.AddForce(direccion * Time.deltaTime);
                    //    this.rb.AddForce(direccion * 1.1f * Time.deltaTime);
                    //if (pInput.ultimoInputHorizontal == 1)
                    //{
                    //    Vector2 direccion = new Vector2(1, 0).normalized * speed;
                    //    this.rb.AddForce(direccion * 1.1f * Time.deltaTime);
                    //}
                    //else if (pInput.inputHorizontal == -1)
                    //{
                    //    Vector2 direccion = new Vector2(1, 0).normalized * speed ;
                    //    this.rb.AddForce(direccion * 1.1f * Time.deltaTime);
                    //}
                    //Vector2 direccion = new Vector2(-ultimaNormal.x, 0).normalized * speed * -pInput.inputHorizontal;
                    //this.rb.AddForce(direccion * 1.1f * Time.deltaTime);
                    //if (pInput.inputHorizontal > 0)
                    //{

                    ////Vector2 velocidadWorld = new Vector2((((velMinima) + (Mathf.Abs(rb.velocity.x * coefAceleracion))) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(-ultimaNormal).x), (((velMinima) + (Mathf.Abs(rb.velocity.y * coefAceleracion))) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(ultimaNormal).y));
                    ////this.rb.AddForce(velocidadWorld * Time.fixedDeltaTime);
                    ///
                    //this.rb.velocity = new Vector2(velMinima, rb.velocity.y);
                    //}
                    //if (pInput.inputHorizontal < 0)
                    //{
                    //    this.rb.velocity = new Vector2(-velMinima, rb.velocity.y);
                    //}
                    //this.rb.AddForce(direccion * Time.deltaTime);
                }
                if (Mathf.Abs(this.rb.velocity.x) < velMaxima)
                {
                    ////if (pInput.inputHorizontal > 0)
                    if (capSpeedUnavez == false)
                    {
                        speed += coefAceleracion * Time.deltaTime;
                    }
                    else if (speed < capSpeed)
                    {
                        speed += coefAceleracion * Time.deltaTime;
                    }
                    else
                    {
                        speed = capSpeed;
                    }
                    //Vector2 direccion = new Vector2(Vector2.Perpendicular(ultimaNormal).y, 0) * speed * 80 * -pInput.inputHorizontal;
                    //print("VEL" + new Vector2(ultimaNormal.x * speed * 80 * -pInput.inputHorizontal, 0));
                    Vector2 direccion = new Vector2(1 /** Mathf.Sign(ultimaNormal.y)*/, 0) * speed * 80 * pInput.inputHorizontal;
                    this.rb.AddForce(direccion * Time.deltaTime);

                    //if (pInput.ultimoInputHorizontal == 1)
                    //{
                    //    Vector2 direccion = new Vector2(1, 0).normalized * speed;
                    //    this.rb.AddForce(direccion * Time.deltaTime);
                    //    Debug.DrawRay(this.transform.position, direccion, Color.red);
                    //}
                    //else if (pInput.inputHorizontal == -1)
                    //{
                    //    Vector2 direccion = new Vector2(1, 0).normalized * speed ;
                    //    this.rb.AddForce(direccion * Time.deltaTime);
                    //    Debug.DrawRay(this.transform.position, direccion, Color.red);
                    //}



                    //Vector2 velocidadWorld = new Vector2(((/*(Mathf.Abs(rb.velocity.x) + */(Mathf.Abs(rb.velocity.x * coefAceleracion))) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(-ultimaNormal).x), ((/*Mathf.Abs(rb.velocity.y) +*/ (Mathf.Abs(rb.velocity.y * coefAceleracion))) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(ultimaNormal).y));
                    //this.rb.AddForce(velocidadWorld * Time.fixedDeltaTime);



                    //}
                    //else if (pInput.inputHorizontal < 0)
                    //{

                    //    Vector3 velocidadWorld = transform.TransformDirection(new Vector3(((Mathf.Abs(rb.velocity.x) + (Mathf.Abs(rb.velocity.x * coefAceleracion) * Time.fixedDeltaTime) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(ultimaNormal).x)), this.rb.velocity.y * Vector2.Perpendicular(-ultimaNormal).y, 0));
                    //    this.rb.velocity = new Vector2(-velocidadWorld.x,this.rb.velocity.y- velocidadWorld.y);
                    //}
                }
                if (Mathf.Abs(this.rb.velocity.x) > velMaxima)
                {
                    if (capSpeedUnavez == false)
                    {
                        capSpeedUnavez = true;
                        capSpeed = speed;
                        print(capSpeed + "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                    }
                    speed = capSpeed;

                    rb.velocity = new Vector2(velMaxima * pInput.inputHorizontal, rb.velocity.y);
                    //this.rb.AddForce(-this.rb.velocity * (coefDeceleracion * 0.1f) * Time.deltaTime);


                    //speed -= coefDeceleracion;
                    //if (Mathf.Abs(speed) > 0)
                    //{
                    //    speed -= coefDeceleracion;
                    //}
                    //if (Mathf.Abs(speed) <= 0)
                    //{
                    //    speed = 0;
                    //}
                    //Vector2 direccion = Vector2.Perpendicular(normal) * speed * -pInput.inputHorizontal;

                    //this.rb.AddForce(new Vector2(-this.rb.velocity.x, 0) * coefDeceleracion * Time.deltaTime);
                    //this.rb.AddForce(-this.rb.velocity * Time.fixedDeltaTime);
                    //this.rb.AddForce(-direccion * Time.fixedDeltaTime);
                    //Vector2 velocidadWorld = new Vector2(((Mathf.Abs(rb.velocity.x) - (Mathf.Abs(rb.velocity.x * coefDeceleracion*0.05f) )) * pInput.inputHorizontal) * Mathf.Abs(Vector2.Perpendicular(-ultimaNormal).x), this.rb.velocity.y * Vector2.Perpendicular(-ultimaNormal).y);
                    //this.rb.AddForce(velocidadWorld*Time.fixedDeltaTime);
                    //rb.velocity


                }
                //print("acelerando");
                //animCC.SetBool("Corriendo", true);
            }
            else
            {
                if (Mathf.Abs(rb.velocity.x) > 0)
                {
                    if (Mathf.Abs(speed) > 0)
                    {
                        speed -= coefDeceleracion;
                    }
                    if (speed <= 0)
                    {
                        speed = 0;
                    }


                    //Vector2 direccion = Vector2.Perpendicular(normal) * speed * -pInput.inputHorizontal;
                    //print("no acelerando");
                    //this.rb.AddForce(-direccion * Time.fixedDeltaTime);
                    this.rb.AddForce(new Vector2(-this.rb.velocity.x, 0).normalized * coefDeceleracion * Time.deltaTime);
                    if (Mathf.Abs(rb.velocity.x) <= 0)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }

                }
                //animCC.SetBool("Corriendo", false);
            }
        }
        if (Mathf.Abs(rb.velocity.magnitude) < velMaxima - 3f)
        {
            tengoMaxspeed = false;
            //particulasVelMax.SetActive(false);
        }
        else
        {
            tengoMaxspeed = true;
            if (grounded)
            {
                if (GetComponent<PlayerInput>().personajeInvertido)
                {
                    GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasvelMax2, VFX.position);
                }
                else
                {
                    GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasvelMax, VFX.position);
                }
            }



            //particulasVelMax.SetActive(true);
            //GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasvelMax, transform.position);
        }
    }
    public IEnumerator CambioAireReciente(float time)
    {
        yield return new WaitForSeconds(time);
        cambioSentidoReciente = false;
    }
    void AplicarGravedad()
    {
        //if (!grounded)
        if (grounded)
        {
            constantegravedad = 1;

            //rb.AddForce(new Vector2(0.0f, -9.81f * rb.mass), ForceMode2D.Force);
            rb.AddForce(-normal * 50 * Time.deltaTime);
            //}
            Collider2D[] hit = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 1.3f);
            foreach (Collider2D col in hit)
            {

                if (col.tag == "Loop")
                {

                    if (pInput.inputHorizontal != 0)
                    {
                        if (tiempomaxTrasTocarsuelo < 0.2)
                        {
                            rb.gravityScale = 0;
                            rb.AddForce(-normal * fuerzaAtraccionLoop * Time.deltaTime);
                        }

                    }
                    else
                    {
                        rb.gravityScale = originalgravity;
                        PermitirGravedad();
                    }

                }
                else
                {
                    rb.gravityScale = originalgravity;
                    PermitirGravedad();
                }


            }
        }
        else
        {

            looping = false;
            if (constantegravedad > 350) constantegravedad = 350;
            //rb.AddForce(new Vector2(0, -100f * Time.deltaTime));


            if (rb.velocity.y < -40)
            {
                rb.AddForce(new Vector2(0, -600f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (800f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
                //if (rb.velocity.y < -40) rb.velocity = new Vector2(rb.velocity.x, -40);
                print("7");
            }
            else if (rb.velocity.y < -30f)
            {
                rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (750f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
                print("6");
            }
            else if (rb.velocity.y < -20f)
            {
                rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (600f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
                print("5");
            }
            else if (rb.velocity.y < -10f)
            {
                rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (600f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
                print("4");
            }
            else if (rb.velocity.y < 0f)
            {
                rb.AddForce(new Vector2(0, -500f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (600f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
                print("3");
            }
            else if (rb.velocity.y < 15f)
            {
                //if (ultimaNormal.y > 0)
                //{
                rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (400f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
                //}
                print("2");
            }
            else if (rb.velocity.y < 150f)
            {
                //if (ultimaNormal.y > 0)
                //{
                rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (500f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
                //}
                print("1");
            }

            rb.AddForce(new Vector2(0.0f, -350) * Time.deltaTime);



        }



    }

    void Dash()
    {

        if (estoyDasheando == false)
        {



            if (Input.GetKeyDown(KeyCode.LeftShift) && mEnergy.actualEnergy > mEnergy.energiaDash)
            {
                if (auxCdDash <= 0)
                {
                    mEnergy.RestarEnergia(mEnergy.energiaDash);
                    rb.velocity = Vector2.zero;
                    speed = velMaxima;
                    if (grounded)
                    {
                        if (pInput.ultimoInputHorizontal < 0)
                        {
                            if (ultimaNormal.x > 0)
                            {
                                rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                //print((new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal));
                            }
                            else
                            {
                                rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                //print((new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal));
                            }
                            //rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * 1.2f * fuerzaDash, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                        }
                        else if (pInput.ultimoInputHorizontal > 0)
                        {
                            if (ultimaNormal.x > 0)
                            {
                                rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                //print(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                            }
                            else
                            {
                                rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                //print(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                            }

                        }
                        else if (pInput.ultimoInputHorizontal == 0)
                        {
                            rb.AddForce(new Vector2(-fuerzaDash * 1.2f * Vector2.Perpendicular(normal).x, fuerzaDash * Vector2.Perpendicular(-normal).y));
                        }
                        //print("Dash");
                    }
                    else
                    {
                        if (lastJumpPared == false)
                        {
                            if (pInput.ultimoInputHorizontal < 0)
                            {
                                rb.AddForce(new Vector2(-1, 0) * fuerzaDash * -pInput.ultimoInputHorizontal);
                            }
                            else if (pInput.ultimoInputHorizontal > 0)
                            {
                                rb.AddForce(new Vector2(-1, 0) * fuerzaDash * -pInput.ultimoInputHorizontal);
                            }
                            else if (pInput.ultimoInputHorizontal == 0)
                            {
                                rb.AddForce(new Vector2(1, 0) * fuerzaDash * -pInput.ultimoInputHorizontal);
                            }
                        }
                        else
                        {
                            lastJumpPared = false;
                            if (pInput.ultimoInputHorizontal < 0)
                            {
                                rb.AddForce(new Vector2(-1, 0) * fuerzaDash * -pInput.ultimoInputHorizontal);
                            }
                            else if (pInput.ultimoInputHorizontal > 0)
                            {
                                rb.AddForce(new Vector2(-1, 0) * fuerzaDash * -pInput.ultimoInputHorizontal);
                            }
                            else if (pInput.ultimoInputHorizontal == 0)
                            {
                                rb.AddForce(new Vector2(1, 0) * fuerzaDash * -pInput.ultimoInputHorizontal);
                            }
                        }
                    }


                    constantegravedad = 1;
                    //print("    constantegravedad = 1;");
                    estoyDasheando = true;
                    auxdash = tiempoDasheo;
                    dashEnCaida = false;
                    animCC.SetBool("cayendo", dashEnCaida);
                    animCC.SetTrigger("Dash");

                    dashCaidaBloqueado = true;
                    auxCdDash = cooldownDash;
                }
                //anim.SetTrigger("Dash");
                Transform m_GroundCheck = transform.Find("GroundCheckç");

            }
        }
        else
        {
            auxdash -= Time.deltaTime;
            if (auxdash > 0)
            {
                if (grounded == false)
                {

                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    constantegravedad = 1;
                    //print("    constantegravedad = 1;");
                    AnularGravedad();
                }
                if (grounded)
                {
                    //rb.velocity = new Vector2(rb.velocity.x, 0);
                    //constantegravedad = 1;
                    //AnularGravedad();

                    dashCaidaBloqueado = false;
                }
            }
            else
            {
                dashCaidaBloqueado = false;
                //if (Mathf.Abs(this.rb.velocity.x) > velMaxima / 60)
                //{
                if (rb.velocity.x > 0)
                {
                    rb.velocity = new Vector2(velMaxima, rb.velocity.y);
                }
                else
                {

                    rb.velocity = new Vector2(-velMaxima, rb.velocity.y);
                }

                //}
                //if (rb.velocity.x>)
            }



            if (auxdash <= 0)
            {
                //print("terminadDash");
                animCC.ResetTrigger("Dash");
                PermitirGravedad();
                estoyDasheando = false;

            }
        }

        //this.GetComponent<Particulas>().SpawnParticulas(this.GetComponent<Particulas>().particulasDash, new Vector2((m_GroundCheck.position.x - this.GetComponent<PlayerInput>().ultimoInputHorizontal), m_GroundCheck.position.y + 0.8f));
        //this.GetComponent<Movimiento>().multSpeed = this.GetComponent<Movimiento>().speedMax - 0.2f;

    }

    void DashEnCaida()
    {
        if (dashEnCaida)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (!grounded && pInput.inputVertical < 0 && pegadoPared == false)
        {
            if (rb.velocity.y > 0)
            {
                if (tiempomaxTrasTocarsuelo < -0.2f && mEnergy.actualEnergy > 0)
                {
                    mEnergy.RestarEnergia(mEnergy.energiaDashAbajo);
                    rb.AddForce(fuerzaDashCaida * Vector2.down * Time.deltaTime);
                    dashEnCaida = true;
                }
            }
            else
            {
                if (tiempomaxTrasTocarsuelo < -0.05f && mEnergy.actualEnergy > 0)
                {
                    mEnergy.RestarEnergia(30);
                    rb.AddForce(fuerzaDashCaida * Vector2.down * Time.deltaTime);
                    dashEnCaida = true;
                }
            }


        }
        else if (grounded)
        {
            dashEnCaida = false;
        }
        animCC.SetBool("cayendo", dashEnCaida);
    }
    void SaltoNormal()
    {

        if (Input.GetButtonDown("Jump"))
        {
            auxpresalto = tiempoPreSalto;
            tiempoPulsadoEspacio = 0;

            if (grounded)
            {
                if (pulsadoEspacio == false)
                {
                    if (pInput.inputHorizontal != 0)
                    {
                        if (pInput.ultimoInputHorizontal > 0)
                        {
                            if (tengoMaxspeed == false)
                            {

                                rb.velocity = new Vector2(velMaxima * 0.3f, rb.velocity.y);
                            }
                            else
                            {
                                rb.velocity = new Vector2(velMaxima, rb.velocity.y);
                            }

                        }
                        else if (pInput.ultimoInputHorizontal < 0)
                        {
                            if (tengoMaxspeed == false)
                            {
                                rb.velocity = new Vector2(-velMaxima * 0.3f, rb.velocity.y);
                            }
                            else
                            {
                                rb.velocity = new Vector2(-velMaxima, rb.velocity.y);
                            }
                        }
                        if (saltoInmediato == false)
                        {
                            lastJumpPared = false;

                            if (looping)
                            {
                                auxTiempoTrasSaltoLoop = tiempoTrasSaltoLoop;
                                rb.velocity = Vector2.zero;
                                //rb.velocity = (ultimaNormal.normalized * fuerzaSaltoMin);
                                //print("saltospeed" + rb.velocity);
                            }
                            else
                            {
                                //rb.velocity = new Vector2(rb.velocity.x, ultimaNormal.y * fuerzaSaltoMin);
                            }
                            //rb.velocity = ;
                            print("hu");
                            rb.AddForce(this.transform.up * fuerzaSaltoMin, ForceMode2D.Impulse);

                        }



                    }
                    else if (pInput.inputHorizontal == 0)
                    {
                        if (saltoInmediato == false)
                        {
                            lastJumpPared = false;
                            if (looping)
                            {
                                rb.velocity = Vector2.zero;
                                auxTiempoTrasSaltoLoop = tiempoTrasSaltoLoop;
                            }
                            print("hStu");
                            //rb.velocity = (ultimaNormal.normalized * fuerzaSaltoMin);
                            //rb.velocity = ultimaNormal.normalized * fuerzaSaltoMin;
                            rb.AddForce(this.transform.up * fuerzaSaltoMin, ForceMode2D.Impulse);
                        }
                    }
                    pulsadoEspacio = true;
                    saltoIniciado = false;
                    animCC.SetTrigger("Salto");
                    GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasSalto, posGround.position);
                }
            }
            else
            {
                if (saltoDobleHecho)
                {
                    pulsadoEspacio = true;
                    saltoIniciado = true;
                    if ((tiempomaxTrasTocarsuelo >= 0) && (rb.velocity.y < 0))
                    {
                        lastJumpPared = false;
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                        print("postsaltodoblemin");
                        animCC.SetTrigger("Salto");
                        //rb.velocity = new Vector2(rb.velocity.x, 1* fuerzaSaltoMin);
                        if (looping) auxTiempoTrasSaltoLoop = tiempoTrasSaltoLoop;
                        rb.AddForce(new Vector2(0, 1) * fuerzaSaltoMin, ForceMode2D.Impulse);
                        pulsadoEspacio = true;
                        saltoIniciado = false;
                    }
                }
                else
                {
                    if (pegadoPared == false)
                    {
                        if (auxtiempoTrasSaltoPared < 0.7f)
                        {


                            //print("saltodoble");

                            saltoDobleHecho = true;
                            dashEnCaida = false;
                            estoyDasheando = false;
                            float speedx = rb.velocity.x;
                            //rb.velocity = new Vector2(rb.velocity.x, 0);
                            if (cambioSentidoReciente)
                            {
                                constantegravedad = 1;
                                rb.velocity = new Vector2(-rb.velocity.x * 0.4f, fuerzaSaltoDoble);
                            }
                            else
                            {
                                constantegravedad = 1;
                                rb.velocity = new Vector2(rb.velocity.x, fuerzaSaltoDoble);
                            }
                            //print("    constantegravedad = 1;");
                            saltoDobleReciente = true;
                            StopCoroutine(SaltoDobleReciente(tiempoTrasCambioSentido));
                            StartCoroutine(SaltoDobleReciente(tiempoTrasCambioSentido));

                            //rb.AddForce(new Vector2(speedx, fuerzaSaltoDoble));
                        }
                    }


                }

            }
        }
        if (grounded)
        {
            dashCaidaBloqueado = false;
            saltoDobleHecho = false;
            tiempoPulsadoEspacio = 0;
            saltoIniciado = false;

            if ((saltoInmediato))
            {


                saltoInmediato = false;
                //auxpresalto = 0;


                if (pulsadoEspacio == true)
                {


                    if (pInput.inputHorizontal < 0)
                    {
                        if (tengoMaxspeed == false)
                        {
                            rb.velocity = new Vector2(rb.velocity.x * 0.55f, rb.velocity.y);
                        }
                        else
                        {
                            rb.velocity = new Vector2(rb.velocity.x * 0.8f, rb.velocity.y);
                        }
                    }
                    else
                    {
                        if (tengoMaxspeed == false)
                        {
                            rb.velocity = new Vector2(rb.velocity.x * 0.55f, rb.velocity.y);
                        }
                        else
                        {
                            rb.velocity = new Vector2(rb.velocity.x * 0.8f, rb.velocity.y);
                        }
                    }
                    //print("fuerzaminX");
                    print("hwu");
                    rb.AddForce(ultimaNormal.normalized * fuerzaSaltoMin, ForceMode2D.Impulse);
                    lastJumpPared = false;

                    //pulsadoEspacio = true;
                }
            }
        }

        if (saltoIniciado == true)
        {
            auxpresalto -= Time.deltaTime;
            //if (rb.velocity.y < 0)
            //{

            //}

            if (auxpresalto >= 0)
            {
                saltoInmediato = true;
            }
            else
            {
                saltoInmediato = false;
                saltoIniciado = false;
            }

        }
        if (!pegadoPared)
        {
            if (Input.GetKey(KeyCode.Space))
            {


                if ((pulsadoEspacio) && (saltoInmediato == false))
                {


                    tiempoPulsadoEspacio += Time.deltaTime;


                    if (tiempoPulsadoEspacio <= tiempoSaltoCompleto)
                    {
                        if (lastJumpPared == false)
                        {
                            if (!grounded)
                            {
                                if (ultimaNormal.y < 0)
                                {
                                    if (rb.velocity.y < fSaltoInicial)
                                    {

                                        if (pInput.inputHorizontal == 0)
                                        {

                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoInicial * ultimaNormal.y);
                                        }
                                        else
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoInicial * 0.92f * ultimaNormal.y);
                                        }

                                        /*rb.AddForce(new Vector2(0, 1).normalized * fuerzaSaltoMax * Time.deltaTime);*/
                                    }
                                    else if (rb.velocity.y < fSaltoMedio)
                                    {

                                        if (pInput.inputHorizontal == 0)
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoMedio * 1.08f * ultimaNormal.y);
                                        }
                                        else
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoMedio * ultimaNormal.y);
                                        }
                                        //rb.AddForce(new Vector2(0, 1).normalized * fuerzaSaltoMax * 0.5f * Time.deltaTime);
                                    }
                                    else if (rb.velocity.y > fSaltoAlto)
                                    {

                                        if (pInput.inputHorizontal == 0)
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoMedio * ultimaNormal.y);
                                        }
                                        else
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoMedio * ultimaNormal.y);
                                        }
                                        //rb.AddForce(new Vector2(0, 1).normalized * fuerzaSaltoMax * 0.5f * Time.deltaTime);
                                    }
                                }
                                else
                                {


                                    if (rb.velocity.y < fSaltoInicial)
                                    {

                                        if (pInput.inputHorizontal == 0)
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoInicial * ultimaNormal.y);
                                        }
                                        else
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoInicial * 0.92f * ultimaNormal.y);
                                        }

                                        /*rb.AddForce(new Vector2(0, 1).normalized * fuerzaSaltoMax * Time.deltaTime);*/
                                    }
                                    else if (rb.velocity.y < fSaltoMedio)
                                    {

                                        if (pInput.inputHorizontal == 0)
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoMedio * ultimaNormal.y);
                                        }
                                        else
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoMedio * 0.92f * ultimaNormal.y);
                                        }
                                        //rb.AddForce(new Vector2(0, 1).normalized * fuerzaSaltoMax * 0.5f * Time.deltaTime);
                                    }
                                    else if (rb.velocity.y > fSaltoAlto)
                                    {

                                        if (pInput.inputHorizontal == 0)
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoAlto * 1.08f * ultimaNormal.y);
                                        }
                                        else
                                        {
                                            rb.velocity = new Vector2(rb.velocity.x, fSaltoAlto * ultimaNormal.y);
                                        }
                                        //rb.AddForce(new Vector2(0, 1).normalized * fuerzaSaltoMax * 0.5f * Time.deltaTime);
                                    }
                                }
                            }
                            else
                            {

                                //rb.AddForce(ultimaNormal.normalized * fuerzaSaltoMax);
                            }
                        }


                    }
                    else
                    {

                        //if ((rb.velocity.y< 0))
                        //{

                        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                        //}


                    }
                }

            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {

            auxpresalto = tiempoPreSalto;
            pulsadoEspacio = false;
            tiempoPulsadoEspacio = 0;


        }


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    pulsadoEspacio = true;
        //    tiempoPulsadoEspacio = 0;
        //    if (!grounded)
        //    {
        //        if (saltoIniciado == false)
        //        {
        //            saltoIniciado = true;

        //        }
        //    }


        //}
        //if (saltoIniciado == true)
        //{
        //    auxpresalto -= Time.fixedDeltaTime;
        //    if (auxpresalto <= 0)
        //    {
        //        //auxpresalto = tiempoPreSalto;
        //        saltoIniciado = false;
        //    }
        //}
        //else
        //{/*if(grounded)   pulsadoEspacio = false;*/

        //    //auxpresalto = tiempoPreSalto;
        //}
        //if ((grounded))
        //{
        //    auxpresalto = tiempoPreSalto;
        //    if (pulsadoEspacio == true)
        //    {
        //        if (saltoIniciado)
        //        {
        //            rb.AddForce(ultimaNormal * fuerzaSaltoMin, ForceMode2D.Impulse);
        //            saltoIniciado = false;
        //        }
        //    }



        //    //float fuerzaSaltoFinal = ((fuerzaSaltoMax - fuerzaSaltoMin) * (pInput.tiempoPulsadoEspacio / tiempoSaltoCompleto)) + fuerzaSaltoMin;
        //    //rb.AddForce(normal * fuerzaSaltoFinal, ForceMode2D.Impulse);

        //    //pulsadoEspacio = true;

        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    if (pulsadoEspacio)
        //    {


        //        tiempoPulsadoEspacio += Time.fixedDeltaTime;


        //        if (tiempoPulsadoEspacio < tiempoSaltoCompleto)
        //        {
        //            rb.AddForce(ultimaNormal * fuerzaSaltoMax);
        //        }
        //        else
        //        {
        //            if (rb.velocity.y > 0)
        //            {
        //                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        //            }
        //            tiempoPulsadoEspacio = tiempoSaltoCompleto;

        //        }
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    pulsadoEspacio = false;
        //    tiempoPulsadoEspacio = 0;
        //    if (rb.velocity.y > 0)
        //    {
        //        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        //    }

        //}



    }
    public IEnumerator SaltoDobleReciente(float time)
    {
        yield return new WaitForSeconds(time);
        saltoDobleReciente = false;
    }
    void DetectarSuelo()
    {
        Debug.DrawRay(transform.position, -this.transform.up * (distanciaAlSuelo + 0.2f), Color.green);

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, -this.transform.up, distanciaAlSuelo + 0.3f, capasSuelo);
        RaycastHit2D hit2;
        RaycastHit2D hit3;
        if (hit.collider != null)
        {


            normal = new Vector2(hit.normal.x, hit.normal.y);
            ultimaNormal = normal;
            Debug.DrawRay(transform.position, normal * 100, Color.red);


        }
        if (normal != null)
        {
            if (pInput.personajeInvertido == true)
            {
                if (normal.y < -0.2)
                {
                    invertirValores = true;
                }
                else
                {
                    invertirValores = false;
                }
            }
            else
            {
                if (normal.y > 0.2)
                {
                    invertirValores = true;
                }
                else
                {
                    invertirValores = false;
                }
            }

        }
        if (invertirValores)
        {
            Debug.DrawRay(transform.position, new Vector2(-this.transform.up.x, -this.transform.up.y - 0.5f) * (distanciaAlSuelo + 0.3f), Color.black);
            Debug.DrawRay(transform.position, new Vector2(-this.transform.up.x, -this.transform.up.y + 0.5f) * (distanciaAlSuelo + 0.3f), Color.blue);
            hit2 = Physics2D.Raycast(this.transform.position, new Vector2(-this.transform.up.x, -this.transform.up.y - 0.5f), distanciaAlSuelo + 0.3f, capasSuelo);
            hit3 = Physics2D.Raycast(this.transform.position, new Vector2(-this.transform.up.x, -this.transform.up.y + 0.5f), distanciaAlSuelo + 0.3f, capasSuelo);
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector2(-this.transform.up.x - 0.5f, -this.transform.up.y) * (distanciaAlSuelo + 0.3f), Color.black);
            Debug.DrawRay(transform.position, new Vector2(-this.transform.up.x + 0.5f, -this.transform.up.y) * (distanciaAlSuelo + 0.3f), Color.blue);
            hit2 = Physics2D.Raycast(this.transform.position, new Vector2(-this.transform.up.x - 0.5f, -this.transform.up.y), distanciaAlSuelo + 0.3f, capasSuelo);
            hit3 = Physics2D.Raycast(this.transform.position, new Vector2(-this.transform.up.x + 0.5f, -this.transform.up.y), distanciaAlSuelo + 0.3f, capasSuelo);
        }

        if (hit.collider != null)
        {
            tiempomaxTrasTocarsuelo = auxtiempoMaxSuelo;
            grounded = true;

            normal = new Vector2(hit.normal.x, hit.normal.y);
            ultimaNormal = normal;
            Debug.DrawRay(transform.position, normal * 100, Color.red);


        }
        else if ((hit2.collider != null))
        {

            tiempomaxTrasTocarsuelo = auxtiempoMaxSuelo;
            grounded = true;

            normal = new Vector2(hit2.normal.x, hit2.normal.y);

            ultimaNormal = normal;

            Debug.DrawRay(transform.position, normal * 100, Color.red);
        }
        else if ((hit3.collider != null))
        {
            tiempomaxTrasTocarsuelo = auxtiempoMaxSuelo;
            grounded = true;

            normal = new Vector2(hit3.normal.x, hit3.normal.y);
            normal3 = new Vector2(hit3.normal.x, hit3.normal.y);
            ultimaNormal = normal;
            Debug.DrawRay(transform.position, normal * 100, Color.red);
        }
        else
        {
            grounded = false;
            tiempomaxTrasTocarsuelo -= Time.deltaTime;


        }
        normal2 = new Vector2(hit2.normal.x, hit2.normal.y);
        normal3 = new Vector2(hit3.normal.x, hit3.normal.y);
        if (normal.y < 0.05f && normal.y > -0.05f)
        {
            if (normal2.y < 0.05f && normal2.y > -0.05f)
            {
                if (normal3.y < 0.05f && normal3.y > -0.05f)
                {
                    //print(normal + "n1" + normal2 + "n2" + normal3 + "n3");
                    //print("Hola");
                    if (!looping) grounded = false;
                }
            }
        }
        if (grounded)
        {
            deteccionParedes = true;

        }
    }
    void GirarPersonaje()
    {
        if (!grounded)
        {
            this.transform.up = Vector3.Slerp(this.transform.up, new Vector2(0, 1), Time.deltaTime * 6);
            //this.transform.up = new Vector2(0, 1);
        }
        else
        {


            //if (ultimaNormal.x != 0)
            //{
            //if (Mathf.Abs(normal.x) > 0.2f)
            //{
            //    rb.freezeRotation = false;
            //}
            //else
            //{
            //    rb.freezeRotation = true;
            //}

            transform.up = Vector3.Slerp(transform.up, ultimaNormal, Time.deltaTime * 180);
            //this.transform.up = Vector3.Lerp(this.transform.up, ultimaNormal, Time.deltaTime * 180);
            //}
            //else
            //{
            //    //rb.freezeRotation = true;
            //}
        }

    }
    void ComprobarParedes()
    {

        if ((!grounded) && (auxCdDash > 0.5f))
        {
            if (this.transform.rotation.z > -18 && this.transform.rotation.z < 18)
            {
                if ((ultimaNormal.y > -0.8f) && (subiendoUnavez == false))
                {


                    tocandoBorde = Physics2D.Raycast(puntoCheckBorde.position, this.transform.right * Mathf.Sign(pInput.ultimoInputHorizontal), distanciaBorde, capasSuelo);
                    tocandoBorde2 = Physics2D.Raycast(puntoCheckBorde.position, this.transform.right * -Mathf.Sign(pInput.ultimoInputHorizontal), distanciaBorde, capasSuelo);

                    //if (ultimaNormal.y > 0.8f||ultimaNormal.y==0)
                    //{
                   
                    if (tocandoBorde.collider != null && tocandoBorde.collider.tag != "Enemigo" && tocandoBorde.collider.tag != "NoClimb" && tocandoBorde.collider.tag != "Loop" && tocandoBorde.collider.tag != "Pinchos")
                    {
                      
                        //print(tocandoBorde.collider.name);
                        tocando = true;
                    }
                    else if (tocandoBorde2.collider != null && tocandoBorde2.collider.tag != "Enemigo" && tocandoBorde2.collider.tag != "NoClimb" && tocandoBorde2.collider.tag != "Loop" && tocandoBorde2.collider.tag != "Pinchos")
                    {
                       
                        //print(tocandoBorde2.collider.name);
                        tocando = true;
                    }
                    if(tocandoBorde.collider==null&& tocandoBorde2.collider==null)
                    {
                      
                           tocando = false;

                    }
                    if (!tocando && tocandoizquierda)
                    {
                        if (pInput.ultimoInputHorizontal != 1)
                        {
                            speedAntes = new Vector2(-8, 0);

                            //speedAntes = rb.velocity;
                            movimientoBloqueado = true;

                            //ledgePos1 = new Vector2((puntoCheckBorde.position.x - distanciaBorde) - ledgeClimbXOffset1, (puntoCheckBorde.position.y) + ledgeClimbYOffset1);
                            //ledgePos2 = new Vector2((puntoCheckBorde.position.x - distanciaBorde) - ledgeClimbXOffset2, (puntoCheckBorde.position.y) + ledgeClimbYOffset2);
                           puntoChoque= izquierda.point;
                            ledgePos3 = new Vector2((puntoCheckBorde.position.x - distanciaBorde) - ledgeClimbXOffset3, (puntoCheckBorde.position.y) + ledgeClimbYOffset3);
                            ledgePos1 = puntoChoque;
                            ledgePos2 = puntoChoque ;
                           
                            print(puntoChoque + "p1"); puntoChoque = this.transform.position;
                            print("SUBIENDO");
                            //if (rb.velocity.x > 0)
                            //{
                            //    ledgePos1 = new Vector2(Mathf.Floor(puntoCheckBorde.position.x + distanciaBorde) + ledgeClimbXOffset1, Mathf.Floor(puntoCheckBorde.position.y) + ledgeClimbYOffset1);
                            //    ledgePos2 = new Vector2(Mathf.Floor(puntoCheckBorde.position.x + distanciaBorde) + ledgeClimbXOffset2, Mathf.Floor(puntoCheckBorde.position.y) + ledgeClimbYOffset2);

                            //    ledgePos3 = new Vector2(Mathf.Floor(puntoCheckBorde.position.x + distanciaBorde) + ledgeClimbXOffset3, Mathf.Floor(puntoCheckBorde.position.y) + ledgeClimbYOffset3);
                            //    subiendoUnavez = true;
                            //}
                            //else if (rb.velocity.x < 0)
                            //{
                            //    ledgePos1 = new Vector2(Mathf.Ceil(puntoCheckBorde.position.x - distanciaBorde) - ledgeClimbXOffset1, Mathf.Floor(puntoCheckBorde.position.y) + ledgeClimbYOffset1);
                            //    ledgePos2 = new Vector2(Mathf.Ceil(puntoCheckBorde.position.x - distanciaBorde) - ledgeClimbXOffset2, Mathf.Floor(puntoCheckBorde.position.y) + ledgeClimbYOffset2);

                            //    ledgePos3 = new Vector2(Mathf.Ceil(puntoCheckBorde.position.x - distanciaBorde) - ledgeClimbXOffset3, Mathf.Floor(puntoCheckBorde.position.y) + ledgeClimbYOffset3);
                            //    subiendoUnavez = true;
                            //}
                            //else
                            //{
                            if (!pInput.personajeInvertido)
                            {

                            }
                            else
                            {
                                subiendoUnavez = true;
                            }
                            //}


                        }
                    }
                    else if (!tocando && tocandoderecha)
                    {
                        if (pInput.ultimoInputHorizontal != -1)
                        {
                            puntoChoque = derecha.point;
                            speedAntes = new Vector2(8, 0);
                            //speedAntes = rb.velocity;
                            print(speedAntes + "AVERGe");

                            //print("TocandoBorde" + tocandoizquierda + "iz" + tocandoderecha + "der");
                            movimientoBloqueado = true;
                            
                            ledgePos1 = puntoChoque;
                            //ledgePos1 = new Vector2((puntoCheckBorde.position.x + distanciaBorde + ledgeClimbXOffset1),( puntoCheckBorde.position.y + ledgeClimbYOffset1));
                            //ledgePos2 = new Vector2(puntoCheckBorde.position.x + distanciaBorde + ledgeClimbXOffset2, puntoCheckBorde.position.y+ ledgeClimbYOffset2);
                            ledgePos2 = puntoChoque;
                           
                            print(puntoChoque+"p2"); puntoChoque = this.transform.position;
                            ledgePos3 = new Vector2(puntoCheckBorde.position.x + distanciaBorde + ledgeClimbXOffset3, puntoCheckBorde.position.y + ledgeClimbYOffset3);
                            if (!pInput.personajeInvertido)
                            {
                                subiendoUnavez = true;
                            }
                            else
                            {

                            }
                        }

                    }
                    else
                    {

                    }
                    //}

                }
            }
            //}
        }
    }
    IEnumerator SubirBorde()
    {
        //print("empizoscalada");

        //rb.velocity = Vector2.zero;
        animCC.SetBool("canClimbLedge", true);
        //transform.position = ledgePos1;



        //transform.position = Vector3.MoveTowards(ledgePos1, ledgePos3, 2f);

        yield return new WaitForSeconds(0.00f);

        //transform.position = Vector3.MoveTowards(transform.position, ledgePos2, 2f);
        transform.position =  new Vector3(ledgePos2.x,ledgePos2.y+2f,0);
        animCC.SetBool("canClimbLedge", false);
        movimientoBloqueado = false;

        subiendoUnavez = false;
        print(speedAntes);
        if (speedAntes.x < 0)
        {
            rb.velocity = speedAntes * 4;
            
        }
        else
        {
            rb.velocity = speedAntes * 4;
        }
        
        //rb.AddForce(speedAntes * 80);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coleccionable")
        {

            GameManager.Instance.CogerColeccionableNivel(collision.gameObject);

        }
        if (collision.tag == "Estrella")
        {

            GameManager.Instance.CogerEstrellaNivel(collision.gameObject);

        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Pared")
    //    {
    //        if(pegadoPared==false && rb.velocity.y < -1)
    //        {
    //            ultimaParedPosicion = collision.GetContact(0).point;
    //            pegadoPared = true;
    //        }
    //    }
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Pared")
    //    {
    //        if (pegadoPared == false && rb.velocity.y < -1)
    //        {
    //            ultimaParedPosicion = collision.GetContact(0).point;
    //            pegadoPared = true;
    //        }
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Pared")
    //    {
    //        pegadoPared = false;
    //    }
    //}

}
