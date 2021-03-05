using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Animator pjanimator;
    public Transform positionRespawn;
    public Personaje pjScripit;
    public GameObject PJ;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Entra en collision");
            PlayerDamage();
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !pjScripit.Dead)
        {
            Debug.Log("Entra en trigger");
            PlayerDamage();
        }
    }
    public void PlayerDamage()
    {
        PJ.transform.position = (new Vector2(positionRespawn.position.x, positionRespawn.position.y));
        pjanimator.Play("PlayerRespawn");
        pjScripit.Lives --;
    }
}
