using UnityEngine;
using Zenject;

public class PlayerSelectionController : MonoBehaviour
{
    [Inject] private PlayerFlowerDataModel _playerFlowerDataModel = default;
    [Inject] private PlayerSpawner _playerSpawner = default;
    
    [SerializeField] private Transform _container = default;
    [SerializeField] private PlayerFlowerButton _playerFlowerButtonPrefab = default;
    
    void Start()
    {
        foreach (var playerFlower in _playerFlowerDataModel.playerFlowers)
        {
            var playerFlowerButton = Instantiate(_playerFlowerButtonPrefab, _container);
            playerFlowerButton.SetData(playerFlower);
            playerFlowerButton.playerFlowerSelectedEvent += HandlePlayerFlowerSelected;
        }

        Show();
    }

    private void HandlePlayerFlowerSelected(PlayerFlowerType type)
    {
        _playerSpawner.SpawnPlayer(type);

        Hide();
    }

    public void Show()
    {
        Time.timeScale = 0;
        
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Time.timeScale = 1;
        
        gameObject.SetActive(false);
    }
}
