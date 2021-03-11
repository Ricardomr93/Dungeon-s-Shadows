using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour
{
    public Animator animator;
    public GameObject enemy;
    public HellFire hellFire;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") && !hellFire.Dead)
        {
            enemy.gameObject.GetComponent<Collider2D>().enabled = false;
            hellFire.Dead = true;
            AudioManager.PlayEnemyKick();
            animator.SetBool("Dead", true);
            Invoke("Destroy_Enemy", 3.0f);
        }
    }
    private void Destroy_Enemy()
    {
        Destroy(enemy,.4f);
    }
}
