using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personaje : MonoBehaviour
{
    //atributos propios del pj
    private bool damage;

    public GameObject gameOverTxt;
    public GameObject restartButton;
    public GameObject exitButton;

    public bool Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private bool hitting;
    private bool recovering;
    private bool attacking;
    //private float horizMove = 0;
    //private float vertMove = 0;
    //public Joystick joystick;
    public Animator animator;
    public Collider2D[] attackCol;
    public float timeDontDamage = 1.3f;
    // Start is called before the first frame update
    void Start()
    {
        Damage = true;

        Debug.Log("NO LO MOSTRAMOS");
        GameObject.Find("GameOver").GetComponentInChildren<Text>().text = "";
        restartButton.SetActive(false);
        exitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // horizMove = joystick.Horizontal * runSpeedHorizontal;
        // vertMove = joystick.Vertical * runSpeedVertical;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        recovering = stateInfo.IsName("PlayerRespawn");
        hitting = stateInfo.IsName("PJ_Hit");
        attacking = stateInfo.IsName("PJ_Attack");
        CompruebaMuerto();

        if (recovering || hitting)
        {
            damage = false;
            GameManager.RespawnTrue();
        }
        else
        {
            GameManager.RespawnFalse();
            damage = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && damage)
        {

            Debug.Log("Entra OnTriggerEnter2D - attacking " + attacking);

            if (!attacking)
            {

                Debug.Log("Entra OnTriggerEnter2D - PJ_Hit ");
                GameManager.PlayerHit();
                animator.Play("PJ_Hit");
                Invoke("Damage_again", timeDontDamage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Potion"))GameManager.PlayerUpLives();
        if (collision.gameObject.CompareTag("Respawn"))
        {
            AudioManager.PlayRespawnAudio();
        }

        //Debug.Log("Entra OnTriggerEnter2D - PERSONAJE " + collision.gameObject.tag + " DAMAGE " + damage);

        if (collision.gameObject.CompareTag("Enemy") && damage)
        {

            //Debug.Log("Entra OnTriggerEnter2D - attacking " + attacking);

            if (!attacking)
            {

                //Debug.Log("Entra OnTriggerEnter2D - PJ_Hit ");
                GameManager.PlayerHit();
                animator.Play("PJ_Hit");
                Invoke("Damage_again", timeDontDamage);
            }
        }
    }
    private void CompruebaMuerto()
    {
        if (GameManager.PlayerDied())//si no tiene vidas y no está en el aire
        {
            Debug.Log("Entra en muerto");
            animator.Play("PJ_Dead");

            GameObject.Find("GameOver").GetComponentInChildren<Text>().text = "Game Over";
            restartButton.SetActive(true);
            exitButton.SetActive(true);
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        //swordSource.Play();
    }
    public void restart()
    {
        GameManager.RestartScene();
    }
}
