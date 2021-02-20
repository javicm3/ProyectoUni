using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //public bool ultimoCheck = false;

    bool used = false;
    List<string> coleccionablesGuardados = new List<string>();
    List<string> estrellasGuardadas = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used && collision.gameObject.tag=="Player")
        {
            used = true;
            GameManager.Instance.UltimoCheck = this;
            // AQUI HABRIA QUE METER ALGUN TIPO DE FEEDBACK SOBRE QUE SE HA USADO EL CHECKPOINT

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
        foreach (string item in GameManager.Instance.NivelActual.actualEstrellasCogidas)
        {
            estrellasGuardadas.Add(item);
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

        aux.Clear();


        foreach (string go in GameManager.Instance.NivelActual.actualEstrellasCogidas)
        {
            if (!estrellasGuardadas.Contains(go))
            {
                aux.Add(go);
                GameObject estrella = GameObject.Find(go);

                estrella.GetComponent<SpriteRenderer>().enabled = false;
                estrella.GetComponent<Collider2D>().enabled = false;
            }
        }
        foreach (string go in aux)
        { GameManager.Instance.NivelActual.actualEstrellasCogidas.Remove(go); }


        print(coleccionablesGuardados.Count + " " + GameManager.Instance.NivelActual.actualColeccionablesCogidos.Count);
        GameManager.Instance.textoActualColecc.text = coleccionablesGuardados.Count.ToString();
        GameManager.Instance.textoActualEstrellas.text = estrellasGuardadas.ToString();

        GameManager.Instance.NivelActual.actualEstrellasCogidas = estrellasGuardadas;

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
