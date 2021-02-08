using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPath : MonoBehaviour
{
    [SerializeField] private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector2 catPosition;
    Quaternion catRotation;
    public Animator anim;
    GameObject elegido;
    bool rotado;
    GameObject player;
    public bool triggerPausa;
    public GameObject cuerpoGusano;
    //public float[] speedModifier;

    public Ruta cmpRuta;

    bool tienePausa = false;

    private bool coroutineAllowed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        routeToGo = 0;
        tParam = 0f;
        
        //speedModifier = 0.5f;
        coroutineAllowed = true;
        //anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        triggerPausa = true;
        cmpRuta = routes[routeToGo].gameObject.GetComponent<Ruta>();
        elegido = cmpRuta.gameObject;
        //if ()
            
        if(coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        anim.SetTrigger(cmpRuta.animacion);
        if(cmpRuta.pausa && cmpRuta.pausaTerminada == false)
        {
            Quaternion target = Quaternion.Euler(0, 0, cmpRuta.rotacion);
            cuerpoGusano.transform.rotation = Quaternion.Slerp(transform.rotation, target, 10000 * Time.deltaTime);
            StartCoroutine(EsperarPausa());
        }
        else if(cmpRuta.pausaConTrigger == true && cmpRuta.pausaTerminada == false)
        {
            //EsperarPausaConTrigger();
            Quaternion target = Quaternion.Euler(0, 0, cmpRuta.rotacion);
            cuerpoGusano.transform.rotation = Quaternion.Slerp(transform.rotation, target, 10000 * Time.deltaTime);
        }
        else if (cmpRuta.disparo)
        {
            cmpRuta.GetComponent<DisparosBOSS>().Disparar();
        }
        else
        {
            coroutineAllowed = false;

            Vector2 p0 = routes[routeNumber].GetChild(0).position;
            Vector2 p1 = routes[routeNumber].GetChild(1).position;
            Vector2 p2 = routes[routeNumber].GetChild(2).position;
            Vector2 p3 = routes[routeNumber].GetChild(3).position;

            Quaternion p0r = routes[routeNumber].GetChild(0).rotation;
            Quaternion p1r = routes[routeNumber].GetChild(1).rotation;
            Quaternion p2r = routes[routeNumber].GetChild(2).rotation;
            Quaternion p3r = routes[routeNumber].GetChild(3).rotation;
            while (tParam < 1)
            {
                tParam += Time.deltaTime * cmpRuta.speedModifier;

                catPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
                //catRotation = Mathf.Pow(1 - tParam, 3) * p0r + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1r + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2r + Mathf.Pow(tParam, 3) * p3r;

                transform.position = catPosition;
                //transform.rotation = Quaternion.LookRotation(cmpRuta.rotacion);
                Quaternion target = Quaternion.Euler(0, 0, cmpRuta.rotacion);
                cuerpoGusano.transform.rotation = Quaternion.Slerp(transform.rotation, target, 10000 * Time.deltaTime);

                //Vector3 rotacionGusano = Vector3.RotateTowards(transform.forward, Quaternion.Angle()
                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;

            routeToGo += 1;

            if (routeToGo > routes.Length - 1)
                routeToGo = 0; ;

            coroutineAllowed = true;
        }
        //if (elegido != cmpRuta.gameObject)
        //{
        //    rotado = true;
        //    if(rotado == true)
        //    {
        //        transform.Rotate(new Vector3(0, 0, cmpRuta.rotacion));
                
        //        elegido = cmpRuta.gameObject;
        //        rotado = false;
        //    }
        //}
    }
    private IEnumerator EsperarPausa()
    {
        //print("dejame trankilo");
        yield return new WaitForSeconds(cmpRuta.segundosPausa);
        //cmpRuta.pausa = false;
        cmpRuta.pausaTerminada = true;
        //coroutineAllowed = false;
    }
    //void EsperarPausaConTrigger()
    //{

    //    if (triggerPausa == false)
    //    {
    //        cmpRuta.pausaTerminada = true;
    //        cmpRuta.pausaConTrigger = false;
    //        triggerPausa = true;
    //    }
    //}
}
