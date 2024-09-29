using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public delegate void DoDamage(int damage);
    public static event DoDamage doDamage;
    private float speed = 10f;
    private int damage = 9;
    private float destroyTime = 1.6f;
    private CapsuleCollider2D collider2D;
    private Animator ani;
    // Start is called before the first frame update
    void Awake()
    {
    }
    void Start()
    {
        ani = GameObject.Find("bullet").GetComponent<Animator>();
        collider2D = GetComponent<CapsuleCollider2D>();
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // print(collision);
        enemy Enemy = collision.gameObject.GetComponent<enemy>();
        boss01 boss1 = collision.gameObject.GetComponent<boss01>();

        if (Enemy != null)
        {
            Enemy.takeDamage(damage);
        }
        if (boss1 != null)
        {
            boss1.takeDamage(damage);
        }
        Destroy(gameObject);

    }
}
