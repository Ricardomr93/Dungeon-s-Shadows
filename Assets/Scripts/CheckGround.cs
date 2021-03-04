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
            
            jumping = false;
            Debug.Log("entra al suelo");
            anim.SetBool("Falling", false);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        jumping = true;
        Debug.Log("sale de suelo");
    }
}
