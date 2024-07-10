using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthbar;
    public Camera Camera;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthbar.value = currentHealth / maxHealth;
    }

    void Update()
    {
        transform.rotation = Camera.transform.rotation;
    }
}
