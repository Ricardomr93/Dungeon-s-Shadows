﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBroke : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator wallAnim;
    public int numKick = 3;
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            wallAnim.Play("WallBroken");
            numKick--;
            if (numKick <= 0)
            {
                wallAnim.Play("Destroy");
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, 2.4f);
            }
        }
    }
}
