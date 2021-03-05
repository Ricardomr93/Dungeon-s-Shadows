﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellFire : MonoBehaviour
{
    public GameObject fire;
    private float waitTime;
    public float waitTimeToAttack = 3;
    public Animator anim;
    public Transform positionFire;
    private bool dead;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = waitTimeToAttack;
        dead = false;
    }
    public bool Dead { get; set; }
    // Update is called once per frame
    void Update()
    {
        if (waitTime <= 0 && !dead)
        {
            waitTime = waitTimeToAttack;
            anim.Play("BallFire");
            Invoke("LaunchFire", .5f);
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
    public void LaunchFire()
    {
        Instantiate(fire, positionFire.position, Quaternion.identity);
    }
}
