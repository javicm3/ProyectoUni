using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChispasSuperficie : MonoBehaviour
{
    float azar;
    float tiempoPart = 0.6f;
    float tmp;
    float random;
    public GameObject particulaRayo;
    ContactPoint2D[] contactosss;
    // Start is called before the first frame update
    void Start()
    {
        tmp = tiempoPart;
    }

    // Update is called once per frame
    void Update()
    {
        tmp -= Time.deltaTime;
        
        if(tmp <= 0 && contactosss != null)
        {
            int random = Random.Range(0, contactosss.Length);
            Vector2 direccionPart = new Vector2(transform.position.x, transform.position.y) - (contactosss[random].point + (Vector2.down * 0.5f));
            //Instantiate(particulaRayo, contactosss[random].point, Quaternion.LookRotation(direccionPart, direccionPart));
            Instantiate(particulaRayo, transform.position, Quaternion.LookRotation(-direccionPart, -direccionPart));
            tmp = tiempoPart;
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        contactosss = null;
        contactosss = col.contacts;
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        contactosss = null;
    }
}
