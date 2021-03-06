using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSword : MonoBehaviour
{

    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            AudioManager.PlayGroundKickAudio();
        }
    }
}
