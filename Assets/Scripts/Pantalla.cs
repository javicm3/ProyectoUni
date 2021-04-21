using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pantalla : MonoBehaviour
{

    [SerializeField] GameObject screen;
    [SerializeField] GameObject animDash;
    [SerializeField] GameObject animChispazo;
    [SerializeField] GameObject animParedes;
    [SerializeField] GameObject animCables;

    public void ChangeScreen(DesbloquearHabilidades.habilidad hab)
    {
        screen.SetActive(false);
        switch (hab)
        {
            case DesbloquearHabilidades.habilidad.dash:
                animDash.SetActive(true);
                break;

            case DesbloquearHabilidades.habilidad.chispazo:
                animChispazo.SetActive(true);
                break;

            case DesbloquearHabilidades.habilidad.movimientoPared:
                animParedes.SetActive(true);
                break;

            case DesbloquearHabilidades.habilidad.movimientoCable:
                animCables.SetActive(true);
                break;
            default:
                break;
        }
    }
}