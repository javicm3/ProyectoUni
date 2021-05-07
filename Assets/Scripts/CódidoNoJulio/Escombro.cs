using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escombro : MonoBehaviour
{
    //setear gravedad a 4 poniendo el rb en dynamic

    Rigidbody2D rb;
    public GameObject particulas;
    public string tagCollider;
    [Header("Vibracion Boss")]
    public float intensidadVibracionBoss = 0.10f;
    public float velocidadVibracion = 1f;
    public float tiempoVibracion = 0.25f;
    Animator anim;
    public float tiempoDestruir = 3f;
    public float auxTiempoDestruir = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == tagCollider)
        {
            rb.isKinematic = false;
            //CinemachineShake.Instance.ShakeCamera(intensidadVibracionBoss, velocidadVibracion, tiempoVibracion);
            particulas.SetActive(false);
            auxTiempoDestruir = tiempoDestruir;
            anim.SetTrigger("Off");

            if (GetComponent<Rigidbody2D>() != null) 
            {
                if(GetComponent<Rigidbody2D>().velocity.y < 1f)
                {

                    //GetComponent<Rigidbody2D>().isKinematic = true;

                    NewAudioManager.Instance.Play("CaidaEscombros");
                }
            }
            else
            {
                if (GetComponentInParent<Rigidbody2D>() != null)
                {
                    if (GetComponentInParent<Rigidbody2D>().velocity.y < 1f)
                    {

                        //GetComponentInParent<Rigidbody2D>().isKinematic = true;

                        NewAudioManager.Instance.Play("CaidaEscombros");
                    }
                }
            }


        }
    }

    //void OnCollisionEnter2D (Collision2D col)
    //{

    //}
    // Update is called once per frame
    void Update()
    {
        if (auxTiempoDestruir > 0 && SceneManager.GetActiveScene().name != "Nivel_4_Boss1")
        {
            auxTiempoDestruir -= Time.deltaTime;
            if (auxTiempoDestruir <= 0)
            {
                //NewAudioManager.Instance.Play("CaidaEscombroRoto");
                Destroy(this.transform.parent.gameObject);
            }
        }
    }
}
