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
    private void Start()
    {
        switch (desbloquear)
        {
            case habilidad.dash:
                if (GameManager.Instance.Habilidades.dash == true)
                { if(GameObject.FindObjectOfType < Estrella > ()!=null&&GameObject.FindObjectOfType<Estrella>().gameObject!=null)Destroy(GameObject.FindObjectOfType<Estrella>().gameObject); }

                break;
            case habilidad.chispazo:
                if (GameManager.Instance.Habilidades.chispazo == true)
                { if (GameObject.FindObjectOfType<Estrella>() != null && GameObject.FindObjectOfType<Estrella>().gameObject != null) Destroy(GameObject.FindObjectOfType<Estrella>().gameObject); }
                break;
            case habilidad.movimientoPared:
                if (GameManager.Instance.Habilidades.movParedes == true)
                { if (GameObject.FindObjectOfType<Estrella>() != null && GameObject.FindObjectOfType<Estrella>().gameObject != null) Destroy(GameObject.FindObjectOfType<Estrella>().gameObject); }
                break;
            case habilidad.movimientoCable:
                if (GameManager.Instance.Habilidades.movCables == true)
                { if (GameObject.FindObjectOfType<Estrella>() != null && GameObject.FindObjectOfType<Estrella>().gameObject != null) Destroy(GameObject.FindObjectOfType<Estrella>().gameObject); }
                break;
        }
    }
    public void DesbloquearHabilidad()
    {
        switch (desbloquear)
        {
            case habilidad.dash:
                if (GameManager.Instance.Habilidades.dash == false)
                { GameManager.Instance.Habilidades.dash = true; ActivarAnim(); }
                
                break;
            case habilidad.chispazo:
                if (GameManager.Instance.Habilidades.chispazo == false)
                { GameManager.Instance.Habilidades.chispazo = true; ActivarAnim(); }
                break;
            case habilidad.movimientoPared:
                if (GameManager.Instance.Habilidades.movParedes == false)
                { GameManager.Instance.Habilidades.movParedes = true; ActivarAnim(); }
                break;
            case habilidad.movimientoCable:
                if (GameManager.Instance.Habilidades.movCables == false)
                { GameManager.Instance.Habilidades.movCables = true; ActivarAnim(); }
                break;
        }

        usado = true;
    }

    void ActivarAnim()
    {
        GameManager.Instance.animDesbloquear = true;
        GameManager.Instance.habilidad = desbloquear;
    }
}
