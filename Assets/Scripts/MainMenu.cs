using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    RectTransform buttonsContainer;

    [SerializeField]
    RectTransform selectLevelContainer;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void onStart()
    {
        gameManager.LoadNextLevel();
    }

    public void onSelectLevel()
    {
        ToggleSelectLevel(true);
    }

    public void onSelectLevelBack()
    {
        ToggleSelectLevel(false);
    }

    public void onExit()
    {
        Application.Quit();
    }

    private void ToggleSelectLevel(bool show)
    {
        buttonsContainer.gameObject.SetActive(!show);
        selectLevelContainer.gameObject.SetActive(show);
    }
}
