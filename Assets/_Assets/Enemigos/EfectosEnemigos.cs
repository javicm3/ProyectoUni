using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectosEnemigos : MonoBehaviour
{
    public GameObject particulasDaño;
    public Transform puntoDaño;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ParticulasDañado()
    {
        Instantiate(particulasDaño, puntoDaño);
    }
}
