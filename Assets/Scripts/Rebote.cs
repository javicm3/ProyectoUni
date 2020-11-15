using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebote : MonoBehaviour
{
    public GameObject HijoColliderRebote;
    public CharacterController2D cc;
    public float parryCooldown;
    public float duracionParry = 0.2f;
    float aux = 0;
    bool activo = false;
    float auxparrycd;
    public bool puedoParry = false;
    // Start is called before the first frame update
    void Start()
    {
        aux = duracionParry;
    }

    // Update is called once per frame
    void Update()
    {/*aux -= Time.deltaTime;*/
        if (activo == true)
        {
            if (HijoColliderRebote != null) HijoColliderRebote.SetActive(true);

            //if (aux <= 0)
            //{
            //    aux = 0;
            //    activo = false;
            //    puedoParry = true;
            //}
            //if (auxparrycd > 0)
            //{
            //    auxparrycd -= Time.deltaTime;
            //}else
            //{
            //    auxparrycd = 0;
            //}
            //if (Input.GetKeyDown(KeyCode.E)){
            //    if (auxparrycd <= 0.05)
            //    {
            //        auxparrycd += parryCooldown;
            //        ActivarHijo();
            //    }
            //}

        }
        else
        {
            if (HijoColliderRebote != null) HijoColliderRebote.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if ((cc.Grounded == false) && (cc.pegadoPared != true))
            {

                activo = true;

            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (activo == true) activo = false;

            puedoParry = true;
        }
    }

    void ActivarHijo()
    {
        HijoColliderRebote.SetActive(true);
        StartCoroutine(Desact(duracionParry));
    }
    IEnumerator Desact(float time)
    {
        yield return new WaitForSeconds(time);

    }
}

