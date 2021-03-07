using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEnemigoAbs : MonoBehaviour
{
    EnemigoAbsorb enemigo;

    void Start()
    {
        enemigo = GetComponentInParent<EnemigoAbsorb>();
    }

    public void EventoAtacar()
    {
         enemigo.EjecutarAtaque();
    }

}
