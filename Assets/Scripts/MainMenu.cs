using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    RectTransform buttonsContainer;

    [SerializeField]
    RectTransform selectLevelContainer;

    [SerializeField]
    Animator menuAnimator;
    [SerializeField]
    TextMeshProUGUI faderText;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        faderText.text = "";
    }

    public void onStart()
    {
        faderText.text = "Level 1";
        menuAnimator.SetTrigger("fade");
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
