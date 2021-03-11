using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            AudioManager.PlayEnemyKick();
            animator.Play("Dead");
            Invoke("Destroy_Enemy", 3.0f);
        }
    }
    private void Destroy_Enemy()
    {
        Destroy(gameObject,.4f);
    }
}
