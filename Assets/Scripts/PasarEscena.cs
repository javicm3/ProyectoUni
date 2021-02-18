using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasarEscena : MonoBehaviour
{
    public string nextLevelName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void GuardarDatos()
    {
        //if (GameManager.Instance.tempMoneda1 == true) GameManager.Instance.pickMoneda1 = true;
        //if (GameManager.Instance.tempMoneda2 == true) GameManager.Instance.pickMoneda2 = true;
        //if (GameManager.Instance.tempMoneda3 == true) GameManager.Instance.pickMoneda3 = true;
        GameManager.Instance.futureSpawn = this.transform.position;
        //GameManager.Instance.currentRespawn = this.gameObject.transform.position;
        //GameManager.Instance.lastcoleccionablesneed = GameManager.Instance.coleccionablesneed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GameManager.Instance.NextScene(nextLevelName);
            GuardarDatos();
        }
    }
    
}
