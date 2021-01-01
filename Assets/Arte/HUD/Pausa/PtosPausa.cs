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
    Gradient gradient;
    float alpha = 1.0f;
    public float coeficienteTransparencia;
    GameObject elegido;
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
        
        gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.blue, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(Mathf.Clamp(1 / alpha - coeficienteTransparencia, 0, 0.2f), 0.0f), new GradientAlphaKey(Mathf.Clamp(1 / alpha - coeficienteTransparencia, 0, 0.2f), 1.0f) }
            //new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.blue, 1.0f) },
            //new GradientAlphaKey[] { new GradientAlphaKey(Mathf.Clamp(coeficienteTransparencia - 1 / alpha, 0, 0.75f), 0.0f), new GradientAlphaKey(Mathf.Clamp(coeficienteTransparencia - 1 / alpha, 0, 0.75f), 1.0f) }
        );
        lastFrameVelocity = rb.velocity;
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Pausa");

        for(int i = 0; i < gos.Length; i++)
        {
           
            
            if (Vector2.Distance(gos[i].transform.position, transform.position) < distanciaMinima && gos[i] != this.gameObject)
            {
                
                
                GetComponent<LineRenderer>().SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, 0));
                GetComponent<LineRenderer>().SetPosition(1, new Vector3(gos[i].transform.position.x, gos[i].transform.position.y, 0));
                alpha = Mathf.Abs(Vector2.Distance(transform.position, gos[i].transform.position) - distanciaMinima / distanciaMinima);
                GetComponent<LineRenderer>().colorGradient = gradient;
                
            }
            
        }
        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) < distanciaMinima)
        {
            GetComponent<LineRenderer>().SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, 0));
            GetComponent<LineRenderer>().SetPosition(1, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
            alpha = Mathf.Abs(Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) - distanciaMinima / distanciaMinima);
            GetComponent<LineRenderer>().colorGradient = gradient;
        }
        if (rb.velocity == Vector2.zero)
        {
            Launch();
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
        Bounce(col.contacts[0].normal);
    }
     void Bounce(Vector2 collisionNormal)
    {
        speed = lastFrameVelocity.magnitude;
        direccion = Vector2.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = direccion * Mathf.Max(speed, minVelocity);
    }

   
}
