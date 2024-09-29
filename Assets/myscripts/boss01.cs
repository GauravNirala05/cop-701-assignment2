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
    private int AttackDamege = 5;
    private float AttackTime;
    private float AttackRate = 1f;
    public Slider slider;

    public delegate void DemageBoss(int demage, Vector2 position);
    public static event DemageBoss DemageInfo;

    void Awake()
    {

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
        ani.SetTrigger("hurt");

    }
    void Update()
    {
        checkHealth();
    }

    private void checkHealth()
    {
        if (TotalHealth <= 0)
        {
            ani.SetTrigger("dead");
            int temp = GameManager.manager.BossBattle;
            temp++;
            // temp++;
            GameManager.manager.BossBattle = temp;
            SceneManager.LoadScene("Next-level");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

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

                    print("boss demage");
                    if (DemageInfo != null)
                    {
                        print("boss demage sent");
                        DemageInfo(AttackDamege, attacker);
                    }
                }

            }
        }

    }
}
