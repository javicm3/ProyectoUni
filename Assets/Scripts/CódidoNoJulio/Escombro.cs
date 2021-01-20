﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escombro : MonoBehaviour
{
    //setear gravedad a 4 poniendo el rb en dynamic

    Rigidbody2D rb;
    public GameObject particulas;
    public string tagCollider;
    [Header("Vibracion Boss")]
    public float intensidadVibracionBoss = 0.10f;
    public float velocidadVibracion = 1f;
    public float tiempoVibracion = 0.25f;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == tagCollider)
        {
            rb.isKinematic = false;
            CinemachineShake.Instance.ShakeCamera(intensidadVibracionBoss, velocidadVibracion, tiempoVibracion);
            particulas.SetActive(false);
            anim.SetTrigger("Off");
        }
    }

    //void OnCollisionEnter2D (Collision2D col)
    //{

    //}
    // Update is called once per frame
    void Update()
    {

    }
}
