using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntero : MonoBehaviour {

    public Transform boss;
    GameObject player;
    RectTransform rt;
    Vector2 bounds;
    
    public float coeficienteCamara;
    public float posicionPunteroY;
    public float posicionPunteroX;
    Canvas canvasPadre;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rt = GetComponent<RectTransform>();
        canvasPadre = GetComponentInParent<Canvas>();
    }
    private void Update()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
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

        ////transform.position = new Vector3(Mathf.Clamp(boss.transform.position.x, -(Camera.main.aspect*Camera.main.orthographicSize*Camera.main.transform.position.x), Camera.main.aspect * Camera.main.orthographicSize * Camera.main.transform.position.x),0 , 0);
        //transform.position = new Vector3(Mathf.Clamp(boss.transform.position.x, (player.transform.position.x - posicionPunteroX) /** (Camera.main.orthographicSize * coeficienteCamara)*/, (player.transform.position.x + posicionPunteroX)/* * (Camera.main.orthographicSize * coeficienteCamara)*/),
        //    Mathf.Clamp(boss.transform.position.y, (player.transform.position.y - 1) /** (Camera.main.orthographicSize * coeficienteCamara)*/, (player.transform.position.y + posicionPunteroY) /** (Camera.main.orthographicSize * coeficienteCamara)*/), 0);
        //transform.position = new Vector3(Mathf.Clamp(boss.transform.position.x, -(Camera.main.aspect*Camera.main.orthographicSize*Camera.main.transform.position.x), Camera.main.aspect * Camera.main.orthographicSize * Camera.main.transform.position.x),0 , 0);
        //GetComponent<RectTransform>().anchoredPosition = new Vector3(Mathf.Clamp(boss.transform.position.x, -canvasPadre.GetComponent<RectTransform>().rect.width, canvasPadre.GetComponent<RectTransform>().rect.width),
        //    Mathf.Clamp(boss.transform.position.y, -canvasPadre.GetComponent<RectTransform>().rect.height, canvasPadre.GetComponent<RectTransform>().rect.height), 0);
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(boss.position);
        Vector3 capped = targetPositionScreenPoint;
        if (capped.x <= 0) capped.x = 35;
        if (capped.x >= Screen.width) capped.x = Screen.width - 35;
        if (capped.y <= 0) capped.y = 35;
        if (capped.y >= Screen.height) capped.y = Screen.height - 35;

        Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(capped);

        rt.position = pointerWorldPosition;
        rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y, 0);

        
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
        if (isOffScreen)
        {
            GetComponentInChildren<Image>().enabled = true;
        }
        else
        {
            GetComponentInChildren<Image>().enabled = false;
        }
        //transform.position = new Vector3(Mathf.Clamp(boss.transform.position.x, -(Camera.main.orthographicSize), Camera.main.orthographicSize), Mathf.Clamp(boss.transform.position.y, -(Camera.main.orthographicSize), Camera.main.orthographicSize), 0);
        //transform.position = Camera.main.WorldToScreenPoint(boss.transform.position);
    }
}
