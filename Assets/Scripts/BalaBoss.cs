using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaBoss : MonoBehaviour
{
    public Transform objetivo;
    public int velocidadBala;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, objetivo.position, velocidadBala * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "Boss")
        {
            Destroy(this.gameObject);
        }
    }
}
