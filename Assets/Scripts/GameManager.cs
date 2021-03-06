using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager current;

    private bool start;
    private bool end;
    List<Item> items;
    
    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        items = new List<Item>();
        DontDestroyOnLoad(gameObject);
    }

}
