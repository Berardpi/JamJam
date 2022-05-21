using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public enum PowerUp
    {
        DoubleJump,
        RunFaster,
        Blink,
    };

    Dictionary<PowerUp, bool> powerUps;
    private static PowerUpManager instance;

    private void Awake()
    {
        ManageSingleton();
    }

    private void Start()
    {
        Reset();
    }

    public bool IsPowerUpActive(PowerUp powerUp)
    {
        return powerUps[powerUp];
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        powerUps[powerUp] = true;
        UiPowerUpManager UiManager = FindObjectOfType<UiPowerUpManager>();
        UiManager.Refresh();
    }

    public void Reset()
    {
        powerUps = new Dictionary<PowerUp, bool>();
        foreach (PowerUp powerup in Enum.GetValues(typeof(PowerUp)))
        {
            powerUps.Add(powerup, false);
        }
        UiPowerUpManager UiManager = FindObjectOfType<UiPowerUpManager>();
        UiManager.Refresh();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
