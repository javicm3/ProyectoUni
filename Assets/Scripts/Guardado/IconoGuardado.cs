using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconoGuardado : MonoBehaviour
{
    public float duracion = 2;

    void Start()
    {
        if (!GameManager.Instance.MostrarSaveIcon)
        {
            gameObject.SetActive(false);
        }
    }


    void Update()
    {
        if (duracion > 0)
        { duracion -= Time.deltaTime; }
        else { gameObject.SetActive(false); }
    }
}
