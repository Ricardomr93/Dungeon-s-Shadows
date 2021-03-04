using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    private int lives;
    private bool dead;
    private Rigidbody2D rb2d;
    public float velPlayer = 1;
    public float velPlayerJump = 2.9f;
    public float fallMultipler;
    public float lowMultipler;
    public float jumpHoldDuration = .1f;
    private int coins;
    private bool damage;
    private bool movRig;
    private bool movLef;
    private bool attacking;
    private AudioSource pjAudioSource;
    //private float horizMove = 0;
    //private float vertMove = 0;
    // public Joystick joystick;
    public float runSpeedHorizontal;
    public float runSpeedVertical;
    public GameObject pj;
    public Animator animator;
    public Collider2D attackCol;
    public bool realJumping;

    public AudioSource swordSource;
    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        dead = false;
        rb2d = GetComponent<Rigidbody2D>();
        attackCol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // horizMove = joystick.Horizontal * runSpeedHorizontal;
        // vertMove = joystick.Vertical * runSpeedVertical;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        attacking = stateInfo.IsName("PJ_Attack");
        if (attacking)
        {
            damage = false;
            Invoke("no_Damage", 2.0f);
            attackCol.enabled = true;
        }
        else attackCol.enabled = false;
        if (rb2d.velocity.y < 0 && CheckGround.jumping)
        {
            animator.SetBool("Falling", true);
        }
        else if (rb2d.velocity.y > 0)
        {
            animator.SetBool("Falling", false);
        }

        animator.SetBool("Jump", CheckGround.jumping);

    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Run(-velPlayer, true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Run(velPlayer, false);
        }
        else
        {
            animator.SetBool("Run", false);
        }
        if (Input.GetKey(KeyCode.W) && !CheckGround.jumping)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            Attack();
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
            float jumpTime = Time.time + jumpHoldDuration;
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
