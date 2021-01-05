using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoCaidaPolea : MonoBehaviour
{
    public bool seRompe = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            if (seRompe)
            {if (this.GetComponent<SpriteRenderer>() != null) this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, this.GetComponent<SpriteRenderer>().color.a *0.3f);
                Destroy(this.gameObject, 1f);
            }
        }
    }
}
