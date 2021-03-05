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
        if (collision.gameObject.CompareTag("Attack"))
        {
            enemy.gameObject.GetComponent<Collider2D>().enabled = false;
            animator.SetBool("Dead", true);
            hellFire.Dead = true;
            Invoke("Destroy_Enemy", 3.0f);
        }
    }
    private void Destroy_Enemy()
    {
        enemy.SetActive(false);
        Destroy(enemy);
    }
}
