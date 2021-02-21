using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquesBoss : MonoBehaviour
{
   
    public GameObject rayoVert;
    public GameObject rayoHoriz;
    public GameObject disparoLaser;

    public Transform posRayoVert1;
    public Transform posRayoVert2;
    public Transform[] posicionesHorizontales;
    public Transform puntoDisparo;

    public float speedRayosVert;
    public float tiempoSpawnRayoHorizontal;
    public float duracionRayoHorizontal;
    public float tiempoDisparos;
    public float desviacionDisparos;
    public float desviacionDiagonal = 30;
    GameObject vert1;
    GameObject vert2;
    GameObject dia1;
    GameObject horizontal1;

    public Material materialHorzOff;
    public Material materialHorzOn;
    GameObject player;
    public float numeroDisparos;
    float numDisparosAux = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            vert1 = Instantiate(rayoVert, posRayoVert1.position, Quaternion.identity);
            vert2 = Instantiate(rayoVert, posRayoVert2.position, Quaternion.identity);
            
        }
        if(vert1 != null || vert2 != null)
        {
            vert1.transform.position = Vector3.MoveTowards(vert1.transform.position, posRayoVert2.position, speedRayosVert);
            vert2.transform.position = Vector3.MoveTowards(vert2.transform.position, posRayoVert1.position, speedRayosVert);
            if (vert1.transform.position == posRayoVert2.position)
            {
                Destroy(vert1);
            }
            if (vert2.transform.position == posRayoVert1.position)
            {
                Destroy(vert2);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            StartCoroutine(RayoHorizontal());
           
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartCoroutine(DisparosBoss());
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StartCoroutine(RayoDiagonal());
        }
        //vert1.transform.Translate(Vector3.right * Time.deltaTime * speedRayosVert);
        //vert2.transform.Translate(Vector3.left * Time.deltaTime * speedRayosVert);


    }
    IEnumerator RayoHorizontal()
    {
        horizontal1 = Instantiate(rayoHoriz, posicionesHorizontales[Random.Range(0, posicionesHorizontales.Length - 1)]);
        horizontal1.GetComponent<LineRenderer>().material = materialHorzOff;
        yield return new WaitForSeconds(tiempoSpawnRayoHorizontal);
        horizontal1.GetComponent<LineRenderer>().material = materialHorzOn;
        yield return new WaitForSeconds(duracionRayoHorizontal);
        Destroy(horizontal1);
       
    }
    IEnumerator DisparosBoss()
    {
        if (numDisparosAux < numeroDisparos)
        {
            GameObject disparo1;
            GameObject disparo2;

            //primer rayo dirigido
            disparo1 = Instantiate(disparoLaser, puntoDisparo.position, Quaternion.identity);
            disparo1.transform.parent = null;
            Vector3 direccionLaser = (player.transform.position - puntoDisparo.position).normalized;
            //disparo1.transform.LookAt(player.transform.position);
            //Vector3 targetPos = player.transform.position;
            float angle = Mathf.Atan2(direccionLaser.y, direccionLaser.x) * Mathf.Rad2Deg;
            disparo1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //disparo1.transform.forward = Vector3.down + direccionLaser;
            yield return new WaitForSeconds(tiempoDisparos);

            //segundo rayo random
            disparo2 = Instantiate(disparoLaser, puntoDisparo);
            disparo2.transform.parent = null;
            direccionLaser = (player.transform.position - puntoDisparo.position).normalized;
            
            angle = Mathf.Atan2(direccionLaser.y, direccionLaser.x) * Mathf.Rad2Deg;
            angle = angle + Random.Range(-desviacionDisparos, desviacionDisparos);
            disparo2.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return new WaitForSeconds(tiempoDisparos);
            numDisparosAux++;
            StartCoroutine(DisparosBoss());
        }
        else
        {
            numDisparosAux = 0;
        }
        
    }
    IEnumerator RayoDiagonal()
    {
        dia1 = Instantiate(rayoVert, puntoDisparo.position, Quaternion.identity);
        //vert2 = Instantiate(rayoVert, posicionDisparo.position, Quaternion.identity);
        Vector3 direccionLaser = (player.transform.position - puntoDisparo.position).normalized;
        float angle1 = Mathf.Atan2(direccionLaser.y, direccionLaser.x) * Mathf.Rad2Deg;
        dia1.transform.rotation = Quaternion.AngleAxis(angle1 + desviacionDiagonal, Vector3.forward);
        
        RaycastHit2D hit = Physics2D.Raycast(puntoDisparo.position, dia1.transform.right, Mathf.Infinity, layerMask: 8);
        dia1.GetComponent<LineRenderer>().SetPosition(0, puntoDisparo.position);
        if(hit.collider != null)
        {
            dia1.GetComponent<LineRenderer>().SetPosition(1, hit.point);
        }
        
        yield return new WaitForSeconds(1);
    }
}
