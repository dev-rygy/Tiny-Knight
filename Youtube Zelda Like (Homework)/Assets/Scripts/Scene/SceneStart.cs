using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    public bool playTitleAtSceneStart = false;
    public bool haltPlayerMovementAtSceneStart = false;

    private TitleCard myTitleCard;

    void Start()
    {
        myTitleCard = GetComponent<TitleCard>();

        if (haltPlayerMovementAtSceneStart)
            FindObjectOfType<Player>().Transition();

        if (playTitleAtSceneStart && myTitleCard != null)
            myTitleCard.SpawnTitle();
    }
}
