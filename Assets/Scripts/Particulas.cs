using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particulas : MonoBehaviour
{
    public GameObject particulasBounce;
    public GameObject particulasSalto;
    public GameObject particulasWallJump;
    public GameObject particulasDash;
    public GameObject particulasvelMax;
    public GameObject particulasvelMax2;
    public GameObject particulasExplosion;
    public GameObject particulasViajeCables;
    public GameObject particulasCaida;
    public GameObject particulasDaño;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SpawnParticulas(GameObject particulas,Vector3 posicion)
    {
        if (particulas == particulasDash)
        {
            if (this.GetComponent<PlayerInput>().personajeInvertido == false)
            {
                GameObject part = Instantiate(particulas, posicion, Quaternion.Euler(0, 0, 90), this.GetComponentInChildren<AnimAux>().gameObject.transform);
            }
            else
            {
                GameObject part = Instantiate(particulas, posicion, Quaternion.Euler(0, 0, -90), this.GetComponentInChildren<AnimAux>().gameObject.transform);
            }

        }
        else
        {
            Instantiate(particulas, posicion, Quaternion.identity);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
