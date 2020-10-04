using UnityEngine;

public class Health : MonoBehaviour
{
    public event System.Action DamageTaken;
    public event System.Action HealthDepletedEvent;
    public bool Invincible;
    private int _currentHealth;
    private int _maxHealth;
    private float _invincibleTicker;
    public float invincibleTime;
    public float ressurectIvincibleTimer;
    public int maxHealth => _maxHealth;
    public int currentHealth => _currentHealth;
    private SpriteRenderer spriteRenderer;
    public bool HasFullHealth => _currentHealth == _maxHealth;
    public bool IsAlive => _currentHealth > 0;

    public void SetMaxHealth(int maxHealth)
    {
        _currentHealth = _maxHealth = maxHealth;
    }
    public void DealDamage(int amount)
    {
        Debug.Log(gameObject.name + " got damage " + amount);

        if (!Invincible)
        {
            DamageTaken?.Invoke();
            _currentHealth -= amount;
            if (this.tag != "Enemy")
            {
                Invincible = true;
                _invincibleTicker = invincibleTime;
            }
            if (_currentHealth <= 0)
            {
                HealthDepletedEvent?.Invoke();
            }

        }
    }
    private void FixedUpdate()
    {
        if (_invincibleTicker > 0)
        {
            _invincibleTicker -= Time.deltaTime;
        }
        else
        {
            Invincible = false;
        }
    }
    public void setRessurectinvicinvible()
    {
        _invincibleTicker = ressurectIvincibleTimer;
        Invincible = true;
    }
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _currentHealth = _maxHealth;
    }
}