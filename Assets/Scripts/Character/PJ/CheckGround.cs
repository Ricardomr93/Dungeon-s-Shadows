using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool jumping;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Debug.Log("Entra Trigger");
            jumping = false;
            anim.SetBool("Falling", false);
            anim.SetBool("Jump", false);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {

        }
    }
}
