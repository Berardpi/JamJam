using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;
    int currentHealth;

    private static HealthManager instance;

    private void Awake()
    {
        ManageSingleton();
    }

    private void Start()
    {
        ResetHealth();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoseHealth()
    {
        currentHealth = Mathf.Max(0, currentHealth - 1);
        UiHealthManager UiManager = FindObjectOfType<UiHealthManager>();
        UiManager.Refresh(currentHealth, maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UiHealthManager UiManager = FindObjectOfType<UiHealthManager>();
        UiManager.Refresh(currentHealth, maxHealth);
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }
}
