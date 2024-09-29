using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindWithTag("audio").GetComponent<AudioManager>();
    }
    public void Home()
    {
        audioManager.jumpSFX(audioManager.button);
        SceneManager.LoadScene("Main-Manu");
    }
    public void Restart()
    {
        audioManager.jumpSFX(audioManager.button);

        SceneManager.LoadScene("My_game");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SelectPlayer()
    {
        audioManager.jumpSFX(audioManager.button);

        SceneManager.LoadScene("Player-Select");
    }
    public void nextLevel()
    {
        audioManager.jumpSFX(audioManager.button);

        if (GameManager.manager.BossBattle == 1)
        {
            SceneManager.LoadScene("Level-2");
        }
        if (GameManager.manager.BossBattle == 3)
        {
            SceneManager.LoadScene("Level-3");
        }
    }
}
