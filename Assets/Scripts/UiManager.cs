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
        setLevelText(SceneManager.GetActiveScene().buildIndex);
    }

    public void triggerFade()
    {
        faderAnimator.SetTrigger("crossfade");
    }

    public void setLevelText(int level = 1)
    {
        faderText.text = "Level " + level;
    }
}
