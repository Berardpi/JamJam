using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isLoadingLevel = false;
    PowerUpManager powerUpManager;
    HealthManager healthManager;

    private void Awake()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        healthManager = FindObjectOfType<HealthManager>();
    }

    public void LoadNextLevel()
    {
        if (!isLoadingLevel)
        {
            isLoadingLevel = true;
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadLevel(string levelName)
    {
        if (!isLoadingLevel)
        {
            isLoadingLevel = true;
            LoadScene(SceneManager.GetSceneByName(levelName).buildIndex);
        }
    }

    private void LoadScene(int sceneIdx)
    {
        Destroy(healthManager.gameObject);
        Destroy(powerUpManager.gameObject);
        SceneManager.LoadScene(sceneIdx);
        healthManager = FindObjectOfType<HealthManager>();
        powerUpManager = FindObjectOfType<PowerUpManager>();
        SoftResetLevel();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SoftResetLevel()
    {
        healthManager.ResetHealth();
        powerUpManager.Reset();
    }

    public void HardResetLevel()
    {
        SoftResetLevel();
        ResetLevel();
    }

    void OnReset(InputValue value)
    {
        HardResetLevel();
    }
}
