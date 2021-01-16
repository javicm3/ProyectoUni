using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    CinemachineVirtualCamera cinemakina;
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
    public float maxPaneoHorizontal = 10;
    public float maxPaneoVertical = 5;
    public float velocidadPaneoHorizontal = 10;
    public float velocidadPaneoVertical = 5;
    public float indiceMultiplicadorZoom = 0.2f;
    public bool soloplayer;
    bool pausado = false;
    public GameObject pausaZoom;
    GameObject player;
    public bool limitarDistancia = false;
    // Start is called before the first frame update
    void Start()
    {
        auxfinalsize = finalsize;
        auxstartsize = startsize;
        tiempoSinInput = 0;
        cinemakina = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        cc = GameObject.FindObjectOfType<ControllerPersonaje>();
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
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
    void Update()
    {
        //if (GameManager.Instance.personajevivo == true)
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

                startsize = tamañoCamaraPausa;
                for (int i = 0; i < targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets.Length; i++)
                {
                    if (i == 0) { targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target = pausaZoom.transform; }
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
                    cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize - indiceMultiplicadorPausa * Time.deltaTime;
                    if (cinemakina.m_Lens.OrthographicSize <= startsize)
                    {
                        cinemakina.m_Lens.OrthographicSize = startsize;
                    }
                }
            }
            else if (cc.haciendoCombate == false)
            {
                if (soloplayer == true)
                {
                    if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target != null) { targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].weight = 12;
                        targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].radius = 10;
                    }

                    startsize = auxstartsize;
                    if (cinemakina.m_Lens.OrthographicSize < startsize && pausado == true)
                    {
                        cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + indiceMultiplicadorPausa * Time.deltaTime;
                        if (cinemakina.m_Lens.OrthographicSize >= startsize && pausado == true)
                        {
                            cinemakina.m_Lens.OrthographicSize = startsize;
                            pausado = false;
                        }
                    }
                    if (this.GetComponent<PlayerInput>().inputHorizontal != 0 && this.GetComponent<ControllerPersonaje>().ultimaNormal.y > 0.7f)
                    {

                        if (Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x) > 15)
                        {


                            if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) < maxPaneoHorizontal + 2)
                            {


                                if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) < maxPaneoHorizontal)
                                {

                                    if (Mathf.Sign(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) != this.GetComponent<PlayerInput>().inputHorizontal)
                                    {

                                        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x += this.GetComponent<PlayerInput>().inputHorizontal * velocidadPaneoHorizontal * 2 * Mathf.Clamp(Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x), 0.8f, 1.5f) * Time.deltaTime;

                                    }
                                    else
                                    {

                                        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x += this.GetComponent<PlayerInput>().inputHorizontal * velocidadPaneoHorizontal * Mathf.Clamp(Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x), 0.8f, 1.5f) * Time.deltaTime;

                                    }
                                }
                                else if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) >= maxPaneoHorizontal && Mathf.Sign(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) == Mathf.Sign(this.GetComponent<PlayerInput>().inputHorizontal))
                                {
                                    if (this.GetComponent<PlayerInput>().ultimoInputHorizontal != 0)
                                    {

                                        if (Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x) > 7)
                                        {

                                            cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = maxPaneoHorizontal * Mathf.Sign(this.GetComponent<PlayerInput>().ultimoInputHorizontal);
                                        }
                                    }
                                    //if (this.GetComponent<PlayerInput>().ultimoInputHorizontal > 0)
                                    //{
                                    //    if (this.GetComponent<ControllerPersonaje>().rb.velocity.x > 7)
                                    //    {
                                    //        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = maxPaneoHorizontal;
                                    //    }


                                    //}
                                    //else if (this.GetComponent<PlayerInput>().ultimoInputHorizontal < 0)
                                    //{
                                    //    if (this.GetComponent<ControllerPersonaje>().rb.velocity.x < -7)
                                    //    {
                                    //        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = -maxPaneoHorizontal;
                                    //    }


                                    //}

                                }
                                else if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) >= maxPaneoHorizontal)
                                {
                                    if (this.GetComponent<PlayerInput>().ultimoInputHorizontal > 0)
                                    {


                                        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x += velocidadPaneoHorizontal * Mathf.Clamp(Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x), 0.8f, 1.5f) * Time.deltaTime;


                                    }
                                    else if (this.GetComponent<PlayerInput>().ultimoInputHorizontal < 0)
                                    {

                                        cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x -= velocidadPaneoHorizontal * Mathf.Clamp(Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.x), 0.8f, 1.5f) * Time.deltaTime;


                                    }

                                }



                                //cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x += this.GetComponent<PlayerInput>().inputHorizontal * velocidadPaneoHorizontal * Time.deltaTime;
                            }
                            else
                            {

                                //cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = maxPaneoHorizontal * Mathf.Sign(this.GetComponent<ControllerPersonaje>().rb.velocity.x);
                            }

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        //if (this.GetComponent<ControllerPersonaje>().ultimaNormal.y > 0.7f)
                        //{

                        if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x) > 0.3f)
                        {
                            cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x -= velocidadPaneoHorizontal * Time.deltaTime * Mathf.Sign(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x);

                        }
                        else
                        {
                            cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = 0;
                        }

                    }
                    if (this.GetComponent<ControllerPersonaje>().rb.velocity.y < -5 && this.GetComponent<ControllerPersonaje>().grounded == false)
                    {

                        if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y) < maxPaneoVertical)
                        {
                            cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y -= (velocidadPaneoVertical * Mathf.Clamp(Mathf.Abs(this.GetComponent<ControllerPersonaje>().rb.velocity.y), 0.8f, 3)) * Time.deltaTime;
                        }
                        else
                        {
                            cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = -maxPaneoVertical;
                        }


                    }
                    else
                    {
                        if (Mathf.Abs(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y) > 0.3f)
                        {
                            cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y -= velocidadPaneoHorizontal * 2 * Time.deltaTime * Mathf.Sign(cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y);
                        }
                        else
                        {
                            cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 0;
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
                            cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 1f * Time.deltaTime, 0, 7);
                        }

                    }

                    else if (Mathf.Abs(cc.rb.velocity.y) > 16f)
                    {
                        tiempoSinInput = 0;
                        if (cinemakina.m_Lens.OrthographicSize < finalsize)
                        {
                            cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 0.4f * Time.deltaTime, 0, 7);
                        }

                    }
                    else if (Mathf.Abs(cc.rb.velocity.y) > 8f)
                    {
                        tiempoSinInput = 0;
                        if (cinemakina.m_Lens.OrthographicSize < finalsize)
                        {
                            cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize + Mathf.Clamp(Mathf.Abs(cc.rb.velocity.y) * indiceMultiplicador * 0.2f * Time.deltaTime, 0, 7);
                        }

                    }

                    else
                    {
                        if ((cinemakina.m_Lens.OrthographicSize > startsize) && (Mathf.Abs(cc.rb.velocity.y) < 8f) && (Mathf.Abs(cc.rb.velocity.y) > 0.0f))
                        {
                            tiempoSinInput += Time.deltaTime * 0.5f;
                            cinemakina.m_Lens.OrthographicSize -= (Time.deltaTime + tiempoSinInput);
                        }
                        else
                        {
                            if (cinemakina.m_Lens.OrthographicSize > startsize)
                            {
                                tiempoSinInput += Time.deltaTime * 0.5f;
                                cinemakina.m_Lens.OrthographicSize -= (Time.deltaTime + tiempoSinInput);
                            }
                        }
                    }
                    if (cinemakina.m_Lens.OrthographicSize > finalsize + 4)
                    {
                        cinemakina.m_Lens.OrthographicSize -= (Time.deltaTime * 5);
                    }
                }
                else
                {
                    if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_XDamping <= 3f) cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_XDamping += 10 * Time.deltaTime;
                    if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_YDamping <= 3f) cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_YDamping += 10 * Time.deltaTime;
                    if (cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping <= 3f) cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping += 10 * Time.deltaTime;
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
                    else if(cinemakina.m_Lens.OrthographicSize > maxDistance * 0.8f)
                    {
                        cinemakina.m_Lens.OrthographicSize = cinemakina.m_Lens.OrthographicSize - indiceMultiplicadorZoom * Time.deltaTime;
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
