using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public FloatReference heartContainers;

    // Start is called before the first frame update
    void Start()
    {
        InitializeHearts();
    }

    public void InitializeHearts()
    {
        for (int i = 0; i < heartContainers.GetValue(); i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }
}
