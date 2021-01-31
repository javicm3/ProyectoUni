using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    Animator anim;
    Animator playerAnim;
    GameObject[] ptosPausa;
    public bool paused;
    ControllerPersonaje controllerAndInput;
    PlayerInput pi;
    float gravedadNormal;
    GameObject targetGroup;
    GameObject player;
    public Vector3 offset;
    // Start is called before the first frame update
    void Awake()
    {


    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name!= "PantallaInicio")
        {
            menuPausa = GameObject.Find("MenuPausa");
        controllerAndInput = GameObject.Find("Player").GetComponent<ControllerPersonaje>();
        gravedadNormal = controllerAndInput.gameObject.GetComponent<Rigidbody2D>().gravityScale;
        controllerAndInput = controllerAndInput.gameObject.GetComponent<ControllerPersonaje>();
        playerAnim = controllerAndInput.gameObject.GetComponentInChildren<Animator>();
        pi = controllerAndInput.gameObject.GetComponent<PlayerInput>();
        if (menuPausa != null)
        {
            menuPausa.SetActive(false);
            anim = menuPausa.GetComponent<Animator>();
        }
        paused = false;
        ptosPausa = GameObject.FindGameObjectsWithTag("Pausa");
        for (int i = 0; i < ptosPausa.Length; i++)
        {
            Destroy(ptosPausa[i]);
        }
        targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>().gameObject;
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
        }
    }
    public void InicioScript()
    {
        menuPausa = GameObject.Find("MenuPausa");
        controllerAndInput = GameObject.Find("Player").GetComponent<ControllerPersonaje>();
        gravedadNormal = controllerAndInput.gameObject.GetComponent<Rigidbody2D>().gravityScale;
        controllerAndInput = controllerAndInput.gameObject.GetComponent<ControllerPersonaje>();
        playerAnim = controllerAndInput.gameObject.GetComponentInChildren<Animator>();
        pi = controllerAndInput.gameObject.GetComponent<PlayerInput>();
        if (menuPausa != null)
        {
            menuPausa.SetActive(false);
            anim = menuPausa.GetComponent<Animator>();
        }
        paused = false;
        ptosPausa = GameObject.FindGameObjectsWithTag("Pausa");
        for (int i = 0; i < ptosPausa.Length; i++)
        {
            Destroy(ptosPausa[i]);
        }
        targetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>().gameObject;
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            if (player != null) player.GetComponent<CameraZoom>().cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 0;
            if (player != null) player.GetComponent<CameraZoom>().cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = 0;
            if (player != null) player.GetComponent<CameraZoom>().cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = 0;
            if (player != null) player.GetComponent<CameraZoom>().cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 0;
            if (player != null) player.GetComponent<CameraZoom>().cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x = 0;
            if (player != null)
            {
                if (player.GetComponent<CameraZoom>().cinemakina.m_Lens.OrthographicSize < player.GetComponent<CameraZoom>().tamañoCamaraPausa)
                {

                    player.GetComponent<CameraZoom>().cinemakina.m_Lens.OrthographicSize = player.GetComponent<CameraZoom>().tamañoCamaraPausa - 0.01f;

                    //player.GetComponent<CameraZoom>().cinemakina.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = 0;
                    Time.timeScale = 0;
                }
            }
        }
        ptosPausa = GameObject.FindGameObjectsWithTag("Pausa");
        //transform.rotation = Quaternion.identity;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Time.timeScale == 1)
            {

                AbrirMenu();

            }
            else
            {
                CerrarMenu();
                TiempoNormal();
            }
            //if (paused)
            //{
            //    CerrarMenu();
            //}
            //else
            //{
            //    AbrirMenu();
            //}

        }
    }
    public void TiempoNormal()
    {
        Time.timeScale = 1;
    }
    private void LateUpdate()
    {
        if (menuPausa != null)
        {
            if (GameObject.FindGameObjectWithTag("NucleoCabeza") != null)
            {
                menuPausa.GetComponent<RectTransform>().position = GameObject.FindGameObjectWithTag("NucleoCabeza").transform.position + offset;
            }
            else
            {
                menuPausa.GetComponent<RectTransform>().position = player.transform.position + offset;
            }
        }

    }
    private void AbrirMenu()
    {
        if (menuPausa == null) InicioScript();
        player.GetComponentInChildren<ComportamientoHUD>().bloqueado = true;
        controllerAndInput.enabled = false;
        pi.enabled = false;
        controllerAndInput.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        controllerAndInput.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        if (menuPausa != null) menuPausa.SetActive(true);
        playerAnim.speed = 0;
        if (menuPausa != null) if (menuPausa.GetComponentInChildren<CreadorPuntos>() != null) menuPausa.GetComponentInChildren<CreadorPuntos>().CreatePoints();
        paused = true;

    }
    public void CerrarMenu()
    {
        GameObject.FindObjectOfType<FuncionalidadPausa>().Limpiar();
        GameObject.FindObjectOfType<FuncionalidadPausa>().MostrarObjetos();
        player.GetComponentInChildren<ComportamientoHUD>().bloqueado = false;
        for (int i = 0; i < targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets.Length; i++)
        {
            if (i == 0) { targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[0].target = player.transform; }
            else
            {
                if (targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target != null)
                {
                    targetGroup.GetComponent<CinemachineTargetGroup>().m_Targets[i].target = null;

                }
            }
        }

        player.GetComponent<CameraZoom>().startsize = player.GetComponent<CameraZoom>().auxstartsize;
        paused = false;
        controllerAndInput.enabled = true;
        pi.enabled = true;
        if (menuPausa != null) menuPausa.SetActive(false);
        playerAnim.speed = 1;
        controllerAndInput.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravedadNormal;
        for (int i = 0; i < ptosPausa.Length; i++)
        {
            Destroy(ptosPausa[i]);
        }
    }
}
