using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawn;

    private GameObject newspawn;
    private Transform t1, t2, t3;
    private Transform PlayerPosi;
    private bool t1spawned = false, t2spawned = false, t3spawned = false;

    private int randnum;

    // Start is called before the first frame update
    void Awake()
    {
        t1 = GameObject.FindWithTag("s1").transform;
        t2 = GameObject.FindWithTag("s2").transform;
        t3 = GameObject.FindWithTag("s3").transform;
    }
    void Start()
    {

        PlayerPosi = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        spawnMoster();

    }
    void spawnMoster()
    {

        // print("cam posi "+PlayerPosi.position.x);
        // print("t1 posi "+t1.position.x);

        if ((PlayerPosi.position.x + 20f <= t1.position.x + 1f && PlayerPosi.position.x + 20f >= t1.position.x - 1f) && !t1spawned)
        {
            // Debug.Log("its here");
            t1spawned = true;
            StartCoroutine(SpawnEnemy1());

        }
        if ((PlayerPosi.position.x + 20f <= t2.position.x + 1f && PlayerPosi.position.x + 20f >= t2.position.x - 1f) && t2spawned == false)
        {
            t2spawned = true;
            StartCoroutine(SpawnEnemy2());


        }
        if ((PlayerPosi.position.x + 20f <= t3.position.x + 1f && PlayerPosi.position.x + 20f >= t3.position.x - 1f) && t3spawned == false)
        {
            t3spawned = true;
            StartCoroutine(SpawnEnemy3());

        }

    }
    IEnumerator SpawnEnemy1()
    {
        int t = 0; ;
        while (t<2)
        {
            randnum = Random.Range(0, 2);
            yield return new WaitForSeconds(0.5f);
            newspawn = Instantiate(spawn[randnum]);
            newspawn.transform.position = t1.position;
            t++;
        }
    }
    IEnumerator SpawnEnemy2()
    {
        int t = 0; ;
        newspawn = Instantiate(spawn[0]);
        newspawn.transform.position = t1.position;
        while (t<3)
        {
            randnum = Random.Range(0, 2);
            yield return new WaitForSeconds(0.5f);
            newspawn = Instantiate(spawn[1]);
            newspawn.transform.position = t2.position;
            t++;
        }
    }
    IEnumerator SpawnEnemy3()
    {
        newspawn = Instantiate(spawn[2]);
        newspawn.transform.position = t3.position;
        newspawn = Instantiate(spawn[0]);
        newspawn.transform.position = t3.position;
        newspawn = Instantiate(spawn[1]);
        newspawn.transform.position = t2.position;
        while (true)
        {
            randnum = Random.Range(0, 2);
            yield return new WaitForSeconds(8f);
            newspawn = Instantiate(spawn[randnum]);
            newspawn.transform.position = t3.position;
        }
    }


}
