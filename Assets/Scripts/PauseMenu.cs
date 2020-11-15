using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;
    bool paused=true;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale =0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused == true)
            {
                Time.timeScale = 1;
                paused = false;
            }
            else
            {
                paused = true;
                Time.timeScale = 0;
            }
           
            print(panel.activeSelf);
            panel.gameObject.SetActive(!panel.activeSelf);
        }
    }

}
