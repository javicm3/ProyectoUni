using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInicioBoss2 : MonoBehaviour
{
    public GameObject boss;
    public float tiempoInicio;
    public GameObject triggerInicioCamara;
    public GameObject triggerNormal;
    CinematicaBoss cb;
    // Start is called before the first frame update
    void Start()
    {
        //triggerNormal.SetActive(false);
        cb = boss.GetComponent<CinematicaBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<BoxCollider2D>().enabled = false;

            if (GameManager.Instance.cinematicaVistaBossFinal == false)
            {
                StartCoroutine(InicioBoss());
            }
            else {
                StartCoroutine(InicioSinCinematica());
            }
           
           
        }
    }
    IEnumerator InicioBoss()
    {
        StartCoroutine(cb.Encendiendo());
        yield return new WaitForSeconds(tiempoInicio);
        boss.GetComponent<EstadosBoss2>().bossActivo = true;
        GameManager.Instance.cinematicaVistaBossFinal = true;
        //triggerNormal.SetActive(true);
        //triggerInicioCamara.SetActive(false);

    }
    IEnumerator InicioSinCinematica() {

        yield return new WaitForSeconds(tiempoInicio / 4);
        boss.GetComponent<EstadosBoss2>().bossActivo = true;
    }
}
