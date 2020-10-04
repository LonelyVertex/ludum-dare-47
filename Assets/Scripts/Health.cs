using UnityEngine;

public class Health : MonoBehaviour
{
    public event System.Action DamageTaken;
    public event System.Action HealthDepletedEvent;

    private int _currentHealth;
    private int _maxHealth;

    public int maxHealth => _maxHealth;
    public int currentHealth => _currentHealth;
    
    public bool HasFullHealth => _currentHealth == _maxHealth;
    public bool IsAlive => _currentHealth > 0;

    public void SetMaxHealth(int maxHealth)
    {
        _currentHealth = _maxHealth = maxHealth;
    }

    public void DealDamage(int amount)
    {
        Debug.Log(gameObject.name + " got damage " + amount);
        DamageTaken?.Invoke();
        
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            HealthDepletedEvent?.Invoke();
        }
    }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
}