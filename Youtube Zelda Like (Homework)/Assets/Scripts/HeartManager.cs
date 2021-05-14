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
    public FloatReference playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitializeHearts();
    }

    public void InitializeHearts() // Initialize heart containers at the beginning of game
    {
        for (int i = 0; i < heartContainers.GetValue(); i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.GetRuntimeValue() / 2f; // hearts to be displayed on screen

        for (int i = 0; i < heartContainers.GetRuntimeValue(); i++)
        {
            if (i <= tempHealth - 1)
            {
                hearts[i].sprite = fullHeart; // Full Heart
            }
            else if (i >= tempHealth)
            {
                hearts[i].sprite = emptyHeart; // Empty Heart
            }
            else
            {
                hearts[i].sprite = halfHeart; // Half Heart
            }
        }
    }
}
