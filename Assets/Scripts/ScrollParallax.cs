using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollParallax : MonoBehaviour
{
    GameObject player;
    Transform posicionCam;
    Vector3 posicionFinal;
    public GameObject nearBackground;
    public GameObject Background;
    Vector3 posNB;
    Vector3 posB;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        posicionCam = player.transform;

        //nearBackground = GameObject.Find("EdificiosFondo");
        //Background = GameObject.Find("EdificiosFondoFondo");
        posNB = nearBackground.transform.position;
        posB = Background.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameObject.Find("Player") != null)
        {
            if (posicionCam != null)
            {
                posicionFinal = posicionCam.position + player.transform.position;
                nearBackground.transform.position = new Vector3(posNB.x - (posicionFinal.x * 0.030f), nearBackground.transform.position.y, nearBackground.transform.position.z);
                Background.transform.position = new Vector3(posB.x - (posicionFinal.x * 0.015f), Background.transform.position.y, Background.transform.position.z);
            }
        }

    }
}
