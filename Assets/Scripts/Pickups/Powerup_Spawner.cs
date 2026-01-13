using System.Collections.Generic;
using UnityEngine;

public class Powerup_Spawner : MonoBehaviour
{

    public GameObject powerupPrefab;
    public GameObject powerupsFolder;
    public float spawnRate;
    public float maxSpawnRate = 7f;
    public float minSpawnRate = 5f;
    private float timer = 0;
    private int maxSpawnAmount = 1;

    //Boundries on left and right walls for spawning enemies
    private float leftBound = -2.5f;
    private float rightBound = 4.5f;

    //Keeps track of all the spawns in a list
    public List<GameObject> powerupList = new List<GameObject>();

    private void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        SpawnPowerup();
    }

    private void FixedUpdate()
    {
        //Checks if enough time has passed between obstacle spawns
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPowerup();
            //Debug.Log(spawnRate);
            timer = 0;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    private void SpawnPowerup()
    {
        if (powerupList.Count < maxSpawnAmount)
        {
            powerupList.Add(Instantiate(powerupPrefab, new Vector3(Random.Range(leftBound, rightBound), transform.position.y, transform.position.z), transform.rotation, powerupsFolder.transform));
        }
    }

}
