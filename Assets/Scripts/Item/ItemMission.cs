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
        if (collision.gameObject.CompareTag("Player"))
        {
            startEnd.End = true;
            GameManager.PlayerColletedItemMission(this);
            gameObject.SetActive(false);
            Destroy(gameObject, 1.5f);
        }
    }
}
