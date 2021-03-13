using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablesBoss2 : MonoBehaviour
{

    bool activo;
    public GameObject tentaculoNormal;
    public GameObject tentaculoRoto1;
    public GameObject tentaculoRoto2;
    public GameObject target1;
    public GameObject target2;
    public GameObject particulasExplosion;
    public float fuerza;
    GameObject player;
    public bool fuerzaDerecha = true;
    // Start is called before the first frame update
    void Start()
    {
        activo = false;
        tentaculoNormal.SetActive(true);
        tentaculoRoto1.SetActive(false);
        tentaculoRoto2.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentInParent<EstadosBoss2>().bossStuneado == true && activo == false)
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.2f )
            {
                if (activo == false)
                {
                    //GetComponent<SpriteRenderer>().enabled = false;
                    GetComponentInParent<EstadosBoss2>().brazosCortados++;
                    
                    tentaculoNormal.SetActive(false);
                    tentaculoRoto1.SetActive(true);
                    tentaculoRoto2.SetActive(true);
                    Instantiate(particulasExplosion, transform);
                    //particulasExplosion.transform.parent = null;
                   
                    GetComponentInParent<EstadosBoss2>().bossStuneado = false;
                    if (fuerzaDerecha == true)
                    {
                        target1.GetComponent<Rigidbody2D>().AddForce(Vector3.right * fuerza);
                        target2.GetComponent<Rigidbody2D>().AddForce(Vector3.right * fuerza);
                    }
                    else
                    {
                        target1.GetComponent<Rigidbody2D>().AddForce(Vector3.left * fuerza);
                        target2.GetComponent<Rigidbody2D>().AddForce(Vector3.left * fuerza);
                    }
                    activo = true;
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    //Destroy(this.gameObject);
                }  
            }
        }
    }
}
