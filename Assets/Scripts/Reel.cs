using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    public FishingRod fishingRod;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hand")
        {
            fishingRod.WindReel();
        }
    }

}
