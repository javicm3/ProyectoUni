using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    bool unavezrotar = true;
    float originalgravity;
    public Vector2 normal;
    public bool puedoEscalar = true;
    bool primeravezpared = false;
    public float m_JumpForce = 140f;
    public float fuerzaDobleSalto = 50f;
    public float fuerzaWallJump = 350f;
    public float fuerzaImpulso = 400;
    public float fuerzaBouncerGrande = 337.5f;
    public float dashForce = 100;
    public float cooldownDash = 3f;
    public float velocidadSubidaPared = 10f;
    float auxcdDash = 0;
    public bool flipped = false;
    public float tiempoantesCaer = 1f;
    float auxtiempoCaer = 0;
    public bool justdashed = false;
    float auxtiempoImpulso = 0.2f;
    public bool bajandoPared = false;
    public bool impulsohecho = false;
    bool auxunavezPared = false;
    public bool puedoDash = true;
    public bool wallJumping = false;
    float auxtimeGrounded = 0;
    public bool puedomoverme = true;
    public bool puedoSaltar = true;
    public GameObject spriteDash;
    public bool pegadoPared = false;
    public bool dashing = false;
    bool impulsoreciente = false;
    public float bounceForce = 200f;// Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] public Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
    public bool dash2 = false;
    public bool dashgolpeo = false;
    const float k_GroundedRadius = 0.35f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    public Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public Vector3 m_Velocity = Vector3.zero;
    public Vector3 targetVelocity;
    private bool jump;
    public Rebote reboteScript;
    public float speedMov = 4f;
    public float lastspeedx;
    string lastContact;
    [Header("Events")]
    [Space]
    public GameObject lastWall;
    public GameObject lastWall2;
    public bool enabledWallJump = false;
    public bool doneWallJump = false;
    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    public Animator anim;
    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;
    public float slopeFriction;
    EnergyManager energymanager;
    private bool isTouchingLedge;
    private bool canClimbLedge = false;
    private bool ledgeDetected;
    bool canMove = true;
    public bool isTouchingWall = false;
    public bool isTouchingPared = false;
    public LayerMask m_defaultCapa;
    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;
    public Transform wallCheck;
    public Transform ledgeCheck;
    public float wallCheckDistance;
    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;
    GameObject posfinal;
    Movimiento mov;
    public AudioClip saltar;
    public AudioClip dash;


    public AudioClip rebotar;
    public AudioClip pinball;
    public AudioClip saltarpequeño;
    public AudioClip saltopared;
    public AudioClip cogerColeccionable;
    public AudioClip impulsadorsonido;
    public AudioSource source;

    public bool FacingRight
    {
        get
        {
            return m_FacingRight;
        }

        set
        {
            m_FacingRight = value;
        }
    }

    public bool Grounded
    {
        get
        {
            return m_Grounded;
        }

        set
        {
            m_Grounded = value;
        }
    }

    public Vector2 Normal { get => normal; set => normal = value; }

    private void Awake()
    {
        mov = this.GetComponent<Movimiento>();
        energymanager = this.GetComponent<EnergyManager>();
        auxtiempoCaer = tiempoantesCaer;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        originalgravity = m_Rigidbody2D.gravityScale;
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }
    public void StopDash()
    {
        if (dashing == true)
        {
            Invoke("TiempoDañoDash", 0.4f);
            justdashed = true;
        }
        impulsohecho = false;
        dashing = false;
        dash2 = false;
        dashgolpeo = false;

    }
    public void TiempoDañoDash()
    {
        justdashed = false;

    }
    private void CheckLedgeClimb()
    {
        //Collider2D[] hit = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 6);
        //foreach (Collider2D col in hit)
        //{
        //    if (puedoEscalar == true)
        //    {
        //        if (col.CompareTag("Loop"))
        //        {
        //            print("NO ESCALO");
        //            puedoEscalar = false;
        //        }

        //    }
        //}
        //RaycastHit2D[] hit = Physics2D.RaycastAll(this.gameObject.transform.position, new Vector2(0, -1), 2);
        //foreach (RaycastHit2D col in hit)
        //{
        //    if (puedoEscalar == true)
        //    {
        //        if (col.collider.CompareTag("Loop"))
        //        {
        //            print("NO ESCALO");
        //            puedoEscalar = false;
        //        }
        //    }

        //}


        if (ledgeDetected && !canClimbLedge && puedoEscalar == true)
        {
            canClimbLedge = true;

            if (!flipped)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }

            canMove = false;


            anim.SetBool("canClimbLedge", canClimbLedge);
        }

        if (canClimbLedge)
        {
            if (GameObject.FindObjectOfType<ViajeCables>().viajando == false)
            {
                transform.position = ledgePos1;
            }
        }
    }
    public void SpawnParticulasCorrer()
    {
        if (mov.maxSpeed == true)
        {
            GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasvelMax, m_GroundCheck.position);
        }
    }
    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        if (GameObject.FindObjectOfType<ViajeCables>().viajando == false)
        {
            transform.position = ledgePos2;
        }

        canMove = true;

        ledgeDetected = false;
        anim.SetBool("canClimbLedge", canClimbLedge);
    }

    private void CheckSurroundings()
    {
        if (GameObject.FindObjectOfType<ViajeCables>().viajando == false)
        {

            isTouchingWall = Physics2D.Raycast(wallCheck.position, new Vector2(this.GetComponent<Movimiento>().lastdireccionmov, 0), wallCheckDistance, m_WhatIsGround);
            isTouchingPared = Physics2D.Raycast(wallCheck.position, new Vector2(this.GetComponent<Movimiento>().lastdireccionmov, 0), wallCheckDistance, m_defaultCapa);
            isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, new Vector2(this.GetComponent<Movimiento>().lastdireccionmov, 0), wallCheckDistance, m_WhatIsGround);

            Collider2D[] hit = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 1.5f);
            foreach (Collider2D col in hit)
            {
                if (puedoEscalar == true)
                {
                    if (col.CompareTag("Loop"))
                    {
                        print("NO ESCALO");
                        puedoEscalar = false;
                    }

                }


            }
            if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
            {
                print("detectado");
                //if (pegadoPared != true)
                //{
                if (puedoEscalar == true)
                {
                    ledgeDetected = true;
                    ledgePosBot = wallCheck.position;
                }

                //}

            }

            Collider2D[] hot = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 1.9f);
            foreach (Collider2D col in hot)
            {
                if (puedoEscalar == false && (!Grounded))
                {
                    if (col.CompareTag("Loop") == false)
                    {
                        print("Escalo");
                        puedoEscalar = true;
                    }
                }
                if ((puedoEscalar == true))
                {
                    if (col.CompareTag("Loop"))
                    {
                        print("NO ESCALO");
                        puedoEscalar = false;
                        unavezrotar = true;
                        m_Rigidbody2D.freezeRotation = true;
                    }

                }

            }
        }
    }
    private void Update()
    {
        if (puedoEscalar == false)
        {if(Grounded) m_Rigidbody2D.gravityScale = originalgravity;
            if (unavezrotar == true)
            {
                unavezrotar = false;
                m_Rigidbody2D.freezeRotation = false;
            }
            this.gameObject.transform.up = Normal;
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                m_Rigidbody2D.gravityScale = 0;

                m_Rigidbody2D.AddForce(-Normal *1400 * Time.deltaTime);
            }

            print(Normal + "NORMALITAS");
            Debug.DrawRay(transform.position, Normal * 100, Color.red);
        }
        else if (!Grounded)
        {
            transform.up = new Vector2(0, 1);
            m_Rigidbody2D.gravityScale = originalgravity;
            m_Rigidbody2D.freezeRotation = true;
        }
        if (puedoEscalar == true)
        {
            if (Grounded)
            {
                mov.speed = Mathf.Abs(mov.speed);
                m_Rigidbody2D.gravityScale = originalgravity;
            }

            }

        if (m_Grounded) { anim.SetBool("Grounded", true); }
        else { anim.SetBool("Grounded", false); }
        if (mov.cayendoS == true)
        {
            anim.SetBool("cayendo", true);

        }
        else
        {
            anim.SetBool("cayendo", false);
        }


        CheckLedgeClimb();
        CheckSurroundings();
        if (m_Grounded) pegadoPared = false;
        if (impulsoreciente == true)
        {
            auxtiempoImpulso -= Time.deltaTime;
            if (auxtiempoImpulso <= 0)
            {
                auxtiempoImpulso = 0.2f;
                impulsoreciente = false;
            }
        }
        if (pegadoPared == true)
        {
            anim.SetBool("PegadoPared", true);
            if (m_Rigidbody2D.velocity.y >= 1)
            {
                anim.SetBool("MoviendoPared", true);
            }
            else
            {
                anim.SetBool("MoviendoPared", false);
            }
        }
        if (puedomoverme == false)
        {
            mov.multSpeed = 1;
        }
        if (pegadoPared)
        {

            if (bajandoPared == false)
            {
                auxtiempoCaer -= Time.deltaTime;
                if (auxtiempoCaer <= 0) auxtiempoCaer = 0;
            }

            if ((bajandoPared == false) && (auxtiempoCaer == 0))
            {
                //PARTICULAS PARED CAIDA
                GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasWallJump, new Vector2((m_GroundCheck.transform.position.x - this.GetComponent<Movimiento>().lastdireccionmov), m_GroundCheck.transform.position.y));
                GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasWallJump, new Vector2((m_GroundCheck.transform.position.x - this.GetComponent<Movimiento>().lastdireccionmov), m_GroundCheck.transform.position.y + 0.1f));
                bajandoPared = true;
            }
        }
        else
        {
            auxtiempoCaer = tiempoantesCaer;
        }
        if (auxcdDash <= 0)
        {

            if (energymanager.actualEnergy >= energymanager.energiaDash)
            {
                if (puedoDash == true)
                {
                    spriteDash.GetComponent<SpriteRenderer>().enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if ((dashing == false) && (pegadoPared == false))
                    {
                        if (puedoDash == true)
                        {
                            if (wallJumping == true)
                            {



                                if (Input.GetAxisRaw("Horizontal") > 0)
                                {
                                    this.GetComponent<Movimiento>().lastdireccionmov = 1f;
                                }
                                else if (Input.GetAxisRaw("Horizontal") < 0)
                                {
                                    this.GetComponent<Movimiento>().lastdireccionmov = -1f;
                                }
                            }
                            dashing = true;
                            auxcdDash += cooldownDash;
                            this.GetComponent<VidaPlayer>().recienAtacado = true;
                            mov.multSpeed *= 0.7f;
                            Dash();
                            energymanager.RestarEnergia(energymanager.energiaDash);
                            CancelInvoke("StopDash");
                            Invoke("StopDash", 0.15f);

                        }
                    }



                }
            }
            else
            {
                spriteDash.GetComponent<SpriteRenderer>().enabled = false;
            }


        }
        else
        {
            spriteDash.GetComponent<SpriteRenderer>().enabled = false;
            auxcdDash -= Time.deltaTime;
        }
        if (canMove == true)
        {


            if (m_Rigidbody2D.velocity.x < -0.1f)
            {
                if (flipped == false)
                {
                    transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                    flipped = true;
                    //this.GetComponent<SpriteRenderer>().flipX = true;
                    lastspeedx = m_Rigidbody2D.velocity.x;
                }

            }
            else if (m_Rigidbody2D.velocity.x > 0.1f)
            {
                if (flipped == true)
                {
                    transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                    //this.transform.localScale = new Vector2(1, 1);
                    flipped = false;
                    //this.GetComponent<SpriteRenderer>().flipX = false;
                    lastspeedx = m_Rigidbody2D.velocity.x;
                }

            }
            if ((m_Rigidbody2D.velocity.x > -0.1f) && (m_Rigidbody2D.velocity.x < 0.1f))
            {
                if (lastspeedx < 0)
                {
                    //this.GetComponent<SpriteRenderer>().flipX = true;
                }


            }
            else
            {
                if (this.GetComponent<Movimiento>().lastdireccionmov > 0)
                {
                    if (flipped == true)
                    {
                        transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                        //this.transform.localScale = new Vector2(1, 1);
                        flipped = false;
                        //this.GetComponent<SpriteRenderer>().flipX = false;
                        lastspeedx = m_Rigidbody2D.velocity.x;
                    }
                }
                else
                {
                    if (flipped == false)
                    {
                        transform.Find("Cuerpo").localScale *= new Vector2(-1, 1);
                        flipped = true;
                        //this.GetComponent<SpriteRenderer>().flipX = true;
                        lastspeedx = m_Rigidbody2D.velocity.x;
                    }
                }
            }
        }


        if (enabledWallJump == true)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                pegadoPared = false;
                WallJumpPlayer(lastContact);

            }
        }
        else
        {
            if (this.GetComponent<Movimiento>().lastdireccionmov != 0) anim.SetBool("PegadoPared", false); anim.SetBool("MoviendoPared", false);

        }
        if (wallJumping == true)
        {
            puedomoverme = false;
        }
        //if ((m_Rigidbody2D.velocity.x <= 0.5f) && (m_Rigidbody2D.velocity.x >= -0.5f))
        //{
        //    anim.SetBool("Corriendo", false);
        //}
        //else if ((m_Rigidbody2D.velocity.x != 0) && (m_Rigidbody2D.velocity.y <= 0.05) && (m_Rigidbody2D.velocity.y >= -0.05))
        //{
        //    if (pegadoPared == false)
        //    {
        //        anim.SetBool("Corriendo", true);
        //    }

        //}
        if (Mathf.Abs(m_Rigidbody2D.velocity.x) > 0.5f)
        {
            if (pegadoPared == false)
            {
                anim.SetBool("Corriendo", true);
            }
        }
        else
        {
            if (pegadoPared == false)
            {
                anim.SetBool("Corriendo", false);
            }
        }
        anim.SetFloat("SpeedY", m_Rigidbody2D.velocity.y);
        bool wasGrounded = m_Grounded;

        m_Grounded = false;
        NormalizeSlope();
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
        if (m_Grounded == true)
        {
            puedoDash = true;
            reboteScript.puedoParry = true;
            ResetWall();

        }

    }
    void NormalizeSlope()
    {
        // Attempt vertical normalization
        if (m_Grounded)
        {

            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, m_WhatIsGround);

            if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
            {
                Rigidbody2D body = GetComponent<Rigidbody2D>();
                // Apply the opposite force against the slope force 
                // You will need to provide your own slopeFriction to stabalize movement
                body.velocity = new Vector2(body.velocity.x - (hit.normal.x * slopeFriction), body.velocity.y);

                //Move Player up or down to compensate for the slope below them
                Vector3 pos = transform.position;
                pos.y += -hit.normal.x * Mathf.Abs(body.velocity.x) * Time.deltaTime * (body.velocity.x - hit.normal.x > 0 ? 1 : -1);

                transform.position = pos;

            }
        }
    }

    public void Move(Vector2 move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        //if (!crouch)
        //{
        //	// If the character has a ceiling preventing them from standing up, keep them crouching
        //	if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
        //	{
        //		crouch = true;
        //	}
        //}

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }
            if (canMove == true)
            {


                // Move the character by finding the target velocity
                if (GameObject.FindObjectOfType<GameManager>().personajevivo != false)
                {
                    if (bajandoPared == true)
                    {
                        if (m_Grounded == false) targetVelocity = new Vector2(move.x * speedMov, -10);


                    }
                    else
                    {
                        if (puedoEscalar == false)
                        {if(normal.y>-0.2f && normal.y < 0.1f)
                            {
                                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 1.4f * m_Rigidbody2D.velocity.y);
                            }
                            targetVelocity = new Vector2(move.x * speedMov, m_Rigidbody2D.velocity.y);
                        }
                        targetVelocity = new Vector2(move.x * speedMov, m_Rigidbody2D.velocity.y);
                    }

                    // And then smoothing it out and applying it to the character
                    m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                }
            }
            //// If the input is moving the player right and the player is facing left...
            //if (move > 0 && !m_FacingRight)
            //{
            //    // ... flip the player.
            //    Flip();
            //}
            //// Otherwise if the input is moving the player left and the player is facing right...
            //else if (move < 0 && m_FacingRight)
            //{
            //    // ... flip the player.
            //    Flip();
            //}
        }
        if (m_Grounded == false)
        {
            if (puedoSaltar == true)
            {
                auxtimeGrounded += Time.deltaTime;
                if (auxtimeGrounded > 0.16f)
                {
                    auxtimeGrounded = 0;
                    puedoSaltar = false;
                }
            }

        }
        else
        {
            mov.unavezDobleSalto = false;
            puedoSaltar = true;
        }
        // If the player should jump...
        if (puedoSaltar && jump)
        {
            // Add a vertical force to the player.
            SaltoPlayer();
        }

    }
    //private void PendientesVerticales(ref Vector2 deltamovement)
    //{
    //    var centro = (_raycastAbajoIzq.x + _raycastAbajoDer.x) / 2;
    //    var direccion = -Vector2.up;
    //    var distPendiente = limitePendiente * (_raycastAbajoIzq.x - centro);
    //    var rayoPendiente = new Vector2(centro, _raycastAbajoIzq.x);
    //}
    public void Dash()
    {
        source.PlayOneShot(dash);
        if (this.GetComponent<Movimiento>().lastdireccionmov > 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                this.GetComponent<Movimiento>().lastdireccionmov = 1f;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                this.GetComponent<Movimiento>().lastdireccionmov = -1f;
            }



            m_Rigidbody2D.AddForce(new Vector2(dashForce, 0));
        }
        else if (this.GetComponent<Movimiento>().lastdireccionmov < 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                this.GetComponent<Movimiento>().lastdireccionmov = 1f;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                this.GetComponent<Movimiento>().lastdireccionmov = -1f;
            }
            m_Rigidbody2D.AddForce(new Vector2(-dashForce, 0));
        }
        if (this.GetComponentInChildren<TriggerColliderPLayer>() != null) this.GetComponentInChildren<TriggerColliderPLayer>().IniciarAtaque("dash");
        this.GetComponent<Movimiento>().cayendoS = false;

        anim.SetTrigger("Dash");
        this.GetComponent<Particulas>().SpawnParticulas(this.GetComponent<Particulas>().particulasDash, new Vector2((m_GroundCheck.transform.position.x - this.GetComponent<Movimiento>().lastdireccionmov), m_GroundCheck.transform.position.y + 0.8f));
        this.GetComponent<Movimiento>().multSpeed = this.GetComponent<Movimiento>().speedMax - 0.2f;
        puedoDash = false;
    }
    public void SaltoPlayer()
    { 
        source.PlayOneShot(saltar);
        GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasSalto, m_GroundCheck.position);
        m_Grounded = false;

        if (puedoEscalar)
        {
            float speedx = m_Rigidbody2D.velocity.x;
            if (mov.maxSpeed == false)
            {
                if (mov.parado == true)
                {
                    mov.multSpeed = mov.speedMax * 0.1f;
                }
                else
                {
                    mov.multSpeed = mov.speedMax * 0.68f;
                }

            }
            else
            {
                mov.multSpeed = mov.speedMax;
            }
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Rigidbody2D.AddForce(new Vector2(0, m_JumpForce));
        }
        else
        {
            m_Rigidbody2D.AddForce(new Vector2(normal.x *1.4f* m_JumpForce, normal.y * m_JumpForce));
        }
       
        //this.GetComponent<Movimiento>().multSpeed *= 0.8f; 

        puedoEscalar = true;
    }
    public void ReboteEnemigo()
    {


        puedoSaltar = false;
        dashing = false;
        //anim.SetTrigger("Salto2");
        ResetWall();
        m_Grounded = false;
        float speedx = m_Rigidbody2D.velocity.x;
        m_Rigidbody2D.velocity = new Vector2(0, 0);
        m_Rigidbody2D.AddForce(new Vector2(speedx, bounceForce * 0.8f));
        puedomoverme = true;
        mov.unavezDobleSalto = false;
    }
    public void DobleSaltoPlayer()
    {
        source.PlayOneShot(saltarpequeño);
        print("doblesalto");
        //energymanager.SumarEnergia(energymanager.energiaSumadaBouncer);
        //mov.lastdireccionmov = 0;
        puedoSaltar = false;
        dashing = false;
        anim.SetTrigger("Bounce");
        ResetWall();
        m_Grounded = false;
        float speedx = m_Rigidbody2D.velocity.x;
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
        m_Rigidbody2D.AddForce(new Vector2(speedx, fuerzaDobleSalto));
    }
    public void BouncePlayer()
    {

        source.PlayOneShot(rebotar);
        energymanager.SumarEnergia(energymanager.energiaSumadaBouncer);
        puedoSaltar = false;
        dashing = false;
        anim.SetTrigger("Bounce");
        ResetWall();
        m_Grounded = false;
        float speedx = m_Rigidbody2D.velocity.x;
        m_Rigidbody2D.velocity = new Vector2(0, 0);
        m_Rigidbody2D.AddForce(new Vector2(speedx, bounceForce)); mov.unavezDobleSalto = false;
    }
    public void BounceDireccionPlayer(GameObject posChoque, GameObject poscentro)
    {
        source.PlayOneShot(pinball);
        energymanager.SumarEnergia(energymanager.energiaSumadaBouncer);
        puedoSaltar = false;
        dashing = false;
        anim.SetTrigger("Bounce");
        ResetWall();
        m_Grounded = false;
        this.GetComponentInParent<CharacterController2D>().puedomoverme = false;
        this.GetComponent<Movimiento>().cayendoS = false;


        if (impulsoreciente == false)
        {
            impulsohecho = true;

            Vector2 direccionFuerza = (posChoque.transform.localPosition - poscentro.transform.localPosition);
            print(posChoque.name + "  " + poscentro.name + " wnidneinfien" + direccionFuerza.normalized);
            //print(collision.gameObject.transform.GetChild(0).name + collision.gameObject.transform.GetChild(0).transform.localPosition);
            //print("dirf" + direccionFuerza);
            //if (Mathf.Abs(direccionFuerza.normalized.x) > direccionFuerza.normalized.y)
            //{
            this.GetComponent<Movimiento>().multSpeed = 0.4f/*this.GetComponent<Movimiento>().speedMax*0.75f*/;
            dash2 = true;
            dashing = true; CancelInvoke("StopDash");
            Invoke("StopDash", 0.2f);
            //direccionFuerza.y = 0;
            if (direccionFuerza.x > 0.01)
            {
                this.GetComponent<Movimiento>().lastdireccionmov = 1;
            }
            else if (direccionFuerza.x < -0.01)
            {
                this.GetComponent<Movimiento>().lastdireccionmov = -1;
            }
            else
            {
                this.GetComponent<Movimiento>().lastdireccionmov = -0;
            }
            m_Rigidbody2D.velocity = Vector2.zero;
            //direccionFuerza.x *= 1.5f;
            m_Rigidbody2D.AddForce(direccionFuerza.normalized * fuerzaBouncerGrande);

            impulsoreciente = true;
            //}

            //if (Mathf.Abs(direccionFuerza.normalized.y) > Mathf.Abs(direccionFuerza.normalized.x))
            //{
            //    dash2 = false;
            //    dashing = false;
            //    m_Rigidbody2D.velocity = Vector2.zero;
            //    m_Rigidbody2D.AddForce(direccionFuerza.normalized * fuerzaImpulso);

            //    impulsoreciente = true;
            //    //direccionFuerza.x = 0;
            //}

        }
        else
        {


        }
        mov.unavezDobleSalto = false;
        //if (impulsoreciente == false)
        //{
        //    Vector2 direccionFuerza = (this.transform.position - posChoque);


        //    if (Mathf.Abs(direccionFuerza.normalized.x) > direccionFuerza.normalized.y)
        //    {
        //        this.GetComponent<Movimiento>().multSpeed = this.GetComponent<Movimiento>().speedMax;
        //        dash2 = true;
        //        print("hostiespiñloitp");
        //        dashing = true;
        //        Invoke("StopDash", 0.2f);
        //        //direccionFuerza.y = 0;
        //        if (direccionFuerza.x > 0.1)
        //        {
        //            this.GetComponent<Movimiento>().lastdireccionmov = 1;
        //        }
        //        else if (direccionFuerza.x < -0.1)
        //        {
        //            this.GetComponent<Movimiento>().lastdireccionmov = -1;
        //        }
        //        direccionFuerza.x *= 2;
        //        m_Rigidbody2D.velocity = Vector2.zero;
        //        m_Rigidbody2D.AddForce(direccionFuerza.normalized * bounceForce * 1.2f);
        //    }

        //    if (Mathf.Abs(direccionFuerza.normalized.y) > Mathf.Abs(direccionFuerza.normalized.x))
        //    {
        //        this.GetComponent<Movimiento>().multSpeed = this.GetComponent<Movimiento>().speedMax * 0.8f;
        //        //dash2 = true;
        //        //dashing = true;
        //        //Invoke("StopDash", 0.2f);
        //        //direccionFuerza.y = 0;
        //        if (direccionFuerza.x > 0.1)
        //        {
        //            this.GetComponent<Movimiento>().lastdireccionmov = 1;
        //        }
        //        else if (direccionFuerza.x < -0.1)
        //        {
        //            this.GetComponent<Movimiento>().lastdireccionmov = -1;
        //        }
        //        //dash2 = false;
        //        //dashing = false;
        //        //direccionFuerza.x = 0;
        //        direccionFuerza.x *= 1.1f;
        //        m_Rigidbody2D.velocity = Vector2.zero;
        //        m_Rigidbody2D.AddForce(direccionFuerza.normalized * bounceForce * 1.2f);
        //    }



        //}
        //else
        //{


        //}
        ////m_Rigidbody2D.velocity = new Vector2(0, 0);
        ////m_Rigidbody2D.AddForce(new Vector2(speedx, bounceForce));
    }
    public void WallJumpPlayer(string dir)
    {

        if (!m_Grounded)
        {

            if (doneWallJump == false)
            {
                print(dir + "direccion" + lastContact + mov.lastdireccionmov);
                enabledWallJump = false;

                bajandoPared = false;
                pegadoPared = false;

                //this.GetComponent<Movimiento>().cambiadoaire = false;
                //this.GetComponent<Movimiento>().primeraDireccionSalto = 0;
                //this.GetComponent<Rebote>().puedoParry = true;
                m_Rigidbody2D.velocity = new Vector2(0, 0);
                anim.SetTrigger("WallJump");
                if (dir == "r")
                {
                    puedomoverme = false;
                    this.GetComponent<Movimiento>().lastdireccionmov = -1;
                    this.transform.position = new Vector2(this.transform.position.x - 0.04f, this.transform.position.y);
                    this.GetComponent<Movimiento>().multSpeed = 1f;
                    m_Rigidbody2D.AddForce(new Vector2(-fuerzaWallJump * 0.5f, fuerzaWallJump * 0.55f));

                }
                else if (dir == "l")
                {
                    puedomoverme = false;
                    this.GetComponent<Movimiento>().lastdireccionmov = 1;
                    this.transform.position = new Vector2(this.transform.position.x + 0.04f, this.transform.position.y);
                    this.GetComponent<Movimiento>().multSpeed = 1f;
                    m_Rigidbody2D.AddForce(new Vector2(fuerzaWallJump * 0.5f, fuerzaWallJump * 0.55f));

                }
                source.PlayOneShot(saltopared);
                GetComponent<Particulas>().SpawnParticulas(GetComponent<Particulas>().particulasWallJump, new Vector2((m_GroundCheck.transform.position.x - this.GetComponent<Movimiento>().lastdireccionmov), m_GroundCheck.transform.position.y));
                wallJumping = true;
                doneWallJump = true;
                energymanager.SumarEnergia(energymanager.energiaSumadaWallJump);
                CancelInvoke("ResetWallJump");
                Invoke("ResetWallJump", 0.27f);
            }
        }





    }
    private void ResetWallJump()
    {

        this.GetComponent<Movimiento>().multSpeed = this.GetComponent<Movimiento>().speedMax * 0.75f;
        wallJumping = false;
        puedomoverme = true;
        auxunavezPared = true;
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        if (GetComponentInChildren<Canvas>() != null)
        {
            if (!m_FacingRight)
            {
                GetComponentInChildren<Canvas>().transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (m_FacingRight)
            {
                GetComponentInChildren<Canvas>().transform.localScale = new Vector3(1, 1, 1);
            }

        }


    }
    public void ResetWall()
    {
        auxunavezPared = true;
        bajandoPared = false;
        enabledWallJump = false;
        doneWallJump = false;
        lastWall = null;
        lastWall2 = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Moneda")
        {

            //GameManager.Instance.CogerMonedas();
            Destroy(collision.gameObject);
            source.PlayOneShot(cogerColeccionable);
        }
        if (collision.gameObject.tag == "Habilidad")
        {
            source.PlayOneShot(cogerColeccionable);
            Destroy(collision.gameObject);
            //GameManager.Instance.CogerColeccionable();
            if (collision.gameObject.name == "Moneda1")
            {
                GameManager.Instance.tempMoneda1 = true;
            }
            else if (collision.gameObject.name == "Moneda2")
            {
                GameManager.Instance.tempMoneda2 = true;
            }
            else if (collision.gameObject.name == "Moneda3")
            {
                GameManager.Instance.tempMoneda3 = true;
            }


        }
    }

    void ResetImpulso()
    {
        impulsoreciente = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Normal = collision.contacts[0].normal;
        }


        if (collision.gameObject.tag == "Impulsador")
        {
            source.PlayOneShot(impulsadorsonido);
            this.GetComponentInParent<CharacterController2D>().puedomoverme = true;
            this.GetComponent<Movimiento>().cayendoS = false;


            if (impulsoreciente == false)
            {
                impulsohecho = true;
                this.transform.position = collision.gameObject.transform.GetChild(0).transform.position;
                Vector2 direccionFuerza = (collision.gameObject.transform.GetChild(0).transform.position - collision.gameObject.transform.position);
                //print(collision.gameObject.transform.GetChild(0).name + collision.gameObject.transform.GetChild(0).transform.localPosition);
                //print("dirf" + direccionFuerza);
                //if (Mathf.Abs(direccionFuerza.normalized.x) > direccionFuerza.normalized.y)
                //{
                this.GetComponent<Movimiento>().multSpeed = 0.4f/*this.GetComponent<Movimiento>().speedMax*0.75f*/;
                dash2 = true;
                dashing = true; CancelInvoke("StopDash");
                Invoke("StopDash", 0.4f);
                //direccionFuerza.y = 0;
                if (direccionFuerza.x > 0.01)
                {
                    this.GetComponent<Movimiento>().lastdireccionmov = 1;
                }
                else if (direccionFuerza.x < -0.01)
                {
                    this.GetComponent<Movimiento>().lastdireccionmov = -1;
                }
                m_Rigidbody2D.velocity = Vector2.zero;
                //direccionFuerza.x *= 1.5f;
                m_Rigidbody2D.AddForce(direccionFuerza.normalized * fuerzaImpulso);

                impulsoreciente = true;
                //}

                //if (Mathf.Abs(direccionFuerza.normalized.y) > Mathf.Abs(direccionFuerza.normalized.x))
                //{
                //    dash2 = false;
                //    dashing = false;
                //    m_Rigidbody2D.velocity = Vector2.zero;
                //    m_Rigidbody2D.AddForce(direccionFuerza.normalized * fuerzaImpulso);

                //    impulsoreciente = true;
                //    //direccionFuerza.x = 0;
                //}

            }
            else
            {


            }

            mov.unavezDobleSalto = false;

        }

        if (collision.gameObject.tag == "BouncerDireccion")
        {
            if (impulsoreciente == false)
            {

                if (impulsoreciente == false)
                {


                    this.GetComponentInParent<CharacterController2D>().puedomoverme = true;
                    this.GetComponentInParent<Movimiento>().cayendoS = false;
                    //GameObject.FindObjectOfType<DetectorRebote>().recentbounce = true;
                    //Invoke("ResetBounce", 0.5f);
                    GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasBounce, collision.gameObject.transform.position);
                    GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasBounce, collision.gameObject.transform.position);
                    anim.SetTrigger("Bounce");
                    float dist = 88888f;

                    foreach (Transform transf in (collision.gameObject.transform.GetComponentsInChildren<Transform>()))
                    {

                        if (Vector3.Distance(collision.contacts[0].point, transf.position) < dist)
                        {
                            dist = Vector3.Distance(collision.contacts[0].point, transf.position);

                            posfinal = transf.gameObject;
                        }

                    }

                    BounceDireccionPlayer(posfinal.gameObject, collision.gameObject.transform.GetChild(0).gameObject);
                    posfinal = null;
                    this.GetComponent<Rebote>().puedoParry = true;
                }
            }
        }

        if (collision.gameObject.tag == "Pared")
        {
            this.GetComponent<Rebote>().puedoParry = true;
            if (this.GetComponent<Movimiento>().multSpeed != 0) anim.SetBool("PegadoPared", true);
            this.GetComponent<Movimiento>().multSpeed = 0;
            if (m_Grounded == false)
            {


                if (m_Rigidbody2D.velocity.y <= -0.8f)
                {
                    m_Rigidbody2D.velocity = Vector2.zero;

                    pegadoPared = true;

                    enabledWallJump = true;
                    //if (collision.gameObject != lastWall)
                    doneWallJump = false;
                }
                //if (m_Grounded == false)
                //{
                if (collision.GetContact(0).point.x > this.transform.position.x)
                {

                    lastContact = "r";

                }
                else if (collision.GetContact(0).point.x < this.transform.position.x)
                {

                    lastContact = "l";

                }
                //if (collision.gameObject != lastWall)
                //{
                lastWall = collision.gameObject;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    pegadoPared = false;

                    if (collision.GetContact(0).point.x > this.transform.position.x)
                    {

                        WallJumpPlayer("r");
                        enabledWallJump = false;
                    }
                    else if (collision.GetContact(0).point.x < this.transform.position.x)
                    {

                        WallJumpPlayer("l");
                        enabledWallJump = false;
                    }

                }
                //}

                //}
            }
            mov.unavezDobleSalto = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BouncerDireccion")
        {
            if (impulsoreciente == false)
            {


                this.GetComponentInParent<CharacterController2D>().puedomoverme = true;
                this.GetComponentInParent<Movimiento>().cayendoS = false;
                //GameObject.FindObjectOfType<DetectorRebote>().recentbounce = true;
                //Invoke("ResetBounce", 0.5f);
                GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasBounce, collision.gameObject.transform.position);
                GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasBounce, collision.gameObject.transform.position);
                anim.SetTrigger("Bounce");
                float dist = 88888f;

                foreach (Transform transf in (collision.gameObject.transform.GetComponentsInChildren<Transform>()))
                {
                    print("Distancia:" + Vector3.Distance(collision.contacts[0].point, transf.position) + "transf name" + transf.name);
                    if (Vector3.Distance(collision.contacts[0].point, transf.position) < dist)
                    {
                        print("ok");
                        posfinal = transf.gameObject;
                    }
                }

                BounceDireccionPlayer(posfinal.gameObject, collision.gameObject.transform.GetChild(0).gameObject);
                posfinal = null;
                this.GetComponent<Rebote>().puedoParry = true;
            }
        }


        if (collision.gameObject.tag == "Pared")
        {

            if (primeravezpared == false)
            {
                if (m_Rigidbody2D.velocity.y <= -1f)
                {
                    primeravezpared = true;
                }
            }

            else if (primeravezpared == true)
            {


                if ((m_Grounded == false) && (this.GetComponent<Movimiento>().primeraDireccionSalto != 0))
                {
                    anim.SetBool("PegadoPared", true);

                    this.GetComponent<Movimiento>().multSpeed = 0.05f;
                    puedomoverme = true;
                    if (m_Rigidbody2D.velocity.y <= -0.2f)
                    {
                        m_Rigidbody2D.velocity = Vector2.zero;

                        pegadoPared = true;

                        enabledWallJump = true;
                        //if (collision.gameObject != lastWall)
                        doneWallJump = false;
                    }


                    if (bajandoPared != true)
                    {
                        if (Input.GetAxisRaw("Vertical") == 0)
                        {
                            m_Rigidbody2D.velocity = new Vector2(0, 0);
                        }
                        else
                        if (Input.GetAxisRaw("Vertical") != 0)
                        {
                            m_Rigidbody2D.velocity = new Vector2(0, Input.GetAxisRaw("Vertical") * velocidadSubidaPared);
                        }


                    }
                    else
                    {

                        if (auxunavezPared == false)
                        {

                            auxunavezPared = true;
                            auxtiempoCaer = tiempoantesCaer;
                            bajandoPared = false;
                        }

                    }




                    //if (m_Grounded == false)
                    //{
                    if (collision.GetContact(0).point.x > this.transform.position.x)
                    {

                        lastContact = "r";

                    }
                    else if (collision.GetContact(0).point.x < this.transform.position.x)
                    {
                        lastContact = "l";

                    }
                    //if (collision.gameObject != lastWall)
                    //{
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        puedoSaltar = false;
                        lastWall = collision.gameObject;
                        pegadoPared = false;

                        if (collision.GetContact(0).point.x > this.transform.position.x)
                        {

                            WallJumpPlayer("r");
                            enabledWallJump = false;
                        }
                        else if (collision.GetContact(0).point.x < this.transform.position.x)
                        {

                            WallJumpPlayer("l");
                            enabledWallJump = false;
                        }
                    }
                    //}

                    //}
                }
            }
            mov.unavezDobleSalto = false;
        }


    }
    IEnumerator DisableWallJump(float time)
    {
        yield return new WaitForSeconds(time);
        enabledWallJump = false;
        pegadoPared = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pared")
        {
            primeravezpared = false;
            if ((m_Grounded == false) && (this.GetComponent<Movimiento>().primeraDireccionSalto != 0))
            {

                this.GetComponent<Rebote>().puedoParry = true;
                bajandoPared = false;
                enabledWallJump = false;
                auxunavezPared = false;
                auxtiempoCaer = tiempoantesCaer;
                pegadoPared = false;
                //if (enabledWallJump == true)
                //{
                //    StartCoroutine(DisableWallJump(0.20f));
                //}
                //else
                //{

                //} 
            }


        }

    }

}
