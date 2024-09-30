using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public GameObject PauseManu;
    AudioManager audioManager;
    private bool isPaused = false;
    private Button btn;
    void Start()
    {
        audioManager = GameObject.FindWithTag("audio").GetComponent<AudioManager>();
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
        audioManager.jumpSFX(audioManager.button);
        if (PauseManu)
        {
            ResumeGame();
        }
        SceneManager.LoadScene("Main-Manu");
    }
    public void Restart()
    {
        audioManager.jumpSFX(audioManager.button);

        if (PauseManu)
        {
            ResumeGame();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SelectPlayer()
    {
        audioManager.jumpSFX(audioManager.button);

         if (PauseManu)
        {
            ResumeGame();
        }
        SceneManager.LoadScene("Player-Select");
    }
    public void Pause()
    {
        audioManager.jumpSFX(audioManager.button);

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
