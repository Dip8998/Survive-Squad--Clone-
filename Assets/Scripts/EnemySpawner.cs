using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public int startingEnemyCount = 5;
    public float timeBetweenWaves = 30f;
    public int waveIncrement = 2;
    public float spawnRadius = 10f;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            int enemyCount = startingEnemyCount + (currentWave - 1) * waveIncrement;

            for (int i = 0; i < enemyCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPoint();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += player.position;
        randomDirection.y = 0;

        return randomDirection;
    }

    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(player.position, spawnRadius);
        }
    }
}
