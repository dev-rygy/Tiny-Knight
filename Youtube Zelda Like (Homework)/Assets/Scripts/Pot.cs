using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [Header("Coroutines")]
    public float breakDelay = .3f;

    // Private caches
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public IEnumerator BreakCo()
    {
        myAnimator.SetBool("isBreaking", true);
        yield return new WaitForSeconds(breakDelay);
        Destroy(this.gameObject);
    }
}
