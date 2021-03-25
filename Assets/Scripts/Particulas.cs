using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particulas : MonoBehaviour
{
    public GameObject particulasBounce;
    public GameObject particulasSalto;
    public GameObject particulasWallJump;
    public GameObject particulasDash;
    public GameObject particulasDashCaida;
    public GameObject particulasvelMax;
    public GameObject particulasvelMax2;
    public GameObject particulasMuerte;
    public GameObject particulasViajeCables;
    public GameObject particulasCaida;
    public GameObject particulasDaño;
    public GameObject particulasDobleSalto;
    public GameObject particulasDobleSaltoInvertido;
    public GameObject particulasBolaViajeCables;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SpawnParticulas(GameObject particulas,Vector3 posicion, Transform parent)
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
        else if (particulas == particulasDobleSalto)
        {
            if (this.GetComponent<PlayerInput>().personajeInvertido == false)
            {
                GameObject part = Instantiate(particulasDobleSalto, posicion, Quaternion.identity, parent);
            }
            else
            {
                GameObject part = Instantiate(particulasDobleSaltoInvertido, posicion, Quaternion.identity, parent);
            }

        }
        else if (particulas == particulasvelMax)
        {
           GameObject part= Instantiate(particulas, posicion, Quaternion.identity);
            part.gameObject.SetActive(true);

        }
        else if (particulas == particulasvelMax2)
        {
            GameObject part = Instantiate(particulas, posicion, Quaternion.identity);
            part.gameObject.SetActive(true);
        }
        else if (parent == null)
        {
            Instantiate(particulas, posicion, Quaternion.identity);
        }
        else
        {
            Instantiate(particulas, posicion, Quaternion.identity, parent);
        }

    }
    public void SpawnParticulasSinTransform(GameObject particulas, Vector3 posicion)
    {
       
        Instantiate(particulas, posicion, Quaternion.identity);
        particulas.transform.parent = null;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
