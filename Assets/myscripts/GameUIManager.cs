using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public GameObject PauseManu;
    private bool isPaused = false;
    private Button btn;
    void Start()
    {
        if (PauseManu)
        {
            PauseManu.SetActive(false);
        }
    }
    void OnEnable()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "My_game")
        {
            player.OnOver += GameOver;
        }
    }
    void OnDisable()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "My_game")
        {
            player.OnOver -= GameOver;
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("Game-Over");
        print("Game over called");
    }

    public void Home()
    {
        if (PauseManu)
        {
            ResumeGame();
        }
        SceneManager.LoadScene("Main-Manu");
    }
    public void Restart()
    {
        if (PauseManu)
        {
            ResumeGame();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SelectPlayer()
    {
         if (PauseManu)
        {
            ResumeGame();
        }
        SceneManager.LoadScene("Player-Select");
    }
    public void Pause()
    {
        // SceneManager.LoadScene("Player-Select");
        if (isPaused)
        {
            PauseManu.SetActive(false);
            ResumeGame();
        }
        else
        {
            PauseManu.SetActive(true);
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0; // Freezes game time
        isPaused = true;
        // Optionally: Disable player input, UI updates, or show a pause menu
    }

    void ResumeGame()
    {
        Time.timeScale = 1; // Resumes game time
        isPaused = false;
        // Optionally: Enable player input, hide pause menu
    }
}
