using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSaltamontesRespawn : MonoBehaviour
{
    public float pequeñosStun = 0;
    public Transform posicionInicio;
    public GameObject padreSaltamontes;
    public float tiempoTrasMatarPequeños = 2f;
    float auxtime;
  
    // Start is called before the first frame update
    void Start()
    {
       
        pequeñosStun = 0;
    }
    public void SumarPeq()
    {
        pequeñosStun += 1;
        if (pequeñosStun == 4)
        {
            auxtime = tiempoTrasMatarPequeños;
        }
    }
    void Reiniciar()
    {
        padreSaltamontes.gameObject.SetActive(true);
        pequeñosStun = 0;
        padreSaltamontes.transform.position = posicionInicio.position;
        padreSaltamontes.GetComponent<EnemigoSaltamontes>().stun = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (auxtime > 0)
        {
            auxtime -= Time.deltaTime;
            if (auxtime <= 0)
            {
                Reiniciar();
                auxtime = 0;
            }
        }

    }
}
