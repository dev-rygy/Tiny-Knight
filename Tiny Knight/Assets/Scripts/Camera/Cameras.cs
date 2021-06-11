using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public GameObject startingCam; // Default camera at the start of the scene
    public GameObject currentPlayerCam; // The current camera focused on the Player

    // Start is called before the first frame update
    void Start()
    {
        if(!startingCam.activeSelf) // Checks to see if the starting camera is active
        {
            startingCam.SetActive(true); // if not set to active
        }
        currentPlayerCam = startingCam;
    }

    public void ChangePlayerCam(GameObject transitionCam) // Method called in RoomTransition script that changes the active cam to the transitionCam on the roomTransition object
    {
        currentPlayerCam.SetActive(false); // Disable the current active cam
        currentPlayerCam = transitionCam; // switch to new cam
    }
}
