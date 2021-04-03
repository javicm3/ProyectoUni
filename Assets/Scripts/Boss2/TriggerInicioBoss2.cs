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
            StartCoroutine(InicioBoss());
        }
    }
    IEnumerator InicioBoss()
    {
        if(cb.primeravez == true)
        {
            StartCoroutine(cb.Encendiendo());
        }
        yield return new WaitForSeconds(tiempoInicio/2);
        //triggerNormal.SetActive(true);
        //triggerInicioCamara.SetActive(false);
        yield return new WaitForSeconds(tiempoInicio/2);
        boss.GetComponent<EstadosBoss2>().bossActivo = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
