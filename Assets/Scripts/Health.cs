using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;

    public event System.Action healthDepletedEvent;

    private int currentHealth;

    public void DealDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " got damage of " + amount + ". Ouch that hurt! I have " + currentHealth + " hitpoints left");

        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " should die now. :'(");
            healthDepletedEvent?.Invoke();
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }
}