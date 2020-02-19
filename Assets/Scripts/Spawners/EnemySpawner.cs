using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    public List<EnemyController> enemies;

    public float spawnRateMin;
    public float spawnRateMax;
    public float initalSpawn;

    private int spawnIndex;
    private void Start()
    {
        StartCoroutine(DelaySpawn());
    }

    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(initalSpawn);
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        spawnIndex= Random.Range(0, enemies.Count);
        
        while (true)
        {
            if (GameMaster.instance.enemyList.Count >= GameMaster.instance.enemyLimit)
            {
                yield return new WaitWhile(()=>GameMaster.instance.enemyList.Count >= GameMaster.instance.enemyLimit);
            }
            Instantiate(enemies[spawnIndex], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(spawnRateMin,spawnRateMax));
            spawnIndex= Random.Range(0, enemies.Count);
            
        }
        
    }
    
    
}
