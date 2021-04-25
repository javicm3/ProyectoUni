using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoBoss1 : MonoBehaviour
{
    public bool dentroDeTierra = false;
    public float tiempoTierra = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        print(LayerMask.LayerToName(collision.gameObject.layer) + "layer");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (dentroDeTierra == false)
            {
                dentroDeTierra = true;
                NewAudioManager.Instance.Play("BossTierra");
            }
            print("dentro");
            tiempoTierra += Time.deltaTime;
        }
        else
        {
            if (dentroDeTierra == true)
            {
                if (tiempoTierra > 1)
                {
                    NewAudioManager.Instance.Stop("BossTierra");
                    NewAudioManager.Instance.Play("BossSalir");
                    dentroDeTierra = false;
                    print("SALGO");
                }
            }
        }
    }
}
