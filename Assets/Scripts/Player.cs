using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject] private GameState _gameState = default;
    [Inject] private UIPanelController _uiPanelController = default;
    [Inject] private GameScenesController _gameScenesController = default;
    
    public Health health;
    public Rigidbody2D rigidbody2D;
    public FlowerGraphicSwapper flowerGraphicSwapper = default;

    
    private PlayerFlowerSO _playerFlower;

    void Start()
    {
        health.HealthDepletedEvent += PlayerHealthHealthDepletedEvent;
        
        _gameState.InitArena();
        _gameState.Pause();
        _uiPanelController.ShowPlayerSelectionUI();
    }

    void Update()
    {
        if (_gameState.bossSpawned && _gameState.spawnedEnemyCount <= 0)
        {
            enabled = false;
            _gameState.Pause();
            _uiPanelController.ShowArenaCompletedUI();
            _gameState.CloseArena();
        }
    }

    private void PlayerHealthHealthDepletedEvent()    
    {
        _gameState.Pause();
        
        _gameState.KillFlower(_playerFlower.playerFlowerType);
        if (_gameState.availableFlowers.Count > 0)
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
        
        flowerGraphicSwapper.SwapGraphics(playerFlowerSo.playerFlowerType);
        
        health.SetMaxHealth(playerFlowerSo.maxHealth);
        health.setRessurectinvicinvible();
        _uiPanelController.ShowInGameUI();
        _gameState.Unpause();
    }
}
