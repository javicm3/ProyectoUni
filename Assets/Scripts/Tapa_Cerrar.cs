using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapa_Cerrar : MonoBehaviour
{
    public GameObject sueloC;
    private void Start()
    {
        sueloC.SetActive(false);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInChildren<Animator>().SetTrigger("HaPasado");
            sueloC.SetActive(true);
        }
    }
}
