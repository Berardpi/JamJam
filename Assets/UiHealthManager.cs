using UnityEngine;

public class UiHealthManager : MonoBehaviour
{
    [SerializeField]
    GameObject prefabHeart;

    [SerializeField]
    GameObject prefabHeartEmpty;
    HealthManager healthManager;

    private void Awake()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }

    private void Start()
    {
        Refresh(healthManager.getCurrentHealth(), healthManager.getMaxHealth());
    }

    public void Refresh(int currentHealth, int maxHealth)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(prefabHeart, transform);
        }

        for (int i = 0; i < maxHealth - currentHealth; i++)
        {
            Instantiate(prefabHeartEmpty, transform);
        }
    }
}
