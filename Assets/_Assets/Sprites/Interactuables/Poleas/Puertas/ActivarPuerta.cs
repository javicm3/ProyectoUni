using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPuerta : MonoBehaviour
{
    Animator puertaAnim;
    BoxCollider2D colision;
    Polea scriptPolea;
    // Start is called before the first frame update
    void Start()
    {
        puertaAnim = GetComponent<Animator>();
        colision = GetComponent<BoxCollider2D>();
        scriptPolea = GetComponentInParent<Polea>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
