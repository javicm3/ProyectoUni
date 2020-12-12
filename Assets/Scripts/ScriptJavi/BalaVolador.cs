using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaVolador : MonoBehaviour
{
    GameObject Player;
    Vector3 punto;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        punto = Player.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, punto * 1.5f, 10 * Time.deltaTime);
        Destroy(this.gameObject, 2);
    }
}
