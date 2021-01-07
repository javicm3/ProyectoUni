﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escombro : MonoBehaviour
{
    //setear gravedad a 4 poniendo el rb en dynamic

    Rigidbody2D rb;
    public string tagCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == tagCollider)
        {
            rb.isKinematic = false;
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
