using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerSelectionController : MonoBehaviour
{
    [Inject] private GameState _gameState = default;
    [Inject] private PlayerSpawner _playerSpawner = default;
    
    [SerializeField] private Transform _container = default;
    [SerializeField] private PlayerFlowerButton _playerFlowerButtonPrefab = default;

    private Dictionary<PlayerFlowerType, PlayerFlowerButton> _playerFlowerButtons;
    
    public void Show()
    {
        if (_playerFlowerButtons == null)
        {
            return;
        }
        
        foreach (var btn in _playerFlowerButtons)
        {
            btn.Value.gameObject.SetActive(false);
        }
        
        foreach (var playerFlower in _gameState.availableFlowers)
        {
            _playerFlowerButtons[playerFlower.playerFlowerType].gameObject.SetActive(true);
        }
    }
    
    void Start()
    {
        _playerFlowerButtons = new Dictionary<PlayerFlowerType, PlayerFlowerButton>();
        
        foreach (var playerFlower in _gameState.availableFlowers)
        {
            var playerFlowerButton = Instantiate(_playerFlowerButtonPrefab, _container);
            playerFlowerButton.SetData(playerFlower);
            playerFlowerButton.playerFlowerSelectedEvent += HandlePlayerFlowerSelected;

            _playerFlowerButtons[playerFlower.playerFlowerType] = playerFlowerButton;
        }
    }

    private void HandlePlayerFlowerSelected(PlayerFlowerType type)
    {
        _playerSpawner.SpawnPlayer(type);
    }
}
