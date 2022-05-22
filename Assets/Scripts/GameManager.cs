using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isLoadingLevel = false;

    public void LoadNextLevel()
    {
        if (!isLoadingLevel)
        {
            isLoadingLevel = true;

            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextIndex + 1 > SceneManager.sceneCountInBuildSettings)
            {
                nextIndex = 0;
            }
            LoadScene(nextIndex);
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
        // SceneManager.LoadScene(sceneIdx);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIdx);
        asyncOperation.completed += (asyncOperation) =>
        {
            SoftResetLevel();
        };
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SoftResetLevel()
    {
        HealthManager.Instance?.ResetHealth();
        PowerUpManager.Instance?.Reset();
        AudioManager.Instance?.Reset();
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
