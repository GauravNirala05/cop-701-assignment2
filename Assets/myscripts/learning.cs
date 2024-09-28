using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class learning : MonoBehaviour
{
    public float run = 5f;
    public float jump = 8f;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(something);
        
    }
    // Enumerator something(){
    //     yield return new WaitForSecond(3);
    // }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        pos.x += h * run * Time.deltaTime;
        pos.y += v * jump * Time.deltaTime;
        transform.position = pos;
    }
}
