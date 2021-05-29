using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; //The next scene to load after the trigger
    public Vector2 playerPosition; // The player's position in the next scene after load
    public VectorValue playerStorage; // Scriptable object PlayerPosition
    public GameObject fadeInPanel; // Fade From White - GameObject, Prefab
    public GameObject fadeOutPanel; // Fade To White - GameObject, Prefab
    public float fadeDelay; // Delay till scene transition

    private void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector2.zero, Quaternion.identity) as GameObject; // Instantiate Fade From White Object
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger) // If the collision is the Player's feet and not the hitbox collider
        {
            playerStorage.initialValue = playerPosition;
            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector2.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeDelay);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
