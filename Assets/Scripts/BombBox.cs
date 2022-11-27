using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBox : MonoBehaviour
{
    public int bombNumber = 0;
    public int maxBombNumber = 10;
    public float timer = 0;
    public float bombSpawnTime = 5f;

    public GameObject bombPrefab;
    public Transform bombSpawnPoint;

    public List<GameObject> bombList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        SpawnBomb();

    }

    public void SpawnBomb()
    {
        foreach (GameObject bomb in bombList)
        {
            if (bomb == null)
            {
                bombList.Remove(bomb);
                break;
            }
        }

        if (timer >= bombSpawnTime && bombList.Count < maxBombNumber)
        {
            timer = 0f;
            GameObject bomb = Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
            bombList.Add(bomb);
        }
    }


}
