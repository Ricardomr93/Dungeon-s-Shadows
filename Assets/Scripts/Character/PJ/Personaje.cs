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
    private AudioSource pjAudioSource;
    //private float horizMove = 0;
    //private float vertMove = 0;
    //public Joystick joystick;
    public float runSpeedHorizontal;
    public float runSpeedVertical;
    public GameObject pj;
    public Animator animator;
    public Collider2D[] attackCol;
    public float timeDontDamage = 1.3f;

    //public AudioSource swordSource;
    // Start is called before the first frame update
    void Start()
    {
        lives = 5;
        dead = false;
        rb2d = GetComponent<Rigidbody2D>();
        attackCol[0].enabled = false;
        attackCol[1].enabled = false;
        Debug.Log("NUMERO DE VIDAS: " + lives);
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
        animator.SetBool("Jump", CheckGround.jumping);
        if (Input.GetKeyDown(KeyCode.Space) && !attacking && !recovering)
        {
            Attack();
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) && !dead && !recovering)
        {
            Run(-velPlayer, true);
            direRig = false;
        }
        else if (Input.GetKey(KeyCode.D) && !dead && !recovering)
        {
            Run(velPlayer, false);
            direRig = true;
        }
        else
        {
            animator.SetBool("Run", false);
        }
        if (Input.GetKey(KeyCode.W) && !CheckGround.jumping && !dead && !recovering)
        {
            Jump();
        }

    }
    private void Damage_again()
    {
        damage = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn") && damage && !dead)
        {
            damage = false;
            //tiempo hasta que invoca el daño otra vez
            Invoke("Damage_again", timeDontDamage);
            //pjAudioSource.Play();  
        }
        if (collision.gameObject.CompareTag("Enemy") && damage && !dead)
        {
            damage = false;
            lives--;
            Invoke("Damage_again", timeDontDamage);
            animator.Play("PJ_Hit");
            // pjAudioSource.clip = hitClip;
        }
        Debug.Log("NUMERO DE VIDAS: " + lives);

    }
    private void CompruebaMuerto()
    {
        if (lives <= 0 && !CheckGround.jumping)//si no tiene vidas y no está en el aire
        {
            animator.SetBool("Dead", true);
            dead = true;
        }
    }
    private void Run(float dire, bool flip)
    {
        rb2d.velocity = new Vector2(dire, rb2d.velocity.y);
        pj.GetComponent<SpriteRenderer>().flipX = flip;
        animator.SetBool("Run", true);
    }
    public void Jump()
    {
        if (!dead)
        {
            rb2d.AddForce(new Vector2(0f, velPlayerJump), ForceMode2D.Impulse);
            // rb2d.velocity = new Vector2(rb2d.velocity.x, velPlayerJump);
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        //swordSource.Play();
    }
}
