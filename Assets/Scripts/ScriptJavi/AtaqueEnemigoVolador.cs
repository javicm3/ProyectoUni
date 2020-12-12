using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEnemigoVolador : MonoBehaviour
{
    [SerializeField] GameObject bala;
    [SerializeField] float TiempoEntreDisparos;
    bool atacando;
    Coroutine corrutinaLanzar;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            EmpezarLanzar();
        }
    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    Player = collision.gameObject.transform.position;
    //}
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TerminarLanzar();
        }
    }

    void EmpezarLanzar()
    {
        if (corrutinaLanzar == null)
        {
            corrutinaLanzar = StartCoroutine(LanzarProyectil());
        }
    }

    void TerminarLanzar()
    {
        StopCoroutine(LanzarProyectil());
        corrutinaLanzar = null;
    }

    IEnumerator LanzarProyectil()
    {
        while (true)
        {
            Instantiate(bala, transform.position, transform.rotation);
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.transform.position);
            //if (hit.transform.gameObject.CompareTag("Player"))
            //{
            //    Instantiate(bala, transform.position, transform.rotation);
            //    print("Dentro");

            //}

            //scriptbala.target = player
            yield return new WaitForSeconds(TiempoEntreDisparos);
        }
    }
}
