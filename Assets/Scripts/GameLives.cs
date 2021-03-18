using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLives : MonoBehaviour
{

    public static int lives = 3;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
