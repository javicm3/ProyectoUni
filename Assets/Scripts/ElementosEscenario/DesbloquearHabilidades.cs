using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesbloquearHabilidades : MonoBehaviour
{
    enum habilidad
    {
        dash,
        chispazo,
        movimientoPared,
        movimientoCable,
    }

    bool usado = false;

    [SerializeField] habilidad desbloquear;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !usado)
        {
            DesbloquearHabilidad();
        }
    }

    void DesbloquearHabilidad()
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

        usado = true;
    }
}
