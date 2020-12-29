using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFall : MonoBehaviour
{
    bool isGrounded;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }
}
