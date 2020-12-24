//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class EnergyManager : MonoBehaviour
//{
//    public float maxEnergy = 100;
//    public float actualEnergy = 0;
//    public float energiaDash = 50;
//    public float energiaSumadaWallJump = 5;
//    public float energiaSumadaBouncer = 10;
//    public float energiaDashAbajo = 15f;
//    public float energiaDisparo = 25f;
//    GameObject barraEnergia;
//    //CharacterController2D cc;
//    public float indiceMultiplicador = 2;
//    Movimiento mov;
//    // Start is called before the first frame update
//    void Start()
//    {
//        if (GameObject.Find("ColorBarra").gameObject != null) barraEnergia = GameObject.Find("ColorBarra").gameObject;
//        actualEnergy = 0;
//        cc = this.GetComponent<CharacterController2D>();
//       mov= this.GetComponent<Movimiento>();
//    }
//   public  void RestarEnergia(float valor)
//    {
//       actualEnergy -= valor;
//    }
//    public void SumarEnergia(float valor)
//    {
//        actualEnergy += valor;
//        if (actualEnergy > maxEnergy)
//        {
//            actualEnergy = maxEnergy;
//        }
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        barraEnergia.GetComponent<Image>().fillAmount = actualEnergy / maxEnergy;
//        if (cc.m_Grounded)
//        {

//            if (actualEnergy < maxEnergy)
//            {
//                if (cc.m_Rigidbody2D.velocity.x != 0)
//                {
//                    if ((cc.isTouchingWall == false)&&(cc.isTouchingPared==false))
//                    {
                          
//                        actualEnergy += Mathf.Abs(cc.m_Rigidbody2D.velocity.x) * indiceMultiplicador * Time.deltaTime;
//                    }
                   
//                }
//            }
//            else actualEnergy = maxEnergy;

//        }
//    }
//}
