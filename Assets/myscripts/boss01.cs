using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class boss01 : MonoBehaviour
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
    private int AttackDamege = 3;
    private float AttackTime;
    private float AttackRate = 1f;
    public Slider slider;
    private bool IsAlive = true;
    public delegate void DemageBoss(int demage, Vector2 position);
    public static event DemageBoss DemageInfo;

    AudioManager audioManager;
    void Awake()
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
        audioManager.jumpSFX(audioManager.boss1);
    }

    // void OnEnable()
    // {
    //     Bullet.doDamage += takeDamage;
    // }
    // void OnDisable()
    // {
    //     Bullet.doDamage -= takeDamage;
    // }
    public void bossDead()
    {
        speed = 0;
        print("boss1 defeat");

        if (GameManager.manager.BossBattle == 1)
        {
            SceneManager.LoadScene("Next-level");

        }
        if (GameManager.manager.BossBattle == 3)
        {
            SceneManager.LoadScene("Next-level");
        }
        Destroy(gameObject);
    }
    public void DoDamage()
    {
        if (DemageInfo != null)
        {
            Vector3 attacker = transform.position;
            DemageInfo(AttackDamege, attacker);
        }
    }
    public void takeDamage(int damage)
    {
        TotalHealth = TotalHealth - damage;
        float currenthealth = slider.value;
        currenthealth -= damage;
        slider.value = currenthealth;
        // ani.SetTrigger("hurt");
        audioManager.jumpSFX(audioManager.skeleton);

    }

    private void checkHealth()
    {
        if (TotalHealth <= 0 && IsAlive)
        {
            IsAlive = false;
            ani.SetTrigger("dead");
            // audioManager.jumpSFX(audioManager.boss1);
            audioManager.jumpSFX(audioManager.skeletondead);
            int temp = GameManager.manager.BossBattle;
            temp++;
            // temp++;
            GameManager.manager.BossBattle = temp;

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsAlive)
        {
            checkHealth();

            attacker = transform.position;
            if ((attacker.x + 3f) < target.position.x)
            {
                attackerBody.velocity = new Vector2(speed, attackerBody.velocity.y);
                sr.flipX = false;
                ani.SetBool("attack", false);
                ani.SetBool("run", true);

            }
            else if ((attacker.x - 3f) > target.position.x)
            {
                attackerBody.velocity = new Vector2(-speed, attackerBody.velocity.y);
                sr.flipX = true;
                ani.SetBool("attack", false);
                ani.SetBool("run", true);

            }
            else
            {
                ani.SetBool("run", false);
                if (target.position.y > -3f)
                {
                    ani.SetBool("attack", false);

                }
                else
                {
                    if (Time.time > AttackTime)
                    {
                        AttackTime = Time.time + AttackRate;

                        ani.SetBool("attack", true);
                        audioManager.jumpSFX(audioManager.bossAttack);
                    }

                }
            }
        }

    }
}
