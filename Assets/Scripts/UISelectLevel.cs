using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISelectLevel : MonoBehaviour
{
    [SerializeField]
    GameObject levelButtonPrefab;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        Draw();
    }

    private void Draw()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < gameManager.GetNumberOfLevels(); i++)
        {
            GameObject button = Instantiate(levelButtonPrefab, transform);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "Level " + (i + 1);

            int levelIndex = i + 1;
            button
                .GetComponent<Button>()
                .onClick.AddListener(
                    delegate
                    {
                        LoadLevel(levelIndex);
                    }
                );
        }
    }

    private void LoadLevel(int index)
    {
        gameManager.LoadLevel(index);
    }
}
