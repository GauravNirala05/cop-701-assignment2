using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("Main-Manu");
    }
    public void Restart()
    {
        SceneManager.LoadScene("My_game");
    }

    public void selectPlayer()
    {
        SceneManager.LoadScene("Player-Select");
    }
    
}
