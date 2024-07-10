using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpScreen : MonoBehaviour
{
    public GameObject powerUpPanel;
    public Button fireRateButton;
    public Button teammateButton;

    private Player player;

    void Start()
    {
        powerUpPanel.SetActive(false);
        player = FindObjectOfType<Player>();

        if (fireRateButton != null)
        {
            fireRateButton.onClick.AddListener(OnFireRateButtonClick);
        }

        if (teammateButton != null)
        {
            teammateButton.onClick.AddListener(OnTeammateButtonClick);
        }
    }

    public void ShowPowerUpScreen()
    {
        powerUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePowerUpScreen()
    {
        powerUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void OnFireRateButtonClick()
    {
        if (player != null)
        {
            player.SetFireRateToMin();
        }
        HidePowerUpScreen();
    }

    void OnTeammateButtonClick()
    {
        if (player != null)
        {
            player.SpawnTeammate();
        }
        HidePowerUpScreen();
    }
}
