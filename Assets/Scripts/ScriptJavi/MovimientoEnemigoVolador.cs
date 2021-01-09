using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigoVolador : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform[] puntosPersonaje;
    bool arrived = false;
    int indexArray;

    [SerializeField] float tiempoEspera;


    // Start is called before the first frame update
    void Start()
    {
        indexArray = 0;
       
    }

    // Update is called once per frame
    void Update()
    {

      
        if (Vector3.Distance(this.transform.position, puntosPersonaje[indexArray].position) > 1 && !arrived)
        {
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(tiempoEspera);
        arrived = false;
    }

 



 
}


