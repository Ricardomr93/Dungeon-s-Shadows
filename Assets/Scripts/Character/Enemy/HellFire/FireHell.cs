using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHell : MonoBehaviour
{
    public float speed = 2;
    public bool right;
    public float lifeTime = 8;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
