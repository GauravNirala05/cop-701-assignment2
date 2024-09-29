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
    private float AttackRate = 1f;
    public Slider slider;

    public delegate void Demage(int demage, Vector2 position);
    public static event Demage DemageInfo;

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

    }
    // Start is called before the first frame update
    void Start()
    {
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
            int temp = GameManager.manager.KillsInfo;
            temp++;
            GameManager.manager.KillsInfo = temp;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetAni.SetBool("hurt", false);

        attacker = transform.position;
        // if ((attacker.x + 2f) < target.position.x)
        // {
        //     attackerBody.velocity = new Vector2(speed, attackerBody.velocity.y);
        //     sr.flipX = false;
        //     ani.SetBool("attack", false);
        //     ani.SetBool("run", true);

        // }
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

                    ani.SetBool("attack", true);

                    if (DemageInfo != null)
                    {
                        DemageInfo(AttackDamege, attacker);
                    }
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
