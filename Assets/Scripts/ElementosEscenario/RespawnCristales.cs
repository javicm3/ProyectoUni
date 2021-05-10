using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class InfoCristales
//{
//    public Vector3 posicion;
//    public bool roto;
   
//    public InfoCristales(Vector3 pos, bool broken)
//    {
       
//        posicion = pos;
//        roto = broken;
//    }

//}
public class RespawnCristales : MonoBehaviour
{
    VidaPlayer vida;
    Explodable[] cristales;
    //InfoCristales[] info;
    // Start is called before the first frame update
    void Start()
    {
        vida = FindObjectOfType<VidaPlayer>();
     cristales = FindObjectsOfType<Explodable>();
       
    }
    
    // Update is called once per frame
    void Update()
    {
        if (vida.reiniciando)
        {
            foreach(Explodable exp in cristales)
            {
                if (exp.haExplotado)
                {
                    exp.haExplotado = false;
                    exp.gameObject.SetActive(true);
                    exp.fragmentInEditor();
                    exp.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
                }
            }
        }
    }
}
