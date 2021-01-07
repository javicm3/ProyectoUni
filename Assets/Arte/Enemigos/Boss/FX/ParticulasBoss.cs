using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasBoss : MonoBehaviour
{
    public GameObject particulas;
    bool activarParticulas;
    // Start is called before the first frame update
    void Start()
    {
        //particulas = GameObject.FindInChild
        activarParticulas = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activarParticulas == true)
        {
            particulas.SetActive(true);
            particulas.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            //particulas.GetComponent<ParticleSystem>().Stop();
            particulas.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Boss")
        {
            activarParticulas = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.gameObject.tag == "Boss")
        {
            activarParticulas = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            activarParticulas = false;
        }
    }
}
