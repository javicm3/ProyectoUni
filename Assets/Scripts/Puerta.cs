using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    bool activado = false;
    float auxY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activado == true)
        {

            if ( this.transform.position.y<auxY+2) {

                this.transform.position += Vector3.up * (0.33f * Time.deltaTime);
            }
              
            
        }   
    }
    public void Activar()
    {
        auxY = this.transform.position.y;
        activado = true;
        GetComponent<AudioSource>().Play();
    }
}
