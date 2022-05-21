using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PowerUpManager powerUpManager;
    HealthManager healthManager;

    private void Awake()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        healthManager = FindObjectOfType<HealthManager>();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HardResetLevel()
    {
        healthManager.ResetHealth();
        powerUpManager.Reset();
        ResetLevel();
    }

    void OnReset(InputValue value)
    {
        HardResetLevel();
    }
}
