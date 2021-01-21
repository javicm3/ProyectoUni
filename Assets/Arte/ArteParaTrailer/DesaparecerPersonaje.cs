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
    // Start is called before the first frame update
    void Start()
    {
        cuerpo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > tiempoDeCambio)
        {

            cuerpo.SetActive(true);
        }
        if (timer > 15f)
        {
            if(Vector2.Distance(transform.position, puntoOrdenador.position) >.5f)
            this.gameObject.transform.Translate(new Vector3 (puntoOrdenador.position.x,0,0) * 0.3f * Time.deltaTime);
        }
        if (/*transform.position == new Vector3(puntoOrdenador.position.x, transform.position.y, transform.position.z)*/ timer > 19.5f)
        {
            PersonajeTrailerAnim.SetTrigger("Idle");
        }
    }
}
