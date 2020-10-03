using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;

    public event System.Action healthDepletedEvent;
    
    private int currentHealth;
    
    public void DealDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            healthDepletedEvent?.Invoke();
        }
    }
    
    private void Awake()
    {
        currentHealth = maxHealth;
    }
}
