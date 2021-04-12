using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public Vector3 playerChange;
    public GameObject transitionCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position += playerChange;
            FindObjectOfType<Cameras>().ChangePlayerCam(transitionCam);
            transitionCam.SetActive(true);
        }
    }
}
