
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
    private float maxSpawnRate = 7f;
    private float minSpawnRate = 5f;
    private float timer = 0;
    private int maxSpawnAmount = 3;
    private float soulSpawnXValue;

    private int spawnerLocationNumber;

    public GameObject player;
    private Transform playerTracker;

    //Keeps track of all the spawns in a list
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> rangeList = new List<GameObject>();
    public List<GameObject> soulList = new List<GameObject>();


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (meleeSpawner && !rangedSpawner && !soulSpawner)
        {
            if (enemyList.Count < maxSpawnAmount)
            {
                enemyList.Add(Instantiate(meleeEnemy, new Vector3(Random.Range(Game_Manager.Instance.meleeLeftSpawnerBound, Game_Manager.Instance.meleeRightSpawnerBound), transform.position.y, transform.position.z), transform.rotation, enemiesFolder.transform));
            }
        }
        else if (rangedSpawner && !meleeSpawner && !soulSpawner)
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
        else if (!rangedSpawner && !meleeSpawner && soulSpawner)
        {
           
            if (player.GetComponent<Rigidbody2D>().linearVelocity.x > 0.01)
            {
                soulSpawnXValue = player.GetComponent<Player_Controller>().transform.position.x + 2;
            }
            else if (player.GetComponent<Rigidbody2D>().linearVelocity.x < -0.01)
            {
                soulSpawnXValue = player.GetComponent<Player_Controller>().transform.position.x - 2;
            }
            else if (player.GetComponent<Rigidbody2D>().linearVelocity.x == 0)
            {
                soulSpawnXValue = player.GetComponent<Player_Controller>().transform.position.x;
            }

            soulList.Add(Instantiate(soulEnemy, new Vector3(soulSpawnXValue, transform.position.y, transform.position.z), transform.rotation, enemiesFolder.transform));
        }
        else
        {
            Debug.Log("Error: enemy spawner type not selected or multiple types selected.");
        }

    }
}
