using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 target;
    private float speed;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void SetTarget(Vector3 targetPosition)
    {
        target = targetPosition;
        speed = GameObject.FindObjectOfType<Shooting3D>().bulletSpeed;
    }

    void Update()
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
