using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public float time = 1f;
    public float intensity = 2f;

    // private
    private CinemachineVirtualCamera vCam;
    private float shakeTimer = 0f;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>(); // Get the Virtual Camera component of the object on awake
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                // Time over!
                CinemachineBasicMultiChannelPerlin cinemachineBasicPerlin = 
                    vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicPerlin =
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); // set cinemachingBasicPerlin to the vCam's perlin
        cinemachineBasicPerlin.m_AmplitudeGain = intensity; // set the intensity of the screen shake
        shakeTimer = time;
    }
}
