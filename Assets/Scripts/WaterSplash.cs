using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    public GameObject waterSplash;

    private float timer = 0f;

    private float splashTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(timer >= splashTime && other.tag == "Water")
        {
            timer = 0f;
            Instantiate(waterSplash, transform.position, Quaternion.identity);
        }
    }
}
