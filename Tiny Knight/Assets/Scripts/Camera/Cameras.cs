using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cameras : MonoBehaviour
{
    [Header("Active Cameras")]
    public GameObject startingCam; // Default camera at the start of the scene
    public GameObject currentPlayerCam; // The current camera focused on the Player

    [Header("Camera Shake")]
    public float time = 0.2f;
    public float intensity = 5f;

    // private
    private CinemachineVirtualCamera currentVCam;
    private float shakeTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if(!startingCam.activeSelf) // Checks to see if the starting camera is active
        {
            startingCam.SetActive(true); // if not set to active
        }
        currentPlayerCam = startingCam;
        currentVCam = startingCam.GetComponent<CinemachineVirtualCamera>();
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
                    currentVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicPerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ChangePlayerCam(GameObject transitionCam) // Method called in RoomTransition script that changes the active cam to the transitionCam on the roomTransition object
    {
        currentPlayerCam.SetActive(false); // Disable the current active cam
        currentPlayerCam = transitionCam; // switch to new cam
        currentVCam = transitionCam.GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicPerlin =
            currentVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); // set cinemachingBasicPerlin to the vCam's perlin
        cinemachineBasicPerlin.m_AmplitudeGain = intensity; // set the intensity of the screen shake
        shakeTimer = time;
    }
}
