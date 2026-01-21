
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public bool meleeSpawner;
    public bool rangedSpawner;
    public bool soulSpawner;
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    public GameObject soulEnemy;
    public GameObject enemiesFolder;
    private float spawnRate;
    private float maxSpawnRate;
    private float minSpawnRate;
    private float timer = 0;
    private int maxSpawnAmount = 3;
    private float soulSpawnXValue;
    private float distanceTracker;

    private int spawnerLocationNumber;
    private bool xValueReady;
    public GameObject player;
    private Transform playerTracker;

    //Keeps track of all the spawns in a list
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> rangeList = new List<GameObject>();
    public List<GameObject> soulList = new List<GameObject>();


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        spawnRate = 0;

        if (soulSpawner)
        {
            spawnRate = 12;
            maxSpawnAmount = 2;
        }
        if (rangedSpawner)
        {
            maxSpawnRate = 12; // - 8
            minSpawnRate = 10; // - 6
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
            maxSpawnAmount = 4;
        }
        if (meleeSpawner)
        {
            maxSpawnRate = 6; // - 4
            minSpawnRate = 4; // - 2
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
            maxSpawnAmount = 4;
        }

        timer = spawnRate / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (soulSpawner)
        {
            if (Stats_Manager.Instance.distancesTravelled >= 500 && Stats_Manager.Instance.distancesTravelled <= 10000) spawnRate = (14750 - Stats_Manager.Instance.distancesTravelled) / 1187.5f;
        }

        if (rangedSpawner)
        {
            if (Stats_Manager.Instance.distancesTravelled >= 500 && Stats_Manager.Instance.distancesTravelled <= 10000) maxSpawnRate = (29000 - Stats_Manager.Instance.distancesTravelled) / 2375;
            if (Stats_Manager.Instance.distancesTravelled >= 500 && Stats_Manager.Instance.distancesTravelled <= 10000) minSpawnRate = (24250 - Stats_Manager.Instance.distancesTravelled) / 2375;
            
        }

        if (meleeSpawner)
        {
            if (Stats_Manager.Instance.distancesTravelled >= 500 && Stats_Manager.Instance.distancesTravelled <= 10000) maxSpawnRate = (29000 - Stats_Manager.Instance.distancesTravelled) / 4750;
            if (Stats_Manager.Instance.distancesTravelled >= 500 && Stats_Manager.Instance.distancesTravelled <= 10000) minSpawnRate = (19500 - Stats_Manager.Instance.distancesTravelled) / 4750;
            
        }
  
        //Checks if enough time has passed between obstacle spawns
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            xValueReady = false;
            SpawnEnemy();
            //Debug.Log(spawnRate);
            timer = 0;
            if (!soulSpawner)
            {
                spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
            }
            else if (Stats_Manager.Instance.distancesTravelled < 500)
            {
                spawnRate = 12;
            }
            
        }
    }

    void SpawnEnemy()
    {
        //Spawns an obstacle between the bounds of the left and right walls
        if (meleeSpawner && !rangedSpawner && !soulSpawner)
        {
            if (enemyList.Count <= maxSpawnAmount)
            {
                enemyList.Add(Instantiate(meleeEnemy, new Vector3(Random.Range(Game_Manager.Instance.meleeLeftSpawnerBound, Game_Manager.Instance.meleeRightSpawnerBound), transform.position.y, transform.position.z), transform.rotation, enemiesFolder.transform));
            }
        }
        else if (rangedSpawner && !meleeSpawner && !soulSpawner)
        {
            if (rangeList.Count <= maxSpawnAmount)
            {
                spawnerLocationNumber = Random.Range(1, 4);

                if (spawnerLocationNumber == 1)
                {
                    // Top Spawner Bounds
                    rangeList.Add(Instantiate(rangedEnemy, new Vector3(Random.Range(Game_Manager.Instance.rangedTopLeftSpawnerBound, Game_Manager.Instance.rangedTopRightSpawnerBound), Game_Manager.Instance.rangedTopYValue, transform.position.z), transform.rotation, enemiesFolder.transform));
                }
                else if (spawnerLocationNumber == 2)
                {
                    // Right Spawner Bounds
                    rangeList.Add(Instantiate(rangedEnemy, new Vector3(Game_Manager.Instance.rangedRightSpawnerXValue, Random.Range(Game_Manager.Instance.rangedSideBottomSpawnerBound, Game_Manager.Instance.rangedSideTopSpawnerBound), transform.position.z), transform.rotation, enemiesFolder.transform));
                }
                else if (spawnerLocationNumber == 3)
                {
                    // Left Spawner Bounds
                    rangeList.Add(Instantiate(rangedEnemy, new Vector3(Game_Manager.Instance.rangedLeftSpawnerXValue, Random.Range(Game_Manager.Instance.rangedSideBottomSpawnerBound, Game_Manager.Instance.rangedSideTopSpawnerBound), transform.position.z), transform.rotation, enemiesFolder.transform));
                }
            }
            

        }
        else if (!rangedSpawner && !meleeSpawner && soulSpawner)
        {
           
            if (soulList.Count <= maxSpawnAmount)
            {
                if (player.GetComponent<Rigidbody2D>().linearVelocity.x > 0.01 && !xValueReady)
                {
                    soulSpawnXValue = player.GetComponent<Player_Controller>().transform.position.x + 2;
                    xValueReady = true;
                }
                else if (player.GetComponent<Rigidbody2D>().linearVelocity.x < -0.01 && !xValueReady)
                {
                    soulSpawnXValue = player.GetComponent<Player_Controller>().transform.position.x - 2;
                    xValueReady = true;
                }
                else if (player.GetComponent<Rigidbody2D>().linearVelocity.x == 0 && !xValueReady)
                {
                    soulSpawnXValue = player.GetComponent<Player_Controller>().transform.position.x;
                    xValueReady = true;
                }

                soulList.Add(Instantiate(soulEnemy, new Vector3(soulSpawnXValue, transform.position.y, transform.position.z), transform.rotation, enemiesFolder.transform));
            }

            
                
        }
        else
        {
            Debug.Log("Error: enemy spawner type not selected or multiple types selected.");
        }

    }
}
