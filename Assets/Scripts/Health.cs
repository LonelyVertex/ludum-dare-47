using UnityEngine;

public class Health : MonoBehaviour
{

    public event System.Action healthDepletedEvent;

    private int _currentHealth;
    private int _maxHealth;
    
    public bool HasFullHealth => _currentHealth == _maxHealth;

    public void SetMaxHealth(int maxHealth)
    {
        _currentHealth = _maxHealth = maxHealth;
    }

    public void DealDamage(int amount)
    {
        Debug.Log(gameObject.name + " got damage " + amount);
        
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            healthDepletedEvent?.Invoke();
        }
    }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
}