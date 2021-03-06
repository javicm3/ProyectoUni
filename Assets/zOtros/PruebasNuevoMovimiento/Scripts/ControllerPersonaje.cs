﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;

public class ControllerPersonaje : MonoBehaviour
{
    bool hechoConseguidoPuntoHabChispazo = false;
    bool hechoConseguidoPuntoHabDashAbajo = false;
    string escenaActual;
    public InputDevice joystick;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] private PlayerInput pInput;


    public bool tocandoRebote = false;

    // VARIABLES HABILIDADES BLOQUEADAS (puedes renamearlas si quieres)
    //En el start se llama a un método que las setea según lo que pone en el GM
    bool dashUnlock;
    public bool chispazoUnlook;
    public bool movParedesUnlook;
    public bool movCablesUnlook;
    float auxTiempoSonidoSinEnergia;
    float tiempoSonidoSinEnergia = 0.5f;
    Vector2 ultimaParedPosicion;
    int habilidadesSinTocarSuelo = 0;
    float tiempoSinTocarSuelo = 0;
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
    public bool heEntradoParedUnaVez = false;
    public float velocidadDespegarPared = 150;

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
    public bool saltoInmediato = false;
    public float fuerzaSaltoMax = 11;
    public float fuerzaSaltoMin = 4;
    public float fSaltoInicial = 7;
    public float fSaltoMedio = 9;
    public float fSaltoAlto = 10;
    public bool saltoDobleHecho = false;
    public float fuerzaSaltoDoble = 100;
    public float tiempoSaltoCompleto = 1.2f;
    public float constantegravedad = 1;
    public float tiempoPreSalto = 0.5f;
    public float tiempoPulsadoEspacio;
    public float auxpresalto;
    public bool saltoDobleHechoParaDashCaida = false;

    [Header("Dash")]
    public float fuerzaDash = 6000;
    public float fuerzaDashAire = 4000;
    public float fuerzaDashCaida = 100;
    public float tiempoDasheo = 1f;
    public bool estoyDasheando = true;
    public float auxdash;
    public float cooldownDashAtravesar = 1.5f;
    public float cooldownDashReal = 0.5f;
    public float auxCdDashAtravesar;
    public float auxCdDashReal;
    bool puedoDash = true;
    [Header("EscalarEsquinas")]
    public float distanciaBorde = 5;
    public Transform puntoCheckBorde;


    [Header("Combate")]
    public float tiempoUsarCombateTrasEscudo = 1;
    public float auxTiempoUsar;
    public float tiempoTrasSalirCombateInvuln = 1f;
    public float auxtiempoTrasSalirCombateInvuln;
    public float offsetSalidaEnemigoTerrestreY = 35f;
    public float offsetSalidaEnemigoVoladorY = 0f;
    public GameObject ultimoEnemigoDetectado;
    public float distanciaCombate = 10f;
    public Vector3 destinoCombate;
    public bool haciendoCombate = false;
    public LayerMask maskCombate;
    public List<GameObject> enemigosPasados;
    public float velocidadCombate = 10f;
    GameObject enemigoSeleccionado = null;
    GameObject ultimoEnemigoPasado;
    public GameObject enemigoTemporal = null;
    public Vector3 direccionCombate;
    public Vector3 velocidadCombateUltima;
    public float tiempoTrasSalirCombate = 0.3f;
    public float auxTiempoTrasSalirCombate;
    public GameObject trailChispazo;
    [Range(0.0f, 1f)]
    public float salirCombate;
    //public bool puedeSalirChispazo = false;
    //public float tiempoAntesChispazo = 1f;
    //public float auxTiempoChispazo;
    //public bool unavezSalirChispazo = false;
    //public float fuerzaSalidaChispazo = 500f;
    //public float fuerzaAcercarseChispazo = 500f;
    //public bool chispazoPerdido = false;
    //public float fuerzaChispazoPerdido = 200f;

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



    [Header("Loops")]
    public bool bocabajocambiotecla = false;
    float tiempoTrasSaltoLoop = 0.3f;
    public float auxTiempoTrasSaltoLoop;
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
    public bool combateBloqueado;
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
    public float tiempoCOYOTE = 0.2f;
    public float originalgravity;
    public bool invertirValores = false;
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
    public GameObject boss;
    [SerializeField] private LayerMask capasSuelo;
    [SerializeField] private LayerMask capasEnemigos;
    //[Header("Control")]
    //PlayerControls controles;
    //bool saltoPulsado=false;
    //bool saltoSoltado=false;
    //bool dashPulsado=false;
    public bool haciendoEnfoque = false;
    public bool pulsadoChispazo = false;
    public GameObject feedbackMonedas;
    [Header("TRAMPOLIN")]
    public float auxTiempoTrasImpulso;
    public float tiempoTrasImpulso = 0.35f;
    public bool yaimpulsado = false;
    //public Vector2 move;
    private int Xbox_One_Controller = 0;
    private int PS4_Controller = 0;
    float auxCheckJoystick;
    public float tiempoMaxSpeedLogro = 0;
    float tiempoLoopAntesConsiderarLoop = 0f;
    public float maxYAntesMorir = -999999f;
    // Start is called before the first frame update
    private void Awake()
    {
        enemigosPasados = new List<GameObject>();
        joystick = InputManager.ActiveDevice;


        //controles = new PlayerControls();
        //controles.Gameplay.Salto.performed += ctx => saltoPulsado = true;
        //controles.Gameplay.Salto.canceled += ctx =>
        //{
        //    saltoPulsado = false;
        //    saltoSoltado = true;
        //};
        //controles.Gameplay.Dash.performed += ctx => dashPulsado = true;
        //controles.Gameplay.Dash.canceled += ctx => dashPulsado = false;
        //controles.Gameplay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        //controles.Gameplay.Movement.canceled += ctx => move = Vector2.zero;
    }

    private void OnEnable()
    {
        //controles.Gameplay.Enable();
    }
    private void OnDisable()
    {
        //controles.Gameplay.Disable();
    }
    void Start()
    {
        if (joystick.Name == "NullInputDevice") joystick = null;
        Application.targetFrameRate = 120;
        CargarHabilidadesGM();
        //auxTiempoChispazo = tiempoAntesChispazo;
        auxpared = maxTiempoPared;
        auxtiempoMaxSuelo = tiempoCOYOTE;
        auxdash = tiempoDasheo;
        auxTiempoTrasImpulso = 0;
        auxTiempoUsar = tiempoUsarCombateTrasEscudo;
        auxpresalto = tiempoPreSalto;
        rb = this.GetComponent<Rigidbody2D>();
        pInput = this.GetComponent<PlayerInput>();
        animCC = GetComponentInChildren<Animator>();
        mEnergy = GetComponent<ManagerEnergia>();
        originalgravity = rb.gravityScale;
        auxCdDashAtravesar = cooldownDashAtravesar;
        auxCdDashReal = cooldownDashReal;
        escenaActual = SceneManager.GetActiveScene().name;
        auxtiempoTrasSalirCombateInvuln = tiempoTrasSalirCombateInvuln;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (looping)
        {
            tiempoLoopAntesConsiderarLoop += Time.deltaTime;

        }
        else
        {
            tiempoLoopAntesConsiderarLoop = 0;
        }
        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            GameManager.Instance.NivelActual.tiempoCorriendoTotal += Time.deltaTime;
            if (tengoMaxspeed)
            {


                if (Mathf.Floor(GameManager.Instance.NivelActual.tiempoCorriendoTotal % 20) == 0)
                {
                    float totalMax = 0;
                    foreach (LevelInfo inf in GameManager.Instance.ListaNiveles)
                    {
                        if (inf.nombreNivel != GameManager.Instance.NivelActual.nombreNivel)
                        {
                            totalMax += inf.tiempoCorriendoTotal;
                          
                        }

                    }
                    if (totalMax + GameManager.Instance.NivelActual.tiempoCorriendoTotal > 360)
                    {
                        ManagerLogros.Instance.DesbloquearLogro(31);

                    }
                  
                }
                tiempoMaxSpeedLogro += Time.deltaTime;

                if (tiempoMaxSpeedLogro > 5f)
                {

                    ManagerLogros.Instance.DesbloquearLogro(21);
                }
            }
            else
            {
                tiempoMaxSpeedLogro = 0;
            }
        }
        if (grounded)
        {
            maxYAntesMorir = this.transform.position.y;
            habilidadesSinTocarSuelo = 0;
            tiempoSinTocarSuelo = 0;
        }
        else
        {
            if (maxYAntesMorir < this.transform.position.y)
            {
                maxYAntesMorir = this.transform.position.y;
            }


            tiempoSinTocarSuelo += Time.deltaTime;
            if (tiempoSinTocarSuelo > 7f)
            {
                ManagerLogros.Instance.DesbloquearLogro(24);
            }
            if (habilidadesSinTocarSuelo >= 5)
            {
                ManagerLogros.Instance.DesbloquearLogro(20);
            }
        }

        //if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.N))
        //{
        //    this.transform.position = FindObjectOfType<VueltaLobby>().transform.position;
        //}
        //if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.B))
        //{
        //    GameObject[] coleccionablesMonedas = GameObject.FindGameObjectsWithTag("Coleccionable");
        //    for (int i = 0; i < coleccionablesMonedas.Length; i++)
        //    {
        //        if (coleccionablesMonedas[i].GetComponentInChildren<ParticleSystem>() != null)
        //        {
        //            GameManager.Instance.CogerColeccionableNivel(coleccionablesMonedas[i]);

        //        }
        //    }
        //}
        if (auxTiempoSonidoSinEnergia > 0)
        {
            auxTiempoSonidoSinEnergia -= Time.deltaTime;
            if (auxTiempoSonidoSinEnergia < 0) auxTiempoSonidoSinEnergia = 0;
        }
        if (auxCheckJoystick <= 0)
        {
            auxCheckJoystick += 2;
            string[] names = Input.GetJoystickNames();
            for (int x = 0; x < names.Length; x++)
            {

                if (names[x].Length == 19)
                {
                    //print("PS4 CONTROLLER IS CONNECTED");
                    PS4_Controller = 1;
                    Xbox_One_Controller = 0;
                }
                if (names[x].Length == 33)
                {
                    //print("XBOX ONE CONTROLLER IS CONNECTED");
                    //set a controller bool to true
                    PS4_Controller = 0;
                    Xbox_One_Controller = 1;

                }
            }


            if (Xbox_One_Controller == 1)
            {
                joystick = InputManager.ActiveDevice;
            }
            else if (PS4_Controller == 1)
            {
                joystick = InputManager.ActiveDevice;
            }
            else
            {
                joystick = null;
            }
        }
        auxCheckJoystick -= Time.deltaTime;



        if (auxTiempoUsar > 0)
        {
            auxTiempoUsar -= Time.deltaTime;
            //GameObject.FindObjectOfType<ControllerPersonaje>().combateBloqueado = true;
        }
        else
        {
            if (GetComponent<VidaPlayer>().reiniciando == false)
            {
                auxTiempoUsar = 0;
                combateBloqueado = false;
            }

        }
        //move.x = controles.Gameplay.Movement.ReadValue<float>();

        if (auxCdDashReal > 0)
        {
            auxCdDashReal -= Time.deltaTime;
            if (grounded) puedoDash = true;
        }
        else
        {
            if (grounded) puedoDash = true;
        }
        if (auxCdDashAtravesar > 0)
        {
            auxCdDashAtravesar -= Time.deltaTime;
        }
        if (deteccionParedes) if (deteccionParedes2) if (ultimaNormal.y > -0.9f) DetectarPared();
        if (!movParedBloq) ComprobarParedes();
        if (auxtiempoTrasSaltoPared > 0)
        {
            movParedBloq = true;
            auxtiempoTrasSaltoPared -= Time.deltaTime;
            if (unavezSaltoDobleTrasPared == false)
            {
                unavezSaltoDobleTrasPared = true;
                saltoDobleHecho = false;
                saltoDobleHechoParaDashCaida = false;
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
            if (haciendoEnfoque == false) if (GetComponent<VidaPlayer>().reiniciando == false) dashBloqueado = false;
        }
     
        /*    if (pegadoPared == false)*/ if (!saltoBloqueado)
        {
            SaltoNormal();
        } 
       

     


        if (dashUnlock)
        {


            if (!dashBloqueado)
            {
                Dash();
            }
        }
        if (dashUnlock)
        {

            if (!dashCaidaBloqueado)
            {
                DashEnCaida();
            }
        }
        if (auxTiempoTrasSalirCombate > 0)
        {
            if (auxTiempoTrasSalirCombate < tiempoTrasSalirCombate * 0.5f)
            {
                dashBloqueado = false;
            }
            auxTiempoTrasSalirCombate -= Time.deltaTime;
            if (auxTiempoTrasSalirCombate <= 0)
            {
                AnularCombate();
                auxTiempoTrasSalirCombate = 0;
            }
        }
        if (movParedesUnlook) if (movParedBloq == false) MovimientoPared();
        animCC.SetFloat("SpeedY", rb.velocity.y);
        animCC.SetBool("Grounded", grounded);
        animCC.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));

        //DetectarEnemigos();
        //Chispazo();
    }
    private void FixedUpdate()
    {
       
        DetectarSuelo();
    if (chispazoUnlook) if (!combateBloqueado) Combate();
        DetectarSuelo();
        if (grounded && tengoMaxspeed)
        {
            //if (this.GetComponent<AudioManager>().sonidoLoop.isPlaying == false) this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidoLoop, this.GetComponent<AudioManager>().velMaxima);
            if (GetComponent<PlayerInput>().personajeInvertido)
            {
                GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasvelMax2, VFX.position, VFX.transform);
            }
            else
            {
                GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasvelMax, VFX.position, VFX.transform);
            }
        }
        if (auxTiempoTrasImpulso > 0)
        {
            auxTiempoTrasImpulso -= Time.deltaTime;
            if (auxTiempoTrasImpulso < tiempoTrasImpulso * 0.5f)
            {
                yaimpulsado = false;
            }
            if (auxTiempoTrasImpulso <= 0)
            {
                movimientoBloqueado = false;
                dashCaidaBloqueado = false;
            }
        }




        if (!movimientoBloqueado)
        {
            MoverPersonaje();
        }
        GirarPersonaje();
   





        if (tieneGravedad)
        {
            AplicarGravedad();
        }
        if (tiempoLoopAntesConsiderarLoop > 0.5f)
        {
            if (looping == false)
            {
                ManagerLogros.Instance.AddStat("TotalLoops");
                tiempoLoopAntesConsiderarLoop = 0;
            }
        }
    }
    public void AplicarImpulso(GameObject hijoDireccion, GameObject origen, float fuerza)
    {
        grounded = true;
        Vector2 dir = hijoDireccion.transform.position - origen.transform.position;
        dashEnCaida = false;
        dashCaidaBloqueado = true;
        if (Mathf.Abs(dir.y) < Mathf.Abs(dir.x))
        {

            movimientoBloqueado = true;

            rb.velocity = dir.normalized * fuerza;
        }
        else
        {
            Vector2 vel = rb.velocity;
            if (vel.y < 0)
            {
                vel.y = 0;
            }
            rb.velocity = dir.normalized * fuerza + vel;
        }

        //rb.velocity = Vector2.zero;
        speed = capSpeed;

        auxTiempoTrasImpulso = tiempoTrasImpulso;
        speed = capSpeed;
        tengoMaxspeed = true;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, distanciaCombate);
    }
    void AnularCombate()
    {
        movimientoBloqueado = false;
        saltoBloqueado = false;
        dashBloqueado = false;
        pInput.inputHorizBlock = false;
    }

    public void Combate()
    {
        if (haciendoCombate == false)
        {
            auxtiempoTrasSalirCombateInvuln -= Time.deltaTime;
            if (auxtiempoTrasSalirCombateInvuln <= 0.5F)
            {
                trailChispazo.GetComponent<TrailRenderer>().time -= Time.deltaTime;

                if (trailChispazo.GetComponent<TrailRenderer>().time < 0f)
                {
                    trailChispazo.SetActive(false);
                }
            }
        }
        if (enemigoCerca && mEnergy.actualEnergy >= mEnergy.energiaxEnemigoCombate)
        {
            Collider2D[] hitCirculo2 = Physics2D.OverlapCircleAll(this.transform.position, distanciaCombate);
            float mejorDistancia2 = 900000f;


            foreach (Collider2D col in hitCirculo2)
            {
                if (col != null)
                {
                    if (col.GetComponent<EnemigoPadre>() != null && !enemigosPasados.Contains(col.gameObject))
                    {
                        if (!col.GetComponent<EnemigoPadre>().stun)
                        {
                            if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) < mejorDistancia2)
                            {
                                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, col.gameObject.transform.position - this.transform.position, Vector2.Distance(this.transform.position, col.gameObject.transform.position) * 1.3f, capasEnemigos);

                                Debug.DrawRay(this.transform.position, col.gameObject.transform.position - this.transform.position, Color.magenta);
                                if (hit.collider != null)
                                {

                                    if (hit.collider.tag == "EnemigoDetectar" && Vector2.Distance(this.transform.position, col.gameObject.transform.position) < distanciaCombate)
                                    {

                                        enemigoTemporal = col.gameObject;
                                        mejorDistancia2 = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
                                    }
                                    else
                                    {
                                        if (enemigoTemporal != null && enemigoTemporal.gameObject.GetComponent<EnemigoPadre>() != null)
                                        {
                                            foreach (SpriteRenderer sr in enemigoTemporal.gameObject.GetComponentsInChildren<SpriteRenderer>())
                                            {
                                                sr.material.SetFloat("Grosor", 0f);
                                            }
                                        }
                                        EnemigoPadre[] enemigosTotal = GameObject.FindObjectsOfType<EnemigoPadre>();
                                        foreach (EnemigoPadre ep in enemigosTotal)
                                        {
                                            if (ep.GetComponent<SpriteRenderer>() != null)
                                            {
                                                ep.gameObject.GetComponent<SpriteRenderer>().material.SetFloat("Grosor", 0f);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            if (enemigoTemporal != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, enemigoTemporal.transform.position - this.transform.position, Vector2.Distance(this.transform.position, enemigoTemporal.transform.position) * 1.3f, capasEnemigos);

                Debug.DrawRay(this.transform.position, enemigoTemporal.transform.position - this.transform.position, Color.magenta);
                if (hit.collider != null)
                {

                    if (hit.collider.tag == "EnemigoDetectar" && Vector2.Distance(this.transform.position, enemigoTemporal.transform.position) < distanciaCombate)
                    {
                        if (enemigoTemporal.gameObject.GetComponent<EnemigoPadre>() != null)
                        {
                            foreach (SpriteRenderer sr in enemigoTemporal.gameObject.GetComponentsInChildren<SpriteRenderer>())
                            {
                                sr.material.SetFloat("Grosor", 0.99f);
                            }
                        }
                    }
                }
                else
                {

                }
            }

            if ((joystick != null && joystick.RightTrigger.IsPressed) || Input.GetKey(KeyCode.LeftControl))
            {
                if (pulsadoChispazo == false)
                {
                    pulsadoChispazo = true;

                }


                Collider2D[] hitCirculo = Physics2D.OverlapCircleAll(this.transform.position, distanciaCombate);



                float mejorDistancia = 900000f;

                enemigoSeleccionado = null;  //No se si esto va bien así
                ultimoEnemigoDetectado = null;

                foreach (Collider2D col in hitCirculo)
                {
                    if (col != null)
                    {

                        if (col.GetComponent<EnemigoPadre>() != null && !enemigosPasados.Contains(col.gameObject))
                        {
                            if (!col.GetComponent<EnemigoPadre>().stun)
                            {
                                if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) < mejorDistancia)
                                {
                                    RaycastHit2D hit = Physics2D.Raycast(this.transform.position, col.gameObject.transform.position - this.transform.position, Vector2.Distance(this.transform.position, col.gameObject.transform.position) * 1.3f, capasEnemigos);

                                    Debug.DrawRay(this.transform.position, col.gameObject.transform.position - this.transform.position, Color.magenta);
                                    if (hit.collider != null)
                                    {
                                        if (hit.collider.tag == "EnemigoDetectar" && Vector2.Distance(this.transform.position, col.gameObject.transform.position) < distanciaCombate)
                                        {

                                            if (hit.collider.name == col.name)
                                            {
                                                trailChispazo.SetActive(true);
                                                trailChispazo.GetComponent<TrailRenderer>().time = 0.5f;
                                                enemigoSeleccionado = col.gameObject;
                                                ultimoEnemigoDetectado = enemigoSeleccionado;
                                                //ultimoEnemigoDetectado.GetComponentInChildren<SpriteRenderer>().material.SetFloat("Grosor", Mathf.Lerp(0, 0.99f, 1));

                                                mejorDistancia = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
                                            }

                                        }
                                        else
                                        {
                                            EnemigoPadre[] enemigosTotal = GameObject.FindObjectsOfType<EnemigoPadre>();
                                            foreach (EnemigoPadre ep in enemigosTotal)
                                            {
                                                if (ep.GetComponent<SpriteRenderer>() != null)
                                                {
                                                    ep.gameObject.GetComponent<SpriteRenderer>().material.SetFloat("Grosor", 0f);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                if (ultimoEnemigoDetectado != null)
                {
                    RaycastHit2D hit = Physics2D.Raycast(this.transform.position, ultimoEnemigoDetectado.transform.position - this.transform.position, Vector2.Distance(this.transform.position, ultimoEnemigoDetectado.transform.position) * 1.3f, capasEnemigos);

                    Debug.DrawRay(this.transform.position, ultimoEnemigoDetectado.transform.position - this.transform.position, Color.magenta);
                    if (hit.collider != null)
                    {

                        if (hit.collider.tag == "EnemigoDetectar" && Vector2.Distance(this.transform.position, ultimoEnemigoDetectado.transform.position) < distanciaCombate)
                        {
                            destinoCombate = ultimoEnemigoDetectado.transform./*.parent.GetChild(0).transform.*/position;

                            haciendoCombate = true;
                        }
                        else
                        {
                            //haciendoCombate = false;
                        }
                    }
                }
            }
        }
        else if ((joystick != null && joystick.RightTrigger.IsPressed) || Input.GetKey(KeyCode.LeftControl) && mEnergy.actualEnergy < mEnergy.energiaxEnemigoCombate)
        {
            if (auxTiempoSonidoSinEnergia == 0)
            {
                NewAudioManager.Instance.Play("PlayerNoEnergy");
                auxTiempoSonidoSinEnergia = tiempoSonidoSinEnergia;
            }

        }

        if (haciendoCombate)
        {
            hechoConseguidoPuntoHabChispazo = false;
            ultimaNormal = Vector2.zero;
            bocabajocambiotecla = false;
            auxtiempoTrasSalirCombateInvuln = tiempoTrasSalirCombateInvuln;
            pInput.inputHorizBlock = true;
            movimientoBloqueado = true;
            saltoBloqueado = true;
            dashBloqueado = true;
            if (saltoDobleHecho == true)
            {
                saltoDobleHecho = false;
                saltoDobleHechoParaDashCaida = false;
            }
            if (puedoDash == false) puedoDash = true;
            rb.gravityScale = 0;


            if (ultimoEnemigoDetectado != null)
            {

                if (Vector3.Distance(ultimoEnemigoDetectado.transform.position, this.transform.position) > 2f)
                {
                    if (!enemigosPasados.Contains(ultimoEnemigoDetectado))
                    {
                        direccionCombate = (ultimoEnemigoDetectado.transform.position - this.transform.position).normalized;
                        direccionCombate = new Vector3(direccionCombate.x, direccionCombate.y, 0);
                    }

                    //if(Vector3.Distance(ultimoEnemigoDetectado.transform.position, this.transform.position) > 8)
                    //{
                    //    rb.velocity = direccionCombate * velocidadCombate*1.3f;
                    //}
                    //else if (Vector3.Distance(ultimoEnemigoDetectado.transform.position, this.transform.position) > 6)
                    //{
                    //    rb.velocity = direccionCombate * velocidadCombate;
                    //}
                    //if (Vector3.Distance(ultimoEnemigoDetectado.transform.position, this.transform.position) > 3)
                    //{
                    //    rb.velocity = direccionCombate * velocidadCombate*0.7f;
                    //}
                    //if (Vector3.Distance(ultimoEnemigoDetectado.transform.position, this.transform.position) >= 1)
                    //{
                    //    rb.velocity = direccionCombate * velocidadCombate*0.3f;
                    //}
                    if (pulsadoChispazo == true)
                    {
                        rb.velocity = direccionCombate + direccionCombate * velocidadCombate * 0.8f * Mathf.Clamp(Vector3.Distance(ultimoEnemigoDetectado.transform.position, this.transform.position), 0.8f, distanciaCombate * 0.7f) * 0.15f;
                    }

                    velocidadCombateUltima = direccionCombate * velocidadCombate;
                }
                else
                {
                    if (enemigosPasados.Contains(ultimoEnemigoDetectado) == false)
                    {
                        if (Vector3.Distance(ultimoEnemigoDetectado.transform.position, this.transform.position) <= 2f)
                        {

                            //if (!enemigosPasados.Contains(ultimoEnemigoDetectado))
                            //{
                            //    direccionCombate = (ultimoEnemigoDetectado.transform.position - this.transform.position).normalized;
                            //    direccionCombate = new Vector3(direccionCombate.x, direccionCombate.y, 0);
                            //}


                            //direccionCombate = (ultimoEnemigoDetectado.transform.position -this.transform.position).normalized;

                            //direccionCombate = new Vector3(direccionCombate.x, direccionCombate.y, 0);
                            //velocidadCombateUltima = direccionCombate * velocidadCombate;

                            if (this.transform.position != ultimoEnemigoDetectado.transform.position)
                            {
                                this.transform.position = ultimoEnemigoDetectado.transform.position;
                            }
                            if (ultimoEnemigoDetectado.GetComponent<EnemigoEmbestida2>() != null)
                            {
                                if (ultimoEnemigoDetectado.GetComponent<EnemigoEmbestida2>() != null)
                                {
                                    ultimoEnemigoDetectado.GetComponent<EnemigoEmbestida2>().escudo.SetActive(false);
                                }
                            }


                            ultimoEnemigoDetectado.GetComponent<EnemigoPadre>().Stun();

                            Debug.Log("ADD ENEMIGOS STUN");
                            NewAudioManager.Instance.Play("PlayerChispazo");
                            NewAudioManager.Instance.Play("EnemigosStun");
                            mEnergy.RestarEnergia(mEnergy.energiaxEnemigoCombate);
                            mEnergy.tiempoAux = 0.15f;
                            float offset = 0;
                            if (ultimoEnemigoDetectado.GetComponent<EnemigoEmbestida2>() != null)
                            { offset = offsetSalidaEnemigoTerrestreY; }

                            Vector2 resultante = new Vector2(velocidadCombateUltima.x, velocidadCombateUltima.y + offset);
                            rb.velocity = resultante * salirCombate;

                            enemigosPasados.Add(ultimoEnemigoDetectado);
                            foreach (SpriteRenderer sr in ultimoEnemigoDetectado.GetComponentsInChildren<SpriteRenderer>())
                            {
                                if (sr.GetComponent<SpriteRenderer>() != null)
                                {
                                    sr.gameObject.GetComponent<SpriteRenderer>().material.SetFloat("Grosor", 0f);
                                }
                            }
                            //RESETEAR ENEMIGO

                            constantegravedad = 1;
                            ultimoEnemigoPasado = ultimoEnemigoDetectado;
                            ultimoEnemigoDetectado = null;
                        }
                    }

                }
            }
            else
            {
                if (rb.gravityScale == 0)
                {
                    rb.gravityScale = originalgravity;
                }
                //rb.velocity = Vector2.zero;
                //rb.AddForce(velocidadCombateUltima * salirCombate, ForceMode2D.Impulse);



                if (!enemigosPasados.Contains(ultimoEnemigoPasado))
                {
                    enemigosPasados.Add(ultimoEnemigoPasado);
                    foreach (SpriteRenderer sr in ultimoEnemigoPasado.GetComponentsInChildren<SpriteRenderer>())
                    {
                        if (sr.GetComponent<SpriteRenderer>() != null)
                        {
                            sr.gameObject.GetComponent<SpriteRenderer>().material.SetFloat("Grosor", 0f);
                        }
                    }
                }



                haciendoCombate = false;


                if (velocidadCombateUltima != Vector3.zero)
                {
                    //rb.velocity = Vector2.zero;
                    //rb.AddForce(velocidadCombateUltima * salirCombate, ForceMode2D.Impulse);

                    auxTiempoTrasSalirCombate = tiempoTrasSalirCombate;

                    float offset = 0;
                    if (ultimoEnemigoPasado.GetComponent<EnemigoEmbestida2>() != null)
                    {
                        if (ultimoEnemigoPasado.GetComponent<EnemigoEmbestida2>() != null)
                        {
                            ultimoEnemigoPasado.GetComponent<EnemigoEmbestida2>().escudo.SetActive(false);
                        }
                    }
                    ultimoEnemigoPasado.GetComponent<EnemigoPadre>().Stun();

                    Debug.Log("ADD ENEMIGOS STUN");
                    mEnergy.RestarEnergia(mEnergy.energiaxEnemigoCombate);
                    mEnergy.tiempoAux = 0.15f;
                    if (ultimoEnemigoPasado.GetComponent<EnemigoEmbestida2>() != null)
                    {
                        offset = offsetSalidaEnemigoTerrestreY;
                    }

                    Vector2 resultante = new Vector2(velocidadCombateUltima.x, velocidadCombateUltima.y);
                    rb.velocity = resultante * salirCombate;

                }

                direccionCombate = Vector3.zero;
                velocidadCombateUltima = Vector3.zero;
            }
        }

        if ((joystick != null && joystick.RightTrigger.WasReleased) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            //if (movimientoBloqueado == true)
            //{
            //    pulsadoChispazo = false;
            //}

            if (haciendoCombate == true) dashBloqueado = false;
            haciendoCombate = false;
            if (rb.gravityScale == 0)
            {
                rb.gravityScale = originalgravity;
            }
            if (enemigosPasados.Count >= 0)
            {
                pulsadoChispazo = false;
                if (!enemigosPasados.Contains(ultimoEnemigoDetectado))
                {
                    enemigosPasados.Add(ultimoEnemigoDetectado);
                    //foreach (SpriteRenderer sr in ultimoEnemigoDetectado.GetComponentsInChildren<SpriteRenderer>())
                    //{
                    //    if (sr.GetComponent<SpriteRenderer>() != null)
                    //    {
                    //        sr.gameObject.GetComponent<SpriteRenderer>().material.SetFloat("Grosor", 0f);
                    //    }
                    //}
                }

                auxTiempoTrasSalirCombate = tiempoTrasSalirCombate;
                //if (pulsadoChispazo == true) { }
                if (velocidadCombateUltima != Vector3.zero)
                {
                    //rb.velocity = Vector2.zero;
                    //rb.AddForce(velocidadCombateUltima * salirCombate, ForceMode2D.Impulse);
                    if (ultimoEnemigoPasado != null)
                    {
                        ultimoEnemigoPasado.GetComponent<EnemigoPadre>().Stun();

                        Debug.Log("ADD ENEMIGOS STUN");
                        mEnergy.RestarEnergia(mEnergy.energiaxEnemigoCombate);
                        mEnergy.tiempoAux = 0.15f;
                        Vector2 resultante = new Vector2(velocidadCombateUltima.x, velocidadCombateUltima.y + 3);

                        rb.velocity = resultante * salirCombate;
                    }
                    else if (enemigosPasados.Count == 0)
                    {

                        ultimoEnemigoDetectado.GetComponent<EnemigoPadre>().Stun();

                        Debug.Log("ADD ENEMIGOS STUN");
                        mEnergy.RestarEnergia(mEnergy.energiaxEnemigoCombate);
                        mEnergy.tiempoAux = 0.15f;
                        Vector2 resultante = new Vector2(velocidadCombateUltima.x, velocidadCombateUltima.y + 3);

                        rb.velocity = resultante * salirCombate;
                    }
                    else
                    {
                        rb.velocity = velocidadCombateUltima * salirCombate;
                    }

                }
                direccionCombate = Vector3.zero;
                velocidadCombateUltima = Vector3.zero;
            }
        }

        if (enemigosPasados.Count >= 0 && pulsadoChispazo == false && grounded == true)
        {

            enemigosPasados.Clear();
        }
        else if (enemigosPasados.Count >= 0 && grounded)
        {
            enemigosPasados.Clear();

        }

        if (enemigosPasados.Count >= 0 && pulsadoChispazo == false && haciendoCombate == false && grounded == false)
        {

            if (hechoConseguidoPuntoHabChispazo == false) { hechoConseguidoPuntoHabChispazo = true;  habilidadesSinTocarSuelo += 1; }
        }
       

    }

    //public void DetectarEnemigos()
    //{
    //    //DE TECTAR ENEMIGOS AL PASAR RATON
    //    Vector3 pz2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    pz2.z = 0;
    //    Vector3 direction = pz2 - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), direction, 10000, capasEnemigos);
    //    Debug.DrawRay(Input.mousePosition, direction);


    //    if (hit != false)
    //    {

    //        if (hit.collider.tag == "Enemigo")
    //        {
    //            if (haciendoChispazo == false) ultimoEnemigoDetectado = hit.collider.gameObject;
    //        }
    //        else
    //        {
    //            if (haciendoChispazo == false) ultimoEnemigoDetectado = null;
    //        }

    //    }
    //    else
    //    {
    //        //if (haciendoChispazo == false) ultimoEnemigoDetectado = null;
    //    }
    //}
    //public void PonerCollider()
    //{
    //    ultimoenemigo2.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
    //}
    //public void Chispazo()
    //{
    //    if (ultimoEnemigoDetectado != null)
    //    {
    //        if (Input.GetKey(KeyCode.LeftControl))
    //        {
    //            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, ultimoEnemigoDetectado.transform.position - this.transform.position, Vector2.Distance(this.transform.position, ultimoEnemigoDetectado.transform.position) * 1.3f, capasEnemigos);

    //            Debug.DrawRay(this.transform.position, ultimoEnemigoDetectado.transform.position - this.transform.position);
    //            if (hit.collider != null)
    //            {
    //                if (hit.collider.tag == "Enemigo")
    //                {
    //                    if (Vector2.Distance(this.transform.position, ultimoEnemigoDetectado.transform.position) < distanciaChispazo)
    //                    {

    //                        if (haciendoChispazo == false) destinoChispazo = ultimoEnemigoDetectado.transform.parent.GetChild(0).transform.position;
    //                        ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
    //                        //if (haciendoChispazo == false) 
    //                        haciendoChispazo = true;
    //                    }
    //                }
    //            }

    //        }
    //    }
    //    else
    //    {
    //        if (enemigoCerca)
    //        {
    //            if (Input.GetKey(KeyCode.LeftControl))
    //            {
    //                Collider2D[] resultados = new Collider2D[15];
    //                if (pInput.personajeInvertido == false)
    //                {
    //                    //RaycastHit2D[] hitCirculo= Physics2D.CircleCast(this.transform.position,distanciaChispazo,Vector2.zero,0,hitCirculo)

    //                    int enemigos = Physics2D.OverlapCircleNonAlloc(this.transform.position, distanciaChispazo, resultados);
    //                    float mejorDistancia = 900000f;

    //                    foreach (Collider2D col in resultados)
    //                    {

    //                        if (col != null)
    //                        {


    //                            if (col.gameObject.tag == "Enemigo")
    //                            {
    //                                if (col.gameObject.transform.position.x > this.transform.position.x)
    //                                {
    //                                    if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) < mejorDistancia)
    //                                    {
    //                                        enemigoSeleccionado = col.gameObject;
    //                                        mejorDistancia = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    if (enemigoSeleccionado == null)
    //                                    {

    //                                    }
    //                                    else
    //                                    {
    //                                        if (enemigoSeleccionado.transform.position.x > this.transform.position.x)
    //                                        {

    //                                        }
    //                                        else
    //                                        {
    //                                            if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) < mejorDistancia)
    //                                            {
    //                                                enemigoSeleccionado = col.gameObject;
    //                                                mejorDistancia = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //                else
    //                {

    //                    int enemigos = Physics2D.OverlapCircleNonAlloc(this.transform.position, distanciaChispazo, resultados);
    //                    float mejorDistancia = 900000f;

    //                    foreach (Collider2D col in resultados)
    //                    {

    //                        if (col != null)
    //                        {


    //                            if (col.tag == "Enemigo")
    //                            {
    //                                if (col.gameObject.transform.position.x < this.transform.position.x)
    //                                {
    //                                    if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) < mejorDistancia)
    //                                    {
    //                                        enemigoSeleccionado = col.gameObject;
    //                                        mejorDistancia = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    if (enemigoSeleccionado == null)
    //                                    {

    //                                    }
    //                                    else
    //                                    {
    //                                        if (enemigoSeleccionado.transform.position.x < this.transform.position.x)
    //                                        {

    //                                        }
    //                                        else
    //                                        {
    //                                            if (Vector2.Distance(col.gameObject.transform.position, this.transform.position) < mejorDistancia)
    //                                            {
    //                                                enemigoSeleccionado = col.gameObject;
    //                                                mejorDistancia = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                }

    //                ultimoEnemigoDetectado = enemigoSeleccionado;
    //                if (ultimoEnemigoDetectado != null)
    //                {
    //                    RaycastHit2D hit = Physics2D.Raycast(this.transform.position, ultimoEnemigoDetectado.transform.position - this.transform.position, Vector2.Distance(this.transform.position, ultimoEnemigoDetectado.transform.position) * 1.3f, capasEnemigos);
    //                    if (hit.collider != null)
    //                    {
    //                        if (hit.collider.tag == "Enemigo")
    //                        {
    //                            if (Vector2.Distance(this.transform.position, ultimoEnemigoDetectado.transform.position) < distanciaChispazo)
    //                            {
    //                                if (haciendoChispazo == false) destinoChispazo = ultimoEnemigoDetectado.transform.parent.GetChild(0).transform.position;
    //                                ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
    //                                //if (haciendoChispazo == false) 
    //                                haciendoChispazo = true;
    //                            }
    //                        }
    //                    }
    //                }

    //            }
    //        }
    //    }

    //    if (haciendoChispazo)
    //    {
    //        if (!puedeSalirChispazo)
    //        {


    //            if (unavezSalirChispazo == true)
    //            {
    //                unavezSalirChispazo = false;
    //                saltoBloqueado = false;
    //                movimientoBloqueado = false;
    //            }
    //            else
    //            {



    //                Vector2 direccion = transform.position - destinoChispazo;

    //                float distancia = Vector2.Distance(transform.position, destinoChispazo);

    //                float fuerzaRealAtraccion;
    //                fuerzaRealAtraccion = fuerzaAcercarseChispazo + distancia * 100;

    //                if (chispazoPerdido == false)
    //                {
    //                    AnularGravedad();
    //                    if (distancia <= 2f)
    //                    {
    //                        ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;

    //                        constantegravedad = 1;
    //                        dashEnCaida = false;
    //                        animCC.SetBool("cayendo", dashEnCaida);
    //                        dashBloqueado = true;
    //                        dashCaidaBloqueado = true;


    //                        if (tiempoAntesChispazo > 0)
    //                        {
    //                            ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
    //                            saltoDobleHecho = false;
    //                            rb.velocity = Vector2.zero;
    //                            puedeSalirChispazo = true;
    //                            this.transform.position = destinoChispazo;

    //                        }
    //                        else
    //                        {
    //                            ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
    //                            tiempoAntesChispazo = auxTiempoChispazo;
    //                            puedeSalirChispazo = false;
    //                            chispazoPerdido = true;
    //                            haciendoChispazo = false;
    //                            PermitirGravedad();
    //                        }




    //                    }
    //                    else if (distancia > 2f)
    //                    {

    //                        if (chispazoPerdido == false)
    //                        {
    //                            if (entradochispazo == false)
    //                            {
    //                                entradochispazo = true;
    //                                rb.velocity = Vector2.zero;
    //                            }
    //                            ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
    //                            puedeSalirChispazo = false;

    //                            rb.AddForce(-direccion.normalized * fuerzaRealAtraccion * Time.deltaTime);
    //                        }
    //                        else
    //                        {
    //                            ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
    //                        }

    //                    }
    //                    else if (distancia > 3f)
    //                    {
    //                        ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
    //                    }
    //                }
    //            }

    //        }
    //        else
    //        {
    //            tiempoAntesChispazo -= Time.deltaTime;
    //            movimientoBloqueado = true;
    //            saltoBloqueado = true;
    //            if (tiempoAntesChispazo > 0)
    //            {
    //                if (ultimoEnemigoDetectado != null) ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
    //                saltoDobleHecho = false;
    //                rb.velocity = Vector2.zero;
    //                puedeSalirChispazo = true;
    //                this.transform.position = destinoChispazo;

    //            }
    //            else
    //            {
    //                tiempoAntesChispazo = auxTiempoChispazo;
    //                puedeSalirChispazo = false;
    //                chispazoPerdido = true;

    //                ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = false;
    //                rb.AddForce(new Vector2(0, 1) * fuerzaChispazoPerdido); PermitirGravedad();

    //                unavezSalirChispazo = true;
    //                entradochispazo = false;
    //                dashBloqueado = false;
    //                dashCaidaBloqueado = false;

    //                saltoBloqueado = false;
    //                movimientoBloqueado = false;

    //                haciendoChispazo = false;

    //            }



    //            Vector3 pz2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //            pz2.z = 0;

    //            Vector2 direccion2 = (pz2 - this.transform.position).normalized;

    //            if (Input.GetKey(KeyCode.LeftControl))
    //            {
    //                puedeSalirChispazo = false;

    //                entradochispazo = false;
    //                rb.AddForce(new Vector2(direccion2.normalized.x, direccion2.normalized.y) * fuerzaSalidaChispazo);

    //                if (new Vector2(direccion2.normalized.x, direccion2.normalized.y).x < 0)
    //                {
    //                    pInput.personajeInvertido = true;
    //                    transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
    //                }
    //                else
    //                {
    //                    pInput.personajeInvertido = false;
    //                    transform.Find("Cuerpo").localScale = new Vector2(1, 1);
    //                }
    //                ultimoenemigo2 = ultimoEnemigoDetectado;
    //                ultimoEnemigoDetectado.transform.parent.GetChild(1).GetComponent<BoxCollider2D>().isTrigger = true;
    //                Invoke("PonerCollider", 0.5f);
    //                unavezSalirChispazo = true;

    //                dashBloqueado = false;
    //                dashCaidaBloqueado = false;
    //                tiempoAntesChispazo = auxTiempoChispazo;
    //                saltoBloqueado = false;
    //                movimientoBloqueado = false;
    //                PermitirGravedad();

    //                Vector3 pz3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //                pz3.z = 0;
    //                Vector3 direction = pz3 - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), direction, 10000, capasEnemigos);
    //                Debug.DrawRay(Input.mousePosition, direction);


    //                if (hit != false)
    //                {
    //                    if (hit.collider.tag == "Enemigo")
    //                    {
    //                        ultimoEnemigoDetectado = hit.collider.gameObject;
    //                        destinoChispazo = ultimoEnemigoDetectado.transform.parent.GetChild(0).transform.position;
    //                    }
    //                    else
    //                    {
    //                        chispazoPerdido = true;
    //                        haciendoChispazo = false;
    //                    }
    //                }
    //                else
    //                {
    //                    chispazoPerdido = true;
    //                    haciendoChispazo = false;
    //                }


    //            }
    //        }

    //    }
    //    else
    //    {
    //        unavezSalirChispazo = false;
    //        chispazoPerdido = false;
    //    }
    //}
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


        if (pegadoPared)
        {
            if (!pInput.personajeInvertido)
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
        }
        else
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
            puedoDash = true;
            //if (pInput.inputVertical != 0)
            //{
            //    if (this.GetComponent<AudioManager>().sonidoLoop.isPlaying == false) this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidoLoop, this.GetComponent<AudioManager>().moverPared);
            //}
            //else
            //{
            //    this.GetComponent<AudioManager>().Stop(this.GetComponent<AudioManager>().sonidoLoop);
            //}
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
            {
                FindObjectOfType<NewAudioManager>().Play("PlayerWallMove");
            }
            else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
            {
                FindObjectOfType<NewAudioManager>().Stop("PlayerWallMove");
            }
            if (heEntradoParedUnaVez == false)
            {
                transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                if (transform.Find("Cuerpo").localScale.x < 0)
                {
                    pInput.personajeInvertido = true;
                }
                else if (transform.Find("Cuerpo").localScale.x > 0)
                {
                    pInput.personajeInvertido = false;
                }
                speed = 0;
                FindObjectOfType<NewAudioManager>().Play("PlayerWall");
                //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().pegarPared);
                heEntradoParedUnaVez = true;
            }
            saltoDobleHecho = false;
            saltoDobleHechoParaDashCaida = false;
        }
        else
        {
            if (heEntradoParedUnaVez == true)
            {
                //transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                if (transform.Find("Cuerpo").localScale.x < 0)
                {
                    pInput.personajeInvertido = true;
                }
                else if (transform.Find("Cuerpo").localScale.x > 0)
                {
                    pInput.personajeInvertido = false;
                }
                //this.GetComponent<AudioManager>().Stop(this.GetComponent<AudioManager>().sonidoLoop);
                heEntradoParedUnaVez = false;
                //movimientoBloqueado = false;
                ultimaNormal = new Vector2(0, 1);
                //VELOCIDAD A LA QUE SALE DE LA PARED POR ARRIBA
                if (auxtiempoTrasSaltoPared <= 0) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
        if (!tocandoizquierda && !tocandoderecha && pegadoPared)
        {
            pegadoPared = false;
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
            habilidadesSinTocarSuelo = 0;
            tiempoSinTocarSuelo = 0;
            if (grounded == false)
            {
                animCC.SetBool("PegadoPared", true);
                //if (rb.velocity.y < -1)
                //{
                saltoBloqueado = true;
                estoyDasheando = false;
                dashEnCaida = false;
                animCC.SetBool("cayendo", dashEnCaida);
                AnularGravedad();
                dashBloqueado = true;
                dashCaidaBloqueado = true;
                tieneGravedad = false;
                //}
                constantegravedad = 1;
                maxTiempoPared -= Time.deltaTime;


                rb.velocity = new Vector2(0, rb.velocity.y);
                if (((joystick != null && Mathf.Abs(joystick.LeftStickY) > 0.8f) || Mathf.Abs(pInput.inputVertical) > 0.8f) || (joystick == null && Mathf.Abs(pInput.inputVertical) > 0.0f))
                {
                    if (tocandoizquierda)
                    {
                        transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                    }
                    else if (tocandoderecha)
                    {
                        transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                    }
                    rb.velocity = new Vector2(0, pInput.inputVertical * speedpared * Time.fixedDeltaTime);
                    animCC.SetFloat("MovimientoPared", Mathf.Abs(pInput.inputVertical));
                }
                else
                {
                   

                    if (tocandoizquierda)
                    {
                        transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                    }
                    else if (tocandoderecha)
                    {
                        transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                    }
                    rb.velocity = new Vector2(0, 0);
                    animCC.SetFloat("MovimientoPared", 0);
                }



                //if (Input.GetButtonDown("Jump"))
                if (joystick != null)
                {
                    if (joystick.Action1.WasPressed || Input.GetButtonDown("Jump"))
                    {
                        //if (saltoPulsado == true)
                        //{
                        auxtiempoTrasSaltoPared += tiempoTrasSaltoPared;

                        lastJumpPared = true;
                        deteccionParedes2 = false;
                        pegadoPared = false;
                        if (ultimaParedPosicion.x < this.transform.position.x)
                        {
                            pInput.ultimoInputHorizontal = 1;
                            pInput.personajeInvertido = false;
                            //transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                            rb.velocity = Vector2.zero;
                            rb.AddForce(fuerzaSaltoPared * new Vector2(0.6f, 0.6f));
                          
                            habilidadesSinTocarSuelo += 1;
                            maxTiempoPared = auxpared; maxYAntesMorir = -999999;

                        }
                        else
                        {

                            pInput.ultimoInputHorizontal = -1;
                            pInput.personajeInvertido = true;
                            //transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                            rb.velocity = Vector2.zero;
                            rb.AddForce(fuerzaSaltoPared * new Vector2(-0.6f, 0.6f)); habilidadesSinTocarSuelo += 1;
                            maxTiempoPared = auxpared; maxYAntesMorir = -999999;

                        }
                        animCC.SetTrigger("WallJump");
                        ultimaNormal = new Vector2(ultimaNormal.x, 1);
                        tocando = false;
                        dashBloqueado = false;
                        dashCaidaBloqueado = false;
                        unavezsalirpared = true;
                        FindObjectOfType<NewAudioManager>().Play("PlayerWallJump");
                        //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().saltarPared);

                    }
                    else
                    {
                        unavezsalirpared = false;

                        saltoBloqueado = false;
                        dashBloqueado = false;
                        dashCaidaBloqueado = false;
                        maxTiempoPared = auxpared;
                        tieneGravedad = true;
                        saltoDobleHecho = false;
                        movimientoBloqueado = false;
                        saltoDobleHechoParaDashCaida = false;
                    }
                }
                else
                {

                    if (Input.GetButtonDown("Jump"))
                    {
                        //if (saltoPulsado == true)
                        //{
                        auxtiempoTrasSaltoPared += tiempoTrasSaltoPared;

                        lastJumpPared = true;
                        deteccionParedes2 = false;
                        pegadoPared = false;
                        if (ultimaParedPosicion.x < this.transform.position.x)
                        {
                            pInput.ultimoInputHorizontal = 1;
                            pInput.personajeInvertido = false;
                            //transform.Find("Cuerpo").localScale = new Vector2(1, 1);
                            rb.velocity = Vector2.zero;
                            rb.AddForce(fuerzaSaltoPared * new Vector2(0.6f, 0.6f));  habilidadesSinTocarSuelo += 1;
                            maxTiempoPared = auxpared; maxYAntesMorir = -999999;

                        }
                        else
                        {
                            pInput.personajeInvertido = true;
                            pInput.ultimoInputHorizontal = -1;
                            //transform.Find("Cuerpo").localScale = new Vector2(-1, 1);
                            rb.velocity = Vector2.zero;
                            rb.AddForce(fuerzaSaltoPared * new Vector2(-0.6f, 0.6f));  habilidadesSinTocarSuelo += 1;
                            maxTiempoPared = auxpared; maxYAntesMorir = -999999;

                        }
                        animCC.SetTrigger("WallJump");
                        ultimaNormal = new Vector2(ultimaNormal.x, 1);
                        tocando = false;
                        dashBloqueado = false;
                        dashCaidaBloqueado = false;
                        unavezsalirpared = true;
                        FindObjectOfType<NewAudioManager>().Play("PlayerWallJump");
                        //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().saltarPared);
                    }
                    else
                    {
                        unavezsalirpared = false;

                        saltoBloqueado = false;
                        dashBloqueado = false;
                        dashCaidaBloqueado = false;
                        maxTiempoPared = auxpared;
                        tieneGravedad = true;
                        saltoDobleHecho = false;
                        movimientoBloqueado = false;
                        saltoDobleHechoParaDashCaida = false;
                    }
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
                enemigosPasados.Clear();
                haciendoCombate = false;
                pulsadoChispazo = false;
                saltoBloqueado = false;
                dashBloqueado = false;
                dashCaidaBloqueado = false;
                maxTiempoPared = auxpared;
                tieneGravedad = true;
                saltoDobleHecho = false;
                movimientoBloqueado = false;
                saltoDobleHechoParaDashCaida = false;
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
            if (ultimaNormal.y > 0)
            {
                if (pInput.inputHorizontal == 0)
                {
                    bocabajocambiotecla = false;
                }
                if (Math.Sign(pInput.ultimoInputHorizontal) == Math.Sign(rb.velocity.x) && Mathf.Abs(rb.velocity.x) > 2)
                {
                    if (bocabajocambiotecla == true)
                    {
                        bocabajocambiotecla = false;
                    }
                }
            }

            if ((ultimaNormal.y > 0.8f) && (looping == false))
            {

                if (Math.Sign(pInput.ultimoInputHorizontal) != Math.Sign(rb.velocity.x) && Mathf.Abs(rb.velocity.x) > 1)
                {

                    if (auxCdDashReal < auxCdDashReal * 0.8f)
                    {
                        if (tengoMaxspeed)
                        {


                            Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                            this.rb.AddForce(direccion * fuerzaCambioSentido * 0.8f * Time.deltaTime);


                        }
                        else
                        {
                            //rb.velocity =new Vector2 (0, rb.velocity.y);

                            Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * -pInput.inputHorizontal;

                            this.rb.AddForce(direccion * fuerzaCambioSentido * Time.deltaTime);


                        }
                    }

                    cambioSentidoReciente = true;
                    StopCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                    StartCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                    //rb.velocity = new Vector2(-rb.velocity.x * 0.7f, rb.velocity.y);
                    speed = 0;
                }

            }
            else if ((ultimaNormal.y > 0.0f) && (looping == true))
            {


                if (Math.Sign(pInput.ultimoInputHorizontal) != Math.Sign(rb.velocity.x) && Mathf.Abs(rb.velocity.x) > 1)
                {

                    if (auxCdDashReal < auxCdDashReal * 0.5f)
                    {
                        if (tengoMaxspeed)
                        {
                            if (bocabajocambiotecla)
                            {
                                //Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * pInput.inputHorizontal;
                                //this.rb.AddForce(direccion * fuerzaCambioSentido * 0.2f * Time.deltaTime);
                            }
                            else
                            {
                                Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                                this.rb.AddForce(direccion * fuerzaCambioSentido * 0.2f * Time.deltaTime);
                            }




                        }
                        else
                        {
                            //rb.velocity =new Vector2 (0, rb.velocity.y);

                            if (bocabajocambiotecla)
                            {
                                //Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * pInput.inputHorizontal;
                                //this.rb.AddForce(direccion * fuerzaCambioSentido * 0.2f * Time.deltaTime);
                            }
                            else
                            {
                                Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                                this.rb.AddForce(direccion * fuerzaCambioSentido * 0.2f * Time.deltaTime);
                            }


                        }
                    }

                    cambioSentidoReciente = true;
                    StopCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                    StartCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                    //rb.velocity = new Vector2(-rb.velocity.x * 0.7f, rb.velocity.y);
                    speed = 0;
                }
            }
            else if ((looping == true))
            {


                if ((ultimaNormal.y < -0.0f) && (looping == true))
                {
                    if (Math.Sign(pInput.ultimoInputHorizontal) == Math.Sign(rb.velocity.x) && Mathf.Abs(rb.velocity.x) > 1)
                    {
                        bocabajocambiotecla = true;

                    }
                    if (Math.Sign(pInput.ultimoInputHorizontal) != Math.Sign(rb.velocity.x) && Mathf.Abs(rb.velocity.x) > 1)
                    {
                        if (auxCdDashReal < auxCdDashReal * 0.5f)
                        {
                            if (tengoMaxspeed)
                            {
                                if (bocabajocambiotecla)
                                {
                                    Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * pInput.inputHorizontal;
                                    this.rb.AddForce(direccion * fuerzaCambioSentido * 0.2f * Time.deltaTime);
                                }
                                else
                                {
                                    //Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                                    //this.rb.AddForce(direccion * fuerzaCambioSentido * 0.2f * Time.deltaTime);
                                }




                            }
                            else
                            {
                                //rb.velocity =new Vector2 (0, rb.velocity.y);

                                if (bocabajocambiotecla)
                                {
                                    Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * pInput.inputHorizontal;
                                    this.rb.AddForce(direccion * fuerzaCambioSentido * 0.2f * Time.deltaTime);
                                }
                                else
                                {
                                    //    Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                                    //    this.rb.AddForce(direccion * fuerzaCambioSentido * 0.2f * Time.deltaTime);
                                }


                            }
                        }

                        cambioSentidoReciente = true;
                        StopCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                        StartCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                        //rb.velocity = new Vector2(-rb.velocity.x * 0.7f, rb.velocity.y);
                        speed = 0;
                    }
                    if (pInput.inputHorizontal == 0)
                    {
                        bocabajocambiotecla = true;
                    }
                }
                else
                {
                    if ((ultimaNormal.y > 0f) && (looping == true))
                    {

                        if (bocabajocambiotecla == true)
                        {
                            if (pInput.inputHorizontal == 0)
                            {
                                bocabajocambiotecla = false;
                            }

                            //if (Math.Sign(pInput.ultimoInputHorizontal) != Math.Sign(rb.velocity.x) && Mathf.Abs(rb.velocity.x) > 2)
                            //{
                            //    if (auxCdDash < 0.7f)
                            //    {
                            //        if (tengoMaxspeed)
                            //        {


                            //            Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * -pInput.inputHorizontal;
                            //            this.rb.AddForce(direccion * fuerzaCambioSentido * 0.8f * Time.deltaTime);


                            //        }
                            //        else
                            //        {
                            //            //rb.velocity =new Vector2 (0, rb.velocity.y);

                            //            Vector2 direccion = Vector2.Perpendicular(ultimaNormal).normalized * coefDeceleracion * -pInput.inputHorizontal;

                            //            this.rb.AddForce(direccion * fuerzaCambioSentido * Time.deltaTime);


                            //        }
                            //    }
                            cambioSentidoReciente = true;
                            StopCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                            StartCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                            //    //rb.velocity = new Vector2(-rb.velocity.x * 0.7f, rb.velocity.y);
                            //    //speed = 0;
                            //}
                        }
                    }
                }

            }


            if (pInput.inputHorizontal == 0)
            {

                if (looping)
                {
                    if (pInput.inputVertical != 0)
                    {
                        if (Mathf.Abs(rb.velocity.x) > 0)
                        {
                            if (bocabajocambiotecla == false)
                            {

                                if (Mathf.Abs(rb.velocity.x) < (velMinima))
                                {
                                    AnimatorClipInfo[] animInfo = animCC.GetCurrentAnimatorClipInfo(0);
                                    //if (animInfo[0].clip != null)
                                    //{
                                    //    if (animInfo[0].clip.name == ("Sprintar"))
                                    //    {
                                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.035f, 1, 1.25f);
                                    //    }
                                    //    else if (animInfo[0].clip.name == ("Trotar"))
                                    //    {
                                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.05f, 0.6f, 1f);
                                    //    }
                                    //}



                                    speed = velMinima;
                                    if ((ultimaNormal.x < -0.0f))
                                    {
                                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * pInput.inputVertical;
                                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                                    }

                                    else if ((ultimaNormal.x > -0.0f))
                                    {
                                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputVertical;
                                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                                    }
                                    else
                                    {
                                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 80 * pInput.inputVertical;

                                        this.rb.AddForce(direccion * Time.deltaTime);
                                    }
                                    //if ((ultimaNormal.y < -0.0f) && (ultimaNormal.x<0)&&bocabajocambiotecla == true && looping)
                                    //{
                                    //    Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputVertical;
                                    //    this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                                    //}
                                    //if ((ultimaNormal.y < -0.0f) && (ultimaNormal.x < 0) && bocabajocambiotecla == true && looping)
                                    //{
                                    //    Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputVertical;
                                    //    this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                                    //}
                                    //else
                                    //{
                                    //if (bocabajocambiotecla == true)
                                    //{
                                    //    Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputHorizontal;
                                    //    this.rb.AddForce(new Vector2(-direccion.x, direccion.y) * Time.deltaTime);
                                    //}
                                    //else
                                    //{

                                    //}

                                    //}



                                    //rb.velocity += direccion * 1.6f * Time.deltaTime;

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
                                else if (Mathf.Abs(this.rb.velocity.x) < velMaxima)
                                {
                                    AnimatorClipInfo[] animInfo = animCC.GetCurrentAnimatorClipInfo(0);
                                    //if (animInfo[0].clip != null)
                                    //{
                                    //    if (animInfo[0].clip.name == ("Sprintar"))
                                    //    {
                                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.035f, 1, 1.25f);
                                    //    }
                                    //    else if (animInfo[0].clip.name == ("Trotar"))
                                    //    {
                                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.05f, 0.6f, 1f);
                                    //    }
                                    //}

                                    //if (pInput.inputHorizontal > 0)
                                    if (speed < velMinima)
                                    {
                                        speed = velMinima;
                                    }
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
                                    Vector2 direccion;
                                    if ((ultimaNormal.x < -0.0f))
                                    {
                                        direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * pInput.inputVertical;
                                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);

                                    }

                                    else if ((ultimaNormal.x > -0.0f))
                                    {
                                        direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputVertical;
                                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);

                                    }
                                    else
                                    {
                                        direccion = Vector2.Perpendicular(normal).normalized * speed * 80 * pInput.inputVertical;

                                        this.rb.AddForce(direccion * Time.deltaTime);
                                    }





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


                            }
                            else
                            {
                                if (Mathf.Abs(rb.velocity.x) < (velMinima))
                                {
                                    AnimatorClipInfo[] animInfo = animCC.GetCurrentAnimatorClipInfo(0);
                                    //if (animInfo[0].clip != null)
                                    //{
                                    //    if (animInfo[0].clip.name == ("Sprintar"))
                                    //    {
                                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.035f, 1, 1.25f);
                                    //    }
                                    //    else if (animInfo[0].clip.name == ("Trotar"))
                                    //    {
                                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.05f, 0.6f, 1f);
                                    //    }
                                    //}



                                    speed = velMinima;
                                    if ((ultimaNormal.x < -0.0f))
                                    {
                                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * pInput.inputVertical;
                                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                                    }

                                    else if ((ultimaNormal.x > -0.0f))
                                    {
                                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputVertical;
                                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                                    }
                                    else
                                    {
                                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 80 * pInput.inputVertical;

                                        this.rb.AddForce(direccion * Time.deltaTime);
                                    }
                                    //if ((ultimaNormal.y < -0.0f) && (ultimaNormal.x<0)&&bocabajocambiotecla == true && looping)
                                    //{
                                    //    Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputVertical;
                                    //    this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                                    //}
                                    //if ((ultimaNormal.y < -0.0f) && (ultimaNormal.x < 0) && bocabajocambiotecla == true && looping)
                                    //{
                                    //    Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputVertical;
                                    //    this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                                    //}
                                    //else
                                    //{
                                    //if (bocabajocambiotecla == true)
                                    //{
                                    //    Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputHorizontal;
                                    //    this.rb.AddForce(new Vector2(-direccion.x, direccion.y) * Time.deltaTime);
                                    //}
                                    //else
                                    //{

                                    //}

                                    //}



                                    //rb.velocity += direccion * 1.6f * Time.deltaTime;

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
                                else if (Mathf.Abs(this.rb.velocity.x) < velMaxima)
                                {
                                    AnimatorClipInfo[] animInfo = animCC.GetCurrentAnimatorClipInfo(0);
                                    //if (animInfo[0].clip != null)
                                    //{
                                    //    if (animInfo[0].clip.name == ("Sprintar"))
                                    //    {
                                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.035f, 1, 1.25f);
                                    //    }
                                    //    else if (animInfo[0].clip.name == ("Trotar"))
                                    //    {
                                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.05f, 0.6f, 1f);
                                    //    }
                                    //}

                                    //if (pInput.inputHorizontal > 0)
                                    if (speed < velMinima)
                                    {
                                        speed = velMinima;
                                    }
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
                                    Vector2 direccion;
                                    if ((ultimaNormal.x < -0.0f))
                                    {
                                        direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * pInput.inputVertical;
                                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);

                                    }

                                    else if ((ultimaNormal.x > -0.0f))
                                    {
                                        direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputVertical;
                                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);

                                    }
                                    else
                                    {
                                        direccion = Vector2.Perpendicular(normal).normalized * speed * 80 * pInput.inputVertical;

                                        this.rb.AddForce(direccion * Time.deltaTime);
                                    }





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


                                //if (auxCdDashReal > cooldownDashReal * 0.5f)
                                //{
                                //    this.rb.AddForce(-this.rb.velocity * coefDeceleracion * Time.deltaTime);
                                //}
                                //else
                                //{
                                //    this.rb.AddForce(-this.rb.velocity * coefDeceleracion * 0.2f * Time.deltaTime);
                                //}

                            }
                            //else
                            //{
                            //    //if (auxCdDash < cooldownDash * 0.5f)
                            //    //{
                            //    //    this.rb.AddForce(-this.rb.velocity * coefDeceleracion * Time.deltaTime);
                            //    //}
                            //    //else
                            //    //{
                            //    //    this.rb.AddForce(-this.rb.velocity*coefAceleracion*2 * Time.deltaTime);
                            //    //}
                            //}



                        }
                    }
                    else
                    {
                        if (Mathf.Abs(rb.velocity.x) > 0)
                        {
                            if (bocabajocambiotecla == false)
                            {


                                //if (ultimaNormal.y > 0)
                                //{
                                if (Mathf.Abs(speed) > 2)
                                {
                                    speed -= coefDeceleracion * Time.deltaTime;
                                }
                                if (speed <= 2)
                                {
                                    speed = 0;
                                }




                                //Vector2 direccion = Vector2.Perpendicular(normal) * speed * -pInput.inputHorizontal;
                                //this.rb.AddForce(-direccion * Time.fixedDeltaTime);
                                if (auxCdDashReal > cooldownDashReal * 0.5f)
                                {
                                    this.rb.AddForce(-this.rb.velocity * coefDeceleracion * Time.deltaTime);
                                }
                                else
                                {
                                    this.rb.AddForce(-this.rb.velocity * coefDeceleracion * 0.5f * Time.deltaTime);
                                }
                                if (Mathf.Abs(rb.velocity.x) <= 0.5f)
                                {
                                    rb.velocity = new Vector2(0, rb.velocity.y);
                                }
                            }
                            else
                            {
                                if (auxCdDashReal > cooldownDashReal * 0.5f)
                                {
                                    this.rb.AddForce(-this.rb.velocity * coefDeceleracion * Time.deltaTime);
                                }
                                else
                                {
                                    this.rb.AddForce(-this.rb.velocity * coefDeceleracion * 0.2f * Time.deltaTime);
                                }

                            }
                            //else
                            //{
                            //    //if (auxCdDash < cooldownDash * 0.5f)
                            //    //{
                            //    //    this.rb.AddForce(-this.rb.velocity * coefDeceleracion * Time.deltaTime);
                            //    //}
                            //    //else
                            //    //{
                            //    //    this.rb.AddForce(-this.rb.velocity*coefAceleracion*2 * Time.deltaTime);
                            //    //}
                            //}



                        }
                    }


                }
                else
                {
                    if (Mathf.Abs(rb.velocity.x) > 0)
                    {
                        if (bocabajocambiotecla == false)
                        {


                            //if (ultimaNormal.y > 0)
                            //{
                            if (Mathf.Abs(speed) > 2)
                            {
                                speed -= coefDeceleracion * Time.deltaTime;
                            }
                            if (speed <= 2)
                            {
                                speed = 0;
                            }




                            //Vector2 direccion = Vector2.Perpendicular(normal) * speed * -pInput.inputHorizontal;
                            //this.rb.AddForce(-direccion * Time.fixedDeltaTime);
                            if (auxCdDashReal > cooldownDashReal * 0.7f)
                            {
                                this.rb.AddForce(-this.rb.velocity * coefDeceleracion * Time.deltaTime);
                            }
                            else
                            {
                                this.rb.AddForce(-this.rb.velocity * coefDeceleracion * 0.5f * Time.deltaTime);
                            }
                            if (Mathf.Abs(rb.velocity.x) <= 0.7f)
                            {
                                rb.velocity = new Vector2(0, rb.velocity.y);
                            }
                        }
                        else
                        {
                            if (auxCdDashReal > cooldownDashReal * 0.7f)
                            {
                                this.rb.AddForce(-this.rb.velocity * coefDeceleracion * Time.deltaTime);
                            }
                            else
                            {
                                this.rb.AddForce(-this.rb.velocity * coefDeceleracion * 0.2f * Time.deltaTime);
                            }

                        }
                        //else
                        //{
                        //    //if (auxCdDash < cooldownDash * 0.5f)
                        //    //{
                        //    //    this.rb.AddForce(-this.rb.velocity * coefDeceleracion * Time.deltaTime);
                        //    //}
                        //    //else
                        //    //{
                        //    //    this.rb.AddForce(-this.rb.velocity*coefAceleracion*2 * Time.deltaTime);
                        //    //}
                        //}



                    }

                }
                if (rb.velocity.magnitude < 3) animCC.SetBool("Corriendo", false);
            }
            else if (pInput.inputHorizontal != 0)
            {
                if (Mathf.Abs(rb.velocity.x) < (velMinima))
                {
                    AnimatorClipInfo[] animInfo = animCC.GetCurrentAnimatorClipInfo(0);
                    //if (animInfo[0].clip != null)
                    //{
                    //    if (animInfo[0].clip.name == ("Sprintar"))
                    //    {
                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.035f, 1, 1.25f);
                    //    }
                    //    else if (animInfo[0].clip.name == ("Trotar"))
                    //    {
                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.05f, 0.6f, 1f);
                    //    }
                    //}

                    speed = velMinima;
                    if ((ultimaNormal.y < -0.0f) && bocabajocambiotecla == true && looping)
                    {
                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputHorizontal;
                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                    }
                    else if ((ultimaNormal.y < -0.0f) && bocabajocambiotecla == true && !looping)
                    {
                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * pInput.inputHorizontal;
                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                    }
                    else if ((ultimaNormal.y > -0.0f) && bocabajocambiotecla == true && looping == true)
                    {
                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputHorizontal;
                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                    }
                    else
                    {
                        //if (bocabajocambiotecla == true)
                        //{
                        //    Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputHorizontal;
                        //    this.rb.AddForce(new Vector2(-direccion.x, direccion.y) * Time.deltaTime);
                        //}
                        //else
                        //{
                        Vector2 direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputHorizontal;
                        this.rb.AddForce(direccion * Time.deltaTime);
                        //}

                    }

                    //rb.velocity += direccion * 1.6f * Time.deltaTime;

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
                else if (Mathf.Abs(this.rb.velocity.x) < velMaxima)
                {
                    AnimatorClipInfo[] animInfo = animCC.GetCurrentAnimatorClipInfo(0);
                    //if (animInfo[0].clip != null)
                    //{
                    //    if (animInfo[0].clip.name == ("Sprintar"))
                    //    {
                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.035f, 1, 1.25f);
                    //    }
                    //    else if (animInfo[0].clip.name == ("Trotar"))
                    //    {
                    //        animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.05f, 0.6f, 1f);
                    //    }
                    //}

                    //if (pInput.inputHorizontal > 0)
                    if (speed < velMinima)
                    {
                        speed = velMinima;
                    }
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
                    Vector2 direccion;
                    if ((ultimaNormal.y < -0.0f) && bocabajocambiotecla == true && looping)
                    {
                        direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputHorizontal;
                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                    }
                    else if ((ultimaNormal.y < -0.0f) && bocabajocambiotecla == true && !looping)
                    {
                        direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * pInput.inputHorizontal;
                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                    }
                    else if ((ultimaNormal.y > -0.0f) && bocabajocambiotecla == true && looping == true)
                    {
                        direccion = Vector2.Perpendicular(normal).normalized * speed * 90 * -pInput.inputHorizontal;
                        this.rb.AddForce(new Vector2(-direccion.x, -direccion.y) * Time.deltaTime);
                    }
                    else
                    {
                        direccion = Vector2.Perpendicular(normal).normalized * speed * 80 * -pInput.inputHorizontal;

                        this.rb.AddForce(direccion * Time.deltaTime);
                    }





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
                else if (Mathf.Abs(this.rb.velocity.x) >= velMaxima)
                {
                    AnimatorClipInfo[] animInfo = animCC.GetCurrentAnimatorClipInfo(0);
                    if (animInfo[0].clip != null)
                    {
                        if (animInfo[0].clip.name == ("Sprintar"))
                        {
                            animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.035f, 1, 1.25f);
                        }
                        else if (animInfo[0].clip.name == ("Trotar"))
                        {
                            animCC.speed = Mathf.Clamp(Mathf.Abs(rb.velocity.x) * 0.05f, 0.6f, 1f);
                        }
                    }

                    if (capSpeedUnavez == false)
                    {
                        capSpeedUnavez = true;
                        capSpeed = speed;
                    }
                    speed = capSpeed;

                    if (auxCdDashReal <= 0.0f && cambioSentidoReciente == false && bocabajocambiotecla == false) rb.velocity = new Vector2((velMaxima - 1) * Mathf.Sign(ultimaNormal.y) * pInput.ultimoInputHorizontal, rb.velocity.y);
                    if (auxCdDashReal <= 0.0f && cambioSentidoReciente == false && bocabajocambiotecla == true) rb.velocity = new Vector2((velMaxima - 1) * Mathf.Sign(ultimaNormal.y) * -pInput.ultimoInputHorizontal, rb.velocity.y);
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


                animCC.SetBool("Corriendo", true);
            }



        }
        else
        {
            bocabajocambiotecla = false;
            if (pInput.inputHorizontal == 0)
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
                    //this.rb.AddForce(-direccion * Time.fixedDeltaTime);
                    if (estoyDasheando == false) this.rb.AddForce(new Vector2(-this.rb.velocity.x, 0).normalized * coefDeceleracion * Time.deltaTime);
                    if (Mathf.Abs(rb.velocity.x) <= 0)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }

                }
                //animCC.SetBool("Corriendo", false);
            }
            if (pInput.inputHorizontal != 0)
            {
                //if (rb.velocity.y < 0)
                //{
                //    rb.velocity = new Vector2(rb.velocity.x * 0.99f, rb.velocity.y * 0.99f);
                //}



                if (pegadoPared)
                {
                    if (tocandoderecha)
                    {
                        if (pInput.inputHorizontal < 0)
                        {
                            speed += coefAceleracion * Time.deltaTime;
                            Vector2 direccion = new Vector2(1/**Mathf.Sign(ultimaNormal.y)*/, 0) * speed * velocidadDespegarPared * pInput.inputHorizontal;


                            this.rb.AddForce(direccion * Time.deltaTime);
                        }
                    }
                    else if (tocandoizquierda)
                    {
                        if (pInput.inputHorizontal > 0)
                        {
                            speed += coefAceleracion * Time.deltaTime;
                            Vector2 direccion = new Vector2(1/**Mathf.Sign(ultimaNormal.y)*/, 0) * speed * velocidadDespegarPared * pInput.inputHorizontal;



                            this.rb.AddForce(direccion * Time.deltaTime);
                        }
                    }

                }
                else
                {
                    if (Mathf.Sign(pInput.inputHorizontal) != Mathf.Sign(rb.velocity.x) && Mathf.Abs(rb.velocity.x) > 1)
                    {
                        if (auxtiempoTrasSaltoPared < tiempoTrasSaltoPared * 0.8f)
                        {
                            if ((auxTiempoTrasSaltoLoop <= 0) && (Mathf.Sign(ultimaNormal.y) > 0))
                            {
                                if (auxCdDashReal < 0.7f * auxCdDashReal)
                                {
                                    if (tengoMaxspeed)
                                    {
                                        //if (saltoDobleReciente)
                                        //{
                                        //    Vector2 direccion = new Vector2(1, 0) * coefDeceleracion * pInput.inputHorizontal;
                                        //    this.rb.AddForce(direccion * fuerzaCambioSentido * 2.8f * Time.deltaTime);
                                        //}
                                        //else
                                        {


                                            Vector2 direccion = new Vector2(1, 0) * coefDeceleracion * pInput.inputHorizontal;
                                            this.rb.AddForce(direccion * fuerzaCambioSentidoAire * 0.8f * Time.deltaTime);
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



                                        Vector2 direccion = new Vector2(1, 0) * coefDeceleracion * pInput.inputHorizontal;
                                        this.rb.AddForce(direccion * fuerzaCambioSentidoAire * 0.8f * Time.deltaTime);

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
                                }
                                speed = 0;
                                //if (lastJumpPared == true) lastJumpPared = false;
                                cambioSentidoReciente = true;
                                StopCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                                StartCoroutine(CambioAireReciente(tiempoTrasCambioSentido));

                            }
                            else
                            {
                                //if (ultimaNormal.y < 0)
                                //{


                                //    Vector2 direccion = new Vector2(1, 0) * coefDeceleracion * pInput.inputHorizontal;
                                //    this.rb.AddForce(direccion * fuerzaCambioSentidoAire * 0.8f * Time.deltaTime);
                                //    speed = 0;
                                //    //if (lastJumpPared == true) lastJumpPared = false;
                                //    cambioSentidoReciente = true;
                                //    StopCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                                //    StartCoroutine(CambioAireReciente(tiempoTrasCambioSentido));
                                //}
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
                    else if (Mathf.Abs(this.rb.velocity.x) < velMaxima)
                    {
                        //if (speed < velMinima)
                        //{
                        //    speed = velMinima;
                        //}
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
                        Vector2 direccion = new Vector2(1 /** Mathf.Sign(ultimaNormal.y)*/, 0) * speed * 90 * pInput.inputHorizontal;
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
                    else if (Mathf.Abs(this.rb.velocity.x) > velMaxima + 2)
                    {
                        if (capSpeedUnavez == false)
                        {
                            capSpeedUnavez = true;
                            capSpeed = speed;
                        }
                        speed = capSpeed;
                        //if (auxCdDashReal < 0 && cambioSentidoReciente == false) rb.velocity = new Vector2((velMaxima - 1) * Mathf.Sign(ultimaNormal.y) * pInput.ultimoInputHorizontal, rb.velocity.y);
                        //if (auxCdDash<0) rb.velocity = new Vector2(velMaxima * pInput.inputHorizontal *Mathf.Sign(ultimaNormal.y), rb.velocity.y);
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
                }


                //animCC.SetBool("Corriendo", true);
            }

        }
        if (Mathf.Abs(rb.velocity.x) < velMaxima - 3f)
        {
            //if (!pegadoPared) this.GetComponent<AudioManager>().Stop(this.GetComponent<AudioManager>().sonidoLoop);
            tengoMaxspeed = false;
            //particulasVelMax.SetActive(false);
        }
        else
        {

            tengoMaxspeed = true;




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
                        if (tiempoCOYOTE < 0.2)
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
            if (constantegravedad > 400) constantegravedad = 400;
            //rb.AddForce(new Vector2(0, -100f * Time.deltaTime));


            if (rb.velocity.y < -40)
            {
                rb.AddForce(new Vector2(0, -600f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (750f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
                //if (rb.velocity.y < -40) rb.velocity = new Vector2(rb.velocity.x, -40);
            }
            else if (rb.velocity.y < -30f)
            {
                rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (600f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
            }
            else if (rb.velocity.y < -20f)
            {
                rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (600f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
            }
            else if (rb.velocity.y < -10f)
            {
                rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (500f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
            }
            else if (rb.velocity.y < 0f)
            {
                rb.AddForce(new Vector2(0, -500f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (600f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
            }
            else if (rb.velocity.y < 15f)
            {
                //if (ultimaNormal.y > 0)
                //{
                rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (700f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
                //}
            }
            else if (rb.velocity.y < 150f)
            {
                //if (ultimaNormal.y > 0)
                //{
                rb.AddForce(new Vector2(0, -400f * Time.deltaTime));
                /* if (rb.velocity.y < -12) rb.velocity = new Vector2(rb.velocity.x, -12f);*//*rb.AddForce(new Vector2(0, -0.005f)); *//*rb.velocity = new Vector2(rb.velocity.x,-5f);*/
                constantegravedad *= (450f * Time.deltaTime);
                rb.AddForce(new Vector2(0.0f, -constantegravedad * Time.deltaTime), ForceMode2D.Force);
                //}
            }
            if (rb.velocity.y < 0)
            {
                rb.AddForce(new Vector2(0.0f, -350) * Time.deltaTime);

            }


        }



    }

    void Dash()
    {

        if (estoyDasheando == false&&!dashBloqueado)
        {


            //if (Input.GetButtonDown("Dash") && mEnergy.actualEnergy > mEnergy.energiaDash && pInput.inputVertical!=-1)
            if (joystick != null)
            {
                if ((joystick.Action3.WasPressed || Input.GetButtonDown("Dash")) && mEnergy.actualEnergy > mEnergy.energiaDash && (looping || (Mathf.Abs(joystick.LeftStick.X) >= 1.2f || Mathf.Abs(pInput.Yinput) < 1.8)))
                {
                    //if (dashPulsado && mEnergy.actualEnergy > mEnergy.energiaDash)
                    //{
                    //if (dashPulsado && mEnergy.actualEnergy > mEnergy.energiaDash)
                    //{
                    if ((auxCdDashReal <= 0) && (puedoDash))
                    {
                        puedoDash = false;
                        mEnergy.RestarEnergia(mEnergy.energiaDash);
                        rb.velocity = Vector2.zero;
                        speed = capSpeed;
                        if (grounded)
                        {
                            if (pInput.inputHorizontal == 0 && pInput.inputVertical != 0 && looping)
                            {
                                if (bocabajocambiotecla == true)
                                {
                                    //rb.AddForce(new Vector2(-fuerzaDash * 1.2f * Vector2.Perpendicular(normal).x, fuerzaDash * Vector2.Perpendicular(-normal).y));
                                    if (pInput.inputVertical == 1 && looping && ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.inputVertical);
                                    }
                                    else if (pInput.inputVertical == 1 && looping && ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                    }
                                    else if (pInput.inputVertical == -1 && looping && ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                    }
                                    else if (pInput.inputVertical == -1 && looping && ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.inputVertical);
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash));
                                    }
                                }
                                else
                                {
                                    if (pInput.inputVertical == 1 && looping && ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.inputVertical);
                                    }
                                    else if (pInput.inputVertical == 1 && looping && ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                    }
                                    else if (pInput.inputVertical == -1 && looping && ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                    }
                                    else if (pInput.inputVertical == -1 && looping && ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.inputVertical);
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash));
                                    }
                                }

                            }
                            else if (pInput.ultimoInputHorizontal < 0)
                            {
                                if (bocabajocambiotecla == true)
                                {
                                    if (ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.ultimoInputHorizontal);
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.ultimoInputHorizontal);
                                    }
                                }
                                else
                                {
                                    if (ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                    }
                                }

                                //rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * 1.2f * fuerzaDash, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                            }
                            else if (pInput.ultimoInputHorizontal > 0)
                            {
                                if (bocabajocambiotecla == true)
                                {
                                    if (ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.ultimoInputHorizontal);

                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.ultimoInputHorizontal);

                                    }
                                }
                                else
                                {
                                    if (ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                    }
                                }

                            }
                            else if (pInput.ultimoInputHorizontal == 0)
                            {
                                if (bocabajocambiotecla == true)
                                {
                                    //rb.AddForce(new Vector2(-fuerzaDash * 1.2f * Vector2.Perpendicular(normal).x, fuerzaDash * Vector2.Perpendicular(-normal).y));
                                    if (pInput.inputVertical == 1 && looping && ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                    }
                                    else if (pInput.inputVertical == 1 && looping && ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                    }
                                    else if (pInput.inputVertical == -1 && looping && ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                    }
                                    else if (pInput.inputVertical == -1 && looping && ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash));
                                    }
                                }
                                else
                                {
                                    if (pInput.inputVertical == 1 && looping && ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                      
                                    }
                                    else if (pInput.inputVertical == 1 && looping && ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                       
                                    }
                                    else if (pInput.inputVertical == -1 && looping && ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                      
                                    }
                                    else if (pInput.inputVertical == -1 && looping && ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.inputVertical);
                                       
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash));
                                    }
                                }

                            }
                        }
                        else
                        {
                        habilidadesSinTocarSuelo += 1;
                            if (lastJumpPared == false)
                            {
                                if (pInput.ultimoInputHorizontal < 0)
                                {
                                    rb.AddForce(new Vector2(-1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);

                                }
                                else if (pInput.ultimoInputHorizontal > 0)
                                {
                                    rb.AddForce(new Vector2(-1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);
                                }
                                else if (pInput.ultimoInputHorizontal == 0)
                                {
                                    rb.AddForce(new Vector2(1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);
                                }
                            }
                            else
                            {
                                lastJumpPared = false;
                                if (pInput.ultimoInputHorizontal < 0)
                                {
                                    rb.AddForce(new Vector2(-1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);
                                }
                                else if (pInput.ultimoInputHorizontal > 0)
                                {
                                    rb.AddForce(new Vector2(-1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);
                                }
                                else if (pInput.ultimoInputHorizontal == 0)
                                {
                                    rb.AddForce(new Vector2(1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);
                                }
                            }
                        }


                        constantegravedad = 1;
                        estoyDasheando = true;
                        auxdash = tiempoDasheo;
                        dashEnCaida = false;
                        animCC.SetBool("cayendo", dashEnCaida);
                        animCC.SetTrigger("Dash");

                        GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasDash, transform.position, transform);

                        //movimientoBloqueado = true;

                        dashCaidaBloqueado = true;
                        auxCdDashReal = cooldownDashReal;
                        auxCdDashAtravesar = cooldownDashAtravesar;

                        FindObjectOfType<NewAudioManager>().Play("PlayerDash");
                        //FindObjectOfType<NewAudioManager>().Change("Theme");
                        //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dash);
                    }
                    //anim.SetTrigger("Dash");
                    Transform m_GroundCheck = transform.Find("GroundCheckç");
                    //es normal esa ç?

                }
                else if ((joystick.Action3.WasPressed || Input.GetButtonDown("Dash")) && mEnergy.actualEnergy < mEnergy.energiaDash)
                {
                    if (auxTiempoSonidoSinEnergia == 0)
                    {
                        NewAudioManager.Instance.Play("PlayerNoEnergy");
                        auxTiempoSonidoSinEnergia = tiempoSonidoSinEnergia;
                    }
                }

            }
            else
            {
                if (Input.GetButtonDown("Dash") && mEnergy.actualEnergy > mEnergy.energiaDash && (looping || pInput.inputVertical != -1))
                {
                    //if (dashPulsado && mEnergy.actualEnergy > mEnergy.energiaDash)
                    //{
                    if ((auxCdDashReal <= 0) && (puedoDash))
                    {

                        puedoDash = false;
                        mEnergy.RestarEnergia(mEnergy.energiaDash);
                        rb.velocity = Vector2.zero;
                        speed = capSpeed;
                        if (grounded)
                        {
                            if (pInput.ultimoInputHorizontal < 0)
                            {
                                if (bocabajocambiotecla == true)
                                {
                                    if (ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.ultimoInputHorizontal);
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.ultimoInputHorizontal);
                                    }
                                }
                                else
                                {
                                    if (ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                    }
                                }

                                //rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * 1.2f * fuerzaDash, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                            }
                            else if (pInput.ultimoInputHorizontal > 0)
                            {
                                if (bocabajocambiotecla == true)
                                {
                                    if (ultimaNormal.x > 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.ultimoInputHorizontal);
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * pInput.ultimoInputHorizontal);
                                    }
                                }
                                else
                                {
                                    if (ultimaNormal.x < 0)
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                    }
                                    else
                                    {
                                        rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash) * -pInput.ultimoInputHorizontal);
                                    }
                                }

                            }
                            else if (pInput.ultimoInputHorizontal == 0)
                            {
                                //rb.AddForce(new Vector2(-fuerzaDash * 1.2f * Vector2.Perpendicular(normal).x, fuerzaDash * Vector2.Perpendicular(-normal).y));
                                rb.AddForce(new Vector2(Vector2.Perpendicular(normal).x * fuerzaDash * 1.2f, Vector2.Perpendicular(normal).y * fuerzaDash));
                            }
                        }
                        else
                        {
                           habilidadesSinTocarSuelo += 1;
                            if (lastJumpPared == false)
                            {
                                if (pInput.ultimoInputHorizontal < 0)
                                {
                                    rb.AddForce(new Vector2(-1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);

                                }
                                else if (pInput.ultimoInputHorizontal > 0)
                                {
                                    rb.AddForce(new Vector2(-1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);
                                }
                                else if (pInput.ultimoInputHorizontal == 0)
                                {
                                    rb.AddForce(new Vector2(1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);
                                }
                            }
                            else
                            {
                                lastJumpPared = false;
                                if (pInput.ultimoInputHorizontal < 0)
                                {
                                    rb.AddForce(new Vector2(-1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);
                                }
                                else if (pInput.ultimoInputHorizontal > 0)
                                {
                                    rb.AddForce(new Vector2(-1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);
                                }
                                else if (pInput.ultimoInputHorizontal == 0)
                                {
                                    rb.AddForce(new Vector2(1, 0) * fuerzaDashAire * -pInput.ultimoInputHorizontal);
                                }
                            }
                        }


                        constantegravedad = 1;
                        estoyDasheando = true;
                        speed = capSpeed;
                        auxdash = tiempoDasheo;
                        dashEnCaida = false;
                        animCC.SetBool("cayendo", dashEnCaida);
                        animCC.SetTrigger("Dash");
                        GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasDash, transform.position, transform);
                        //movimientoBloqueado = true;
                        dashCaidaBloqueado = true;
                        auxCdDashReal = cooldownDashReal;
                        auxCdDashAtravesar = cooldownDashAtravesar;
                        FindObjectOfType<NewAudioManager>().Play("PlayerDash");
                        //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dash);
                    }
                    //anim.SetTrigger("Dash");
                    Transform m_GroundCheck = transform.Find("GroundCheckç");

                }
                else if ((Input.GetButtonDown("Dash")) && mEnergy.actualEnergy < mEnergy.energiaDash)
                {
                    if (auxTiempoSonidoSinEnergia == 0)
                    {
                        NewAudioManager.Instance.Play("PlayerNoEnergy");
                        auxTiempoSonidoSinEnergia = tiempoSonidoSinEnergia;
                    }
                }
            }
            //if (auxCdDash <= cooldownDash*0.8f)
            //{
            //    movimientoBloqueado = false;
            //}
        }
        else
        {
            tiempoPulsadoEspacio = tiempoSaltoCompleto + 0.5f;
            auxdash -= Time.deltaTime;

            if (auxdash > 0)
            {

                speed = capSpeed;
                if (grounded == false)
                {
                    if (ultimaNormal.y == 1 && grounded)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                    }
                    else if (!grounded)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                    }

                    constantegravedad = 1;
                    AnularGravedad();
                }
                if (grounded)
                {
                    if (ultimaNormal.y == 1)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                    }
                    constantegravedad = 1;
                    AnularGravedad();

                    dashCaidaBloqueado = false;
                }
            }
            else
            {


                dashCaidaBloqueado = false;
                //if (Mathf.Abs(this.rb.velocity.x) > velMaxima / 60)
                //{
                if (grounded)
                {
                    if (ultimaNormal.y < 0)
                    {
                        if (rb.velocity.x > 1)
                        {
                            rb.velocity = new Vector2(-velMaxima * ultimaNormal.y, rb.velocity.y);
                        }
                        else if (rb.velocity.x < -1)
                        {

                            rb.velocity = new Vector2(velMaxima * ultimaNormal.y, rb.velocity.y);
                        }
                    }
                    else
                    {
                        if (rb.velocity.x > 1)
                        {
                            rb.velocity = new Vector2(velMaxima * ultimaNormal.y, rb.velocity.y);
                        }
                        else if (rb.velocity.x < -1)
                        {

                            rb.velocity = new Vector2(-velMaxima * ultimaNormal.y, rb.velocity.y);
                        }
                    }
                }
                else
                {
                    if (ultimaNormal.y < 0)
                    {
                        if (rb.velocity.x > 1)
                        {
                            rb.velocity = new Vector2(velMaxima, rb.velocity.y);
                        }
                        else if (rb.velocity.x < -1)
                        {

                            rb.velocity = new Vector2(-velMaxima, rb.velocity.y);
                        }
                    }
                    else
                    {
                        if (rb.velocity.x > 1)
                        {
                            rb.velocity = new Vector2(velMaxima, rb.velocity.y);
                        }
                        else if (rb.velocity.x < -1)
                        {

                            rb.velocity = new Vector2(-velMaxima, rb.velocity.y);
                        }
                    }
                }



                //}
                //if (rb.velocity.x>)
            }



            if (auxdash <= 0)
            {
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
            if (saltoDobleHechoParaDashCaida || rb.velocity.y > 2) { hechoConseguidoPuntoHabDashAbajo = false; dashEnCaida = false; saltoDobleHechoParaDashCaida = false; }
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //if (!grounded && pInput.inputVertical ==-1 && pegadoPared == false && Input.GetButtonDown("Dash"))
        if (joystick != null)
        {
            //if (!grounded && pegadoPared == false && (pInput.inputVertical == -1 || (joystick.Action2.WasPressed || Input.GetButtonDown("Dash"))))
            if (!grounded && pInput.inputVertical == -1 && pegadoPared == false && joystick.LeftStickY == 0 || (joystick.Action3.WasPressed && pInput.inputVertical == -1 /*&& joystick.LeftStick.Y <=-0.7f*/ /*&& Mathf.Abs(joystick.LeftStick.X) < 1.25f*//*|| Input.GetButtonDown("Dash")*/))
            {
                if (dashEnCaida == false)
                {
                    if (rb.velocity.y > 0)
                    {
                        if (tiempoCOYOTE < -0.2f && mEnergy.actualEnergy > mEnergy.energiaDashAbajo)
                        {
                            mEnergy.RestarEnergia(mEnergy.energiaDashAbajo);
                            if (hechoConseguidoPuntoHabDashAbajo == false) {  habilidadesSinTocarSuelo += 1; hechoConseguidoPuntoHabDashAbajo = true; }
                            rb.AddForce(fuerzaDashCaida * Vector2.down/* * Time.deltaTime*/);
                            FindObjectOfType<NewAudioManager>().Play("PlayerDashFall");
                            //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dashAbajo);
                            dashEnCaida = true;
                            saltoDobleHechoParaDashCaida = false;
                            GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasDashCaida, posGround.position, posGround);
                            GetComponent<Particulas>().particulasDashCaida.gameObject.SetActive(true);
                            rb.velocity = new Vector2(rb.velocity.x, -1);
                        }
                        else if (mEnergy.actualEnergy < mEnergy.energiaDashAbajo)
                        {
                            if (auxTiempoSonidoSinEnergia == 0)
                            {
                                NewAudioManager.Instance.Play("PlayerNoEnergy");
                                auxTiempoSonidoSinEnergia = tiempoSonidoSinEnergia;
                            }
                        }
                    }
                    else
                    {
                        if (tiempoCOYOTE < -0.05f && mEnergy.actualEnergy > mEnergy.energiaDashAbajo)
                        {
                            mEnergy.RestarEnergia(mEnergy.energiaDashAbajo);
                            if (hechoConseguidoPuntoHabDashAbajo == false) {  habilidadesSinTocarSuelo += 1; hechoConseguidoPuntoHabDashAbajo = true; }
                            rb.AddForce(fuerzaDashCaida * Vector2.down/* * Time.deltaTime*/);
                            FindObjectOfType<NewAudioManager>().Play("PlayerDashFall");
                            //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dashAbajo);
                            dashEnCaida = true;
                            saltoDobleHechoParaDashCaida = false;
                            GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasDashCaida, posGround.position, posGround);
                            GetComponent<Particulas>().particulasDashCaida.gameObject.SetActive(true);
                            rb.velocity = new Vector2(rb.velocity.x, -1);
                        }
                        else if (mEnergy.actualEnergy < mEnergy.energiaDashAbajo)
                        {
                            if (auxTiempoSonidoSinEnergia == 0)
                            {
                                NewAudioManager.Instance.Play("PlayerNoEnergy");
                                auxTiempoSonidoSinEnergia = tiempoSonidoSinEnergia;
                            }
                        }
                    }
                }

            }
            else if (grounded)
            {
                dashEnCaida = false;
                //GetComponent<Particulas>().particulasDashCaida.gameObject.SetActive(false);
            }
        }
        else
        {
            if (!grounded && pInput.inputVertical == -1 && pegadoPared == false /*&& Input.GetButtonDown("Dash")*/)
            {
                if (dashEnCaida == false)
                {

                    if (rb.velocity.y > 0)
                    {
                        if (tiempoCOYOTE < -0.2f && mEnergy.actualEnergy > mEnergy.energiaDashAbajo && dashEnCaida == false)
                        {
                            mEnergy.RestarEnergia(mEnergy.energiaDashAbajo);
                            if (hechoConseguidoPuntoHabDashAbajo == false) {  habilidadesSinTocarSuelo += 1; hechoConseguidoPuntoHabDashAbajo = true; }
                            FindObjectOfType<NewAudioManager>().Play("PlayerDashFall");
                            //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dashAbajo);
                            rb.AddForce(fuerzaDashCaida * Vector2.down/* * Time.deltaTime*/);
                            dashEnCaida = true;
                            GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasDashCaida, posGround.position, posGround);
                            GetComponent<Particulas>().particulasDashCaida.gameObject.SetActive(true);
                            if (rb.velocity.y > -1) rb.velocity = new Vector2(rb.velocity.x, -1);

                        }
                        else if (mEnergy.actualEnergy < mEnergy.energiaDashAbajo)
                        {
                            if (auxTiempoSonidoSinEnergia == 0)
                            {
                                NewAudioManager.Instance.Play("PlayerNoEnergy");
                                auxTiempoSonidoSinEnergia = tiempoSonidoSinEnergia;
                            }
                        }
                    }
                    else
                    {
                        if (tiempoCOYOTE < -0.05f && mEnergy.actualEnergy > mEnergy.energiaDashAbajo && dashEnCaida == false)
                        {
                            if (hechoConseguidoPuntoHabDashAbajo == false) { habilidadesSinTocarSuelo += 1; hechoConseguidoPuntoHabDashAbajo = true; }
                            FindObjectOfType<NewAudioManager>().Play("PlayerDashFall");
                            //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dashAbajo);
                            rb.AddForce(fuerzaDashCaida * Vector2.down/* * Time.deltaTime*/);
                            dashEnCaida = true;
                            GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasDashCaida, posGround.position, posGround);
                            GetComponent<Particulas>().particulasDashCaida.gameObject.SetActive(true);
                            if (rb.velocity.y > -1) rb.velocity = new Vector2(rb.velocity.x, -1);

                        }
                        else if (mEnergy.actualEnergy < mEnergy.energiaDashAbajo)
                        {
                            if (auxTiempoSonidoSinEnergia == 0)
                            {
                                NewAudioManager.Instance.Play("PlayerNoEnergy");
                                auxTiempoSonidoSinEnergia = tiempoSonidoSinEnergia;
                            }
                        }
                    }
                }

            }
            if (grounded)
            {
                hechoConseguidoPuntoHabDashAbajo = false;
                dashEnCaida = false;
                //GetComponent<Particulas>().particulasDashCaida.gameObject.SetActive(false);

            }
        }
        if (rb.velocity.y == 0 || grounded)
        {
            dashEnCaida = false;

        }
        animCC.SetBool("cayendo", dashEnCaida);

    }
    void SaltoNormal()
    {

        //if (Input.GetButtonDown("Jump"))
        if (joystick != null)
        {
            if (joystick.Action1.WasPressed || Input.GetButtonDown("Jump"))
            {
                auxpresalto = tiempoPreSalto;
                tiempoPulsadoEspacio = 0;

                if (grounded)
                {
                    maxYAntesMorir = -999999;
                    if (pulsadoEspacio == false)
                    {
                        if (pInput.inputHorizontal != 0)
                        {
                            if (pInput.ultimoInputHorizontal > 0)
                            {
                                if (tengoMaxspeed == false)
                                {

                                    //rb.velocity = new Vector2(velMaxima * 0.4f, rb.velocity.y);
                                }
                                else
                                {
                                    if (cambioSentidoReciente == true)
                                    {
                                        rb.velocity = new Vector2(velMaxima * 0.4f, rb.velocity.y);
                                    }
                                    else
                                    {
                                        rb.velocity = new Vector2(velMaxima, rb.velocity.y);
                                    }
                                }

                            }
                            else if (pInput.ultimoInputHorizontal < 0)
                            {
                                if (tengoMaxspeed == false)
                                {
                                    //rb.velocity = new Vector2(-velMaxima * 0.4f, rb.velocity.y);
                                }
                                else
                                {
                                    if (cambioSentidoReciente == true)
                                    {
                                        rb.velocity = new Vector2(-velMaxima * 0.4f, rb.velocity.y);
                                    }
                                    else
                                    {
                                        rb.velocity = new Vector2(-velMaxima, rb.velocity.y);
                                    }
                                }
                            }
                            if (saltoInmediato == false)
                            {
                                lastJumpPared = false;

                                if (looping)
                                {
                                    auxTiempoTrasSaltoLoop = tiempoTrasSaltoLoop;
                                    if (normal.y < 0.8f && normal.y > -0.8f) rb.velocity = Vector2.zero;
                                    //rb.velocity = (ultimaNormal.normalized * fuerzaSaltoMin);
                                }
                                else
                                {
                                    //rb.velocity = new Vector2(rb.velocity.x, ultimaNormal.y * fuerzaSaltoMin);
                                }
                                //rb.velocity = ;
                                //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().salto);
                                FindObjectOfType<NewAudioManager>().Play("PlayerJump");
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
                                    if (normal.y < 0.8f && normal.y > -0.8f) rb.velocity = Vector2.zero; auxTiempoTrasSaltoLoop = tiempoTrasSaltoLoop;
                                }
                                FindObjectOfType<NewAudioManager>().Play("PlayerJump");
                                //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().salto);
                                //rb.velocity = (ultimaNormal.normalized * fuerzaSaltoMin);
                                //rb.velocity = ultimaNormal.normalized * fuerzaSaltoMin;
                                rb.AddForce(this.transform.up * fuerzaSaltoMin, ForceMode2D.Impulse);
                            }
                        }
                        pulsadoEspacio = true;
                        saltoIniciado = false;
                        animCC.SetTrigger("Salto");
                        GetComponent<Particulas>().SpawnParticulasSinTransform(GetComponent<Particulas>().particulasSalto, posGround.position);
                    }
                }
                else
                {
                    if (saltoDobleHecho)
                    {

                        pulsadoEspacio = true;
                        saltoIniciado = true;
                        if ((tiempoCOYOTE > 0) && (rb.velocity.y < 0) && ultimaNormal.y > 0.1f)
                        {
                            lastJumpPared = false;
                            rb.velocity = new Vector2(rb.velocity.x, 0);
                            animCC.SetTrigger("Salto");
                            //rb.velocity = new Vector2(rb.velocity.x, 1* fuerzaSaltoMin);
                            if (looping) auxTiempoTrasSaltoLoop = tiempoTrasSaltoLoop;
                            FindObjectOfType<NewAudioManager>().Play("PlayerJump");
                            //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().salto);
                            rb.AddForce(new Vector2(0, 1) * fuerzaSaltoMin, ForceMode2D.Impulse);
                            pulsadoEspacio = true;
                            saltoIniciado = false;
                        }
                    }
                    else
                    {
                        if (pegadoPared == false)
                        {
                            if ((tiempoCOYOTE > 0) && (rb.velocity.y < 0))
                            {
                                lastJumpPared = false;
                                rb.velocity = new Vector2(rb.velocity.x, 0);
                                animCC.SetTrigger("Salto");
                                //rb.velocity = new Vector2(rb.velocity.x, 1* fuerzaSaltoMin);
                                if (looping) auxTiempoTrasSaltoLoop = tiempoTrasSaltoLoop;
                                FindObjectOfType<NewAudioManager>().Play("PlayerJump");
                                //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().salto);
                                rb.AddForce(new Vector2(0, 1) * fuerzaSaltoMin, ForceMode2D.Impulse);
                                pulsadoEspacio = true;
                                saltoIniciado = false;
                            }
                            else if (auxtiempoTrasSaltoPared <= 0.0f)
                            {


                                animCC.SetTrigger("DobleSalto");
                                //GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasDobleSalto, posGround.position, posGround);
                                saltoDobleHecho = true;
                                if (auxCdDashAtravesar <= 0) saltoDobleHechoParaDashCaida = true;
                                ultimaNormal = Vector2.zero;
                                dashEnCaida = false;
                                estoyDasheando = false;
                                PermitirGravedad();
                                float speedx = rb.velocity.x;
                                //rb.velocity = new Vector2(rb.velocity.x, 0);
                                //if (cambioSentidoReciente)
                                //{
                                //    constantegravedad = 1;
                                //    rb.velocity = new Vector2(-rb.velocity.x * 0.4f, fuerzaSaltoDoble);
                                //}
                                //else
                                {
                                    constantegravedad = 1;
                                    FindObjectOfType<NewAudioManager>().Play("PlayerDoubleJump");
                                    //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dobleSalto);

                                    rb.velocity = new Vector2(rb.velocity.x, fuerzaSaltoDoble);
                                }
                                saltoDobleReciente = true;
                                StopCoroutine(SaltoDobleReciente(tiempoTrasCambioSentido));
                                StartCoroutine(SaltoDobleReciente(tiempoTrasCambioSentido));

                                //rb.AddForce(new Vector2(speedx, fuerzaSaltoDoble));
                            }
                        }


                    }

                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                auxpresalto = tiempoPreSalto;
                tiempoPulsadoEspacio = 0;

                if (grounded)
                {
                    maxYAntesMorir = -999999;
                    if (pulsadoEspacio == false)
                    {
                        if (pInput.inputHorizontal != 0)
                        {
                            if (pInput.ultimoInputHorizontal > 0)
                            {
                                if (tengoMaxspeed == false)
                                {

                                    //rb.velocity = new Vector2(velMaxima * 0.4f, rb.velocity.y);
                                }
                                else
                                {
                                    if (cambioSentidoReciente == true)
                                    {
                                        rb.velocity = new Vector2(velMaxima * 0.4f, rb.velocity.y);
                                    }
                                    else
                                    {
                                        rb.velocity = new Vector2(velMaxima, rb.velocity.y);
                                    }

                                }

                            }
                            else if (pInput.ultimoInputHorizontal < 0)
                            {
                                if (tengoMaxspeed == false)
                                {
                                    //rb.velocity = new Vector2(-velMaxima * 0.4f, rb.velocity.y);
                                }
                                else
                                {
                                    if (cambioSentidoReciente == true)
                                    {
                                        rb.velocity = new Vector2(-velMaxima * 0.4f, rb.velocity.y);
                                    }
                                    else
                                    {
                                        rb.velocity = new Vector2(-velMaxima, rb.velocity.y);
                                    }
                                }
                            }
                            if (saltoInmediato == false)
                            {
                                lastJumpPared = false;

                                if (looping)
                                {
                                    auxTiempoTrasSaltoLoop = tiempoTrasSaltoLoop;
                                    if (normal.y < 0.8f && normal.y > -0.8f) rb.velocity = Vector2.zero;
                                }
                                else
                                {
                                    //rb.velocity = new Vector2(rb.velocity.x, ultimaNormal.y * fuerzaSaltoMin);
                                }
                                //rb.velocity = ;
                                FindObjectOfType<NewAudioManager>().Play("PlayerDashFall");
                                //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dashAbajo);
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
                                    if (normal.y < 0.8f && normal.y > -0.8f) rb.velocity = Vector2.zero;
                                }
                                FindObjectOfType<NewAudioManager>().Play("PlayerDashFall");
                                //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dashAbajo);
                                //rb.velocity = (ultimaNormal.normalized * fuerzaSaltoMin);
                                //rb.velocity = ultimaNormal.normalized * fuerzaSaltoMin;
                                rb.AddForce(this.transform.up * fuerzaSaltoMin, ForceMode2D.Impulse);
                            }
                        }
                        pulsadoEspacio = true;
                        saltoIniciado = false;
                        animCC.SetTrigger("Salto");
                        GetComponent<Particulas>().SpawnParticulasSinTransform(GetComponent<Particulas>().particulasSalto, posGround.position);
                    }
                }
                else
                {
                    if (saltoDobleHecho)
                    {
                        pulsadoEspacio = true;
                        saltoIniciado = true;
                        if ((tiempoCOYOTE > 0) && (rb.velocity.y < 0) && ultimaNormal.y > 0.1f)
                        {
                            lastJumpPared = false;
                            rb.velocity = new Vector2(rb.velocity.x, 0);
                            animCC.SetTrigger("Salto");
                            //rb.velocity = new Vector2(rb.velocity.x, 1* fuerzaSaltoMin);
                            if (looping) auxTiempoTrasSaltoLoop = tiempoTrasSaltoLoop;
                            FindObjectOfType<NewAudioManager>().Play("PlayerDashFall");
                            //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dashAbajo);
                            rb.AddForce(new Vector2(0, 1) * fuerzaSaltoMin, ForceMode2D.Impulse);
                            pulsadoEspacio = true;
                            saltoIniciado = false;
                        }
                    }
                    else
                    {
                        if (pegadoPared == false)
                        {
                            if ((tiempoCOYOTE > 0) && (rb.velocity.y < 0))
                            {
                                lastJumpPared = false;
                                rb.velocity = new Vector2(rb.velocity.x, 0);
                                animCC.SetTrigger("Salto");
                                //rb.velocity = new Vector2(rb.velocity.x, 1* fuerzaSaltoMin);
                                if (looping) auxTiempoTrasSaltoLoop = tiempoTrasSaltoLoop;
                                FindObjectOfType<NewAudioManager>().Play("PlayerJump");
                                //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().salto);
                                rb.AddForce(new Vector2(0, 1) * fuerzaSaltoMin, ForceMode2D.Impulse);
                                pulsadoEspacio = true;
                                saltoIniciado = false;
                            }
                            else if (auxtiempoTrasSaltoPared <= 0.0f)
                            {

                                PermitirGravedad();
                                animCC.SetTrigger("DobleSalto");
                                //GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasDobleSalto, posGround.position, posGround);
                                saltoDobleHecho = true;
                                if (auxCdDashAtravesar <= 0) saltoDobleHechoParaDashCaida = true;
                                ultimaNormal = Vector2.zero;

                                dashEnCaida = false;
                                estoyDasheando = false;
                                float speedx = rb.velocity.x;
                                //rb.velocity = new Vector2(rb.velocity.x, 0);
                                //if (cambioSentidoReciente)
                                //{
                                //    constantegravedad = 1;
                                //    rb.velocity = new Vector2(-rb.velocity.x * 0.4f, fuerzaSaltoDoble);
                                //}
                                //else
                                {
                                    constantegravedad = 1;
                                    FindObjectOfType<NewAudioManager>().Play("PlayerDoubleJump");
                                    //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().dobleSalto);

                                    rb.velocity = new Vector2(rb.velocity.x, fuerzaSaltoDoble);
                                }
                                saltoDobleReciente = true;
                                StopCoroutine(SaltoDobleReciente(tiempoTrasCambioSentido));
                                StartCoroutine(SaltoDobleReciente(tiempoTrasCambioSentido));

                                //rb.AddForce(new Vector2(speedx, fuerzaSaltoDoble));
                            }
                        }


                    }

                }
            }
        }
        if (joystick != null)
        {
            if (joystick.Action1.WasReleased || Input.GetButtonUp("Jump"))
            {
                if ((tiempoPulsadoEspacio < tiempoSaltoCompleto) && (rb.velocity.y > 0))
                {

                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }

                auxpresalto = tiempoPreSalto;
                pulsadoEspacio = false;
                tiempoPulsadoEspacio = 0;
                //saltoSoltado = false;

            }
        }
        else
        {
            if (Input.GetButtonUp("Jump"))
            {

                if ((tiempoPulsadoEspacio < tiempoSaltoCompleto) && (rb.velocity.y > 0))
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }



                auxpresalto = tiempoPreSalto;
                pulsadoEspacio = false;
                tiempoPulsadoEspacio = 0;
                //saltoSoltado = false;

            }
        }
        if (grounded)
        {
            dashEnCaida = false;
            dashCaidaBloqueado = false;
            saltoDobleHecho = false;
            saltoDobleHechoParaDashCaida = false;
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
                            rb.velocity = new Vector2(-velMaxima * 0.4f, rb.velocity.y);
                        }
                        else
                        {
                            if (cambioSentidoReciente == false)
                            {
                                rb.velocity = new Vector2(-velMaxima * 0.4f, rb.velocity.y);
                            }
                            else
                            {
                                rb.velocity = new Vector2(-velMaxima, rb.velocity.y);
                            }
                        }
                    }
                    else if (pInput.inputHorizontal > 0)
                    {

                        if (tengoMaxspeed == false)
                        {
                            rb.velocity = new Vector2(velMaxima * 0.4f, rb.velocity.y);
                        }
                        else
                        {
                            if (cambioSentidoReciente == false)
                            {
                                rb.velocity = new Vector2(velMaxima * 0.4f, rb.velocity.y);
                            }
                            else
                            {
                                rb.velocity = new Vector2(velMaxima, rb.velocity.y);
                            }

                        }
                    }
                    else if (pInput.inputHorizontal == 0)
                    {

                        if (tengoMaxspeed == false)
                        {
                            rb.velocity = new Vector2(0, rb.velocity.y);
                        }
                        else
                        {
                            if (cambioSentidoReciente == false)
                            {
                                rb.velocity = new Vector2(0, rb.velocity.y);
                            }
                            else
                            {
                                rb.velocity = new Vector2(0, rb.velocity.y);
                            }

                        }
                    }
                    FindObjectOfType<NewAudioManager>().Play("PlayerJump");
                    //this.GetComponent<AudioManager>().Play(this.GetComponent<AudioManager>().sonidosUnaVez, this.GetComponent<AudioManager>().salto);
                    maxYAntesMorir = -999999;
                    rb.AddForce(this.transform.up * fuerzaSaltoMin, ForceMode2D.Impulse);
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
        { /*if (Input.GetButton("Jump"))*/
            if (joystick != null)
            {
                if (joystick.Action1.IsPressed || Input.GetButton("Jump"))
                {


                    if ((pulsadoEspacio) && (saltoInmediato == false))
                    {


                        tiempoPulsadoEspacio += Time.deltaTime;


                        if (tiempoPulsadoEspacio < tiempoSaltoCompleto)
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

                                        if (ultimaNormal.y > 0.1f)
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
                                        else
                                        {
                                            if (rb.velocity.y < fSaltoInicial)
                                            {

                                                if (pInput.inputHorizontal == 0)
                                                {
                                                    rb.velocity = new Vector2(rb.velocity.x, fSaltoInicial);
                                                }
                                                else
                                                {
                                                    rb.velocity = new Vector2(rb.velocity.x, fSaltoInicial * 0.92f);
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
                                                    rb.velocity = new Vector2(rb.velocity.x, fSaltoMedio * 0.92f);
                                                }
                                                //rb.AddForce(new Vector2(0, 1).normalized * fuerzaSaltoMax * 0.5f * Time.deltaTime);
                                            }
                                            else if (rb.velocity.y > fSaltoAlto)
                                            {

                                                if (pInput.inputHorizontal == 0)
                                                {
                                                    rb.velocity = new Vector2(rb.velocity.x, fSaltoAlto * 1.08f);
                                                }
                                                else
                                                {
                                                    rb.velocity = new Vector2(rb.velocity.x, fSaltoAlto);
                                                }
                                                //rb.AddForce(new Vector2(0, 1).normalized * fuerzaSaltoMax * 0.5f * Time.deltaTime);
                                            }
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
            else
            {

                if (Input.GetButton("Jump"))
                {


                    if ((pulsadoEspacio) && (saltoInmediato == false))
                    {


                        tiempoPulsadoEspacio += Time.deltaTime;


                        if (tiempoPulsadoEspacio < tiempoSaltoCompleto && tiempoPulsadoEspacio > 0.1f)
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

                                        if (ultimaNormal.y > 0.1f)
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
                                        else
                                        {
                                            if (rb.velocity.y < fSaltoInicial)
                                            {

                                                if (pInput.inputHorizontal == 0)
                                                {
                                                    rb.velocity = new Vector2(rb.velocity.x, fSaltoInicial);
                                                }
                                                else
                                                {
                                                    rb.velocity = new Vector2(rb.velocity.x, fSaltoInicial * 0.92f);
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
                                                    rb.velocity = new Vector2(rb.velocity.x, fSaltoMedio * 0.92f);
                                                }
                                                //rb.AddForce(new Vector2(0, 1).normalized * fuerzaSaltoMax * 0.5f * Time.deltaTime);
                                            }
                                            else if (rb.velocity.y > fSaltoAlto)
                                            {

                                                if (pInput.inputHorizontal == 0)
                                                {
                                                    rb.velocity = new Vector2(rb.velocity.x, fSaltoAlto * 1.08f);
                                                }
                                                else
                                                {
                                                    rb.velocity = new Vector2(rb.velocity.x, fSaltoAlto);
                                                }
                                                //rb.AddForce(new Vector2(0, 1).normalized * fuerzaSaltoMax * 0.5f * Time.deltaTime);
                                            }
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

        }
        //if (Input.GetButtonUp("Jump"))




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
            //Collider2D[] hit = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 1.1f);
            //foreach (Collider2D col in hit)
            //{

            if (hit.collider.tag == "Loop")
            {

                looping = true;

            }
            else
            {
                looping = false;
            }


            //}

            normal = new Vector2(hit.normal.x, hit.normal.y);
            ultimaNormal = normal;
            Debug.DrawRay(transform.position, normal * 100, Color.red);


        }
        if (normal != null)
        {
            //if (pInput.personajeInvertido == true)
            //{
            if (normal.y < -0.15)
            {
                if (grounded)
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
                invertirValores = false;
            }
            //}
            //else
            //{
            //if (normal.y <- 0.15)
            //{
            //    invertirValores = false;
            //}
            //else
            //{
            //    invertirValores = true;
            //}
            //}

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

            grounded = true;
            if (hit.collider.tag == "Loop")
            {

                looping = true;

            }
            else
            {
                looping = false;
            }
            normal = new Vector2(hit.normal.x, hit.normal.y);
            ultimaNormal = normal;
            Debug.DrawRay(transform.position, normal * 100, Color.red);


        }
        else if ((hit2.collider != null))
        {
            if (hit2.collider.tag == "Loop")
            {

                looping = true;

            }
            else
            {
                looping = false;
            }

            grounded = true;

            normal = new Vector2(hit2.normal.x, hit2.normal.y);

            if (estoyDasheando == false) ultimaNormal = normal;

            Debug.DrawRay(transform.position, normal * 100, Color.red);
        }
        else if ((hit3.collider != null))
        {
            if (hit3.collider.tag == "Loop")
            {

                looping = true;

            }
            else
            {
                looping = false;
            }

            grounded = true;

            normal = new Vector2(hit3.normal.x, hit3.normal.y);
            normal3 = new Vector2(hit3.normal.x, hit3.normal.y);
            if (estoyDasheando == false) ultimaNormal = normal;
            Debug.DrawRay(transform.position, normal * 100, Color.red);
        }
        else
        {
            looping = false;
            grounded = false;



        }

        normal2 = new Vector2(hit2.normal.x, hit2.normal.y);
        normal3 = new Vector2(hit3.normal.x, hit3.normal.y);
        if (normal.y < 0.05f && normal.y > -0.05f)
        {
            if (normal2.y < 0.05f && normal2.y > -0.05f)
            {
                if (normal3.y < 0.05f && normal3.y > -0.05f)
                {
                    if (!looping) grounded = false;
                }
            }
        }

        if (grounded)
        {
            deteccionParedes = true;
            tiempoCOYOTE = auxtiempoMaxSuelo;
        }
        else
        {
            tiempoCOYOTE -= Time.deltaTime;
        }
    }
    void GirarPersonaje()
    {
        if (!grounded)
        {
            this.transform.up = Vector3.Slerp(this.transform.up, new Vector3(0, 1, 0), Time.deltaTime * 20);
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

        if ((!grounded) && (auxCdDashAtravesar > 0.0f))
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

                        tocando = true;
                    }
                    else if (tocandoBorde2.collider != null && tocandoBorde2.collider.tag != "Enemigo" && tocandoBorde2.collider.tag != "NoClimb" && tocandoBorde2.collider.tag != "Loop" && tocandoBorde2.collider.tag != "Pinchos")
                    {

                        tocando = true;
                    }
                    if (tocandoBorde.collider == null && tocandoBorde2.collider == null)
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
                            puntoChoque = izquierda.point;
                            ledgePos3 = new Vector2((puntoCheckBorde.position.x - distanciaBorde) - ledgeClimbXOffset3, (puntoCheckBorde.position.y) + ledgeClimbYOffset3);
                            ledgePos1 = puntoChoque;
                            ledgePos2 = puntoChoque;

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

                            movimientoBloqueado = true;

                            ledgePos1 = puntoChoque;
                            //ledgePos1 = new Vector2((puntoCheckBorde.position.x + distanciaBorde + ledgeClimbXOffset1),( puntoCheckBorde.position.y + ledgeClimbYOffset1));
                            //ledgePos2 = new Vector2(puntoCheckBorde.position.x + distanciaBorde + ledgeClimbXOffset2, puntoCheckBorde.position.y+ ledgeClimbYOffset2);
                            ledgePos2 = puntoChoque;

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


        //rb.velocity = Vector2.zero;
        animCC.SetBool("canClimbLedge", true);
        //transform.position = ledgePos1;



        //transform.position = Vector3.MoveTowards(ledgePos1, ledgePos3, 2f);

        yield return new WaitForSeconds(0.00f);

        //transform.position = Vector3.MoveTowards(transform.position, ledgePos2, 2f);
        transform.position = new Vector3(ledgePos2.x, ledgePos2.y + 2f, 0);
        animCC.SetBool("canClimbLedge", false);
        movimientoBloqueado = false;

        subiendoUnavez = false;
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
            Instantiate(feedbackMonedas, collision.gameObject.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        }
        if (collision.tag == "Estrella")
        {
            Destroy(collision.gameObject);
        }

        if (collision.tag == "TriggerPausaBoss")
        {
            if (boss != null)
            {

                boss.GetComponent<BossPath>().cmpRuta.pausaConTrigger = false;

            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "TriggerPausaBoss")
        {
            if (boss != null)
            {

                boss.GetComponent<BossPath>().cmpRuta.pausaConTrigger = false;

            }
        }
    }


    public void CargarHabilidadesGM()
    {
        dashUnlock = GameManager.Instance.Habilidades.dash;
        chispazoUnlook = GameManager.Instance.Habilidades.chispazo;
        movParedesUnlook = GameManager.Instance.Habilidades.movParedes;
        movCablesUnlook = GameManager.Instance.Habilidades.movCables;
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
