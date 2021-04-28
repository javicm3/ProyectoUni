using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    Image image;
    float opacity = 1;

    public GameObject particulasTransicion;


    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        while (opacity > 0)
        {
            opacity -= Time.deltaTime;
            image.color = new Color(0, 0, 0, opacity);
            if (opacity <= 0.0f)
            {
                if (GameManager.Instance.haciendoAnim == false)
                {
                    GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
                    PlayerInput plInput = playerGO.GetComponentInChildren<PlayerInput>();
                    plInput.inputHorizBlock = false;
                    plInput.inputVerticBlock = false;

                    ControllerPersonaje per = playerGO.GetComponentInChildren<ControllerPersonaje>();
                    per.dashBloqueado = false;
                    per.saltoBloqueado = false;
                    per.dashCaidaBloqueado = false;
                    per.movimientoBloqueado = false;
                }
            }

            yield return new WaitForSeconds(Time.deltaTime);


        }
    }

    public IEnumerator FadeOut()
    {
        Instantiate(particulasTransicion, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 2, 0), Quaternion.identity);

        if (opacity < 0) { opacity = 0; }
        while (opacity < 1)
        {
            opacity += Time.deltaTime;
            if (opacity >= 1)
            {

                GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
                PlayerInput plInput = playerGO.GetComponentInChildren<PlayerInput>();
                plInput.inputHorizBlock = true;
                plInput.inputVerticBlock = true;

                ControllerPersonaje per = playerGO.GetComponentInChildren<ControllerPersonaje>();
                per.dashBloqueado = true;
                per.saltoBloqueado = true;
                per.dashCaidaBloqueado = true;
                per.movimientoBloqueado = true;
            }
            image.color = new Color(0, 0, 0, opacity);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

}
