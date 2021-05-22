using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlataformaDron : MonoBehaviour
{
    public float speed;
    public Transform pos1, pos2;
    public Transform startPos;
    public float tiempoParada = 2;
    public float auxtiempoParada;
    public bool vuelta = false;
    public bool verticales = false;
    public Vector3 nextPos;

    enum platformState { desactivada, inmovil, movil }
    platformState estado;

    void Start()
    {
        nextPos = pos1.position;
        auxtiempoParada = tiempoParada;
        estado = platformState.inmovil;
    }

    // Update is called once per frame
    void Update()
    {
        switch (estado)
        {
            case platformState.movil:
                Caer();
                break;

            case platformState.inmovil:
                CheckTiempoParada();
                break;

            default:
                break;
        }

    }

    void Caer()
    {

    
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, nextPos) < 0.05f)
        {
          
            Desactivar();
        }
    }

    public void Desactivar()
    {
      
        estado = platformState.inmovil;
        foreach (Animator go in GetComponentsInChildren<Animator>())
        {
            go.SetBool("Activado", false); //En caso de que se desactive desde el controlador y no por tiempo
        }
       
        transform.gameObject.SetActive(false);
        auxtiempoParada = tiempoParada;
    }

    public void Activar()
    {
        this.transform.position = startPos.position;
        estado = platformState.inmovil;
        foreach (Animator go in GetComponentsInChildren<Animator>())
        {
            go.SetBool("Activado", true);
           
                NewAudioManager.Instance.Play("PlataformaDron");
           
        }
    }


    void CheckTiempoParada()
    {
        auxtiempoParada -= Time.deltaTime;
        if (auxtiempoParada <= 0)
        {
            NewAudioManager.Instance.Play("PlataformaDronBaja");
            estado = platformState.movil; 
            auxtiempoParada = tiempoParada;
        }
        if (auxtiempoParada <= 0.3f)
        {
            foreach (Animator go in GetComponentsInChildren<Animator>())
            {
                go.SetBool("Activado", false);
            }
        }
    }


    private void OndDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}


