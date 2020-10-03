using System.Collections.Generic;
using UnityEngine;

public class PlayerFlowerDataModel : MonoBehaviour
{
    public PlayerFlowerSO[] playerFlowers;

    private Dictionary<PlayerFlowerType, PlayerFlowerSO> _playerFlowersByProjectileType = new Dictionary<PlayerFlowerType, PlayerFlowerSO>();

    public PlayerFlowerSO GetPlayerFlower(PlayerFlowerType type)
    {
        return _playerFlowersByProjectileType[type];
    }
    
    void Awake()
    {
        foreach (var flower in playerFlowers)
        {
            _playerFlowersByProjectileType.Add(flower.playerFlowerType, flower);
        }
    }
}
