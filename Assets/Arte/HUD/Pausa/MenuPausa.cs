using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    Animator anim;
    Animator playerAnim;
    GameObject[] ptosPausa;
    public bool paused;
    public ControllerPersonaje controllerAndInput;
    PlayerInput pi;
    float gravedadNormal;
    // Start is called before the first frame update
    void Start()
    {
        gravedadNormal = controllerAndInput.gameObject.GetComponent<Rigidbody2D>().gravityScale;
        controllerAndInput = controllerAndInput.gameObject.GetComponent<ControllerPersonaje>();
        playerAnim = controllerAndInput.gameObject.GetComponentInChildren<Animator>();
        pi = controllerAndInput.gameObject.GetComponent<PlayerInput>();
        menuPausa.SetActive(false);
        anim = menuPausa.GetComponent<Animator>();
        paused = false;
        ptosPausa = GameObject.FindGameObjectsWithTag("Pausa");
        for (int i = 0; i < ptosPausa.Length; i++)
        {
            Destroy(ptosPausa[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        ptosPausa = GameObject.FindGameObjectsWithTag("Pausa");
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //if(Time.timeScale == 1)
            //{
            //    Time.timeScale = 0;
            //    AbrirMenu();

            //}
            //else
            //{
            //    CerrarMenu();
            //    Time.timeScale = 1;
            //}
            if (paused)
            {
                CerrarMenu();
            }
            else
            {
                AbrirMenu();
            }
        }
    }
    private void AbrirMenu()
    {
        paused = true;
        controllerAndInput.enabled = false;
        pi.enabled = false;
        controllerAndInput.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        controllerAndInput.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        menuPausa.SetActive(true);
        playerAnim.speed = 0;
        menuPausa.GetComponentInChildren<CreadorPuntos>().CreatePoints();
        
    }
    private void CerrarMenu()
    {
        paused = false;
        controllerAndInput.enabled = true;
        pi.enabled = true;
        menuPausa.SetActive(false);
        playerAnim.speed = 1;
        controllerAndInput.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravedadNormal;
        for (int i = 0; i < ptosPausa.Length; i++)
        {
            Destroy(ptosPausa[i]);
        }
    }
}
