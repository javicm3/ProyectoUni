using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlladorAscensorBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<AscensorBoss2>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponentInParent<AscensorBoss2>().enabled = true;
        }
    }
}
