using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool ultimoCheck = false;
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
        if (collision.gameObject.tag == "Player")
        {
         Checkpoint[] checks=   FindObjectsOfType<Checkpoint>();
            foreach(Checkpoint go in checks)
            {
                go.ultimoCheck = false;
            }
            ultimoCheck = true;
        }
    }
}
