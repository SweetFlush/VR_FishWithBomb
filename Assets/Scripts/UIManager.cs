using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public FishingRod rod;

    public GameObject ingame;
    public GameObject cleared;

    public TextMeshPro number;

    private void Update()
    {
        number.text = rod.catchedMonsterFish.ToString() + " / 5";
        if (rod.catchedMonsterFish >= 5)
            FinishGame();
    }

    private void FinishGame()
    {
        ingame.SetActive(false);
        cleared.SetActive(true);
    }
}
