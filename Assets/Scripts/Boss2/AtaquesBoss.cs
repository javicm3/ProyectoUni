using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtaquesBoss : MonoBehaviour
{
   
    public GameObject rayoVert;
    public GameObject rayoHoriz;
    public GameObject disparoLaser;
    public GameObject rayoDiagonal1;
    public GameObject rayoDiagonal2;
    public GameObject drones;
    public GameObject dronesFinal;
    public GameObject chispasLaseres1;
    public GameObject chispasLaseres2;

    //public GameObject[] chapasDestruibles;

    public Transform posRayoVert1;
    public Transform posRayoVert2;
    public Transform[] posicionesHorizontales;
    public Transform puntoDisparo;
    public Transform puntoDisparo2;

    public float speedRayosVert;
    public float tiempoSpawnRayoHorizontal;
    public float duracionRayoHorizontal;
    public float tiempoDisparos;
    public float desviacionDisparos;
    public float desviacionDiagonal = 30;
    public float velocidadDiagonal;
    public float tiempoAparicionDiagonales = 1;
    public float tiempoBarridoDiagonales = 3;
    public float tiempoTrasDash = 0.55f;

    public LayerMask layerM;
    
    GameObject vert1;
    GameObject vert2;
    GameObject dia1;
    GameObject horizontal1;
    GameObject player;
    GameObject bossVisual;

    public Material materialHorzOff;
    public Material materialHorzOn;
    
    public float numeroDisparos;
    float numDisparosAux = 0;
    Quaternion rotationLaser1;
    Quaternion rotationLaser2;
    float duracionDiagonales;
    bool diagonales;
    bool pillarDireccionDiagonal;
    Vector3 direccionLaser;
    Vector3 direccionLaser2;
    Quaternion rot1;
    Quaternion rot2;
    EstadosBoss2 eb;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
        duracionDiagonales = 0;
        drones.SetActive(false);
        dronesFinal.SetActive(false);
        pillarDireccionDiagonal = true;
        eb = GetComponent<EstadosBoss2>();
        animator = gameObject.GetComponent<Animator>();
        //for (int i = 0; i < chapasDestruibles.Length; i++)
        //{
        //    chapasDestruibles[i].GetComponent<BoxCollider2D>().enabled = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    vert1 = Instantiate(rayoVert, posRayoVert1.position, Quaternion.identity);
        //    vert2 = Instantiate(rayoVert, posRayoVert2.position, Quaternion.identity);

        //}
        //if(vert1 != null || vert2 != null)
        //{
        //    vert1.transform.position = Vector3.MoveTowards(vert1.transform.position, posRayoVert2.position, speedRayosVert);
        //    vert2.transform.position = Vector3.MoveTowards(vert2.transform.position, posRayoVert1.position, speedRayosVert);
        //    if (vert1.transform.position == posRayoVert2.position)
        //    {
        //        Destroy(vert1);
        //    }
        //    if (vert2.transform.position == posRayoVert1.position)
        //    {
        //        Destroy(vert2);
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    StartCoroutine(LaserHorizontal());

        //}
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    StartCoroutine(DisparosBoss());
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    StartCoroutine(RayoDiagonal());
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    drones.SetActive(true);
        //}

        //vert1.transform.position = Vector3.MoveTowards(vert1.transform.position, posRayoVert2.position, speedRayosVert);
        //vert2.transform.position = Vector3.MoveTowards(vert2.transform.position, posRayoVert1.position, speedRayosVert);
        //Destroy(vert1, 8);
        //Destroy(vert2, 8);
        
        if (vert1 != null || vert2 != null)
        {
            vert1.transform.position = Vector3.MoveTowards(vert1.transform.position, posRayoVert2.position, speedRayosVert);
            vert2.transform.position = Vector3.MoveTowards(vert2.transform.position, posRayoVert1.position, speedRayosVert);
            //if (vert1.transform.position == posRayoVert2.position)
            //{               
            //    eb.ataqueTerminado = true;
            //    Destroy(vert1);
                
            //}
            //if (vert2.transform.position == posRayoVert1.position)
            //{                
            //    eb.ataqueTerminado = true;
            //    Destroy(vert2);
            //}
        }

        UpdateDiagonalLaser();
    }
    public void LaserVertical()
    {
        animator.SetBool("atacando", false);
        animator.SetBool("stun", false);

        vert1 = Instantiate(rayoVert, posRayoVert1.position, Quaternion.identity);
        vert2 = Instantiate(rayoVert, posRayoVert2.position, Quaternion.identity);
        NewAudioManager.Instance.Play("LaserBossFinal");
        //Destroy(vert1, 8);
        //Destroy(vert2, 8);
    }
    public void SeleccionarLaserHorizontal()
    {
        int elegido = -1;
        for(int i = 0; i < posicionesHorizontales.Length; i++)
        {
            if(elegido == -1)
            {
                elegido = 0;
            }
            else if(Mathf.Abs(posicionesHorizontales[i].position.y - player.transform.position.y) < Mathf.Abs(posicionesHorizontales[elegido].position.y - player.transform.position.y))
            {
                elegido = i;
            }
        }
        StartCoroutine(LaserHorizontal(elegido));
    }
    public IEnumerator LaserHorizontal(int i)
    {      
        animator.SetBool("atacando", false);
        animator.SetBool("stun", false);

        //horizontal1 = Instantiate(rayoHoriz, posicionesHorizontales[Random.Range(0, posicionesHorizontales.Length - 1)]);
        horizontal1 = Instantiate(rayoHoriz, posicionesHorizontales[i]);
        horizontal1.GetComponent<LineRenderer>().material = materialHorzOff;
        horizontal1.GetComponent<BoxCollider2D>().enabled = false;
        NewAudioManager.Instance.Play("AvisoLaserHorizontal");
        yield return new WaitForSeconds(tiempoSpawnRayoHorizontal);
        horizontal1.GetComponent<BoxCollider2D>().enabled = true;
        horizontal1.GetComponent<LineRenderer>().material = materialHorzOn;
        NewAudioManager.Instance.Play("LaserHorizontalBoss");
        yield return new WaitForSeconds(duracionRayoHorizontal);
        Destroy(horizontal1);

        eb.ataqueTerminado = true;
        print("ffff");
        eb.acumulacion++;
       
    }
    public IEnumerator DisparosBoss()
    {
        if (numDisparosAux < numeroDisparos)
        {
            GameObject disparo1;
            GameObject disparo2;

            animator.SetBool("atacando", true);
            animator.SetBool("stun", false);

            //primer rayo dirigido
            disparo1 = Instantiate(disparoLaser, puntoDisparo.position, Quaternion.identity);
            NewAudioManager.Instance.Play("BossRanaDisparo");
            disparo1.transform.parent = null;
            Vector3 direccionLaser = (player.transform.position - puntoDisparo.position).normalized;
            float angle = Mathf.Atan2(direccionLaser.y, direccionLaser.x) * Mathf.Rad2Deg;
            disparo1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return new WaitForSeconds(tiempoDisparos);

            //segundo rayo random
            disparo2 = Instantiate(disparoLaser, puntoDisparo);
            NewAudioManager.Instance.Play("BossRanaDisparo");
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
            eb.ataqueTerminado = true;
            StartCoroutine(ParadaBoss());
        }
        
    }
    public IEnumerator LaserDiagonal(bool terminado)
    {
        NewAudioManager.Instance.Play("LaserBossFinal");
        animator.SetBool("atacando", true);
        animator.SetBool("stun", false);

        rayoDiagonal1.SetActive(true);
        rayoDiagonal2.SetActive(true);
        diagonales = false;

        direccionLaser = player.transform.position - puntoDisparo.position;
        float angle = Mathf.Atan2(direccionLaser.y, direccionLaser.x) * Mathf.Rad2Deg;

        direccionLaser2 = player.transform.position - puntoDisparo2.position;
        float angle2 = Mathf.Atan2(direccionLaser2.y, direccionLaser2.x) * Mathf.Rad2Deg;

        rotationLaser1.eulerAngles = new Vector3(0, 0, angle + desviacionDiagonal);
        rotationLaser2.eulerAngles = new Vector3(0, 0, angle2 - desviacionDiagonal);

        yield return new WaitForSeconds(tiempoAparicionDiagonales);
        duracionDiagonales = 0;
        diagonales = true;
        
        yield return new WaitForSeconds(tiempoBarridoDiagonales);
        diagonales = false;
        terminado = true;
        eb.acumulacion++;
        eb.ataqueTerminado = true;
        NewAudioManager.Instance.Stop("LaserBossFinal");
        rayoDiagonal1.SetActive(false);
        rayoDiagonal2.SetActive(false);
        pillarDireccionDiagonal = true;
    }
    void UpdateDiagonalLaser()
    {
        if (rayoDiagonal1.gameObject.activeSelf)
        {
            chispasLaseres1.SetActive(true);
            chispasLaseres2.SetActive(true);
            RaycastHit2D hit = Physics2D.Raycast(puntoDisparo.position, rayoDiagonal1.transform.right, Mathf.Infinity, layerM);
            rayoDiagonal1.GetComponent<LineRenderer>().SetPosition(0, puntoDisparo.position);
            if (hit)
            {
                rayoDiagonal1.GetComponent<LineRenderer>().SetPosition(1, hit.point);
                chispasLaseres1.transform.position = hit.point;
                chispasLaseres1.transform.rotation = Quaternion.LookRotation(hit.normal, chispasLaseres1.transform.up);
            }

            RaycastHit2D hit2 = Physics2D.Raycast(puntoDisparo2.position, rayoDiagonal2.transform.right, Mathf.Infinity, layerM);
            rayoDiagonal2.GetComponent<LineRenderer>().SetPosition(0, puntoDisparo2.position);
            if (hit2)
            {
                rayoDiagonal2.GetComponent<LineRenderer>().SetPosition(1, hit2.point);
                chispasLaseres2.transform.position = hit2.point;
                chispasLaseres2.transform.rotation = Quaternion.LookRotation(hit2.normal, chispasLaseres2.transform.up);
            }
            if (hit2.collider.tag == "Player" || hit.collider.tag == "Player")
            {
                if(player != null && player.GetComponent<ControllerPersonaje>().auxCdDashAtravesar > 0.2f)
                {

                    //player.gameObject.GetComponent<VidaPlayer>().RecibirDaño(player.gameObject.GetComponent<VidaPlayer>().dañoColliderMuerte, hit2.point, player.transform.position);
                    
                }
                else
                {
                    SceneManager.LoadScene("Nivel_12_BossFinal");
                }
            }


            rayoDiagonal1.transform.rotation = rotationLaser1;
            rayoDiagonal2.transform.rotation = rotationLaser2;
            if (diagonales)
            {
                MoverDiagonales();
            }
            
        }
        else
        {
            chispasLaseres1.SetActive(false);
            chispasLaseres2.SetActive(false);
        }
    }
    void MoverDiagonales()
    {
        if (pillarDireccionDiagonal)
        {
            rot1 = rotationLaser1;
            rot2 = rotationLaser2;
            pillarDireccionDiagonal = false;
        }
        rotationLaser1 = Quaternion.Slerp(rotationLaser1, rot2, Time.deltaTime * velocidadDiagonal);
        rotationLaser2 = Quaternion.Slerp(rotationLaser2, rot1, Time.deltaTime * velocidadDiagonal);
        
    }
    IEnumerator ParadaBoss()
    {
        animator.SetBool("stun", true);
        animator.SetBool("atacando", false);

        //for (int i = 0; i < chapasDestruibles.Length; i++)
        //{
        //    chapasDestruibles[i].GetComponent<BoxCollider2D>().enabled = true;
        //}
        eb.bossStuneado = true;
        eb.bossActivo = false;
        yield return new WaitForSeconds(eb.tiempoParadaActual);

        //for (int i = 0; i < chapasDestruibles.Length; i++)
        //{
        //    chapasDestruibles[i].GetComponent<BoxCollider2D>().enabled = false;
        //}
        eb.bossStuneado = false;
        eb.acumulacion = 0;
        eb.bossActivo = true;
    }
    
}
