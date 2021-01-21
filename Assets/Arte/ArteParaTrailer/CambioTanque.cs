using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioTanque : MonoBehaviour
{
    [SerializeField] GameObject TanqueNormal; 
    [SerializeField] GameObject TanqueRoto;
    float timer = 0; 
    [SerializeField]float tiempoDeCambio = 0;
    [SerializeField] Animator NucleoAnim;
    // Start is called before the first frame update
    void Start()
    {
        TanqueNormal.SetActive(true);
        TanqueRoto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        if (timer > tiempoDeCambio)
        {
            TanqueNormal.SetActive(false);
            TanqueRoto.SetActive(true);
        }
    }
}
