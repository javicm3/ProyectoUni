using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorRebote : MonoBehaviour
{
    public CharacterController2D cc;
    public Rebote rebote;
    public bool recentbounce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ResetBounce()
    {
        recentbounce = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Bouncer")
        {
            this.GetComponentInParent<CharacterController2D>().puedomoverme = true;
            this.GetComponentInParent<Movimiento>().cayendoS = false;
            recentbounce = true;
            Invoke("ResetBounce", 0.5f);
            GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasBounce,collision.gameObject.transform.position);
            GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasBounce, collision.gameObject.transform.position);
            cc.anim.SetTrigger("Bounce");
            cc.BouncePlayer();
           rebote.puedoParry = true;
        }

        //if (collision.gameObject.tag == "BouncerDireccion")
        //{
        //    this.GetComponentInParent<CharacterController2D>().puedomoverme = true;
        //    this.GetComponentInParent<Movimiento>().cayendoS = false;
        //    recentbounce = true;
        //    Invoke("ResetBounce", 0.5f);
        //    GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasBounce, collision.gameObject.transform.position);
        //    GetComponentInParent<Particulas>().SpawnParticulas(GetComponentInParent<Particulas>().particulasBounce, collision.gameObject.transform.position);
        //    cc.anim.SetTrigger("Bounce");
        //    cc.BounceDireccionPlayer(collision.gameObject.transform.position);
        //    rebote.puedoParry = true;
        //}

    }
}
