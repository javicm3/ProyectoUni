using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    Image image;
    float opacity=1;
    public GameObject particulasTransicion;


    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(0,0,0,1);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        while (opacity > 0)
        {
            opacity -= Time.deltaTime;
            image.color = new Color(0, 0, 0, opacity);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public IEnumerator FadeOut()
    {
        Instantiate(particulasTransicion, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0,2,0), Quaternion.identity);

        if (opacity<0) { opacity = 0; }
        while (opacity<1)
        {
            opacity += Time.deltaTime;
            image.color = new Color(0, 0, 0, opacity);
            yield return new WaitForSeconds(Time.deltaTime); 
        }
    }

}
