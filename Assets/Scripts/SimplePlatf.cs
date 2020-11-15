using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatf : MonoBehaviour
{
    public Transform[] puntos;

    int actualPoint;
    public float speedplat;
    public float waitingTime;
    float auxwaitingTime;
    public float startwaitingTime;
    float auxStartWait;
    int puntoRandom;
    // Start is called before the first frame update
    void Start()
    {
        actualPoint = 0;
        auxwaitingTime = waitingTime;
        auxStartWait = startwaitingTime;

    }

    // Update is called once per frame
    void Update()
    {

        auxStartWait -= Time.deltaTime;
        if (Vector2.Distance(this.transform.position, puntos[actualPoint].position) < 0.2f)
        {


            if (auxwaitingTime < 0)
            {

                for (int i = 0; i <= 2; i++)
                {
                    puntoRandom = Random.Range(0, puntos.Length);
                    if (puntoRandom != actualPoint)
                    {
                        actualPoint = puntoRandom;

                        break;
                    }
                    else
                    {

                        puntoRandom = Random.Range(0, puntos.Length);
                    }
                }
                //que no se quede dos veces seguidas en el mismo punto
                auxwaitingTime += waitingTime;
            }
            else
            {
                auxwaitingTime -= Time.deltaTime;
            }
        }
        else
        {

            if (auxStartWait < 0) transform.position = Vector2.MoveTowards(transform.position, puntos[actualPoint].position, speedplat * Time.deltaTime);
        }
    }
}
