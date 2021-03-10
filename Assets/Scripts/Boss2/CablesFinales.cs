using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CablesFinales : MonoBehaviour
{
    public GameObject tentaculoNormal;
    public GameObject tentaculoRoto1;
    public GameObject tentaculoRoto2;
    public GameObject target1;
    public GameObject target2;
    public float fuerza;
    GameObject player;
    public bool fuerzaDerecha = true;
    
    // Start is called before the first frame update
    void Start()
    {
        tentaculoNormal.SetActive(true);
        tentaculoRoto1.SetActive(false);
        tentaculoRoto2.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.GetComponent<ControllerPersonaje>().haciendoCombate == true && collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponentInParent<EstadosBoss2>().brazosCortados++;

            tentaculoNormal.SetActive(false);
            tentaculoRoto1.SetActive(true);
            tentaculoRoto2.SetActive(true);
                    
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

            Destroy(this);
        }
    }
}
