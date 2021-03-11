using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay;
    public float respawnDelay;

    public float shakeAmount;
    public bool readyToShake;

    private Rigidbody2D rb;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToShake)
        {
            Vector3 newPos = startPos + Random.insideUnitSphere * (Time.deltaTime * shakeAmount);
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;

            transform.position = newPos;
        }


    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(falling(fallDelay));
            Invoke("Respawn", fallDelay + respawnDelay);
        }
    }

    IEnumerator falling(float delay)
    {
        startPos = transform.position;
        yield return new WaitForSeconds(delay);
        readyToShake = true;
        yield return new WaitForSeconds(1.0f);
        rb.isKinematic = false;
    }

    void Respawn()
    {
        transform.position = startPos;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        readyToShake = false;
    }

}


