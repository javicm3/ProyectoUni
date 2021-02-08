using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionalidadPausa : MonoBehaviour
{
 
    public GameObject[] mostrarObj;
    public GameObject[] quitarObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Limpiar()
    {
        foreach (GameObject go in quitarObj)
        {

            go.SetActive(false);

        }
        foreach (GameObject go in mostrarObj)
        {

            go.SetActive(false);

        }
    }
    public void MostrarObjetos()
    {
        foreach(GameObject go in mostrarObj)
        {
          
                go.SetActive(true);
          
        }
    }
    public void QuitarObjetos()
    {
        foreach (GameObject go in quitarObj)
        {
           
                go.SetActive(true);
           
        }
    }
    public void CerrarMenu()
    {
        GameObject.FindObjectOfType<MenuPausa>().CerrarMenu();
        GameObject.FindObjectOfType<MenuPausa>().TiempoNormal();

    }
    public void VolverLobby()
    {
        CerrarMenu();
        GameObject.FindObjectOfType<GameManager>().NextScene("PantallaInicio");
        Time.timeScale = 1;

    }
    public void MostrarControles()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
