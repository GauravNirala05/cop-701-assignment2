using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public GameObject foot;
    AudioManager audioManager;
    public static player Player;
    private bool isAlive = true;
    public GameObject bullet;
    public Transform shotSpawn;
    private float nextFire;
    private float fireRate;
    [SerializeField]
    private float move;
    [SerializeField]
    private float jump = 10f;
    private float x;
    private float y;
    private Vector2 CurrentPosition;
    private Rigidbody2D body;

    public delegate void GameOver();
    public static event GameOver OnOver;
    private SpriteRenderer sr;
    private Animator ani;
    private bool isgrounded = true;
    private TextMeshProUGUI healthText, KillCount, stageText, BossKillCount;
    public float maxHealth = 10f; // Maximum health
    public float currentHealth;
    public Slider healthBar;



    void Awake()
    {
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        fireRate = GameManager.manager.fireRateinfo;
        move = GameManager.manager.speedinfo;
    }
    // Start is called before the first frame update
    void Start()
    {
        foot.SetActive(false);
        audioManager = GameObject.FindWithTag("audio").GetComponent<AudioManager>();
        healthBar = GameObject.Find("HealthAndBar").GetComponent<Slider>();
        stageText = GameObject.Find("stage").GetComponent<TextMeshProUGUI>();
        KillCount = GameObject.Find("KillCount").GetComponent<TextMeshProUGUI>();
        BossKillCount = GameObject.Find("BossCount").GetComponent<TextMeshProUGUI>();
        KillCount.text = (GameManager.manager.KillsInfo).ToString();
        BossKillCount.text = (GameManager.manager.BossBattle).ToString();
        // healthText = GameObject.Find("HealthBarText").GetComponent<Text>();
        // print(healthText.text);
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        currentHealth = maxHealth;

        if (GameManager.manager.BossBattle == 0)
        {
            stageText.text = "Level - 1 ";
        }
        if (GameManager.manager.BossBattle == 1)
        {
            stageText.text = "Level - 2 ";
        }
        if (GameManager.manager.BossBattle == 2)
        {
            stageText.text = "Level - 3 ";
        }
    }
    public void addHealth(int health)
    {

        // print("its called");
        currentHealth += health;
        if (health < 0)
        {
            ani.SetTrigger("hurt");
            audioManager.jumpSFX(audioManager.hurt);

        }
        else
        {
            audioManager.jumpSFX(audioManager.heart);

        }
        if (currentHealth >= maxHealth)
        {
            healthBar.value = maxHealth;
            currentHealth = maxHealth;
        }
        else
        {
            healthBar.value = currentHealth;

        }
    }
    void OnEnable()
    {
        enemy.DemageInfo += HealthUpdate;
        boss01.DemageInfo += HealthUpdateByBoss;
        boss2.DemageInfo += HealthUpdateByBoss2;

    }
    void OnDisable()
    {

        enemy.DemageInfo -= HealthUpdate;
        boss01.DemageInfo -= HealthUpdateByBoss;
        boss2.DemageInfo -= HealthUpdateByBoss2;

    }
    void HealthUpdateByBoss(int demage, Vector2 AttackerPosition)
    {
        if (isAlive)
        {

            CurrentPosition = transform.position;
            float distance = Vector2.Distance(CurrentPosition, AttackerPosition);
            if (distance < 3f)
            {
                ani.SetTrigger("hurt");
                audioManager.jumpSFX(audioManager.hurt);

                currentHealth = currentHealth - demage;
                healthBar.value = currentHealth;
            }
        }

    }
    void HealthUpdateByBoss2(int demage, Vector2 AttackerPosition)
    {
        if (isAlive)
        {

            CurrentPosition = transform.position;
            float distance = Vector2.Distance(CurrentPosition, AttackerPosition);
            if (distance < 3f)
            {
                ani.SetTrigger("hurt");
                audioManager.jumpSFX(audioManager.hurt);

                currentHealth = currentHealth - demage;
                healthBar.value = currentHealth;
            }
        }

    }

    void HealthUpdate(int demage, Vector2 AttackerPosition)
    {
        if (isAlive)
        {

            CurrentPosition = transform.position;
            float distance = Vector2.Distance(CurrentPosition, AttackerPosition);
            if (distance < 2.1f)
            {
                ani.SetTrigger("hurt");
                audioManager.jumpSFX(audioManager.hurt);

                currentHealth = currentHealth - demage;
                healthBar.value = currentHealth;
            }
        }

    }

    private void updateStageFlag(int temp)
    {

        if (GameManager.manager.BossBattle == 0)
        {
            if (temp < 1)
            {
                stageText.text = "Level - 1";
            }
            else if (temp < 3)
            {
                stageText.text = "wave - 1";
            }
            else if (temp <= 5)
            {
                stageText.text = "wave - 2";
            }
            else if (temp > 5)
            {
                stageText.text = "Final wave";
            }
        }
        if (GameManager.manager.BossBattle == 1)
        {
            stageText.text = "Level - 2";

            // if (temp < 17)
            // {
            //     stageText.text = "Level - 2";
            // }
            // else if (temp < 20)
            // {
            //     stageText.text = "wave - 1";
            // }
            // else if (temp <= 25)
            // {
            //     stageText.text = "wave - 2";
            // }
            // else if (temp > 30)
            // {
            //     stageText.text = "Final wave";
            // }

        }
        if (GameManager.manager.BossBattle == 3)
        {

            stageText.text = "Level - 3";
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            playerMovement();
            checkHealth();
            PlayerAnimation();
            updateUI();
            FootSteps();

        }
    }
    private void updateUI()
    {
        KillCount.text = (GameManager.manager.KillsInfo).ToString();
        BossKillCount.text = (GameManager.manager.BossBattle).ToString();
        updateStageFlag(GameManager.manager.KillsInfo);
    }
    private void checkHealth()
    {
        if (healthBar.value <= 0 && isAlive)
        {
            isAlive = false;
            ani.SetTrigger("dead");
        }
    }
    public void playerDead()
    {
        SceneManager.LoadScene("Game-Over");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // print("yaha h tu");
        if (collision.gameObject.CompareTag("ground"))
        {
            // print("got the ground");
            isgrounded = true;
        }

        ani.SetBool("jump", false);
    }
    void playerMovement()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        // Debug.Log("X , Y : " + x + " , " + y);
        transform.position += new Vector3(x, 0f, 0f) * Time.deltaTime * move;

    }
    public void bulletShoot()
    {
        audioManager.jumpSFX(audioManager.gun);
        GameObject temp = Instantiate(bullet, shotSpawn.position, transform.rotation);
    }
    void PlayerAnimation()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ani.SetTrigger("shot");

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ani.SetTrigger("reload");
        }

        if (Input.GetButtonDown("Jump") && isgrounded)
        {
            ani.SetBool("crauch", false);

            audioManager.jumpSFX(audioManager.jump);
            isgrounded = false;
            ani.SetBool("jump", true);
            body.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && isgrounded)
        {
            ani.SetBool("crauch", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && isgrounded)
        {
            ani.SetBool("crauch", false);
        }
        if (x > 0f)
        {
            // sr.flipX = false;
            // transform.Rotate(0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            ani.SetBool("Runing", true);
        }
        else if (x < 0f)
        {
            // sr.flipX = true;
            // transform.Rotate(0, 180, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            ani.SetBool("Runing", true);

        }
        else
        {
            ani.SetBool("Runing", false);
        }

    }
    private void FootSteps()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            foot.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            foot.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            foot.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            foot.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            foot.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            foot.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            foot.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            foot.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            foot.SetActive(false);
        }
    }
}