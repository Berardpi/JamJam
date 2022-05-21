using System;
using System.Collections.Generic;
using UnityEngine;
using static PowerUpManager;

public class UiPowerUpManager : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBlink;

    [SerializeField]
    GameObject prefabDoubleJump;

    [SerializeField]
    GameObject prefabSpeed;

    Dictionary<PowerUp, GameObject> prefabs;

    PowerUpManager powerUpManager;

    void Awake()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    void Start()
    {
        prefabs = new Dictionary<PowerUp, GameObject>();
        prefabs.Add(PowerUp.DoubleJump, prefabDoubleJump);
        prefabs.Add(PowerUp.Blink, prefabBlink);
        prefabs.Add(PowerUp.RunFaster, prefabSpeed);
        Refresh();
    }

    public void Refresh()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (PowerUp powerup in Enum.GetValues(typeof(PowerUp)))
        {
            if (powerUpManager.IsPowerUpActive(powerup))
            {
                Instantiate(prefabs[powerup], transform);
            }
        }
    }
}
