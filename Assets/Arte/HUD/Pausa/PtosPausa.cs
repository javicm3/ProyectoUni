using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtosPausa : MonoBehaviour
{
    public float speed;
    Vector2 direccion;
    Rigidbody2D rb;
    Vector2 lastFrameVelocity;
    float minVelocity;
    public float distanciaMinima;
    // Start is called before the first frame update
    void Start()
    {
        minVelocity = speed;
        
        rb = GetComponent<Rigidbody2D>();
        Launch();
        
        //rb.velocity = new Vector2(direccion.x * speed, direccion.y * speed);
    }

    // Update is called once per frame
    void Update()
    {
        lastFrameVelocity = rb.velocity;
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Pausa");

        for(int i = 0; i < gos.Length; i++)
        {
            if(Vector2.Distance(gos[i].transform.position, transform.position) < distanciaMinima && gos[i] != this.gameObject)
            {
                GetComponent<LineRenderer>().SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, 0));
                GetComponent<LineRenderer>().SetPosition(1, new Vector3(gos[i].transform.position.x, gos[i].transform.position.y, 0));
            }
        }
        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) < distanciaMinima)
        {
            GetComponent<LineRenderer>().SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, 0));
            GetComponent<LineRenderer>().SetPosition(1, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
        }
    }
    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //Vector2 bounceAngle = GetBounceAngle(transform.position, col.transform.position, col.collider.bounds.size.y);
        Bounce(col.contacts[0].normal);
        //if (col.gameObject.tag == "Pausa")
        //{
        //    //Vector2 direction = new Vector2(Random.Range(-1,1), bounceAngle).normalized;
        //    direccion = Vector2.Reflect(transform.position, col.gameObject.transform.up);
        //    rb.velocity = direccion * speed;
        //}
    }
     void Bounce(Vector2 collisionNormal)
    {
        speed = lastFrameVelocity.magnitude;
        direccion = Vector2.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = direccion * Mathf.Max(speed, minVelocity);
    }

    float GetBounceAngle(Vector2 ballPosition, Vector2 playerPosition, float playerSizeHeight)
    {
        //return (ballPosition.y - playerPosition.y) / playerSizeHeight;
        return (ballPosition.y - playerPosition.y);


    }
}
