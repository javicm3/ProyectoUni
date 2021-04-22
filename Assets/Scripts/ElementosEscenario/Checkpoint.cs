using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //public bool ultimoCheck = false;

    bool used = false;
    List<string> coleccionablesGuardados = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used && collision.gameObject.tag=="Player")
        {
            used = true;
            GameManager.Instance.UltimoCheck = this;
            // AQUI HABRIA QUE METER ALGUN TIPO DE FEEDBACK SOBRE QUE SE HA USADO EL CHECKPOINT
            FindObjectOfType<NewAudioManager>().Play("Checkpoint");
            GuardarColeccionables();
        }
    }

    void GuardarColeccionables()
    {
        print("guardar coleccionables");
        foreach (string item in GameManager.Instance.NivelActual.actualColeccionablesCogidos)
        {
            coleccionablesGuardados.Add(item);
        }
    }

    public void CargarColeccionables()
    {
        print("cargar coleccionables");
        //REACTIVAR COLECCIONABLES COGIDOS DESPUÉS DEL CHECKPOINT
        List<string> aux = new List<string>();
        foreach (string go in GameManager.Instance.NivelActual.actualColeccionablesCogidos)
        {
            if (!coleccionablesGuardados.Contains(go))
            {
                aux.Add(go);
                GameObject coleccionable = GameObject.Find(go);

                coleccionable.GetComponent<Moneda>().Activar();
            }            
        }
        foreach (string go in aux)
        { GameManager.Instance.NivelActual.actualColeccionablesCogidos.Remove(go); }
 
        GameManager.Instance.textoColecc.text = coleccionablesGuardados.Count.ToString() +"  /  "+ GameManager.Instance.NivelActual.maxColeccionables;


    }


    /*   private void OnTriggerEnter2D(Collider2D collision)
       {
           if (collision.gameObject.tag == "Player")
           {
               Checkpoint[] checks=   FindObjectsOfType<Checkpoint>();
               foreach(Checkpoint go in checks)
               {
                   go.ultimoCheck = false;
               }
               ultimoCheck = true;
           }
       }*/
}
