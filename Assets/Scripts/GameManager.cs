using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public Text vidasTxt;
    static GameManager current;
    //private static int lives;
    private bool gameOver;
    private static bool dead;
    private bool respawn;
    private List<ItemMission> items;
    public StartEnd startEnd;
    //SceneFader sceneFader; TODO->

    public void Exit()
    {
        Application.Quit();
    }

    public void MenuButton()
    {
        Restart();
        AudioManager.RestartAudio();
        SceneManager.LoadScene("MainScene");
    }

    public void Restart()
    {
        Debug.Log("Restart");
        AudioManager.RestartAudio();
        GameLives.lives = 3;
        GameObject.Find("Lives").GetComponentInChildren<Text>().text = "";
        GameObject.Find("Lives").GetComponentInChildren<Text>().text = "x " + GameLives.lives;
        dead = false;

        AudioManager.PlayEnemyKick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void Awake()
    {   
        if (current != null && current != this)
        {
            //Destroy(gameObject);
            return;
        }
        
        dead = false;
        Debug.Log(dead);
        current = this;
        items = new List<ItemMission>();
        //DontDestroyOnLoad(gameObject);
        GameObject.Find("Lives").GetComponentInChildren<Text>().text = "";
        GameObject.Find("Lives").GetComponentInChildren<Text>().text = "x " + GameLives.lives;
    }
    private void Update()
    {
        if (gameOver) return;
    }
    public static bool IsDead()
    {
        //if (current == null) return false;
        return dead;
    }
    public static bool IsGameOver()
    {
        if (current == null) return false;
        return current.gameOver;
    }
    //TODO ->
    /*public static void RegistrerSceneFade(SceneFader fader)
    {
        if (current == null) return;
        current.sceneFader = fader;
    }*/
    public static void RegistrerItem(ItemMission item)
    {
        if (current == null) return;
        if (!current.items.Contains(item))
        {
            current.items.Add(item);
            Debug.Log(current.items.Count);
        }

        //UIManager.UpdateItemsUI(current.items.Count); TODO ->
    }
    public static void PlayerColletedItemMission(ItemMission item)
    {
        if (current == null) return;
        //si no está en la lista no hace nada
        if (!current.items.Contains(item)) return;
        current.items.Remove(item);
        if (item.CompareTag("Key")) 
        {
            AudioManager.PlayKeyAudio();
        }
        if (item.CompareTag("Lever"))
        {
            AudioManager.PlayKeyAudio();
        }
        if (current.items.Count == 0)
        {
            Debug.Log("Puede ir para terminar");
            current.startEnd.End = true;
        }
        //UIManager.UpdateItemsUI(current.items.Count); TODO ->
    }
    public static void PlayerHit()
    {
        if (current == null) return;
        if(0 < GameLives.lives)
        {
            GameLives.lives--;
            //current.vidasTxt.text = "x " + current.lives;
            GameObject.Find("Lives").GetComponentInChildren<Text>().text = "";
            GameObject.Find("Lives").GetComponentInChildren<Text>().text = "x " + GameLives.lives;
            AudioManager.PlayHitAudio();
            PlayerDied();

            //UImanager.UpdateLivesUI(current.lives); ->
        }
    }

    public static void PlayerUpLives()
    {
        if (current == null) return;
        GameLives.lives++;
        GameObject.Find("Lives").GetComponentInChildren<Text>().text = "";
        GameObject.Find("Lives").GetComponentInChildren<Text>().text = "x " + GameLives.lives;
    }
    public static bool PlayerDied()
    {
        if (current == null) return false;
        if (GameLives.lives <= 0 && !dead)
        {
            Debug.Log("current.lives " + GameLives.lives + " - current.dead " + dead);
            dead = true;
            //TODO -> implementar metodo para enseñar pantalla muerte etc...
        }
        return dead;
    }

    public static void RestartScene()
    {
        current.items.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void PlayerWon()
    {
        current.gameOver = true;
    }
    public static void PlayerNextScene()
    {
        current.gameOver = false;
    }
    public static bool IsRespawn()
    {
        if (current == null) return false;
        return current.respawn;
    }
    public static void RespawnTrue()
    {
        current.respawn = true;
    }
    public static void RespawnFalse()
    {
        current.respawn = false;
    }
}
