using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour
{
    bool activado = false;
    public GameObject objetoactivado;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("TAG" + collision.transform.tag);
        if (collision.gameObject.tag == "rayos")
        {
            if (activado == false) {
              if(objetoactivado.GetComponent < Puerta >()!=null) objetoactivado.GetComponent<Puerta>().Activar();
                //if (objetoactivado.GetComponent<PlataformaActivable>() != null)
                //{
                //    objetoactivado.GetComponent<PlataformaActivable>().pactivado = true;
                //   objetoactivado.GetComponentInChildren<SustituirSpritesActivados>().estoyact = true;
                //}
                //this.GetComponent<SustituirSpritesActivados>().estoyact = true;
                GetComponent<AudioSource>().Play();

                activado = true;}
        }
    }
}
