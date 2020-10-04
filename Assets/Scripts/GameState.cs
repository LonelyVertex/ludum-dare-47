using System.Linq;
using UnityEngine;
using Zenject;

public class GameState : MonoBehaviour
{
    [Inject] private PlayerFlowerDataModel _playerFlowerDataModel = default;
    
    public enum GameStateType
    {
        Running,
        Paused
    }

    public PlayerFlowerSO[] availableFlowers;

    public int spawnedEnemyCount => _spawnedEnemyCount;
    public int spawnedWaveCount => _spawnedWaveCount;
    
    private ArenaController _arenaController;
    private GameStateType _gameStateType;

    private int _spawnedWaveCount;
    private int _spawnedEnemyCount;
    
    public void ResetState()
    {
        _gameStateType = GameStateType.Paused;
        availableFlowers = _playerFlowerDataModel.playerFlowers;

        _arenaController = null;
    }

    public void InitArena()
    {
        _arenaController = FindObjectOfType<ArenaController>();

        _arenaController.waveSpawnedEvent += HandleWaveSpawned;
        _arenaController.enemySpawnedEvent += HandleEnemySpawned;

        _spawnedEnemyCount = 0;
        _spawnedWaveCount = 0;
    }

    public void CloseArena()
    {
        _arenaController.waveSpawnedEvent -= HandleWaveSpawned;
        _arenaController.enemySpawnedEvent -= HandleEnemySpawned;
    }

    public void Pause()
    {
        _gameStateType = GameStateType.Paused;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        _gameStateType = GameStateType.Running;
        Time.timeScale = 1;
    }

    public void KillFlower(PlayerFlowerType playerFlowerType)
    {
        availableFlowers = availableFlowers.Where(playerFlower => playerFlower.playerFlowerType != playerFlowerType)
            .ToArray();
    }

    public void EnemySpawned()
    {
        _spawnedEnemyCount++;
    }
    
    public void EnemyDied()
    {
        _spawnedEnemyCount--;
    }

    private void Awake()
    {
        ResetState();
    }
    
    private void HandleWaveSpawned()
    {
        _spawnedWaveCount++;
    }
    
    private void HandleEnemySpawned()
    {
        _spawnedEnemyCount++;
    }
}
