using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatRotatoria : MonoBehaviour
{
 



       public float auxtiempoEntreGiros = 2f;
    public float tiempoentreGiros = 2f;
    public float velocidadRotacion = 30f;
    float rotacioninicialZ;
    float aux;
   
    // Start is called before the first frame update
    void Start()
    {
        rotacioninicialZ = this.transform.rotation.z;
        aux = rotacioninicialZ;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (tiempoentreGiros >= auxtiempoEntreGiros)
        {
            //rotacioninicial = this.transform.rotation;
            //this.transform.rotation = this.transform.rotation + new Quaternion(0, 0, 5, 0);
            if (aux < rotacioninicialZ + 180)
            {
                this.transform.Rotate(0, 0, rotacioninicialZ + velocidadRotacion);
                //GetComponent<AudioSource>().Play();
                StartCoroutine(PararAudio(0.3f));
                aux += velocidadRotacion;
            }
            else
            {
               
                aux -= 180;
                auxtiempoEntreGiros += tiempoentreGiros;
            }



        }
        else {
            auxtiempoEntreGiros -= Time.deltaTime;
        }

    }
    public IEnumerator PararAudio(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        //GetComponent<AudioSource>().Stop();
    }
}

