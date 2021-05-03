using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pantalla : MonoBehaviour
{

    [SerializeField] GameObject otherScreen;
    [SerializeField] GameObject habScreen;

    [SerializeField] GameObject animDash;
    [SerializeField] GameObject animChispazo;
    [SerializeField] GameObject animParedes;
    [SerializeField] GameObject animCables;

    [SerializeField] TextMeshProUGUI comando;

    DesbloquearHabilidades.habilidad hab;

    public void ChangeScreen(DesbloquearHabilidades.habilidad hab)
    {
        this.hab = hab;
        StartCoroutine(ScreenDelay());
    }

    IEnumerator ScreenDelay()
    {
        yield return new WaitForSeconds(4.75f);

        bool controller=false;
        if (FindObjectOfType<ControllerPersonaje>().joystick != null && FindObjectOfType<ControllerPersonaje>().joystick.Name != "NullInputDevice")
        {
            controller = true;
        }

        habScreen.SetActive(true);
        otherScreen.SetActive(false);
        switch (hab)
        {
            case DesbloquearHabilidades.habilidad.dash:
                animDash.SetActive(true);
                if (controller) { comando.text = "X"; }
                break;

            case DesbloquearHabilidades.habilidad.chispazo:
                animChispazo.SetActive(true);
                if (controller) { comando.text = "RT"; }
                break;

            case DesbloquearHabilidades.habilidad.movimientoPared:
                animParedes.SetActive(true);
                if (controller) { comando.text = "X"; } else {comando.text="ctrl"; }
                break;

            case DesbloquearHabilidades.habilidad.movimientoCable:
                animCables.SetActive(true);
                comando.enabled = false;
                break;
            default:
                break;
        }
    }
}