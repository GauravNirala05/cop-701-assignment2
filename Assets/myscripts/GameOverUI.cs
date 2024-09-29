using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
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

        SceneManager.LoadScene("Player-Select");
    }

    public void selectPlayer()
    {
        audioManager.jumpSFX(audioManager.button);

        SceneManager.LoadScene("Player-Select");
    }

}
