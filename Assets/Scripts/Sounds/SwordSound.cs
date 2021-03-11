using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSound : MonoBehaviour
{
    public Collider2D[] swords;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Collider2D item in swords)
        {
            if (item.CompareTag("Enemy"))
            {
                AudioManager.PlayEnemyKick();
            }else if (item.CompareTag("Ground"))
            {
                AudioManager.PlayGroundKickAudio();
            }
        }
    }
}
