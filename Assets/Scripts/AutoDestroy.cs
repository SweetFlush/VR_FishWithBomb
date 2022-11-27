using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", destroyTime);            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
