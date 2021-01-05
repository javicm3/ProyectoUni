using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatRotatoria : MonoBehaviour
{
 



       public float auxtiempoEntreGiros = 2f;
    public float tiempoentreGiros = 2f;
    public float velocidadRotacion = 30f;
    float rotacioninicialZ;
    float aux;
    public AudioClip clip;
    public AudioSource source;
    public GameObject player;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<ControllerPersonaje>().gameObject;
        rotacioninicialZ = this.transform.rotation.z;
        aux = rotacioninicialZ;
        source = this.GetComponent<AudioSource>();
        source.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {

        if (tiempoentreGiros >= auxtiempoEntreGiros)
        {
            //rotacioninicial = this.transform.rotation;
            //this.transform.rotation = this.transform.rotation + new Quaternion(0, 0, 5, 0);
            if (aux < rotacioninicialZ + 180)
            {
                this.transform.Rotate(0, 0, rotacioninicialZ + velocidadRotacion);

                GetComponent<AudioSource>().PlayOneShot(clip);
                //StartCoroutine(PararAudio(0.3f));
                aux += velocidadRotacion;
            }
            else
            {
                GetComponent<AudioSource>().Stop();
                aux -= 180;
                auxtiempoEntreGiros += tiempoentreGiros;
            }



        }
        else {
            GetComponent<AudioSource>().Stop();
            auxtiempoEntreGiros -= Time.deltaTime;
        }

    }
    public IEnumerator PararAudio(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
       
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.name == player.gameObject.name)
        {
            collision.transform.SetParent(this.transform.parent, true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == player.gameObject.name)
        {
            collision.transform.parent = null;
        }
    }
}

