using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAux : MonoBehaviour
{
    public AudioSource source;
    public AudioClip pisada;
    public AudioClip escalada;
    public Transform suelo;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void FinishEdgeClimb()
    {
        //if (GameObject.FindObjectOfType<ViajeCables>().viajando == false)
        //{
            //GameObject.FindObjectOfType<CharacterController2D>().FinishLedgeClimb();
        //}

    }
    public void SpawnParticulas()
    {
        //if (GameObject.FindObjectOfType<Movimiento>().maxSpeed == true)
        //{
        //    GameObject.FindObjectOfType<Particulas>().SpawnParticulas(GameObject.FindObjectOfType<Particulas>().particulasBounce, GameObject.FindObjectOfType<CharacterController2D>().m_GroundCheck.position);
        //}
    }
    public void SonidoCaminar()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidoPasos, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().pasos);
    }
    public void SonidoEscalar()
    {
        source.PlayOneShot(escalada);
    }
    public void ParticulasAlCaer()
    {

        GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasCaida, GetComponentInParent<ControllerPersonaje>().posGround.position, GetComponentInParent<ControllerPersonaje>().posGround);
        
    }
    public void ParticulasCorrer(GameObject particulas)
    {
        if(GetComponentInParent<ControllerPersonaje>().grounded == true)
        {
            Instantiate(particulas, suelo.position, Quaternion.Euler(0, 0, 90));
        }
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
