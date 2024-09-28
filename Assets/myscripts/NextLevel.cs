using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public void Home()
    {
        SceneManager.LoadScene("Main-Manu");
    }
    public void Restart()
    {
        SceneManager.LoadScene("My_game");

        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SelectPlayer()
    {
        SceneManager.LoadScene("Player-Select");
    }
    public void nextLevel()
    {
        if (GameManager.manager.BossBattle == 1)
        {

            SceneManager.LoadScene("Level-2");
        }
        if (GameManager.manager.BossBattle == 2)
        {
            SceneManager.LoadScene("Level-3");
        }
    }
}
