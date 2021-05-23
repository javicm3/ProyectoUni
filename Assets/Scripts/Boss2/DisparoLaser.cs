using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoLaser : MonoBehaviour
{
    public float velocidadBalas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<ControllerPersonaje>().auxCdDashAtravesar > 0)
        {
            this.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            this.GetComponent<Collider2D>().enabled = true;
        }
        transform.Translate(Vector3.right * velocidadBalas * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
