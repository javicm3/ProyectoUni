using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDiagonal : MonoBehaviour
{
    public float tiempoEntreComprobaciones = 2f;
    public float tiempoTrasDash = 0.55f;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ControllerPersonaje>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.GetComponent<BoxCollider2D>() != null)
        {
            if (player.GetComponent<ControllerPersonaje>().auxCdDash - 0.1f > (player.GetComponent<ControllerPersonaje>().cooldownDash - tiempoTrasDash))
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                this.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
