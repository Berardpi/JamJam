using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    Animator faderAnimator;

    [SerializeField]
    TextMeshProUGUI faderText;


    void Awake()
    {
        if (HealthManager.Instance?.getMaxHealth() == HealthManager.Instance?.getCurrentHealth())
            setLevelText(SceneManager.GetActiveScene().buildIndex);
        else
        {
            setLevelText("");
        }
    }

    public void triggerFade()
    {
        faderAnimator.SetTrigger("crossfade");
    }

    public void setLevelText(int level = 1)
    {
        if (level == 0)
        {
            faderText.text = "";
        }
        faderText.text = "Level " + level;
    }

    public void setLevelText(string text)
    {
        faderText.text = text;
    }
}
