using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HUDObject : MonoBehaviour
{
    protected Material startMat;
    protected Material selectMat;

    private void Start()
    {
        HUDController contr = GetComponentInParent<HUDController>();
        startMat = contr.normal;
        selectMat = contr.select;
    }

    public virtual void Select()
    { }

    public virtual void Diselect()
    { }

    public virtual void Use() //Este aun no lo tengo demasiado claro
    { }

    public virtual void Slide(float value)
    { }

    //Metodo dcha izq para sliders (no implementar en otros objetos)
}
