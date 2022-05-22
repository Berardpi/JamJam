using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    Animator faderAnimator;

    [SerializeField]
    TextMeshProUGUI faderText;

    private static UiManager instance;
    public static UiManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        ManageSingleton();
    }

    public void triggerFade()
    {
        faderAnimator.SetTrigger("crossfade");
    }

    public void setLevelText(string text)
    {
        faderText.text = text;
    }

    private void ManageSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
