using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntero : MonoBehaviour {

    public Transform boss;
    GameObject player;
    RectTransform rt;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rt = GetComponent<RectTransform>();

    }
    private void Update()
    {
        //transform.LookAt(boss);
        //Quaternion rotation = Quaternion.LookRotation(boss.position, Vector3.right);
        //transform.rotation = rotation;

        // Get the position of the object in screen space
        Vector3 objScreenPos = Camera.main.WorldToScreenPoint(boss.transform.position);

        // Get the directional vector between your arrow and the object
        Vector3 dir = (objScreenPos - rt.position).normalized;

        // Calculate the angle 
        // We assume the default arrow position at 0° is "up"
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.up));

        // Use the cross product to determine if the angle is clockwise
        // or anticlockwise
        Vector3 cross = Vector3.Cross(dir, Vector3.up);
        angle = -Mathf.Sign(cross.z) * angle;

        // Update the rotation of your arrow
        rt.localEulerAngles = new Vector3(rt.localEulerAngles.x, rt.localEulerAngles.y, angle);

        transform.position = new Vector3(Mathf.Clamp(boss.transform.position.x, -(Camera.main.aspect*Camera.main.orthographicSize*Camera.main.transform.position.x), Camera.main.aspect * Camera.main.orthographicSize * Camera.main.transform.position.x),0 , 0);
        //transform.position = new Vector3(Mathf.Clamp(boss.transform.position.x, -(Camera.main.orthographicSize), Camera.main.orthographicSize), Mathf.Clamp(boss.transform.position.y, -(Camera.main.orthographicSize), Camera.main.orthographicSize), 0);
        //transform.position = Camera.main.WorldToScreenPoint(boss.transform.position);
    }
}
