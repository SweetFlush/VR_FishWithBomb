using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public ParticleSystem fire;
    public ParticleSystem explosion;
    public GameObject waterSplash;

    private OVRGrabbable grabbable;

    public bool isAboutToExplode = false;
    public bool isExploded = false;
    public float timer = 0f;
    public float explodeTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbable.isGrabbed && !isAboutToExplode)
        {
            fire.gameObject.SetActive(true);
            isAboutToExplode = true;
        }

        if(isAboutToExplode && !grabbable.isGrabbed)
        {
            timer += Time.deltaTime;
            if(timer >= explodeTime && !isExploded)
            {
                Explode();
            }
        }
    }

    public void Explode()
    {
        isExploded = true;
        Instantiate(explosion, transform.position, Quaternion.identity);
        GameObject[] fishes = new GameObject[20];
        fishes = GameObject.FindGameObjectsWithTag("Fish");
        
        foreach(GameObject fish in fishes)
        {
            if (fish == null)
                break;
            else if (!fish.GetComponent<Fish>().isDead)
                fish.GetComponent<Fish>().TakeBombDamage();
        }
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water" && !isExploded)
        {
            Instantiate(waterSplash, transform.position, Quaternion.identity);
            Explode();
        }
    }

}
