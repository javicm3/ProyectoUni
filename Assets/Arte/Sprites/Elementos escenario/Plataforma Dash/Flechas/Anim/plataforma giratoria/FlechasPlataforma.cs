using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechasPlataforma : MonoBehaviour
{
    [SerializeField] GameObject scriptPlataforma;
    Animator animPantalla;
    // Start is called before the first frame update
    void Start()
    {
        animPantalla = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (scriptPlataforma.GetComponent<PlataformaND1>().vuelta) 
        {
            animPantalla.SetBool("Activado", true);

        }

        //if (scriptPlataforma.GetComponent<PlataformaND1>().vuelta && scriptPlataforma.GetComponent<PlataformaND1>().verticales == false)
        //{
        //    animPantalla.SetBool("Activado", false);

        //}
    }
}
