using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruta : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;

    private Vector2 gizmosPosition;

    public bool pausa;
    public float segundosPausa;
    public bool pausaTerminada;
    public float speedModifier;
    public string animacion;
    public float rotacion;
    public bool pausaConTrigger;
    public GameObject player;
    public bool disparo;
    public bool seReinicia = false;
    public bool seReiniciaConTrigger = false;
    private void OnDrawGizmos()
    {
        for(float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y),
            new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y),
            new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ControllerPersonaje>().gameObject;
        if (pausa)
        {
            pausaTerminada = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<VidaPlayer>().reiniciando && seReinicia)
        {
            pausaTerminada = false;
            if (seReiniciaConTrigger)
            {
                pausaConTrigger = true;
            }
        }
    }
}
