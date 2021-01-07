using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroMortal : MonoBehaviour
{
    Animator anim;
    public int seccion = 0;
    int seccionAux = 0;
    [SerializeField] bool puedeContar = false;
    // Start is called before the first frame update
    void Start()
    {
        //seccion = 0;
        //seccionAux = 0;
        puedeContar = false;
        anim = GetComponentInParent<Animator>();
        anim.SetBool("InicioBoss", false);
        anim.SetBool("Paron1", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(seccion == 0)
        {
            anim.SetBool("InicioBoss", false);
            anim.SetBool("Paron1", false);
        }
        else if(seccion == 1 && puedeContar == true)
        {
            //seccionAux = 1;
            anim.SetBool("InicioBoss", true);
            anim.SetBool("Paron1", false);
        }
        else if(seccion == 2 && puedeContar == true)
        {
            //seccionAux = 2;
            anim.SetBool("InicioBoss", false);
            anim.SetBool("Paron1", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            puedeContar =  true;
            
            //anim.SetBool("InicioBoss", true);
            //anim.SetBool("Paron1", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            puedeContar = false;
            //anim.SetBool("InicioBoss", false);
            //anim.SetBool("Paron1", false);
            //puedeContar = true;

        }
    }
}
