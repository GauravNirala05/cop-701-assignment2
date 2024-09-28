using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;
    [SerializeField]
    private float minx=-25f, maxx=140f;
    // Start is called before the first frame update
    void Start()
    {
        
        // Debug.Log("clicked camera : " + GameManager.manager.select);
        player=GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void LateUpdate(){
        tempPos=transform.position;
        // print("temp posi : "+tempPos);
        tempPos.x=player.position.x;
        if(tempPos.x<minx){
            tempPos.x=minx;
        }
        if(tempPos.x>maxx){
            tempPos.x=maxx;
        }
        transform.position=tempPos;
    }
}
