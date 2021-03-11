using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    //atributos propios del pj
    private bool damage;

    public bool Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private bool hitting;
    private bool recovering;
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
    }

    // Update is called once per frame
    void Update()
    {
        // horizMove = joystick.Horizontal * runSpeedHorizontal;
        // vertMove = joystick.Vertical * runSpeedVertical;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        recovering = stateInfo.IsName("PlayerRespawn");
        hitting = stateInfo.IsName("PJ_Hit");
        CompruebaMuerto();
        if (recovering || hitting)
        {
            damage = false;
        }
        else
        {
            damage = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))GameManager.PlayerUpLives();
        if (collision.gameObject.CompareTag("Respawn"))
        {
            AudioManager.PlayRespawnAudio();
        }
        if (collision.gameObject.CompareTag("Enemy") && damage && !GameManager.PlayerDied())
        {
            GameManager.PlayerHit();
            animator.Play("PJ_Hit");
            Invoke("Damage_again", timeDontDamage);
        }
    }
    private void CompruebaMuerto()
    {
        Debug.Log(GameManager.PlayerDied());
        if (GameManager.PlayerDied())//si no tiene vidas y no está en el aire
        {
            Debug.Log("Entra en muerto");
            animator.Play("PJ_Dead");
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        //swordSource.Play();
    }
}
