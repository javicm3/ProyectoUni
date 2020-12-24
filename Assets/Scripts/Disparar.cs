using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
   // public GameObject prefabBala;
   // public GameObject puntoDisparo;
   // public GameObject bolitaDisparo;
   // public GameObject fogonazo;
   //public bool puedoDisparar = true;
   // public float tiempoCd = 3;
   //public float auxtiempo = 0;
   //public  LayerMask ChocanConDisparo;
   // public AudioClip disparoSonido;
  
   // public AudioSource source;
   // //EnergyManager em;
   // // Start is called before the first frame update
   // void Start()
   // {
   //     //em = this.GetComponent<EnergyManager>();
   //     //fogonazo.SetActive(false);

   // }
   // void Dispara()
   // {
   //     source.PlayOneShot(disparoSonido);
   //     this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
   //     //Instantiate(prefabBala, puntoDisparo.transform.position, Quaternion.identity);
   //     Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
   //     pz.z = 0;

   //     Vector3 direccion = (pz - this.transform.position).normalized;
   //     RaycastHit2D hit = Physics2D.Raycast(puntoDisparo.transform.position, direccion, 999, ChocanConDisparo);
   //     if (Physics2D.Raycast(puntoDisparo.transform.position, direccion, 999, ChocanConDisparo))
   //     {
   //         Debug.DrawRay(puntoDisparo.transform.position, direccion, Color.green, ChocanConDisparo);
   //         this.GetComponent<Particulas>().SpawnParticulas(this.GetComponent<Particulas>().particulasBounce, puntoDisparo.transform.position);
   //         this.GetComponent<Particulas>().SpawnParticulas(this.GetComponent<Particulas>().particulasBounce, puntoDisparo.transform.position);
   //         this.GetComponent<Particulas>().SpawnParticulas(this.GetComponent<Particulas>().particulasBounce, puntoDisparo.transform.position);
   //         this.GetComponent<Particulas>().SpawnParticulas(this.GetComponent<Particulas>().particulasBounce, hit.point);
   //         this.GetComponent<Particulas>().SpawnParticulas(this.GetComponent<Particulas>().particulasBounce, hit.point);
   //         this.GetComponent<Particulas>().SpawnParticulas(this.GetComponent<Particulas>().particulasBounce, hit.point);
   //         print(hit.collider.gameObject.name);
   //         if (hit.collider.gameObject.GetComponent<VidaEnemigo>() != null) hit.collider.gameObject.GetComponent<VidaEnemigo>().RecibirDaño((hit.collider.gameObject.GetComponent<VidaEnemigo>().dañoDisparo));
   //         if (hit.collider.gameObject.GetComponentInParent<Activador>() != null) hit.collider.gameObject.GetComponentInParent<Activador>().Activar();
   //         LineRenderer lr=GameObject.Find("RayoLaser").GetComponent<LineRenderer>();
   //         lr.enabled = true;
   //         lr.SetPosition(0, puntoDisparo.transform.position);
   //         lr.SetPosition(1, hit.point);
   //         StartCoroutine("Desact", 0.2f);
   //     }
   //     else
   //     {
   //         LineRenderer lr = GameObject.Find("RayoLaser").GetComponent<LineRenderer>();
   //         lr.enabled = true;
   //         lr.SetPosition(0, puntoDisparo.transform.position);
   //         lr.SetPosition(1, hit.point);
   //         StartCoroutine("Desact", 0.2f);
   //     }


   //     this.GetComponent<Rigidbody2D>().AddForce(-direccion * 2f, ForceMode2D.Impulse);
   // }
   // public IEnumerator Desact (float time)
   // {
   //     yield return new WaitForSeconds(time);
   //     GameObject.Find("RayoLaser").GetComponent<LineRenderer>().enabled = false;
   // }
   // // Update is called once per frame
   // void Update()
   // {
   //     if (puedoDisparar == false)
   //     {
   //         auxtiempo += Time.deltaTime;
   //         if (auxtiempo >= tiempoCd)
   //         {
   //             auxtiempo -= tiempoCd;
   //             puedoDisparar = true;
   //         }
   //     }
   //     if (puedoDisparar == true)
   //     {
   //         if (GameManager.Instance.puedoDisparar == true)
   //         {
               
   //             if (em.actualEnergy > em.energiaDisparo)
   //             { bolitaDisparo.GetComponent<SpriteRenderer>().enabled = true;


   //                 if (Input.GetKeyDown(KeyCode.Mouse0))
   //                 {
   //                     em.RestarEnergia(em.energiaDisparo);
   //                     puedoDisparar = false;
   //                     this.GetComponent<CharacterController2D>().anim.SetTrigger("Disparo");
   //                     //fogonazo.SetActive(true);
   //                     //StartCoroutine(FogonazoActivo());
   //                     Invoke("Dispara", 0.3f);
   //                 }
   //             }
   //             else bolitaDisparo.GetComponent<SpriteRenderer>().enabled = false;
   //         }
   //         else
   //         {
   //             bolitaDisparo.GetComponent<SpriteRenderer>().enabled = false;
   //         }

   //     }
   //     else
   //     {
   //         bolitaDisparo.GetComponent<SpriteRenderer>().enabled = false;
   //     }
   // }

   // private IEnumerator FogonazoActivo() 
   // { 
   //     yield return new WaitForSeconds (0.8f);
   //     fogonazo.SetActive(false);
   // }
}
