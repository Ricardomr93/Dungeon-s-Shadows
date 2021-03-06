﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEnd : MonoBehaviour
{
    private bool start;
    private bool end;
    public Animator animGround;
    public Animator pjAnim;

    public bool End
    {
        get { return end; }
        set { end = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        start = false;
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Start: " + start + " end: " + end);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!start && !end)
        {
            animGround.Play("HideGround");
            start = true;
            Invoke("Destroy", 6);
        }else if (start && end)
        {
            pjAnim.Play("UpStairs");
            Invoke("NextLevel", pjAnim.GetCurrentAnimatorStateInfo(0).length);
        }
    }
    private void NextLevel()
    {
        SceneManager.LoadScene("Level2");
    }
}
