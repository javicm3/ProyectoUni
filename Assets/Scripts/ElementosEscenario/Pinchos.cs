using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchos : MonoBehaviour
{
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
            print("colision pinchos");
            if (GameObject.Find("Player") != null)
            {

                GameObject.FindObjectOfType<GameManager>().MuertePJ();



            }
        }

        //collision.GetComponent<SpriteRenderer>().color = Color.black;
        //collision.GetComponent<SpriteRenderer>().enabled = false;

        //Destroy(collision.gameObject, 1.5f);

    }
}

