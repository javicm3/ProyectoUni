using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatf : MonoBehaviour
{
    public Transform[] puntos;

    int actualPoint;
    public float speedplat;
    public float waitingTime;
   public  float auxwaitingTime;
    public float startwaitingTime;
    public float auxStartWait;
    int puntoRandom;
    public bool movHoriz = false;
    public bool movVertic = false;
    // Start is called before the first frame update
    void Start()
    {
        actualPoint = 0;
        auxwaitingTime = waitingTime;
        auxStartWait = startwaitingTime;

    }
    void Mover()
    {
          if (movHoriz)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(puntos[actualPoint].position.x, this.transform.position.y), speedplat * Time.deltaTime);
                    }
                    else if (movVertic)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(this.transform.position.x, puntos[actualPoint].position.y), speedplat * Time.deltaTime);
                    }
    }
    // Update is called once per frame
    void Update()
    {

        auxStartWait -= Time.deltaTime;
        
        if (movHoriz == true)
        {
            if (Vector2.Distance(this.transform.position, new Vector2(puntos[actualPoint].position.x, this.transform.position.y)) < 0.5f)
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

                if (auxStartWait < 0)
                {


                    Mover();

                }
            }
        }
        else if( movVertic==true)
        {
            if (Vector2.Distance(this.transform.position, new Vector2(this.transform.position.x, puntos[actualPoint].position.y)) < 0.5f)
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

                if (auxStartWait < 0)
                {
                    Mover();



                }
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<ControllerPersonaje>() != null)
        {
            print("1");
            if (collision.gameObject.transform.parent == null)
            {
                print("12");

                collision.gameObject.transform.parent = this.transform;
            }
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.GetComponent<ControllerPersonaje>() != null)
        {
            print("1");
            if (collision.gameObject.transform.parent == null)
            {
                print("12");

                collision.gameObject.transform.parent = this.transform;
            }
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<ControllerPersonaje>() != null)
        {
            if (collision.gameObject.transform.parent != null )
            {
                collision.gameObject.transform.parent = null;
            }
        }
    }
}
