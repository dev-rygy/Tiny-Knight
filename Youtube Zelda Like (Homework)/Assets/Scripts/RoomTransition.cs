using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    [Header ("Player & Camera")]
    public Vector3 playerChange;
    public GameObject transitionCam;

    [Header ("Area Title")]
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;
    public float spawnDelay = 2.6f;
    public float lifeTime = 4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position += playerChange;
            FindObjectOfType<Cameras>().ChangePlayerCam(transitionCam);
            transitionCam.SetActive(true);
            if(needText)
            {
                StartCoroutine(AreaTitleCo());
            }
        }
    }

    private IEnumerator AreaTitleCo()
    {
        yield return new WaitForSeconds(spawnDelay);
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(lifeTime);
        text.SetActive(false);
    }
}
