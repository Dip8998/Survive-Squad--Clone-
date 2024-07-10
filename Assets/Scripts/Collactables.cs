using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int scoreValue = 1;
    public AudioClip collectSound;
    private AudioSource collact;
    public float attractionRadius = 5f;
    public float attractionSpeed = 5f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        collact = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attractionRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, attractionSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    void Collect(GameObject player)
    {
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.AddCollectible(scoreValue);

            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
                collact.clip = collectSound;
                collact.Play();
            }

            Destroy(gameObject);
        }
    }
}
