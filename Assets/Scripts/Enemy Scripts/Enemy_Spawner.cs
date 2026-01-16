using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public bool meleeSpawner;
    public bool rangedSpawner;
    public GameObject enemy;
    public GameObject enemiesFolder;
    public float spawnRate;
    public float maxSpawnRate = 7f;
    public float minSpawnRate = 5f;
    private float timer = 0;
    private int maxSpawnAmount = 3;


    //Keeps track of all the spawns in a list
    public List<GameObject> enemyList = new List<GameObject>();


    void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        SpawnEnemy();
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
            SpawnEnemy();
            //Debug.Log(spawnRate);
            timer = 0;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    void SpawnEnemy()
    {
        //Spawns an obstacle between the bounds of the left and right walls
        if (enemyList.Count < maxSpawnAmount)
        {
            if (meleeSpawner && !rangedSpawner)
            {
                enemyList.Add(Instantiate(enemy, new Vector3(Random.Range(Game_Manager.Instance.leftSpawnerBound, Game_Manager.Instance.rightSpawnerBound), transform.position.y, transform.position.z), transform.rotation, enemiesFolder.transform));
            }
            else if (rangedSpawner && !meleeSpawner)
            {
                //ranged enemy spawn
            }
            else
            {
                Debug.Log("Error: enemy spawner type not selected or multiple types selected.");
            }
        }
        
    }
}
