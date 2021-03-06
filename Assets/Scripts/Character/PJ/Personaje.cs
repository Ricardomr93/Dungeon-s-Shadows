using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    //atributos propios del pj
    private int lives;
    private bool dead;
    private Rigidbody2D rb2d;
    private bool damage;
    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

    public bool Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public bool Dead
    {
        get { return dead; }
        set { dead = value; }
    }

    public float velPlayer = 1;
    public float velPlayerJump = 2.9f;
    public float fallMultipler;
    public float lowMultipler;
    public float jumpHoldDuration = .1f;
    private bool movRig;
    private bool movLef;
    private bool attacking;
    private bool recovering;
    private bool direRig;
    private bool wakeUp;
    //private float horizMove = 0;
    //private float vertMove = 0;
    //public Joystick joystick;
    public float runSpeedHorizontal;
    public float runSpeedVertical;
    public GameObject pj;
    public Animator animator;
    public Collider2D[] attackCol;
    public float timeDontDamage = 1.3f;
    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        dead = false;
        rb2d = GetComponent<Rigidbody2D>();
        attackCol[0].enabled = false;
        attackCol[1].enabled = false;
        Damage = true;
    }

    // Update is called once per frame
    void Update()
    {
        // horizMove = joystick.Horizontal * runSpeedHorizontal;
        // vertMove = joystick.Vertical * runSpeedVertical;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        attacking = stateInfo.IsName("PJ_Attack");
        recovering = stateInfo.IsName("PlayerRespawn");
        wakeUp = stateInfo.IsName("WakeUp");
        if (Input.GetKey(KeyCode.A) && !dead && !recovering && !wakeUp)
        {
            Run(-velPlayer, true);
            direRig = false;
        }
        else if (Input.GetKey(KeyCode.D) && !dead && !recovering && !wakeUp)
        {
            Run(velPlayer, false);
            direRig = true;
        }
        else
        {
            animator.SetBool("Run", false);
        }
        if (Input.GetKeyDown(KeyCode.W) && !CheckGround.jumping && !dead && !recovering && !wakeUp)
        {
            Jump();
            animator.SetBool("Jump", true);
        }
        if (attacking)
        {
            if (direRig)
            {
                attackCol[0].enabled = true;
            }
            else
            {
                attackCol[1].enabled = true;
            }
        }
        else
        {
            attackCol[0].enabled = false;
            attackCol[1].enabled = false;
        }
        if (rb2d.velocity.y < 0 && CheckGround.jumping)
        {
            animator.SetBool("Falling", true);
        }
        else if (rb2d.velocity.y > 0)
        {
            animator.SetBool("Falling", false);
        }
        CompruebaMuerto();
        if (Input.GetKeyDown(KeyCode.Space) && !attacking && !recovering && !wakeUp)
        {
            Attack();
        }

    }
    private void Damage_again()
    {
        damage = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion") && !dead) lives++;
        if (collision.gameObject.CompareTag("Respawn") && damage && !dead)
        {
            damage = false;
            Invoke("Damage_again", timeDontDamage);
            AudioManager.PlayRespawnAudio();
        }
        if (collision.gameObject.CompareTag("Enemy") && damage && !dead)
        {
            damage = false;
            lives--;
            Invoke("Damage_again", timeDontDamage);
            AudioManager.PlayHitAudio();
        }
        Debug.Log("NUMERO DE VIDAS: " + lives);

    }
    private void CompruebaMuerto()
    {
        if (lives <= 0 && !CheckGround.jumping && !dead)//si no tiene vidas y no está en el aire
        {
            Debug.Log("Entra en muerto");
            AudioManager.PlayDeadAudio();
            animator.SetBool("Dead", true);
            dead = true;
        }
    }
    private void Run(float dire, bool flip)
    {
        rb2d.velocity = new Vector2(dire, rb2d.velocity.y);
        pj.GetComponent<SpriteRenderer>().flipX = flip;
        animator.SetBool("Run", true);
        if (!CheckGround.jumping)
        {
            AudioManager.PlayRunAudio();
        }
    }
    public void Jump()
    {
        CheckGround.jumping = true;
        rb2d.AddForce(new Vector2(0f, velPlayerJump), ForceMode2D.Impulse);
        AudioManager.PlayJumpAudio();
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        //swordSource.Play();
    }
}
