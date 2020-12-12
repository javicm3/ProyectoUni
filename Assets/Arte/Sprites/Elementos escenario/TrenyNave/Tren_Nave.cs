﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tren_Nave : MonoBehaviour
{
    public float speed;
    public float tiempoMuerte = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        Destroy(this.gameObject, tiempoMuerte);
    }
}
