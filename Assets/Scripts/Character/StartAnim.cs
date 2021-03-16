using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour
{

    public Animator anim;
    private bool starAnim;

    // Start is called before the first frame update
    void Start()
    {
        anim.gameObject.GetComponent<Animator>().enabled = false;
        starAnim = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && starAnim)
        {
            anim.gameObject.GetComponent<Animator>().enabled = true;
            anim.Play(anim.name);
            starAnim = false;
        }
    }
}
