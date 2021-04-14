using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    [Header ("Player & Camera")]
    public Vector3 playerChange; // The players change in position after a room transition
    public GameObject transitionCam; // The Camera to be switched to after a room transition

    [Header ("Title Card")]
    public bool needText; // bool for if the area needs a title card
    public string placeName; // the string for the title card
    public GameObject text; // cached text reference
    public Text placeText; // cached Text Object
    public float spawnDelay = 2.6f; // time till title card shows on screen
    public float lifeTime = 4f; // lifetime of a title card once it's on screen

    private void OnTriggerEnter2D(Collider2D collision) // If Player enters room transition collider
    {
        if (collision.CompareTag("Player")) // Check to see if object is player
        {
            // Change Player position and add title card if nessassary
            collision.transform.position += playerChange;
            FindObjectOfType<Cameras>().ChangePlayerCam(transitionCam);
            transitionCam.SetActive(true);
            if(needText) // If the area needs a title card
            {
                StartCoroutine(AreaTitleCo());
            }
        }
    }

    private IEnumerator AreaTitleCo() // Coroutine for title card
    {
        yield return new WaitForSeconds(spawnDelay);
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(lifeTime);
        text.SetActive(false);
    }
}
