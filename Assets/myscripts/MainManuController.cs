using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject instruction;
    void Start(){
        instruction.SetActive(false);
    }
    public void  playGame(){
        string selected = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        int selectedIndex = int.Parse(selected);
        Debug.Log("clicked : " + selected);
       

        GameManager.manager.select=100;
        SceneManager.LoadScene("Player-Select");
    }
    public void  Instuctions(){
       instruction.SetActive(true);
    }
    public void close(){
        instruction.SetActive(false);
    }
}
