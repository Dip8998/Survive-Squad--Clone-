using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Transform playerTransform;
    public Vector3 offset;
    public Camera cam;

    
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBar.value = currentHealth / maxHealth;
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            transform.rotation = cam.transform.rotation;
            transform.position = playerTransform.position + offset;
            transform.rotation = Quaternion.identity;  
        }
    }
}
