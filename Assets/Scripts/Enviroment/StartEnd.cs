using System.Collections;
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
    private void Update()
    {
        //Debug.Log("Star " + start + " End " + end);
    }
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        end = false;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!start && !end)
        {
            animGround.Play("HideGround");
            AudioManager.PlayGroundMove();
            start = true;
        }else if (start && end)
        {   
            GameManager.PlayerWon();
            pjAnim.Play("UpStairs");
            Invoke("NextLevel", pjAnim.GetCurrentAnimatorStateInfo(0).length+1);
        }else if (!start && end)
        {
            GameManager.PlayerWon();
            NextLevel();
        }
    }
    private void NextLevel()
    {
        GameManager.PlayerNextScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
