using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAux : MonoBehaviour
{
    public AudioSource source;
    public AudioClip pisada;
    public AudioClip escalada;
    public Transform suelo;
    public Transform[] posicionesParticulasMuerte;

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
        FindObjectOfType<NewAudioManager>().Play("PlayerStep");
        //GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().Play(GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().sonidoPasos, GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>().pasos);
    }
    public void SonidoEscalar()
    {
        //source.PlayOneShot(escalada);
    }
    public void SonidoCaer()
    {
        FindObjectOfType<NewAudioManager>().Play("PlayerFall");
    }
    public void SonidoCaerDash()
    {
        FindObjectOfType<NewAudioManager>().Play("PlayerFallDash");
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
    public void ParticulasDobleSalto()
    {
            GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasDobleSalto, suelo.position, suelo);
    }
    public void ParticulasMuerte()
    {
        for(int i = 0; i < posicionesParticulasMuerte.Length; i++)
        {
            GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasMuerte, posicionesParticulasMuerte[i].position, posicionesParticulasMuerte[i].transform);
        }
            
    }
    public void DestelloMuerte()
    {
        GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().destelloMuerte, this.transform.position + new Vector3(0,1,0), this.transform);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
