using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemy : MonoBehaviour
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
    private int AttackDamege = 1;
    private float AttackTime;
    private float AttackRate = 2f;
    public Slider slider;

    public delegate void Demage(int demage, Vector2 position);
    public static event Demage DemageInfo;

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
        audioManager.jumpSFX(audioManager.skeleton);


    }

    void Update()
    {
        checkHealth();
    }

    private void checkHealth()
    {
        if (TotalHealth <= 0)
        {
            ani.SetBool("dead", true);
            audioManager.jumpSFX(audioManager.skeleton);
            audioManager.jumpSFX(audioManager.skeletondead);
            int temp = GameManager.manager.KillsInfo;
            temp++;
            GameManager.manager.KillsInfo = temp;
            Destroy(gameObject);
        }
    }
    public void DoDamage()
    {
        if (DemageInfo != null)
        {
            Vector3 attacker = transform.position;
            DemageInfo(AttackDamege, attacker);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        targetAni.SetBool("hurt", false);

        attacker = transform.position;
        if ((attacker.x + 2f) < target.position.x)
        {
            attackerBody.velocity = new Vector2(speed, attackerBody.velocity.y);
            sr.flipX = false;
            ani.SetBool("attack", false);
            ani.SetBool("run", true);

        }
        else if ((attacker.x - 2f) > target.position.x)
        {
            attackerBody.velocity = new Vector2(-speed, attackerBody.velocity.y);
            sr.flipX = true;
            ani.SetBool("attack", false);
            ani.SetBool("run", true);

        }
        else
        {
            ani.SetBool("run", false);
            if (target.position.y > -1.7f)
            {
                ani.SetBool("attack", false);

            }
            else
            {
                if (Time.time > AttackTime)
                {
                    AttackTime = Time.time + AttackRate;
                    audioManager.jumpSFX(audioManager.skeletonAttack);

                    ani.SetBool("attack", true);
                }
            }
            // ani.SetBool("attack", true);
            // targetAni.SetBool("hurt", true);
            // StopAnimationAfterDelay(0.5f);
        }

    }
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     print("yaha h tu");
    //     if (collision.gameObject.CompareTag("enemy"))
    //     {
    //         ani.SetBool("run", false);
    //     }

    // }
}
