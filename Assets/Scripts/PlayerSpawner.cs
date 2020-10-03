using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    [Inject] private PlayerFlowerDataModel _playerFlowerDataModel = default;
    [Inject] private AmmunitionStorage _ammunitionStorage = default;
    [Inject] private Player _player = default;
    
    [SerializeField] private GameObject _playerPrefab = default;
    
    public void SpawnPlayer(PlayerFlowerType type)
    {
        var playerFlower = _playerFlowerDataModel.GetPlayerFlower(type);
        _player.ResetPlayer(playerFlower);
        
        _ammunitionStorage.playerFlowerType = type;
    }
}
