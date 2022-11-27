using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public Transform playerLookPosition;

    public GameObject[] fishPrefab;

    public GameObject waterSplash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Fish SpawnFish()
    {
        int r = Random.Range(0, fishPrefab.Length);
        Vector3 spawnPos = playerLookPosition.position + (playerLookPosition.forward.normalized * 50f);
        spawnPos.y = -10f;
        GameObject fish = Instantiate(fishPrefab[r], spawnPos, Quaternion.identity);
        spawnPos.y = 0f;
        Instantiate(waterSplash, spawnPos, Quaternion.identity);

        return fish.GetComponent<Fish>();
    }
}
