using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesaparecerPersonaje : MonoBehaviour
{
    float timer = 0;
    [SerializeField] float tiempoDeCambio;
    public GameObject cuerpo;
    [SerializeField] Transform puntoOrdenador;
    [SerializeField] Animator PersonajeTrailerAnim;
    bool caminar;
    // Start is called before the first frame update
    void Start()
    {
        cuerpo.SetActive(false);
        caminar = true;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 punt = new Vector3(puntoOrdenador.position.x, transform.position.y, 0);
        timer += Time.deltaTime;
        if (timer > tiempoDeCambio)
        {

            cuerpo.SetActive(true);
        }
        if (timer > 2f && caminar)
        {
            transform.position = Vector3.MoveTowards(transform.position, punt, 2f * Time.deltaTime);

            //if (Vector2.Distance(transform.position, puntoOrdenador.position) > .5f)
            //{
            //    this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(puntoOrdenador.position.x, transform.position.y, 0), 2.5f * Time.deltaTime);
            //    //this.gameObject.transform.Translate(new Vector3(puntoOrdenador.position.x, 0, 0) * 0.3f * Time.deltaTime);
            //}
        }
        if (/*transform.position == new Vector3(puntoOrdenador.position.x, transform.position.y, transform.position.z)*/ timer > 7.8f && caminar)
        {
            caminar = false;
            PersonajeTrailerAnim.SetTrigger("Idle");
        }
    }
}
