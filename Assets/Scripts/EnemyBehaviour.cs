using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent agent;
    public GameObject collectablePrefab;
    public int collectableCount = 1;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Player != null)
        {
            agent.destination = Player.position;
        }
    }

    public void Die()
    {
        SpawnCollectables();
        Destroy(gameObject);
    }

    void SpawnCollectables()
    {
        if (collectablePrefab != null)
        {
            for (int i = 0; i < collectableCount; i++)
            {
                Instantiate(collectablePrefab, transform.position, Quaternion.identity);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Die();
        }
    }
}
