using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Obstacle_Spawn : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs = new List<GameObject>();
    public GameObject obstaclesFolder;
    private int obstacleToSpawn;

    public float spawnRate = 1;
    public float maxSpawnRate = 3f;
    public float minSpawnRate = 0.1f;
    private float timer = 0;

    void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        obstacleToSpawn = Random.Range(0, obstaclePrefabs.Count);
        if (obstaclePrefabs[obstacleToSpawn] != null)
        {
            spawnObstacle(obstaclePrefabs[obstacleToSpawn]);
        }
        
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
            obstacleToSpawn = Random.Range(0, obstaclePrefabs.Count);
            if (obstaclePrefabs[obstacleToSpawn] != null)
            {
                spawnObstacle(obstaclePrefabs[obstacleToSpawn]);
            }
            //Debug.Log(spawnRate);
            timer = 0;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    void spawnObstacle(GameObject obstacle)
    {
        //Spawns an obstacle between the bounds of the left and right walls
        Instantiate(obstacle, new Vector3(Random.Range(Game_Manager.Instance.meleeLeftSpawnerBound, Game_Manager.Instance.meleeRightSpawnerBound), transform.position.y, transform.position.z), transform.rotation, obstaclesFolder.transform);
    }
}
