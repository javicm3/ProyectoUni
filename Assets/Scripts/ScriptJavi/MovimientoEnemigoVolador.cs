using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigoVolador : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform[] puntosPersonaje;
    public   bool arrived = false;
   public int indexArray;
    public float tiempoStun = 2f;
    public float auxTiempoStun;
    public bool stun = false;
    [SerializeField] float tiempoEspera;


    // Start is called before the first frame update
    void Start()
    {
        indexArray = 0;

    }

    public void Stun()
    {

        stun = true;


        auxTiempoStun = tiempoStun;

    }
    // Update is called once per frame
    void Update()
    {
        if (stun == true)
        {
            if (auxTiempoStun > 0)
            {
                auxTiempoStun -= Time.deltaTime;
                if (auxTiempoStun <= 0)
                {
                    auxTiempoStun = 0;



                    stun = false;
                }
            }
        }
        else
        {



            if (Vector3.Distance(this.transform.position, puntosPersonaje[indexArray].position) > 1 && !arrived)
            {
                if(puntosPersonaje[indexArray].position.x < this.transform.position.x)
                {
                    this.transform.localScale= new Vector3(-1,1,1);
                }
                else
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                this.transform.Translate((puntosPersonaje[indexArray].position - this.transform.position).normalized * Time.deltaTime * speed);

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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(tiempoEspera);
        arrived = false;
    }






}


