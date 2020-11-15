using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderPLayer : MonoBehaviour
{
    public bool activado = false;
    public Collider2D trigger;
    public bool ataquedashing = false;
    public bool ataqueabajo = false;
    public float tiempoAtaquedash = 2;
    float auxataquedash;
    // Start is called before the first frame update
    void Start()
    {
        auxataquedash = tiempoAtaquedash;
    }
    public void IniciarAtaque(string ataque)
    {
      
        if (ataque == "dash")
        {
            print(ataque + "ataquehecho");
            activado = true;
            trigger.enabled = true;
            ataqueabajo = false;
            ataquedashing = true;
        }
        else if (ataque == "abajo")
        {
            print(ataque + "ataquehecho");
            activado = true;
            trigger.enabled = true;
            ataquedashing = false;
            ataqueabajo = true;
        }
    }
    public void Desactivar(string ataque)
    {
        print(ataque + "desact");
        if ((ataquedashing == false) && (ataqueabajo == false)) activado = false;
       if((ataquedashing==false)&&(ataqueabajo==false)) trigger.enabled = false;
        if (ataque == "dash")
        {
            auxataquedash += tiempoAtaquedash;
            ataquedashing = false;
        }
        else if (ataque == "abajo")
        {
            ataqueabajo = false;
        }


    }
    // Update is called once per frame
    void Update()
    {
        if (ataquedashing == true)
        {
            auxataquedash -= Time.deltaTime;
            if (auxataquedash <= 0)
            {


                Desactivar("dash");
               
            }
        }
        if (FindObjectOfType<Movimiento>().cayendoS == false)
        {
            Desactivar("abajo");
        }
       
    }
}
