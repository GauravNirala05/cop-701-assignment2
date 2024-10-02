using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private float speed = 40f;
    private int damage = -1;
    private float destroyTime = 1.5f;
    private CapsuleCollider2D collider2D;
    private Animator ani;
    // Start is called before the first frame update
    void Awake()
    {
    }
    void Start()
    {
        // ani = GameObject.Find("bullet").GetComponent<Animator>();
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
        player Player = collision.gameObject.GetComponent<player>();

        if (Player != null)
        {
            Player.addHealth(damage);
        }

    }
}
