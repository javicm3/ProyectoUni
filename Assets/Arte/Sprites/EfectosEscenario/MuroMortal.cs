using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroMortal : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
       
        anim = GetComponent<Animator>();
        anim.SetBool("InicioBoss", false);
        anim.SetBool("Paron1", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("cocho");
            anim.SetBool("InicioBoss", true);
            anim.SetBool("Paron1", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("cocho");
            //anim.SetBool("InicioBoss", false);
            //anim.SetBool("Paron1", false);

        }
    }
}
