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

    void Awake()
    {
        prefabs = new Dictionary<PowerUp, GameObject>();
        prefabs.Add(PowerUp.DoubleJump, prefabDoubleJump);
        prefabs.Add(PowerUp.Blink, prefabBlink);
        prefabs.Add(PowerUp.RunFaster, prefabSpeed);
    }

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (PowerUp powerUp in Enum.GetValues(typeof(PowerUp)))
        {
            if (PowerUpManager.Instance.IsPowerUpActive(powerUp))
            {
                Instantiate(prefabs[powerUp], transform);
            }
        }
    }
}
