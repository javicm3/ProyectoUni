using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablesBoss2 : MonoBehaviour
{

    bool activo;

    // Start is called before the first frame update
    void Start()
    {
        activo = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDash > 0.2f)
            {
                if (activo == false)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponentInParent<EstadosBoss2>().brazosCortados++;
                    Destroy(this);
                    activo = true;
                }  
            }
        }
    }
}
