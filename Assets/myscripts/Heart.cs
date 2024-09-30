using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"){
            other.GetComponent<player>().addHealth(7);
            Destroy(gameObject);
        }
    }
}
