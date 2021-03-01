using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraZoom : MonoBehaviour
{
    string escenaActual;
    public CinemachineVirtualCamera cinemakina;
    public float startsize = 13;
    public float finalsize = 19;
    public float auxstartsize;
    public float auxfinalsize;
    public float tamañoCamaraPausa = 3f;
    ControllerPersonaje cc;
    public GameObject segundorb;
    public float indiceMultiplicador = 0.05f;
    public float indiceMultiplicadorPausa = 0.05f;
    float tiempoSinInput;
    public GameObject hud;
    float indiceHUD;
    public GameObject targetGroup;
    public float maxZoom;
    public float minZoom;
    public float maxDistance;
    public float maxPaneoHorizontalBase = 10;
    public float maxPaneoHorizontal = 10;
    public float maxPaneoHorizontalConVariosTargets = 0f;
    public float maxPaneoVertical = 5;
    public float velocidadPaneoHorizontalBase = 10;
    public float velocidadPaneoHorizontal = 10;
    public float velocidadPaneoHorizVariosTargets = 0f;
    public float velocidadPaneoVertical = 5;
    public float indiceMultiplicadorZoom = 0.2f;
    public bool soloplayer;
    public bool pausado = false;
    GameObject pausaZoom;
    GameObject player;
    public bool limitarDistancia = false;
    public GameObject ceboCamara;
    public float distanciaCebo = 15;
    public float distanciaCeboNosolo = 45;
    Vector3 targetSpeed;

    // Start is called before the first frame update
    void Start()
    {
        maxPaneoHorizontalConVariosTargets = maxPaneoHorizontalBase * 2;
        velocidadPaneoHorizVariosTargets = 2 * velocidadPaneoHorizontalBase;
        pausaZoom = GameObject.Find("ZoomPausa");
        auxfinalsize = finalsize;
        auxstartsize = startsize;
        tiempoSinInput = 0;
        cinemakina = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        cc = GameObject.FindObjectOfType<ControllerPersonaje>();
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
        escenaActual = SceneManager.GetActiveScene().name;
        if (GameObject.Find("ceboCamara") != null)
        {
            ceboCamara = GameObject.Find("ceboCamara");
            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target = player.transform;
            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[1].target = ceboCamara.transform;
            ceboCamara.transform.parent = null;
        }

    }
    float DistanciaMaxima()
    {
        var bounds = new Bounds(targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target.position, Vector3.zero);

        for (int i = 0; i < (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets.Length); i++)
        {
            if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target != null) bounds.Encapsulate(targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target.position);
        }
        if (bounds.size.x > bounds.size.y)
        {
            return bounds.size.x;
        }
        else
        {
            return bounds.size.y;
        }

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        targetSpeed = new Vector2(cc.rb.velocity.x, cc.rb.velocity.y/* * 0.5f*/).normalized;
    }
    void LateUpdate()
    {
        //if (soloplayer == true)
        //{

        if (cc.auxCdDash > 0.5f)
        {
            if (Mathf.Abs(cc.rb.velocity.x) > 30f)
            {
                ceboCamara.transform.localPosition = Vector2.MoveTowards(ceboCamara.transform.position, player.transform.position + distanciaCebo * targetSpeed, Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * 2f * Time.deltaTime, 50f * Time.deltaTime, 90f * Time.deltaTime));

            }
            else if (Mathf.Abs(cc.rb.velocity.x) > 5f)
            {
                ceboCamara.transform.localPosition = Vector2.MoveTowards(ceboCamara.transform.position, player.transform.position + distanciaCebo * targetSpeed, Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) *2f * Time.deltaTime, 50f*Time.deltaTime, 90f* Time.deltaTime));

            }
            else
            {
                ceboCamara.transform.localPosition = Vector2.MoveTowards(ceboCamara.transform.position, player.transform.position, 30f * Time.deltaTime);

            }
        }
        else
        {
            if (Mathf.Abs(cc.rb.velocity.x) > 30f)
            {
                ceboCamara.transform.localPosition = Vector2.MoveTowards(ceboCamara.transform.position, player.transform.position + distanciaCebo * targetSpeed, Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * 2f * Time.deltaTime, 50f * Time.deltaTime, 90f * Time.deltaTime));

            }
            else if(Mathf.Abs(cc.rb.velocity.x) > 5f)
            {
               
                ceboCamara.transform.localPosition = Vector2.MoveTowards(ceboCamara.transform.position, player.transform.position + distanciaCebo * targetSpeed, Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * 2f * Time.deltaTime, 50f * Time.deltaTime, 90f * Time.deltaTime));

            }
            else
            {
                ceboCamara.transform.localPosition = Vector2.MoveTowards(ceboCamara.transform.position, player.transform.position, 30f * Time.deltaTime);

            }
        }

        //}
        //else
        //{
        //    if (cc.auxCdDash > 0.5f)
        //    {
        //        if (cc.rb.velocity.magnitude > 3.5f)
        //        {
        //            ceboCamara.transform.localPosition = Vector2.MoveTowards(ceboCamara.transform.position, player.transform.position+distanciaCeboNosolo * targetSpeed, cc.rb.velocity.magnitude * 3f * Time.deltaTime);
        //        }
        //        else
        //        {
        //            ceboCamara.transform.localPosition = Vector2.MoveTowards(ceboCamara.transform.position, player.transform.position, 25 * Time.deltaTime);
        //        }
        //    }
        //    else
        //    {
        //        if (cc.rb.velocity.magnitude > 3.5f)
        //        {
        //            ceboCamara.transform.localPosition = Vector2.MoveTowards(ceboCamara.transform.position, player.transform.position + distanciaCeboNosolo * targetSpeed, cc.rb.velocity.magnitude * 3f * Time.deltaTime);
        //        }
        //        else
        //        {
        //            ceboCamara.transform.localPosition = Vector2.MoveTowards(ceboCamara.transform.position, player.transform.position , 25 * Time.deltaTime);
        //        }
        //    }



        //}
        ////if (GameManager.Instance.personajevivo == true)
        //{

        for (int i = 0; i < targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets.Length; i++)
        {
            if (i == 0)
            {
                if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target == this.gameObject.transform)
                {
                    soloplayer = true;
                    //GameObject.Find("Cubito").GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    soloplayer = false;
                    //GameObject.Find("Cubito").GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else if (i == 1)
            {
                if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[1].target == ceboCamara.gameObject.transform)
                {
                    soloplayer = true;
                    //GameObject.Find("Cubito").GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    soloplayer = false;
                    //GameObject.Find("Cubito").GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target != null)
                {
                    soloplayer = false;
                    //GameObject.Find("Cubito").GetComponent<SpriteRenderer>().enabled = true;

                }
            }
        }
        if (GameManager.Instance.GetComponent<MenuPausa>() != null)
        {
            if (GameManager.Instance != null && GameManager.Instance.GetComponent<MenuPausa>().paused)
            {
                cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 0;
                cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = 0;


                startsize = tamañoCamaraPausa;
                for (int i = 0; i < targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets.Length; i++)
                {
                    if (i == 0)
                    {
                        if (pausaZoom != null)
                        {
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target = pausaZoom.transform;
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].weight = 12;
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].radius = 10;
                        }
                    }
                    //else if (i == 1)
                    // {
                    //     if (pausaZoom != null)
                    //     {
                    //         targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target = null;
                    //         targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].weight = 12;
                    //         targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].radius = 10;
                    //     }
                    // }
                    else
                    {
                        if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target != null)
                        {
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target = null;

                        }
                    }
                }
                if (cinemakina.m_Lens.OrthographicSize > startsize)
                {

                    pausado = true;
                    if (cinemakina.m_Lens.OrthographicSize <= startsize)
                    {
                        cinemakina.m_Lens.OrthographicSize = startsize;

                    }
                    else
                    {
                        cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize - indiceMultiplicadorPausa * Time.deltaTime;
                    }
                }
            }
            else if (cc.haciendoCombate == false)
            {
                if (soloplayer == true)
                {
                    if (escenaActual != "ND-3")
                    {
                        if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target != null)
                        {
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].weight = 15;
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].radius = 15;
                        }
                        if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[1].target != null)
                        {
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[1].weight = 25;
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[1].radius = 10;
                        }
                    }
                    else
                    {
                        if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target != null)
                        {
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].weight = 1;
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].radius = 1;
                        }
                        if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[1].target != null)
                        {
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[1].weight = 1;
                            targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[1].radius = 1;
                        }
                    }
                    startsize = auxstartsize;
                    if (cinemakina.m_Lens.OrthographicSize <= startsize && pausado == true)
                    {
                        cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + indiceMultiplicadorPausa * Time.deltaTime;
                        if (cinemakina.m_Lens.OrthographicSize >= startsize && pausado == true)
                        {
                            cinemakina.m_Lens.OrthographicSize = startsize;
                            pausado = false;
                        }
                    }

                    if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_XDamping >= 0.2f) cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_XDamping -= 1 * Time.deltaTime;

                    if (this.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f)
                    {
                        if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_XDamping < 1f)
                        {
                            cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 1f;
                        }
                    }
                    if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_YDamping >= 1f) cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_YDamping -= 1 * Time.deltaTime;
                    if (this.GetComponent<ControllerPersonaje>().dashEnCaida == true)
                    {
                        if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_YDamping < 1.5f)
                        {
                            cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 1.5f;
                        }
                    }
                    if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping >= 0.2f) cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping -= 1 * Time.deltaTime;
                    if (Mathf.Abs(cc.rb.velocity.x) > 7)
                    {
                        tiempoSinInput = 0;
                        if (cinemakina.m_Lens.OrthographicSize < finalsize)
                        {
                            cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * Mathf.Clamp(cc.rb.velocity.x, 1, 1.3f) * Time.deltaTime, 0, 7);

                        }

                    }

                    else if (Mathf.Abs(cc.rb.velocity.x) > 12)
                    {
                        tiempoSinInput = 0;
                        if (cinemakina.m_Lens.OrthographicSize < finalsize)
                        {
                            cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * Mathf.Clamp(cc.rb.velocity.x, 1, 2f) * Time.deltaTime, 0, 7);
                        }

                    }
                    else if (Mathf.Abs(cc.rb.velocity.x) > 25)
                    {
                        tiempoSinInput = 0;
                        if (cinemakina.m_Lens.OrthographicSize < finalsize)
                        {
                            cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * Mathf.Clamp(cc.rb.velocity.x, 1.5f, 2.2f) * Time.deltaTime, 0, 7);
                        }

                    }
                    else if (Mathf.Abs(cc.rb.velocity.y) > 23f)
                    {
                        tiempoSinInput = 0;
                        if (cinemakina.m_Lens.OrthographicSize < finalsize)
                        {
                            cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 0.6f * Time.deltaTime, 0, 7);
                        }

                    }

                    else if (Mathf.Abs(cc.rb.velocity.y) > 16f)
                    {
                        tiempoSinInput = 0;
                        if (cinemakina.m_Lens.OrthographicSize < finalsize)
                        {
                            cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 0.3f * Time.deltaTime, 0, 7);
                        }

                    }
                    else if (Mathf.Abs(cc.rb.velocity.y) > 8f)
                    {
                        tiempoSinInput = 0;
                        if (cinemakina.m_Lens.OrthographicSize < finalsize)
                        {
                            cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 0.15f * Time.deltaTime, 0, 7);
                        }

                    }

                    else
                    {
                        if ((cinemakina.m_Lens.OrthographicSize > startsize) && (Mathf.Abs(cc.rb.velocity.y) < 8f) && (Mathf.Abs(cc.rb.velocity.y) > 0.0f))
                        {
                            tiempoSinInput += Time.deltaTime * 0.5f;
                            cinemakina.m_Lens.OrthographicSize -= (2f * Time.deltaTime + tiempoSinInput);
                        }
                        else
                        {
                            if (cinemakina.m_Lens.OrthographicSize > startsize)
                            {
                                tiempoSinInput += Time.deltaTime * 0.5f;
                                cinemakina.m_Lens.OrthographicSize -= (3 * Time.deltaTime + tiempoSinInput);
                            }
                        }
                    }
                    if (cinemakina.m_Lens.OrthographicSize > finalsize + 4)
                    {
                        cinemakina.m_Lens.OrthographicSize -= (Time.deltaTime * 6);
                    }

                    //if (this.GetComponent<PlayerInput>().inputHorizontal != 0 && this.GetComponent<ControllerPersonaje>().ultimaNormal.y > 0.7f)
                    //{
                    //    if (soloplayer)
                    //    {
                    //        maxPaneoHorizontalBase = maxPaneoHorizontal;
                    //        velocidadPaneoHorizontalBase = velocidadPaneoHorizontal;
                    //    }
                    //    else
                    //    {
                    //        velocidadPaneoHorizontalBase = velocidadPaneoHorizVariosTargets;
                    //        //maxPaneoHorizontalBase = maxPaneoHorizontalConVariosTargets;
                    //    }
                    //    if (Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x) > 15)
                    //    {


                    //        if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) < maxPaneoHorizontalBase + 2)
                    //        {


                    //            if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) < maxPaneoHorizontalBase)
                    //            {

                    //                if (Mathf.Sign(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) != this.GetComponent<PlayerInput>().inputHorizontal)
                    //                {

                    //                    cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x += this.GetComponent<PlayerInput>().inputHorizontal * velocidadPaneoHorizontalBase * 2 * Mathf.Clamp(Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x), 0.8f, 1.5f) * Time.deltaTime;

                    //                }
                    //                else
                    //                {

                    //                    cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x += this.GetComponent<PlayerInput>().inputHorizontal * velocidadPaneoHorizontalBase * Mathf.Clamp(Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x), 0.8f, 1.5f) * Time.deltaTime;

                    //                }
                    //            }
                    //            else if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) >= maxPaneoHorizontalBase && Mathf.Sign(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) == Mathf.Sign(this.GetComponent<PlayerInput>().inputHorizontal))
                    //            {
                    //                if (this.GetComponent<PlayerInput>().ultimoInputHorizontal != 0)
                    //                {

                    //                    if (Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x) > 7)
                    //                    {

                    //                        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = maxPaneoHorizontalBase * Mathf.Sign(this.GetComponent<PlayerInput>().ultimoInputHorizontal);
                    //                    }
                    //                }
                    //                //if (this.GetComponent<PlayerInput>().ultimoInputHorizontal > 0)
                    //                //{
                    //                //    if (this.GetComponent<ControllerPersonaje>().rb.velocity.x > 7)
                    //                //    {
                    //                //        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = maxPaneoHorizontal;
                    //                //    }


                    //                //}
                    //                //else if (this.GetComponent<PlayerInput>().ultimoInputHorizontal < 0)
                    //                //{
                    //                //    if (this.GetComponent<ControllerPersonaje>().rb.velocity.x < -7)
                    //                //    {
                    //                //        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = -maxPaneoHorizontal;
                    //                //    }


                    //                //}

                    //            }
                    //            else if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) >= maxPaneoHorizontalBase)
                    //            {
                    //                if (this.GetComponent<PlayerInput>().ultimoInputHorizontal > 0)
                    //                {


                    //                    cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x += velocidadPaneoHorizontalBase * Mathf.Clamp(Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x), 0.8f, 1.5f) * Time.deltaTime;


                    //                }
                    //                else if (this.GetComponent<PlayerInput>().ultimoInputHorizontal < 0)
                    //                {

                    //                    cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x -= velocidadPaneoHorizontalBase * Mathf.Clamp(Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x), 0.8f, 1.5f) * Time.deltaTime;


                    //                }

                    //            }



                    //            //cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x += this.GetComponent<PlayerInput>().inputHorizontal * velocidadPaneoHorizontal * Time.deltaTime;
                    //        }
                    //        else
                    //        {

                    //            //cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = maxPaneoHorizontal * Mathf.Sign(this.GetComponent<ControllerPersonaje>().rb.velocity.x);
                    //        }

                    //    }
                    //    else
                    //    {

                    //    }
                    //}
                    //else
                    //{
                    //    //if (this.GetComponent<ControllerPersonaje>().ultimaNormal.y > 0.7f)
                    //    //{

                    //    if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) > 0.15f)
                    //    {
                    //        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x -= velocidadPaneoHorizontalBase * Time.deltaTime * Mathf.Sign(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x);

                    //    }
                    //    else
                    //    {
                    //        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = 0;
                    //    }

                    //}
                    //if (this.GetComponent<ControllerPersonaje>().rb.velocity.y < -5 && this.GetComponent<ControllerPersonaje>().grounded == false)
                    //{

                    //    if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y) < maxPaneoVertical)
                    //    {
                    //        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y -= (velocidadPaneoVertical * Mathf.Clamp(Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.y), 0.1f, 3)) * Time.deltaTime;
                    //    }
                    //    else
                    //    {
                    //        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = -maxPaneoVertical;
                    //    }


                    //}
                    //else
                    //{
                    //    if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y) > 0.3f)
                    //    {
                    //        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y -= velocidadPaneoVertical * 2 * Time.deltaTime * Mathf.Sign(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y);
                    //    }
                    //    else
                    //    {
                    //        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 0;
                    //    }

                    //}

                }
                else
                {
                    cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.MoveTowards(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, new Vector3(0, 0, cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z), 5 * Time.deltaTime);
                    if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_XDamping <= 3f) cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_XDamping += 5 * Time.deltaTime;
                    if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_YDamping <= 3f) cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_YDamping += 5 * Time.deltaTime;
                    if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping <= 1f) cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping += 5 * Time.deltaTime;
                    if (limitarDistancia)
                    {

                    }
                    else
                    {
                        maxDistance = DistanciaMaxima();
                    }

                    if (cinemakina.m_Lens.OrthographicSize < maxDistance * 0.6f && cinemakina.m_Lens.OrthographicSize < maxZoom)
                    {
                        cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + indiceMultiplicadorZoom * Time.deltaTime;
                    }
                    else if (cinemakina.m_Lens.OrthographicSize > maxDistance * 1f && cinemakina.m_Lens.OrthographicSize > startsize)
                    {
                        cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize - indiceMultiplicadorZoom * 1.7f * Time.deltaTime;
                    }
                }
            }
            //      }
            //}

            //      else
            //      {
            //          if (cinemakina.m_Lens.OrthographicSize > 2)
            //          {
            //cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize -= Time.deltaTime * 10;
            //          }

            //      }
        }
    }
}
