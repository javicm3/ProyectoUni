using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparosBOSS : MonoBehaviour
{

    public Transform[] targets;
    public GameObject bala;
    public int balasInstanciadas;
    public float tiempoEntreBalas = 0.5f;
    public float min = -30;
    public float max = 30;
    public Transform puntoDisparo;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 RandomVector2(float angle, float angleMin)
    {
        float random = Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    public void Disparar()
    {
        for(int i = 0; i < targets.Length; i++)
        {
            Instantiate(bala, puntoDisparo.position, Quaternion.identity, this.transform);
            bala.GetComponent<BalaBoss>().objetivo = targets[i];
            StartCoroutine(DisparoCoroutine());
        }
        for (int i = 0; i < balasInstanciadas; i++)
        {
            Instantiate(bala, puntoDisparo.position, Quaternion.identity, this.transform);
            bala.GetComponent<BalaBoss>().objetivo.position = RandomVector2(min, max);
            StartCoroutine(DisparoCoroutine());
        }
        GetComponent<Ruta>().disparo = false;
    }

    IEnumerator DisparoCoroutine()
    {
        yield return new WaitForSeconds(tiempoEntreBalas);
    }
}
