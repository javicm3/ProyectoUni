using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInicioBoss2 : MonoBehaviour
{
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            boss.GetComponent<EstadosBoss2>().bossActivo = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
