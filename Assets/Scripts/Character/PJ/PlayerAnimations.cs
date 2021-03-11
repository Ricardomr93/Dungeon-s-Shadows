using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerMovement movement;
    Animator anim;
    Rigidbody2D rigidbody;
    PlayerInput input;

    int speedParamID;
    int groundParamID;
    int fallParamID;

    // Start is called before the first frame update
    void Start()
    {
        speedParamID = Animator.StringToHash("speed");
        groundParamID = Animator.StringToHash("isOnGround");
        fallParamID = Animator.StringToHash("verticalVelocity");
        Transform parent = transform.parent;

        movement = parent.GetComponent<PlayerMovement>();
        rigidbody = parent.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        input = parent.GetComponent<PlayerInput>();

        if (movement == null || rigidbody == null || anim == null || input == null)
        {
            Debug.LogError("Falta poner alguno de los componentes en PlayerAnimations");
            Destroy(this);
        }
    }
    void Update()
    {
        anim.SetBool(groundParamID, movement.isGround);
        anim.SetFloat(fallParamID, rigidbody.velocity.y);
        anim.SetFloat(speedParamID, Mathf.Abs(input.horizontal));
    }
    public void StepAudio()
    {
        AudioManager.PlayRunAudio();
    }
    public void SwordDawn()
    {
        AudioManager.PlayDeadAudio();
    }
    public void FootDawn()
    {
        AudioManager.PlayHitAudio();
    }
    public void ShockDawn()
    {
        AudioManager.PlayHitAudio();
    }
}
