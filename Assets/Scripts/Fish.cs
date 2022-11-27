using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameObject player;
    
    public int health;
    public int damage;
    public bool isFighting = true;
    public bool isDead = false;
    public bool isMonster = false;

    private Rigidbody rigid;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;

        player = GameObject.FindGameObjectWithTag("FishPos");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fished()
    {
        //Vector3 direction = player.transform.position - transform.position;
        if (anim)
            anim.enabled = false;
        isFighting = false;
        isDead = true;
        rigid.useGravity = true;
        //rigid.AddForce(transform.up * 20f, ForceMode.Impulse);
        transform.position = player.transform.position;
    }

    public void Run()
    {
        Destroy(gameObject);
    }

    public void TakeBombDamage()
    {
        health -= 100;
    }

    //private IEnumerator Swim()
    //{
    //    while(isFighting)
    //    yield return new WaitForSeconds(1f);
    //}
}
