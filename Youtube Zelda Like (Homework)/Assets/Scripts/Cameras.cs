using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public GameObject startingCam;
    public GameObject currentPlayerCam;

    // Start is called before the first frame update
    void Start()
    {
        if(!startingCam.activeSelf) // Checks to see if the starting camera is enabled
        {
            startingCam.SetActive(true);
        }
        currentPlayerCam = startingCam;
    }

    public void ChangePlayerCam(GameObject transitionCam)
    {
        currentPlayerCam.SetActive(false);
        currentPlayerCam = transitionCam;
    }
}
