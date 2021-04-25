using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigoVolador : EnemigoPadre
{
    [SerializeField] float speed;
    [SerializeField] Transform[] puntosPersonaje;
    public bool arrived = false;
    public int indexArray;
    public float tiempoStun = 2f;
    public float auxTiempoStun;

    [SerializeField] float tiempoEspera;
    [SerializeField] Animator animCC;


    // Start is called before the first frame update
    void Start()
    {
        indexArray = 0;

    }
    public override void ActivarPrimeraVez()
    {

        if (Vector2.Distance(this.transform.position, player.transform.position) < distanciaActivacion)
        {
            activado = true;
            NewAudioManager.Instance.Play("EnemigoVoladorIdle");

            //reseter posicion
        }
        else
        {

        }
    }
    public override void Stun()
    {
        FindObjectOfType<NewAudioManager>().Play("EnemigoStun");
        stun = true;
        animCC.SetBool("Dañado", true);

        auxTiempoStun = tiempoStun;

    }
    // Update is called once per frame
    public override void Reactivar()
    {
        auxTiempoStun = 0;
        stun = false;
        animCC.SetBool("Dañado", false);

    }
    protected override void Update()
    {
        if (stun == true)
        {
            if (auxTiempoStun > 0)
            {
                auxTiempoStun -= Time.deltaTime;
                if (auxTiempoStun <= 0)
                {
                    Reactivar();
                }
            }
        }
        base.Update();
        if (activado == true)
        {

            if (stun == false)
            {
                if (puntosPersonaje[indexArray] != null)
                {


                    if (Vector3.Distance(this.transform.position, puntosPersonaje[indexArray].position) > 1 && !arrived)
                    {
                        if (puntosPersonaje[indexArray].position.x < this.transform.position.x)
                        {
                            this.transform.localScale = new Vector3(-1, 1, 1);
                        }
                        else
                        {
                            this.transform.localScale = new Vector3(1, 1, 1);
                        }
                        this.transform.Translate((puntosPersonaje[indexArray].position - this.transform.position).normalized * Time.deltaTime * speed);
                        animCC.SetBool("Moviendose", true);

                    }
                    else
                    {
                        if (indexArray < puntosPersonaje.Length - 1)
                        {
                            arrived = true;
                            indexArray++;
                            StartCoroutine("Wait");

                        }
                        else
                        {
                            arrived = true;
                            indexArray = 0;
                            StartCoroutine("Wait");


                        }
                    }
                }
            }
        }
        else
        {
            animCC.SetBool("Moviendose", false);
            NewAudioManager.Instance.Stop("EnemigoVoladorIdle");
        }
    }

    IEnumerator Wait()
    {
        animCC.SetBool("Moviendose", false);

        yield return new WaitForSeconds(tiempoEspera);
        arrived = false;
    }






}


