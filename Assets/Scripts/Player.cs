using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHealth = 3f;
    public float currentHealth;
    public HealthBar healthBar;
    public Shooting3D shoot;

    public Slider collectibleSlider;
    private float collectiblesCollected = 0;

    public float minFireRate = 0.1f;
    public float maxFireRate = 0.5f;
    public int collectiblesThreshold = 10;

    public PowerUpScreen powerUpScreen;
    public GameObject teammatePrefab;
    public Transform teammateSpawnPoint;

    private bool powerUpScreenShown = false;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }

        collectibleSlider.maxValue = collectiblesThreshold;
        UpdateCollectibleSlider();
    }

    void Update()
    {
        if (collectibleSlider != null)
        {
            float sliderValue = collectibleSlider.value;

            if (sliderValue >= collectiblesThreshold && !powerUpScreenShown)
            {
                if (powerUpScreen != null)
                {
                    powerUpScreen.ShowPowerUpScreen();
                    powerUpScreenShown = true;
                }
                collectibleSlider.value = collectiblesThreshold;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentHealth -= 1;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(gameObject);
                Destroy(other.gameObject);
            }

            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(maxHealth, currentHealth);
            }
        }
    }

    public void AddCollectible(int value)
    {
        collectiblesCollected += 0.3f;
        UpdateCollectibleSlider();
    }

    void UpdateCollectibleSlider()
    {
        if (collectibleSlider != null)
        {
            collectibleSlider.value = collectiblesCollected;
        }
    }

    public void SetFireRateToMin()
    {
        if (shoot != null)
        {
            shoot.SetFireRate(minFireRate);
        }
        ResetCollectibles();
    }
    public void SpawnTeammate()
    {
        if (teammatePrefab != null && teammateSpawnPoint != null)
        {
            Instantiate(teammatePrefab, teammateSpawnPoint.position, teammateSpawnPoint.rotation);
        }
        ResetCollectibles();
    }

    private void ResetCollectibles()
    {
        collectiblesCollected = 0;
        UpdateCollectibleSlider();
        powerUpScreenShown = false;
    }
}
