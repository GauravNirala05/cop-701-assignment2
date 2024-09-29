using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class boss2 : MonoBehaviour
{
    private Transform target;
    private Animator targetAni;
    private Vector2 attacker;
    public float speed = 3;
    private Rigidbody2D attackerBody;
    private SpriteRenderer sr;
    private Rigidbody2D body;
    private Animator ani;
    public int TotalHealth = 20;
    private bool IsChecked = true;
    private int AttackDamege = 5;
    private float AttackTime, AttackTime2;
    private float AttackRate = 4f;
    private float AttackRate2 = 3f;
    public Slider slider;

    public delegate void DemageBoss2(int demage, Vector2 position);
    public static event DemageBoss2 DemageInfo;
    AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindWithTag("audio").GetComponent<AudioManager>();
        target = GameObject.FindWithTag("Player").transform;
        targetAni = GameObject.FindWithTag("Player").GetComponent<Animator>();
        attackerBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        slider.maxValue = TotalHealth;
        slider.value = TotalHealth;
        audioManager.jumpSFX(audioManager.boss2);

    }

    // void OnEnable()
    // {
    //     Bullet.doDamage += takeDamage;
    // }
    // void OnDisable()
    // {
    //     Bullet.doDamage -= takeDamage;
    // }

    public void takeDamage(int damage)
    {
        TotalHealth = TotalHealth - damage;
        float currenthealth = slider.value;
        currenthealth -= damage;
        slider.value = currenthealth;
        ani.SetTrigger("hurt");
        audioManager.jumpSFX(audioManager.skeleton);


    }
    public void DoDamage()
    {
        if (DemageInfo != null)
        {
            print("boss demage sent");
            Vector3 attacker = transform.position;
            DemageInfo(AttackDamege, attacker);
        }
    }
    void Update()
    {
        checkHealth();
    }

    private void checkHealth()
    {
        if (TotalHealth <= 0 && IsChecked)
        {
            IsChecked = false;
            ani.SetTrigger("dead");

            audioManager.jumpSFX(audioManager.skeletondead);
            audioManager.jumpSFX(audioManager.boss2);
            int temp = GameManager.manager.BossBattle;
            temp++;
            // temp++;
            GameManager.manager.BossBattle = temp;
            Destroy(gameObject);
            SceneManager.LoadScene("Game-Completed");

            // StartCoroutine("LoadSceneAfterDelay");
        }
    }
    // IEnumerator LoadSceneAfterDelay()
    // {
    //     yield return new WaitForSeconds(3f);
    //     print("its calledd");
    //     SceneManager.LoadScene("Next-level");
    // }
    // Update is called once per frame
    void FixedUpdate()
    {
        attacker = transform.position;
        float distance = Vector2.Distance(attacker, target.position);
        // print("distace : " + distance);
        float time = Time.time;
        if (attacker.x < target.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

        if (distance < 4f)
        {
            attackerBody.velocity = new Vector2(0, attackerBody.velocity.y);

            ani.SetBool("run", false);
            // print("setbool run false");
            if (time > AttackTime)
            {
                AttackTime = Time.time + AttackRate;

                ani.SetTrigger("atk2");
                audioManager.jumpSFX(audioManager.skeleton);


            }

        }
        else if (distance < 9f && distance >= 4f)
        {

            ani.SetBool("run", false);
            // print("setbool run false2");
            attackerBody.velocity = new Vector2(0, attackerBody.velocity.y);

            if (time > AttackTime2)
            {
                print("atk 1 called");
                Vector3 posi = transform.position;
                GameObject.Find("Spawner").GetComponent<spawner>().spawnEnemyNow(2, posi);
                AttackTime2 = Time.time + AttackRate2;
                ani.SetTrigger("atk1");
                audioManager.jumpSFX(audioManager.bossAttack);

                print(posi);
                // print("spawner called");
            }
        }
        else if (distance < 10f && distance >= 9f)
        {

            ani.SetBool("run", true);

            if (sr.flipX)
            {
                attackerBody.velocity = new Vector2(-speed, attackerBody.velocity.y);
            }
            else
            {
                attackerBody.velocity = new Vector2(speed, attackerBody.velocity.y);
            }
        }
        else
        {
            ani.SetBool("run", false);
        }

    }
}
