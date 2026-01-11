using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate;
    public float maxSpawnRate = 7f;
    public float minSpawnRate = 5f;
    private float timer = 0;
    private int maxSpawnAmount = 3;

    //Boundries on left and right walls for spawning enemies
    private float leftBound = -2.5f;
    private float rightBound = 4.5f;


    //Keeps track of all the spawns in a list
    public List<GameObject> enemyList = new List<GameObject>();


    void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        spawnEnemy();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Checks if enough time has passed between obstacle spawns
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnEnemy();
            //Debug.Log(spawnRate);
            timer = 0;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    void spawnEnemy()
    {
        //Spawns an obstacle between the bounds of the left and right walls
        if (enemyList.Count < maxSpawnAmount)
        {
            enemyList.Add(Instantiate(enemy, new Vector3(Random.Range(leftBound, rightBound), transform.position.y, transform.position.z), transform.rotation));
        }
        
    }
}
