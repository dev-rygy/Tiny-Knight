using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    [Header("Room Transition & Camera")]
    public Vector3 playerChange; // The players change in position after a room transition
    public GameObject transitionCam; // The Camera to be switched to after a room transition

    // private
    private TitleCard myTitleCard;

    void Start()
    {
        myTitleCard = GetComponent<TitleCard>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // If Player enters room transition collider
    {
        if (collision.CompareTag("Player") && !collision.isTrigger) // Check to see if object is player
        {
            // Change Player position and add title card if nessassary
            collision.transform.position += playerChange;
            FindObjectOfType<Cameras>().ChangePlayerCam(transitionCam);
            transitionCam.SetActive(true);

            FindObjectOfType<Player>().Transition(); // Start Player Transition Co that stops the Player from moving while in transition

            if(myTitleCard != null) // If the area needs a title card
            {
                myTitleCard.SpawnTitle();
            }
        }
    }
}
