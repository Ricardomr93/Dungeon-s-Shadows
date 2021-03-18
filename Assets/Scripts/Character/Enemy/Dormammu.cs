using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dormammu : MonoBehaviour
{
    private float waitTime;
    public float waitTimeToAttack = 2;
    public Animator anim;
    private bool dead;
    private int damage = 0;
    private bool hitting;
    private bool hit;
    private bool deadEnemy = false;
    public static bool sound = false;


    public bool Dead
    {
        get { return dead; }
        set { dead = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        waitTime = waitTimeToAttack;
        dead = false;
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        hitting = stateInfo.IsName("Dead");

        if (hitting)
        {
            hit = false;
        }
        else
        {
            hit = true;
        }

        if (waitTime <= 0 && !dead && !deadEnemy)
        {
            waitTime = waitTimeToAttack;

            Debug.Log("Dormammu.sound " + sound);
            if (sound)
            {
                AudioManager.PlayFinalEnemyAttack();
            }
                
            anim.Play("FinalEnemy-Attack");
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") && hit)
        {
            AudioManager.PlayEnemyKick();

            if (damage < 4)
            {
                damage++;
                anim.Play("Dead");

                if (damage == 4)
                {
                    deadEnemy = true;
                    damage = 0;
                    Invoke("Destroy_Enemy", 1.0f);
                }
            }
        }
    }
    private void Destroy_Enemy()
    {
        //Destroy(gameObject, .4f);
        AudioManager.PlayFinalEnemyDead();
        anim.Play("FireDead");
        sound = false;
        Invoke("LoadingCredit", 5.0f);
    }


    private void LoadingCredit()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
