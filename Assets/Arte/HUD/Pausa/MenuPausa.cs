﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MenuPausa : MonoBehaviour
{
    GameObject menuPausa;
    Animator anim;
    Animator playerAnim;
    GameObject[] ptosPausa;
    public bool paused;
    ControllerPersonaje controllerAndInput;
    PlayerInput pi;
    float gravedadNormal;
    GameObject targetGroup;
    GameObject player;
    // Start is called before the first frame update
    void Start()
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
        ptosPausa = GameObject.FindGameObjectsWithTag("Pausa");
        //transform.rotation = Quaternion.identity;
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
    private void LateUpdate()
    {
        
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
