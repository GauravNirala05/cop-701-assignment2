using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class boss2 : MonoBehaviour
{
    public GameObject bossLaser;
    public float bossLaserDamage = 1;
    private Transform target;
    private Animator targetAni;
    private Vector2 attacker;
    public float speed = 3;
    private Rigidbody2D attackerBody;
    private SpriteRenderer sr;
    private Rigidbody2D body;
    private Animator ani;
    public int TotalHealth = 20;
    public int currentHealth;
    private bool IsChecked = true;
    private int AttackDamege = 2;
    private float AttackTime, AttackTime2;
    private float AttackRate = 0.5f;
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
        currentHealth = TotalHealth;
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
        currentHealth = currentHealth - damage;
        slider.value = currentHealth;
        if (currentHealth * 2 < TotalHealth)
        {
            ani.SetTrigger("hurt");

        }
        audioManager.jumpSFX(audioManager.skeleton);

    }
    public void bossDead()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Game-Completed");
    }
    public void DoDamage()
    {
        if (DemageInfo != null)
        {
            // print("boss demage sent");
            Vector3 attacker = transform.position;
            DemageInfo(AttackDamege, attacker);
        }
    }

    public void checkHealth()
    {
        if (currentHealth <= 0 && IsChecked)
        {
            IsChecked = false;
            print("boss dead now");
            ani.SetTrigger("dead");

            audioManager.jumpSFX(audioManager.skeletondead);
            // audioManager.jumpSFX(audioManager.boss2);
            int temp = GameManager.manager.BossBattle;
            temp++;
            // temp++;
            GameManager.manager.BossBattle = temp;
            SceneManager.LoadScene("Game-Completed");
            Destroy(gameObject);
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
        if (IsChecked)
        {
            checkHealth();
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
                    audioManager.Play2X(audioManager.bossLaser);

                }

            }
            else if (distance < 9f && distance >= 4f)
            {

                ani.SetBool("run", false);
                // print("setbool run false2");
                attackerBody.velocity = new Vector2(0, attackerBody.velocity.y);

                if (time > AttackTime2)
                {
                    // print("atk 1 called");
                    Vector3 posi = transform.position;
                    GameObject.Find("Spawner").GetComponent<spawner>().spawnEnemyNow(2, posi);
                    AttackTime2 = Time.time + AttackRate2;
                    ani.SetTrigger("atk1");
                    audioManager.jumpSFX(audioManager.skeleton);


                    // print(posi);
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
}
