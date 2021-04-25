using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasEnemigo : MonoBehaviour
{
    public GameObject arenaPies;
    public GameObject pies;
    public Transform enemigoEmbestida;
    public bool girar;
    EnemigoEmbestida2 script;
    public AudioClip ataqueEnemigo;
    public AudioClip pisadaEnemigo;

    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        script = GetComponentInParent<EnemigoEmbestida2>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ParticulasPies()
    {
        Instantiate(arenaPies, pies.transform.position, pies.transform.rotation, enemigoEmbestida);
        if (script.mirandoDerecha == true)
        {
            arenaPies.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            arenaPies.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void PisadaEnemigo()
    {
        //source.PlayOneShot(pisadaEnemigo);
        NewAudioManager.Instance.Play("EnemigoEmbestidaPisada");
    }
    public void AtaqueEnemigo()
    {
        source.PlayOneShot(ataqueEnemigo);
    }
}
