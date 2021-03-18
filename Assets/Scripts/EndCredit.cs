using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredit : MonoBehaviour
{
    private void Start()
    {
        Invoke("MenuButton", 19f);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
