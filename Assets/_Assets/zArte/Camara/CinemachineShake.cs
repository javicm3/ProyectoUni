using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    CinemachineVirtualCamera cinemachineVirtualCamera;
    private float timer = 0;
    float startingIntensity;
    float timeShaking;
    float maxP = 0;
    public bool shaking;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0, 1 - (timer / timeShaking));
            
        }
        else
        {
            shaking = false;
        }

    }
    public void ShakeCamera(float intensity, float speed, float time)
    {
        shaking = true;
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = speed;
        timer = time;
        timeShaking = time;
        startingIntensity = intensity;
    }
   
    //public void StopShake()
    //{
    //    print("Noshakeado");
    //    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    //    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    //    cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
    //}
}
