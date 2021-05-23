using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PantallaFinal : MonoBehaviour
{
    [SerializeField] GameObject pantalla;
    [SerializeField] GameObject pantallaFantasma;
    [SerializeField] TextMeshProUGUI t_tiempo;
    [SerializeField] TextMeshProUGUI t_coleccionables;
    [SerializeField] GameObject newRecord;

    public enum cinematica { none, boss1, boss2 };
    [SerializeField] cinematica cinem;

    [SerializeField] TextMeshProUGUI[] comando;

    ControllerPersonaje cp;



    private void Start()
    {
        cp = FindObjectOfType<ControllerPersonaje>();
    }

    private void Update()
    {
        if (pantalla.activeSelf && (Input.GetKeyDown(KeyCode.Return) || (cp.joystick != null && cp.joystick.Action1.WasPressed)))
        {
            IrLobby();
        }
        else if (pantallaFantasma.activeSelf && (Input.GetKeyDown(KeyCode.Return) || (cp.joystick != null && cp.joystick.Action1.WasPressed)))
        {
            pantallaFantasma.SetActive(false);
            ActivarPantalla();
        }
    }

    public void ActivarPantalla()
    {
        comando[1].text = Comando();
        float tiempo = Time.time - GameManager.Instance.NivelActual.tiempoEmpezar;
        ref float mejorTiempo = ref GameManager.Instance.NivelActual.mejorTiempo;
        if (mejorTiempo==0 || tiempo<mejorTiempo)
        {
            mejorTiempo = tiempo;
           newRecord.SetActive(true);
        }

        pantalla.SetActive(true);
        if (tiempo<60) { t_tiempo.text = "00 : " + (tiempo % 60).ToString("00"); }
        else t_tiempo.text = Mathf.Floor(tiempo / 60).ToString("00") + " : " + Mathf.Floor(tiempo % 60).ToString("00");
        t_coleccionables.text = GameManager.Instance.NivelActual.actualColeccionablesCogidos.Count + " / " + GameManager.Instance.NivelActual.maxColeccionables;
    }

    public void ActivarFeedbackFantasma()
    {
        comando[0].text = Comando();
        pantallaFantasma.SetActive(true);
    }

    string Comando()
    {
        string c = "[ENTER]";
        if (cp.joystick!=null && cp.joystick.Name!= "Unknown Device")
        {
            c = "[A]";
        }

        return c;
    }

    public void IrLobby()
    {
        switch (cinem)
        {
            case cinematica.none:
                GameManager.Instance.cinematicaScene = "Lobby";
                SceneManager.LoadScene("PantallaCarga");
                break;
            case cinematica.boss1:
                GameManager.Instance.PlayCinematica(3, "Lobby");
                break;
            case cinematica.boss2:
                GameManager.Instance.PlayCinematica(5, "Lobby");
                break;
        }
        
    }
}
