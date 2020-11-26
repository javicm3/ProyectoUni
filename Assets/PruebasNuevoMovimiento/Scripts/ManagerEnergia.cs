using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerEnergia : MonoBehaviour
{
    public float maxEnergy = 100;
    public float actualEnergy = 0;
    public float energiaDash = 50;
    public float energiaSumadaWallJump = 5;
    public float energiaSumadaBouncer = 10;
    public float energiaDashAbajo = 15f;
    public float energiaDisparo = 25f;
    GameObject barraEnergia;
    ControllerPersonaje cc;
    public float indiceMultiplicador = 2;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("EnergyBar").gameObject != null) barraEnergia = GameObject.Find("EnergyBar").gameObject;
        actualEnergy = 0;
        cc = GetComponent<ControllerPersonaje>();
    }
   public  void RestarEnergia(float valor)
    {
       actualEnergy -= valor;
        if (actualEnergy <= 0) actualEnergy = 0;
    }
    public void SumarEnergia(float valor)
    {
        actualEnergy += valor;
        if (actualEnergy > maxEnergy)
        {
            actualEnergy = maxEnergy;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            maxEnergy = 10000;
            actualEnergy = 10000;
        }
        barraEnergia.GetComponent<Image>().fillAmount = actualEnergy / maxEnergy;
        if (cc.grounded)
        {
            if (actualEnergy < maxEnergy)
            {
                if (cc.rb.velocity.x != 0)
                {
                    if ((cc.pegadoPared == false))
                    {

                        actualEnergy += Mathf.Abs(cc.rb.velocity.x) * indiceMultiplicador * Time.deltaTime;
                    }
                   
                }
            }
            else actualEnergy = maxEnergy;

        }
        
    }
}
