using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TideSoundScript : MonoBehaviour
{
    public AudioClip[] tideSounds;

    public AudioSource audioSource;


    private int n = 0;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TideCoroutine());
        n = tideSounds.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TideCoroutine()
    {
        while(true)
        {
            int i = Random.Range(0, n);
            audioSource.PlayOneShot(tideSounds[i]);

            float r = Random.Range(40f, 50f);
            yield return new WaitForSeconds(r);
        }
    }
}
