using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject controles;
    bool controlesOn = false;
    public GameObject botonesPrincipales;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Controles()
    {
        if(controlesOn == false)
        {
            controles.SetActive(true);
            controlesOn = true;
            botonesPrincipales.SetActive(false);
        }
        else
        {
            controles.SetActive(false);
            controlesOn = false;
            botonesPrincipales.SetActive(true);
        }
    }
    public void Play()
    {
        SceneManager.LoadScene("NivelSemana26");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
