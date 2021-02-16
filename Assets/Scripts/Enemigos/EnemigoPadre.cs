using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPadre : MonoBehaviour
{
    public bool seActivaPorCercania = true;
    public bool stun = false;
    public bool activado = false;
    public float distanciaActivacion = 40f;
    public GameObject player;
    private void Awake()
    {
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
    }
    public virtual void Stun()
    {

    }
    public virtual void Reactivar()
    {

    }
    public virtual void ActivarPrimeraVez()
    {

    }
    void DesactivarEnemigo()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) > distanciaActivacion + 5f)
        {
            activado = false;
        }
    }
    protected virtual void Update()
    {
        if (seActivaPorCercania && activado==false)ActivarPrimeraVez();
        DesactivarEnemigo();
    }
    // Start is called before the first frame update

}
