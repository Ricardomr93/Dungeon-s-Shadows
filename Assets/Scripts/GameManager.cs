using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager current;
    private int lives;
    private bool gameOver;
    private bool dead;
    private List<ItemMission> items;
    public StartEnd startEnd;
    //SceneFader sceneFader; TODO->
    private void Start()
    {
        lives = 3;
        dead = false;
    }
    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        items = new List<ItemMission>();
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        Debug.Log("Vidas" + lives);
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
        if (current.items.Count == 0) current.startEnd.End = true;
        //UIManager.UpdateItemsUI(current.items.Count); TODO ->
    }
    public static void PlayerHit()
    {
        if (current == null) return;
        current.lives--;
        AudioManager.PlayHitAudio();
        PlayerDied();
        //UImanager.UpdateLivesUI(current.lives); ->
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
            current.dead = true;
            //TODO -> implementar metodo para enseñar pantalla muerte etc...
        }
        return current.dead;
    }
    void RestartScene()
    {
        items.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void PlayerWon()
    {
        current.gameOver = true;
    }
}
