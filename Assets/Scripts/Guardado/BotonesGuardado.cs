using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonesGuardado : MonoBehaviour
{
    //De momento no se puede borrar la partida, pero si no se carga y se vuelve a guardar se sobrescribirán los datos
    //También falta hacer que se guarde y se cargue automáticamente cuando debe dentro del GM

    public void Guardar()
    {
        SistemaGuardado.Guardar();
    }
    public void Cargar()
    {
        SistemaGuardado.Cargar();
    }
}
