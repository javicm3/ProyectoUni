using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparosBOSS : MonoBehaviour
{

    public Transform[] targets;
    public GameObject bala;
    public int balasInstanciadas;
    public float tiempoEntreBalas = 0.5f;
    public float min = -30;
    public float max = 30;
    public Transform puntoDisparo;
    public bool disparando;
    public float auxTiempoEntreBalas;
   public  int posicionArray = 0;
   public  int posicionArray2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        auxTiempoEntreBalas = 0;
        posicionArray = 0;
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
                    Instantiate(bala, puntoDisparo.position, Quaternion.identity);
                    bala.GetComponent<BalaBoss>().objetivo = targets[posicionArray].position;
                    auxTiempoEntreBalas += tiempoEntreBalas;
                    posicionArray++;
                }


            }
            else
            {
               
                if (posicionArray2 <balasInstanciadas)
                {
                    auxTiempoEntreBalas -= Time.deltaTime;
                    if (auxTiempoEntreBalas <= 0)
                    {
                        Instantiate(bala, puntoDisparo.position, Quaternion.identity);
                        bala.GetComponent<BalaBoss>().objetivo = RandomVector2(min, max);
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
