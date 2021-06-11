using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCard : MonoBehaviour
{
    public string placeName; // the string for the title card
    public GameObject text; // cached text reference
    public Text placeText; // cached Text Object
    public float spawnDelay = 2.6f; // time till title card shows on screen
    public float lifeTime = 4f; // lifetime of a title card once it's on screen

    public void SpawnTitle()
    {
        StartCoroutine(AreaTitleCo());
    }

    private IEnumerator AreaTitleCo() // Coroutine for title card
    {
        yield return new WaitForSeconds(spawnDelay); // wait x amount of seconds for title card to appear
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(lifeTime); // wait x amount of seconds before title card vanishes
        text.SetActive(false);
    }
}
