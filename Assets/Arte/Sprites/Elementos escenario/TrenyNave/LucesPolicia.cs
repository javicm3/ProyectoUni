using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucesPolicia : MonoBehaviour
{
    [SerializeField] GameObject colorRojo;
    [SerializeField] GameObject colorAzul;
    bool colorRojoBool; 
    [SerializeField]float timing;
    // Start is called before the first frame update
    void Start()
    {
        colorRojo.SetActive(true);
        colorAzul.SetActive(false);
        colorRojoBool = true;

    }

    // Update is called once per frame
    void Update()
    {
        timing += 1 + Time.deltaTime;
        if (timing > 25)
        {
            if (colorRojoBool)
            {
                colorRojo.SetActive(false);
                colorAzul.SetActive(true);
                colorRojoBool = false;
            }
            else if(!colorRojoBool)
            {
                colorRojo.SetActive(true);
                colorAzul.SetActive(false);
                colorRojoBool = true;
            }
            timing = 0; 
            
            
            //if (timing > 6)
            //{
            //    timing = 0;
            //    colorRojo.SetActive(true);
            //    colorAzul.SetActive(false);
            //}
        }
    }
}
