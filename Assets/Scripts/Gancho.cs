using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gancho : MonoBehaviour
{

    public bool escombro;
    Animator animCC;

    void Start()
    {
        animCC = GetComponent<Animator>();
    }

    void Update()
    {
        if (escombro)
        {
            animCC.SetBool("Escombros", true);
        }
        else
        {
            animCC.SetBool("Escombros", false);
        }
    }
}
