using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HUDObject : MonoBehaviour
{
    public virtual void Select(Material mat)
    { }

    public virtual void Diselect(Material mat)
    { }

    public virtual void Use() //Este aun no lo tengo demasiado claro
    { }

    //Metodo dcha izq para sliders (no implementar en otros objetos)
}
