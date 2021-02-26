using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolvoEscenario : MonoBehaviour
{
    ParticleSystem dustPS;
    ParticleSystem polvilloPS;
    public float minRate;
    public float maxRate = 5;
    float tmp;
    public float tiempoPolvo = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        dustPS = GetComponent<ParticleSystem>();
        //polvilloPS = GetComponentInChildren<ParticleSystem>();
        tmp = tiempoPolvo;
    }

    // Update is called once per frame
    void Update()
    {
        if (CinemachineShake.Instance.shaking)
        {
            if(tmp > 0)
            {
                //var emission2 = polvilloPS.emission;
                //emission2.rateOverTime = Mathf.Lerp(maxRatePolvillo, 0, 1 - (tmp / tiempoPolvo));
                var emission = dustPS.emission;
                emission.rateOverTime = Mathf.Lerp(maxRate, 0, 1 - (tmp / tiempoPolvo)); 
            }
            else
            {
                var emission = dustPS.emission;
                emission.rateOverTime = minRate;
                //var emission2 = polvilloPS.emission;
                //emission2.rateOverTime = 0;
            }
            //GetComponent<ParticleSystem>().emission.rateOverTime = Mathf.Lerp(20, 0, 0.35f);
        }
        else
        {
            tmp = tiempoPolvo;
            var emission = dustPS.emission;
            emission.rateOverTime = minRate;
            //var emission2 = polvilloPS.emission;
            //emission2.rateOverTime = 0;
        }
        
    }
}
