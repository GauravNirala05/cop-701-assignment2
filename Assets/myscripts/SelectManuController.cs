using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManuController : MonoBehaviour
{
    public GameObject stats1;
    public GameObject stats2;
    private Transform arrow;
    private bool checker = true;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindWithTag("audio").GetComponent<AudioManager>();
        stats2.SetActive(false);
    }
    public void Selecting()
    {
        audioManager.jumpSFX(audioManager.button);
        arrow = GameObject.FindWithTag("selector").transform;
        string selected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        int selectedIndex = int.Parse(selected);


        // Debug.Log("clicked : " + selectedIndex);
        if (selectedIndex == 0)
        {
            stats1.SetActive(true);
            stats2.SetActive(false);
            checker = false;

            GameManager.manager.select = selectedIndex;
            GameManager.manager.fireRateinfo = 0.5f;
            GameManager.manager.speedinfo = 8f;
            Vector3 newPosition = arrow.position;
            newPosition.x = -4;
            arrow.position = newPosition;
            // arrow.position.x = -4;
            // print(" 0 clicked");
        }
        if (selectedIndex == 1)
        {
            stats1.SetActive(false);
            stats2.SetActive(true);
            checker = false;
            GameManager.manager.select = selectedIndex;
            GameManager.manager.fireRateinfo = 0.2f;
            GameManager.manager.speedinfo = 6f;
            Vector3 newPosition = arrow.position;
            newPosition.x = 0;
            arrow.position = newPosition;
            // arrow.position.x = 4;
            // print(" 1 clicked");
        }
        if (selectedIndex == 2)
        {
            if (checker)
            {
                GameManager.manager.select = 0;
                GameManager.manager.fireRateinfo = 0.5f;
                GameManager.manager.speedinfo = 8f;
            }
            SceneManager.LoadScene("My_game");
        }
    }
}
