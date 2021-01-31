using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparosBOSS : MonoBehaviour
{

    public Transform[] targets;
    public GameObject bala;
    public GameObject balaRapida;
    public int balasInstanciadas;
    public float tiempoEntreBalasPrimeras;
    public float tiempoEntreBalas = 0.5f;
    public float min = -30;
    public float max = 30;
    public Transform puntoDisparo;
    public Transform puntoParticulas;
    public bool disparando;
    public float auxTiempoEntreBalas;
    public int posicionArray = 0;
    public int posicionArray2 = 0;
    public GameObject particulasBoss;

    // Start is called before the first frame update
    void Start()
    {
        auxTiempoEntreBalas = tiempoEntreBalasPrimeras;
        posicionArray = 0;
        posicionArray2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (disparando)
        {
            if (posicionArray < targets.Length)
            {
                auxTiempoEntreBalas -= Time.deltaTime;
                if (auxTiempoEntreBalas <= 0)
                {
                     GameObject balad=Instantiate(balaRapida, puntoDisparo.position, Quaternion.identity);
                    Instantiate(particulasBoss, puntoParticulas.position, particulasBoss.transform.rotation);
                    balad.GetComponent<BalaBoss>().objetivo = targets[posicionArray].position;
                    auxTiempoEntreBalas += tiempoEntreBalasPrimeras;
                    posicionArray++;
                }


            }
            else
            {
               
                if (posicionArray2 < balasInstanciadas)
                {
                    auxTiempoEntreBalas -= Time.deltaTime;
                    if (auxTiempoEntreBalas <= 0)
                    {
                        Vector3 objetivoAux = RandomVector2(min,max);
                        GameObject balon=Instantiate(bala, puntoDisparo.position, Quaternion.identity);
                        Instantiate(particulasBoss, puntoParticulas.position, particulasBoss.transform.rotation);
                        balon.GetComponent<BalaBoss>().objetivo = objetivoAux;
                        balon= Instantiate(bala, puntoDisparo.position, Quaternion.identity);
                        balon.GetComponent<BalaBoss>().objetivo = objetivoAux + new Vector3(0, 50, 0);
                        balon = Instantiate(bala, puntoDisparo.position, Quaternion.identity);
                        balon.GetComponent<BalaBoss>().objetivo = objetivoAux + new Vector3(0, -50, 0);
                        auxTiempoEntreBalas += tiempoEntreBalas;
                        posicionArray2++;
                    }


                }
                else
                {
                    posicionArray = 0;
                    posicionArray2 = 0;
                    GetComponent<Ruta>().disparo = false;
                    disparando = false;
                }
            }

            //for (int i = 0; i < targets.Length; i++)
            //{

            //    StartCoroutine(DisparoCoroutine());
            //}
            //for (int i = 0; i < balasInstanciadas; i++)
            //{
            //    Instantiate(bala, puntoDisparo.position, Quaternion.identity);
            //    bala.GetComponent<BalaBoss>().objetivo.position = RandomVector2(min, max);
            //    StartCoroutine(DisparoCoroutine());
            //}
           
        }
    }

    public Vector2 RandomVector2(float angle, float angleMin)
    {
        float random = Random.value * angle + angleMin;
        return new Vector2(/*Mathf.Cos(random)*/puntoDisparo.position.x+random, Mathf.Sin(puntoDisparo.position.y+random));
    }

    public void Disparar()
    {
        disparando = true;
 
    }

    IEnumerator DisparoCoroutine()
    {
        yield return new WaitForSeconds(tiempoEntreBalas);
    }
}
