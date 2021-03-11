using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Animator pjanimator;
    public Transform positionRespawn;
    public GameObject PJ;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDamage();
            AudioManager.PlayRespawnAudio();
        }
    }
    public void PlayerDamage()
    {
        PJ.transform.position = (new Vector2(positionRespawn.position.x, positionRespawn.position.y));
        GameManager.PlayerHit();
        if (!GameManager.IsDead())
        {
            pjanimator.Play("PlayerRespawn");
        }
    }
}
