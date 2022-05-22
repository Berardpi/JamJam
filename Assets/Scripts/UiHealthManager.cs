using UnityEngine;

public class UiHealthManager : MonoBehaviour
{
    [SerializeField]
    GameObject prefabHeart;

    [SerializeField]
    GameObject prefabHeartEmpty;

    private void Start()
    {
        Refresh(HealthManager.Instance.getCurrentHealth(), HealthManager.Instance.getMaxHealth());
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
