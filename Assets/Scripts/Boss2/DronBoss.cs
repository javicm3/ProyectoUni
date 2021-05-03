using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DronBoss : EnemigoPadre
{
    public Transform cabeza;
    
    public GameObject pareja;
    public GameObject colliderBoss;
    
    LineRenderer lr;
    
    public int rangoProbabilidadLaser;
    
    public float velocidadMov;
    public float tiempoLaser = 1;
    public float tiempoAviso = 1;
    public LayerMask layer;
    float tmp = 0;
    float tmpParar;
    public float tiempoStop;
    [SerializeField]
    bool go;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Esperar());
        if (colliderBoss != null)
        {
            colliderBoss.SetActive(false);
        }
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        tmpParar = tiempoStop;
    }

    // Update is called once per frame
    protected override void Update()
    {
        tmpParar -= Time.deltaTime;
        if(tmpParar <= 0)
        {
            if(go == true)
            {
                tmpParar = tiempoStop;
                go = false;
            }
            else
            {
                go = true;
                tmpParar = tiempoStop * 2;
            }
            
        }
        //transform.LookAt(pareja.transform);
        tmp += Time.deltaTime;
        if(tmp >= 1)
        {
            int activar = Random.Range(0, rangoProbabilidadLaser);
            print(activar);
            if(activar == 0)
            {
                print("dale");
                StartCoroutine(LaserDron());
            }
            tmp = 0;
        }
        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, pareja.transform.position);

    }
    private void FixedUpdate()
    {
        if (go == true)
        {
            transform.RotateAround(cabeza.position, transform.forward, velocidadMov * Time.deltaTime);
        }
    }
    IEnumerator LaserDron()
    {
        lr.enabled = true;
        lr.material = GetComponentInParent<AtaquesBoss>().materialHorzOff;
             
        
        yield return new WaitForSeconds(tiempoAviso);
        lr.material = GetComponentInParent<AtaquesBoss>().materialHorzOn;
        
        float distancia = Vector3.Distance(transform.position, pareja.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, pareja.transform.position, distancia, layer);
        if (hit)
        {
            GetComponent<LineRenderer>().SetPosition(1, hit.point);
            SceneManager.LoadScene("Nivel_12_BossFinal");
        }
        yield return new WaitForSeconds(tiempoLaser);
        
        lr.enabled = false;
    }
    
    //IEnumerator Esperar()
    //{
    //    go = true;
    //    yield return new WaitForSeconds(1);
    //    go = false;
    //    yield return new WaitForSeconds(1);
    //    StartCoroutine(Esperar());
    //}
}
