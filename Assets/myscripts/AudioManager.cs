using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager;
    public AudioSource musicPlayer;
    public AudioSource SFXPlayer;

    public AudioClip Background;
    public AudioClip button;
    public AudioClip jump;
    public AudioClip gun;
    public AudioClip heart;
    public AudioClip run;
    public AudioClip hurt;

    public AudioClip skeleton;
    public AudioClip skeletonAttack;
    public AudioClip bossAttack;
    public AudioClip skeletondead;
    public AudioClip gameover;
    public AudioClip boss1;
    public AudioClip boss2;
    public AudioClip boss3;
    public AudioClip bossLaser;
    public AudioClip CompleteLevel;
    public AudioClip GameComplete;

    private bool NormalBG = true;
    private bool BossBG = true;
    // private bool CompleteBG=true;
    // void Start()
    // {
    //     musicPlayer.clip = Background;
    //     musicPlayer.Play();

    // }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main-Manu" && NormalBG)
        {
            NormalBG = false;
            musicPlayer.clip = Background;
            musicPlayer.Play();
            BossBG=true;
        }
        if (SceneManager.GetActiveScene().name == "Level-3" && BossBG)
        {
            BossBG = false;
            musicPlayer.clip = boss3;
            musicPlayer.Play();
            NormalBG = true;
        }
        // if (SceneManager.GetActiveScene().name == "Plyer-Select" && NormalBG)
        // {
        //     NormalBG = false;
        //     musicPlayer.clip = Background;
        //     musicPlayer.Play();
        //     CompleteBG = true;
        //     BossBG=true;
        // }
        // if (SceneManager.GetActiveScene().name == "My_game" && NormalBG)
        // {
        //     NormalBG = false;
        //     musicPlayer.clip = Background;
        //     musicPlayer.Play();
        //     CompleteBG = true;
        //     BossBG=true;
        // }
        // if (SceneManager.GetActiveScene().name == "Level-2" && NormalBG)
        // {
        //     NormalBG = false;
        //     musicPlayer.clip = Background;
        //     musicPlayer.Play();
        //     CompleteBG = true;
        //     BossBG=true;
        // }
        // if (SceneManager.GetActiveScene().name == "Game-Completed" && CompleteBG)
        // {
        //     CompleteBG = false;
        //     musicPlayer.clip = GameComplete;
        //     musicPlayer.Play();
        //     NormalBG = true;
        //     BossBG=true;
        // }
        // if (SceneManager.GetActiveScene().name == "Next-level")
        // {
        //     jumpSFX(CompleteLevel);
        // }
        
    }
    // public void PlayBG(AudioClip clip)
    // {
    //     musicPlayer.clip = clip;
    //     musicPlayer.Play();
    // }

    // public void StopBG(AudioClip clip)
    // {
    //     musicPlayer.clip = clip;
    //     musicPlayer.Stop();
    // }
    void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void jumpSFX(AudioClip clip)
    {
        SFXPlayer.PlayOneShot(clip);
    }
    public void PlayClick()
    {
        SFXPlayer.PlayOneShot(button);
    }
    public void Play2X(AudioClip clip)
    {
        SFXPlayer.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXPlayer.clip = clip;
        SFXPlayer.loop = true;
        SFXPlayer.Play();
    }
    public void StopSFX(AudioClip clip)
    {
        SFXPlayer.loop = false;
        SFXPlayer.Stop();
    }

}
