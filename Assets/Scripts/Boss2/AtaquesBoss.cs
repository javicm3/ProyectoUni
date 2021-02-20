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
    GameObject vert1;
    GameObject vert2;
    GameObject horizontal1;
    Transform posicionDisparo;

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
            disparo1.transform.forward = direccionLaser;
            yield return new WaitForSeconds(tiempoDisparos);

            //segundo rayo random
            disparo2 = Instantiate(disparoLaser, puntoDisparo);
            disparo2.transform.parent = null;
            direccionLaser = ((player.transform.position + new Vector3(Random.Range(-3, 3), 0, 0)) - puntoDisparo.position).normalized;
            disparo2.transform.forward = direccionLaser;
            yield return new WaitForSeconds(tiempoDisparos);
            numDisparosAux++;
            StartCoroutine(DisparosBoss());
        }
        else
        {
            numDisparosAux = 0;
        }
    }
}
