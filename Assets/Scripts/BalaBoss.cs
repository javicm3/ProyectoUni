using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaBoss : MonoBehaviour
{
    public Vector2 objetivo;
    Vector3 direccion;
    public int velocidadBala;


    // Start is called before the first frame update
    void Start()
    {
        direccion = objetivo - new Vector2(this.transform.position.x, this.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, this.transform.position + direccion, velocidadBala * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (this.gameObject.tag != "balaVolador")
        {
            if (coll.tag != "Boss")
            {

                Destroy(this.gameObject);
            }
        }
        else
        {
            if (coll.gameObject.layer == 8)
            {
                Destroy(this.gameObject);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (this.gameObject.tag == "balaVolador")
        {
            if (collision.gameObject.tag != "Enemigo")
            {
                Destroy(this.gameObject);
            }

        }
    }
}
