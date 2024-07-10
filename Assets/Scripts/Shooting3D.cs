using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting3D : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 0.5f;
    public float detectionRange = 10f;
    public LayerMask enemyLayer;
    public AudioClip shootClip;

    private AudioSource shootAudio;
    private float nextFireTime = 0f;
    private Transform targetEnemy;

    void Start()
    {
        shootAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        DetectEnemy();
        if (targetEnemy != null && Time.time >= nextFireTime)
        {
            RotateTowardsEnemy();
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void DetectEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);
        targetEnemy = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Vector3 directionToEnemy = hitCollider.transform.position - transform.position;
                Ray ray = new Ray(firePoint.position, directionToEnemy);
                if (Physics.Raycast(ray, out RaycastHit hit, detectionRange))
                {
                    if (hit.transform == hitCollider.transform)
                    {
                        targetEnemy = hitCollider.transform;
                        return;
                    }
                }
            }
        }
    }

    void RotateTowardsEnemy()
    {
        if (targetEnemy != null)
        {
            Vector3 directionToEnemy = targetEnemy.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
            lookRotation.x = 0;
            lookRotation.z = 0;
            transform.rotation = lookRotation;
        }
    }

    public void Shoot()
    {
        if (targetEnemy != null)
        {
            shootAudio.PlayOneShot(shootClip);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletMovement = bullet.GetComponent<Bullet>();
            if (bulletMovement != null)
            {
                bulletMovement.SetTarget(targetEnemy.position);
            }
        }
    }

    public void SetFireRate(float newFireRate)
    {
        fireRate = newFireRate;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (targetEnemy != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(firePoint.position, targetEnemy.position);
        }
    }
}
