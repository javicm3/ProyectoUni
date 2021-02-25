using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trai2 : MonoBehaviour
{
    public Animator animBulva;
    Animator animCC;
    bool Carga;
    bool Ataque;
    bool Herido;
    // Start is called before the first frame update
    void Start()
    {
        Carga = false;
        Ataque = false;
        Herido = false;
        animCC = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!Carga)
            {
                Carga = true;
                animCC.SetBool("Cargando", true);
                animBulva.SetBool("True", true);
            }
            else if (Carga)
            {
                Carga = false;
                animCC.SetBool("Cargando", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!Ataque)
            {
                Ataque = true;
                animCC.SetBool("Ataque", true);
            }
            else if (Ataque)
            {
                Ataque = false;
                animCC.SetBool("Ataque", false);
                animBulva.SetBool("True", false);

            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!Herido)
            {
                Herido = true;
                animCC.SetBool("Estuneado", true);
            }
            else if (Herido)
            {
                Herido = false;
                animCC.SetBool("Estuneado", false);
            }
        }
    }
}
