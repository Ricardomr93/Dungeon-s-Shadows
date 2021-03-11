using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABasic : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRender;
    float speed = .5f;
    float waitTime;
    int i = 0;
    Vector2 actualPos;

    public Transform[] moveEnemy;
    public float startWaitTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CehckEnemyMoving());
        transform.position = Vector2.MoveTowards(transform.position, moveEnemy[i].transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position,moveEnemy[i].transform.position)<.1f)
        {
            if (waitTime<=0)
            {
                if (moveEnemy[i] != moveEnemy[moveEnemy.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                waitTime = Time.deltaTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    IEnumerator CehckEnemyMoving()
    {
        actualPos = transform.position;
        yield return new WaitForSeconds(.5f);
        if (transform.position.x>actualPos.x)
        {
            spriteRender.flipX = true;
        }
        else 
        {
            spriteRender.flipX = false;
        }
    }
}
