using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public Text vidasTxt;
    static GameManager current;
    private int lives;
    private bool gameOver;
    private bool dead;
    private bool respawn;
    private List<ItemMission> items;
    public StartEnd startEnd;
    //SceneFader sceneFader; TODO->

    public void Play()
    {
        GameManager.PlayerNextScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameObject.Find("GameOver").GetComponentInChildren<Text>().text = "";
        Awake();
    }

    private void Awake()
    {   
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        lives = 3;
        dead = false;
        Debug.Log(dead);
        current = this;
        items = new List<ItemMission>();
        DontDestroyOnLoad(gameObject);
        GameObject.Find("Text").GetComponentInChildren<Text>().text = "x " + current.lives;
    }
    private void Update()
    {
        if (gameOver) return;
    }
    public static bool IsDead()
    {
        if (current == null) return false;
        return current.dead;
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
        if(0 < current.lives)
        {
            current.lives--;
            //current.vidasTxt.text = "x " + current.lives;
            GameObject.Find("Text").GetComponentInChildren<Text>().text = "x " + current.lives;
            AudioManager.PlayHitAudio();
            PlayerDied();

            //UImanager.UpdateLivesUI(current.lives); ->
        }
    }
    public static void PlayerUpLives()
    {
        if (current == null) return;
        current.lives++;
    }
    public static bool PlayerDied()
    {
        if (current == null) return false;
        if (current.lives <= 0 && !current.dead)
        {
            Debug.Log("current.lives " + current.lives + " - current.dead " + current.dead);
            current.dead = true;
            //TODO -> implementar metodo para enseñar pantalla muerte etc...
        }
        return current.dead;
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
