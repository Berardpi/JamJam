using UnityEngine;

public class HealthManager : MonoBehaviour
{
    int maxHealth;
    int currentHealth;

    private static HealthManager instance;
    public static HealthManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        ManageSingleton();
    }

    private void Start()
    {
        ResetHealth();
    }

    public void LoseHealth()
    {
        currentHealth = Mathf.Max(0, currentHealth - 1);
        UiHealthManager UiManager = FindObjectOfType<UiHealthManager>();
        UiManager.Refresh(currentHealth, maxHealth);
    }

    public void ResetHealth()
    {
        ResetMaxHealth();
        currentHealth = maxHealth;
        UiHealthManager UiManager = FindObjectOfType<UiHealthManager>();
        UiManager?.Refresh(currentHealth, maxHealth);
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    private void ResetMaxHealth()
    {
        MaxHealth maxHealthObject = FindObjectOfType<MaxHealth>();
        if (maxHealthObject != null)
        {
            maxHealth = maxHealthObject.maxHealth;
        }
        else
        {
            maxHealth = 1;
        }
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
