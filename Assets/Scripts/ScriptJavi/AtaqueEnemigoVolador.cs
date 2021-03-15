using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueEnemigoVolador : MonoBehaviour
{
    [SerializeField] GameObject bala;
    [SerializeField] float tiempoEntreDisparos;
    [SerializeField] float auxTiempo;
    [SerializeField] public float distanciaDisparo = 15f;
    [SerializeField] float tiempoComprobaciones = 1.5f;
    float auxComprobaciones;
    [SerializeField] public GameObject pos1;
    [SerializeField] public GameObject pos2;
    [SerializeField] GameObject player;
    [SerializeField] public float porcentajeDesvio = 2;
    [SerializeField] bool atacando;
    [SerializeField] float index = 0;
    MovimientoEnemigoVolador movEn;
    //Coroutine corrutinaLanzar;
    // Start is called before the first frame update
    void Start()
    {
        movEn = this.GetComponent<MovimientoEnemigoVolador>();
        auxTiempo = tiempoEntreDisparos;
        auxComprobaciones = tiempoComprobaciones;
        player = FindObjectOfType<ControllerPersonaje>().gameObject;
        //pos1 = this.transform.Find("PuntoDisparo").gameObject;
        //pos2 = transform.Find("PuntoDisparo2").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        auxComprobaciones -= Time.deltaTime;
        if (auxComprobaciones < 0)
        {


            if (Vector2.Distance(player.transform.position, this.transform.position) < distanciaDisparo)
            {
                atacando = true;
            }
            else
            {
                atacando = false;
            }

            auxComprobaciones = tiempoComprobaciones;
        }
        else
        {
          
        }

        if( (atacando)&&(movEn.stun==false))
        {
            auxTiempo -= Time.deltaTime;
            if (index == 0)
            {
                if (auxTiempo <= 0)
                {
                    auxTiempo += tiempoEntreDisparos;
                    GameObject balita = Instantiate(bala, pos1.transform.position,Quaternion.identity);
                    balita.GetComponent<BalaBoss>().objetivo = new Vector2(player.transform.position.x + Random.Range(-porcentajeDesvio, porcentajeDesvio), player.transform.position.y + Random.Range(-porcentajeDesvio, porcentajeDesvio));
                    index++;
                }

            }
            else
            {
                if (auxTiempo <= 0)
                {
                    auxTiempo += tiempoEntreDisparos;
                    GameObject balita = Instantiate(bala, pos2.transform.position, Quaternion.identity);
                    balita.GetComponent<BalaBoss>().objetivo = new Vector2(player.transform.position.x + Random.Range(-porcentajeDesvio, porcentajeDesvio), player.transform.position.y + Random.Range(-porcentajeDesvio, porcentajeDesvio));
                    index--;
                }
               

            }


        }
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
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    TerminarLanzar();
        //}
    }

    void EmpezarLanzar()
    {
        //if (corrutinaLanzar == null)
        //{
        //    corrutinaLanzar = StartCoroutine(LanzarProyectil());
        //}
    }

    void TerminarLanzar()
    {
        //StopCoroutine(LanzarProyectil());
        //corrutinaLanzar = null;
    }

    //IEnumerator LanzarProyectil()
    //{
    //    while (true)
    //    {
    //        Instantiate(bala, transform.position, transform.rotation);
    //        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.transform.position);
    //        //if (hit.transform.gameObject.CompareTag("Player"))
    //        //{
    //        //    Instantiate(bala, transform.position, transform.rotation);
    //        //    print("Dentro");

    //        //}

    //        //scriptbala.target = player
    //        yield return new WaitForSeconds(TiempoEntreDisparos);
    //    }
    //}
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDisparo);
    }
}
