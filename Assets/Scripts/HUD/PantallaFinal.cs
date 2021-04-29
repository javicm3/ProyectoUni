using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PantallaFinal : MonoBehaviour
{
    [SerializeField] GameObject pantalla;
    [SerializeField] TextMeshProUGUI t_tiempo;
    [SerializeField] TextMeshProUGUI t_coleccionables;
    [SerializeField] GameObject newRecord;

    public void ActivarPantalla()
    {
        float tiempo = Time.time - GameManager.Instance.NivelActual.tiempoEmpezar;
        ref float mejorTiempo = ref GameManager.Instance.NivelActual.mejorTiempo;
        if (mejorTiempo==0 || tiempo<mejorTiempo)
        {
            mejorTiempo = tiempo;
           newRecord.SetActive(true);
        }

        pantalla.SetActive(true);
        t_tiempo.text = (tiempo / 60).ToString("00") + " : " + (tiempo % 60).ToString("00");
        t_coleccionables.text = GameManager.Instance.NivelActual.actualColeccionablesCogidos.Count + " / " + GameManager.Instance.NivelActual.maxColeccionables;
    }

    public void IrLobby()
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }
}
