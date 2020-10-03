using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput = default;
    
    public Health health;

    void Awake()
    {
        health.healthDepletedEvent += PlayerHealthHealthDepletedEvent;
    }

    private void PlayerHealthHealthDepletedEvent()
    {
        
    }

    public void ResetPlayer(PlayerFlowerSO playerFlowerSo)
    {
        health.maxHealth = playerFlowerSo.maxHealth;
    }
}
