using UnityEngine;
using Zenject;

public class PlayerSelectionController : MonoBehaviour
{
    [Inject] private PlayerFlowerDataModel _playerFlowerDataModel = default;
    [Inject] private PlayerSpawner _playerSpawner = default;
    
    [SerializeField] private Transform _container = default;
    [SerializeField] private PlayerFlowerButton _playerFlowerButtonPrefab = default;

    public event System.Action switchViewEvent;
    
    void Start()
    {
        foreach (var playerFlower in _playerFlowerDataModel.playerFlowers)
        {
            var playerFlowerButton = Instantiate(_playerFlowerButtonPrefab, _container);
            playerFlowerButton.SetData(playerFlower);
            playerFlowerButton.playerFlowerSelectedEvent += HandlePlayerFlowerSelected;
        }
    }

    private void HandlePlayerFlowerSelected(PlayerFlowerType type)
    {
        _playerSpawner.SpawnPlayer(type);
        
        switchViewEvent?.Invoke();
    }
}
