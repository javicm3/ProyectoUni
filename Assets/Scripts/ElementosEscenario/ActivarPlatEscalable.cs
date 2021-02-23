using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPlatEscalable : MonoBehaviour
{
    public bool marcarParaQueFuncione = false;
    public float tiempoEntreActivaciones = 3f;
    public float tiempoDesactivado = 1f;
    public bool activado = true;
    public GameObject[] objetos;
    float auxtiempo;
    // Start is called before the first frame update
    void Start()
    {
        auxtiempo = tiempoEntreActivaciones;
    }

    // Update is called once per frame
    void Update()
    {
        if (marcarParaQueFuncione)
        {
            if (activado)
            {
                this.gameObject.transform.parent.tag = "Pared";
                this.gameObject.transform.tag = "Pared";
                foreach (GameObject go in objetos)
                {
                    go.SetActive(true);
                }
                auxtiempo -= Time.deltaTime;
                if (auxtiempo <= 0)
                {
                    auxtiempo = tiempoDesactivado;
                    activado = false;
                }
            }
            else
            {
                //if (FindObjectOfType<ControllerPersonaje>().gameObject.transform.parent == this.transform.parent)
                //{
                //    FindObjectOfType<ControllerPersonaje>().pegadoPared = false;
                //    FindObjectOfType<ControllerPersonaje>().gameObject.transform.parent = null;
                //}
              this.gameObject.transform.parent.tag = "NoClimb";
                this.gameObject.tag = "NoClimb";
                foreach (GameObject go in objetos)
                {
                    go.SetActive(false);
                }
                auxtiempo -= Time.deltaTime;
                if (auxtiempo <= 0)
                {
                    auxtiempo = tiempoEntreActivaciones;
                    activado = true;
                }
            }
        }
        
    }
}
