using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void PlayButton()
    {
        AudioManager.PlayEnemyKick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MenuButton()
    {
        AudioManager.RestartAudio();
        SceneManager.LoadScene("MainScene");
    }
}
