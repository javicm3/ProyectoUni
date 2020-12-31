using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaDash : MonoBehaviour
{

    ControllerPersonaje CP;

    public Transform Up;
    public Transform Down;

    public float WaitFor = 5;

    void Awake()
    {
        this.transform.position = Down.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ControllerPersonaje>().estoyDasheando)
        {
            transform.position = Up.transform.position;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitFor);

        transform.position = Down.transform.position;
    }
}
