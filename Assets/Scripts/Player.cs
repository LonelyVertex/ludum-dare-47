using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject] private GameState _gameState = default;
    [Inject] private UIPanelController _uiPanelController = default;
    
    public Health health;
    public Rigidbody2D rigidbody2D;

    private PlayerFlowerSO _playerFlower;

    void Start()
    {
        health.healthDepletedEvent += PlayerHealthHealthDepletedEvent;
        
        _gameState.InitArena();
        _gameState.Pause();
        _uiPanelController.ShowPlayerSelectionUI();
    }

    private void PlayerHealthHealthDepletedEvent()    
    {
        _gameState.Pause();
        
        _gameState.KillFlower(_playerFlower.playerFlowerType);
        if (_gameState.availableFlowers.Length > 0)
        {
            _uiPanelController.ShowPlayerSelectionUI();
        }
        else
        {
            _uiPanelController.ShowGameOverUI();
        }
    }

    public void ResetPlayer(PlayerFlowerSO playerFlowerSo)
    {
        _playerFlower = playerFlowerSo;
        
        health.SetMaxHealth(playerFlowerSo.maxHealth);
        
        _uiPanelController.ShowInGameUI();
        _gameState.Unpause();
    }
}
