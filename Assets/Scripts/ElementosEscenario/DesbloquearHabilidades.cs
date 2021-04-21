using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesbloquearHabilidades : MonoBehaviour
{
    public enum habilidad
    {
        dash,
        chispazo,
        movimientoPared,
        movimientoCable,
    }

    bool usado = false;

    [SerializeField] habilidad desbloquear;


    // De momento se llama desde el script vuleta al lobby (si la puerta tiene ambos scripts)

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !usado)
        {
            DesbloquearHabilidad();
        }
    }*/

    public void DesbloquearHabilidad()
    {
        switch (desbloquear)
        {
            case habilidad.dash:
                GameManager.Instance.Habilidades.dash = true;
                break;
            case habilidad.chispazo:
                GameManager.Instance.Habilidades.chispazo = true;
                break;
            case habilidad.movimientoPared:
                GameManager.Instance.Habilidades.movParedes = true;
                break;
            case habilidad.movimientoCable:
                GameManager.Instance.Habilidades.movCables = true;
                break;
        }
        GameManager.Instance.animDesbloquear = true;
        GameManager.Instance.habilidad = desbloquear;
        usado = true;
    }
}
