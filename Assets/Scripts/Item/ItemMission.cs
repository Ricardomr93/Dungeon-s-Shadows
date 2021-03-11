using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMission : MonoBehaviour
{
    public StartEnd startEnd;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.RegistrerItem(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Key"))
        {
            GameManager.PlayerColletedItemMission(this);
            gameObject.SetActive(false);
            Destroy(gameObject, 1.5f);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.PlayerColletedItemMission(this);
        }
    }
}
