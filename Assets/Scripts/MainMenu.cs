using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void onStart()
    {
        gameManager.LoadNextLevel();
    }

    public void onSelectLevel() { }

    public void onExit() { }
}
