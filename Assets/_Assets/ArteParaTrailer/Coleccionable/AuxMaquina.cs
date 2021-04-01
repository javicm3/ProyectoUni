using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxMaquina : MonoBehaviour
{
    public Transform puntoParticulas;
    public GameObject particulasLluviaColeccionable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void particulasCaidaCollecionable()
    {
        Instantiate(particulasLluviaColeccionable, puntoParticulas);
    }
}
