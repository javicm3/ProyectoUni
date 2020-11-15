using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonCerrar : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (this.gameObject.name == "Nivel1")
        {
            GameManager.Instance.mainPanel = GameObject.FindGameObjectWithTag("MainPanel");
            //GameManager.Instance.DesactPanel();
        }
    }
    void Start()
    {
       
    }
    public void Cerrar()
    {
        //GameManager.Instance.DesactivarPanel();
    }
    public void RunLevel()
    {
        //GameManager.Instance.EmpezarNivel();
    }
    public void ActivarPanel(float level)
    {
        //GameManager.Instance.ActivarPanel(level);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
