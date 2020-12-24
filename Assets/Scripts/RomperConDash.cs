using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomperConDash : MonoBehaviour
{
    public GameObject particulas;
    public Transform puntoParticulas;
    public AudioClip rompersonido;

    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if (collision.gameObject.GetComponent<Movimiento>().cayendoS == true)
            //{
            //    //collision.gameObject.GetComponent<Movimiento>().cayendoS = false;
            //    //Destroy(this.gameObject);
            //    GetComponentInParent<Animator>().SetTrigger("HaDasheado");
            //    //StartCoroutine(Romperse());
            //    GetComponent<Collider2D>().enabled = false;
            //    Instantiate(particulas, puntoParticulas);
            //}
        }   
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if (collision.gameObject.GetComponent<Movimiento>().cayendoS == true)
            //{
            //    //collision.gameObject.GetComponent<Movimiento>().cayendoS = false;
            //    //Destroy(this.gameObject);
            //    GetComponentInParent<Animator>().SetTrigger("HaDasheado");
            //    //StartCoroutine(Romperse());
            //    GetComponent<Collider2D>().enabled = false;
            //    Instantiate(particulas, puntoParticulas);
            //}
        }
    }
    IEnumerator Romperse()
    {
        yield return new WaitForSeconds(0.08f);
        GetComponent<Collider2D>().enabled = false;
        source.PlayOneShot(rompersonido);
    }
}
