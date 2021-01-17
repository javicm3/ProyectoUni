using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ParticulasBoss : MonoBehaviour
{
    public GameObject particulas;
    public bool activarParticulas;

    [Header ("Vibracion Boss")]
    public float intensidadVibracionBoss = 0.30f;
    public float velocidadVibracion = 1.3f;
    public float tiempoVibracion = 0.45f;
    // Start is called before the first frame update
    void Start()
    {
        //particulas = GameObject.FindInChild
        activarParticulas = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activarParticulas == true)
        {
            particulas.SetActive(true);
            CinemachineShake.Instance.ShakeCamera(intensidadVibracionBoss, velocidadVibracion, tiempoVibracion);
            //GameObject.FindObjectOfType<CinemachineVirtualCamera>().GetComponent<CinemachineShake>().ShakeCamera(0.35f, 1.4f);
            particulas.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            //particulas.GetComponent<ParticleSystem>().Stop();
            //GameObject.Find("CINEMACHINE").GetComponent<CinemachineShake>().StopShake();
            //GameObject.FindObjectOfType<CinemachineVirtualCamera>().GetComponent<CinemachineShake>().StopShake();
            particulas.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Boss")
        {
            activarParticulas = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.gameObject.tag == "Boss")
        {
            activarParticulas = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            activarParticulas = false;
        }
    }
}
