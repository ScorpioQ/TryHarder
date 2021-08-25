using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerController player;
    private Door doorExit;

    public bool gameOver;

    public List<CombatUnit> enemies = new List<CombatUnit>();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // player = FindObjectOfType<PlayerController>();
        doorExit = FindObjectOfType<Door>();
    }

    public void Update()
    {
        if (player != null)
        {
            gameOver = player.isDead;
        }
        
        UIManager.instance.GameOverUI(gameOver);
    }

    public void IsEnemy(CombatUnit enemy)
    {
        enemies.Add(enemy);
    }

    public void EnemyDead(CombatUnit enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            doorExit.OpenDoor();
        }
    }

    public void IsPlayer(PlayerController controller)
    {
        player = controller;
    }

    public void IsExitDoor(Door door)
    {
        doorExit = door;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.DeleteKey("playerHealth");
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("sceneIndex"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));
        }
        else
        {
            NewGame();
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public float LoadHeadth()
    {
        if (!PlayerPrefs.HasKey("playerHealth"))
        {
            PlayerPrefs.SetFloat("playerHealth", 3f);
        }

        float currentHealth = PlayerPrefs.GetFloat("playerHealth");
        return currentHealth;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("playerHealth", player.health);
        PlayerPrefs.SetInt("sceneIndex", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.Save();
    }
}
