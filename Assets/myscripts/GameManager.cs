using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject[] Player;
    public static GameManager manager;

    private int playSelect;
    public int select{
        get { return playSelect; }
        set { playSelect = value; }
    }
    private float playerFireRate;
    public float fireRateinfo{
        get { return playerFireRate; }
        set { playerFireRate = value; }
    }
    private float playerSpeed;
    public float speedinfo{
        get { return playerSpeed; }
        set { playerSpeed = value; }
    }

    private int Boss=0;
    public int BossBattle{
        get { return Boss; }
        set { Boss = value; }
    }
    private int Kills=0;
    public int KillsInfo{
        get { return Kills; }
        set { Kills = value; }
    }

    void Awake(){
        if(manager == null){
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        playSelect=0;
    }

    private void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
     private void OnDiable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene,LoadSceneMode mode){
        if(scene.name=="Player-Select"){
            Boss=0;
            Kills=0;
        }
        if(scene.name=="My_game"){
            Instantiate(Player[playSelect]);
            // GameObject player= Instantiate(Player[0]);
            // Transform posi= GameObject.FindWithTag("Player").transform;
            // player.transform.position = posi.position;
        }
        if(scene.name=="Level-2"){
            Instantiate(Player[playSelect]);
            // GameObject player= Instantiate(Player[0]);
            // Transform posi= GameObject.FindWithTag("Player").transform;
            // player.transform.position = posi.position;
        }
        if(scene.name=="Level-3"){
            Instantiate(Player[playSelect]);
            // GameObject player= Instantiate(Player[0]);
            // Transform posi= GameObject.FindWithTag("Player").transform;
            // player.transform.position = posi.position;
        }
    }
    
}
