using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool drawDebugRaycast = true;
    
    [Header("Move Properties")]
    public float speed = 1;
    public float coyoteDuration = .05f;
    public float maxFallSpeed = -2.5f;
    
    [Header("Jump Properties")]
    public float jumpForce = 3f;             //fuerza inicial
    public float jumpHoldForce = 1.9f;      //incrementos de fuerza al tener pulsada la tecla saltar
    public float jumpHoldDuration = .1f;    //duracion de mantener la tecla pulsada
    
    [Header("Enviroment Properties")]
    public float footOffset = .4f;    //distancia de rayos en pies en x
    public float headClearance = .5f; //distancia de muro a cabeza
    public float groundDistance = .2f;  //distancia de pj al suelo
    public LayerMask groundLayer;       //layer de suelo

    [Header("Status Booleans")]
    public bool isGround;   //si está en el suelo
    public bool isJumping;  //está saltando
    public bool isHeadBlocked;  //tiene la cabeza atrapada


    PlayerInput input;
    Rigidbody2D rigidBody;
    BoxCollider2D bodyCollider;
    Collider2D attack;
    Animator anim;

    float jumpTime;
    float coyoteTime;
    bool attacking;
    bool recovering;
    float origXScale; // para flipear todo del personaje
    int direction = 1;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        origXScale = transform.localScale.x;
        attack = gameObject.transform.GetChild(1).GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        PysicsCheck();
        //procesar los movimientos
        GroundMovement();
        AirMovement();
        AttackMovent();
    }
    private void AttackMovent()
    {
        if (input.attackPressed && !attacking)
        {
            anim.SetTrigger("Attack");
        }

    }
    private void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        attacking = stateInfo.IsName("PJ_Attack");
        recovering = stateInfo.IsName("PlayerRespawn");

        if (attacking)
        {
            attack.enabled = true;
        }
        else
        {
            attack.enabled = false;
        }
        
    }
    private void AirMovement()
    {
        if (input.jumPressed && !isJumping && (isGround || coyoteTime > Time.time))
        {
            if (!isHeadBlocked)
            {
                isGround = false;
                isJumping = true;
                //guardamos la variable para ver cuanto vamos a permitir que se salte
                jumpTime = Time.time + jumpHoldDuration;
                rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                AudioManager.PlayJumpAudio();
            }

        }
        else if (isJumping)
        {
            //si sigue presionando le damos un poco mas de impulso para que suba un poco mas
            if (input.jumpHeld) rigidBody.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
            if (jumpTime <= Time.time) isJumping = false;//ha pasado el tiempo para que no siga saltando
        }
        if (rigidBody.velocity.y < maxFallSpeed) rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxFallSpeed);//definimos el maximo de velocidad de caida
    }
    void FlipCharacterDirection()
    {
        direction *= -1;//si vale 1 lo cambie a -1 y viceversa
        //guarda la escala actual y cambia el valor de la x y vuelve a aplicar para actualizarlo
        Vector3 scale = transform.localScale;
        scale.x = origXScale * direction;
        transform.localScale = scale;
    }

    private void GroundMovement()
    {
        float xVelocity = speed * input.horizontal;
        if (xVelocity * direction < 0f) FlipCharacterDirection();// si la velocidad es negativa hace el flip
        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
        if (isGround) coyoteTime = Time.time + coyoteDuration;// para que pueda saltar un poco mas aun estando en el aire
    }

    private void PysicsCheck()
    {
        isGround = false;
        isHeadBlocked = false;
        //RayCast de los pies
        RaycastHit2D footCheck = Raycast(new Vector2(footOffset, 0f), Vector2.down, groundDistance);
        //si tiene los pies en el suelo ponemos la variable a true
        if (footCheck) isGround = true;

        //Raycast de la cabeza
        RaycastHit2D headCheck = Raycast(new Vector2(0f, bodyCollider.size.y), Vector2.up, headClearance);
        if (headCheck) isHeadBlocked = true;

    }
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        return Raycast(offset, rayDirection, length, groundLayer);
    }
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        //guardamos la posicion del personaje
        Vector2 pos = transform.position;
        //enviamos el rayo
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        if (drawDebugRaycast)
        {
            Color color = hit ? Color.red : Color.green;
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }
        return hit;
    }
}

