using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EsperarVideo : MonoBehaviour
{
    [SerializeField] float tiempoEspera = 3;

    void Start()
    {
        StartCoroutine(Wait(tiempoEspera));
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(1);
    }
}
