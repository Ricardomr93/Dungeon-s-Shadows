using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBroke : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator wallAnim;
    public int numKick = 3;
    private AudioSource mySource;
    public AudioClip[] clips;
    // Update is called once per frame
    void Update()
    {
        mySource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            wallAnim.Play("WallBroken");
            AudioManager.PlayGroundKickAudio();
            numKick--;
            if (numKick <= 0)
            {
                AudioManager.PlayGroundDestroyAudio();
                wallAnim.Play("Destroy");
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject,wallAnim.GetCurrentAnimatorStateInfo(0).length);
            }
        }
    }
}
