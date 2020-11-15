using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAux : MonoBehaviour
{
    public AudioSource source;
    public AudioClip pisada;
    public AudioClip escalada;

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
        if (GameObject.FindObjectOfType<Movimiento>().maxSpeed == true)
        {
            GameObject.FindObjectOfType<Particulas>().SpawnParticulas(GameObject.FindObjectOfType<Particulas>().particulasBounce, GameObject.FindObjectOfType<CharacterController2D>().m_GroundCheck.position);
        }
    }
    public void SonidoCaminar()
    {
        source.PlayOneShot(pisada);
    }
    public void SonidoEscalar()
    {
        source.PlayOneShot(escalada);
    }
    public void ParticulasAlCaer()
    {
        GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasCaida, GetComponentInParent<ControllerPersonaje>().posGround.position);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
