using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameState : MonoBehaviour
{
    [Inject] private PlayerFlowerDataModel _playerFlowerDataModel = default;
    [Inject] private PlayerInputState _playerInputState = default;
    
    public enum GameStateType
    {
        Running,
        Paused
    }

    public List<PlayerFlowerSO> availableFlowers;

    public int spawnedEnemyCount => _spawnedEnemyCount;
    public int spawnedWaveCount => _spawnedWaveCount;
    public bool bossSpawned => _bossSpawned;

    public int level;
    
    private ArenaController _arenaController;
    private GameStateType _gameStateType;

    private int _spawnedWaveCount;
    private int _spawnedEnemyCount;
    private bool _bossSpawned;
    
    public void ResetState()
    {
        _gameStateType = GameStateType.Paused;
        availableFlowers = new List<PlayerFlowerSO>(_playerFlowerDataModel.playerFlowers);

        _arenaController = null;
        level = 0;
    }

    public void InitArena()
    {
        _arenaController = FindObjectOfType<ArenaController>();

        _arenaController.waveSpawnedEvent += HandleWaveSpawned;
        _arenaController.enemySpawnedEvent += HandleEnemySpawned;
        _arenaController.bossWaveSpawned += HandleBossWaveSpawned;

        _spawnedEnemyCount = 0;
        _spawnedWaveCount = 0;
        _bossSpawned = false;
        level++;
    }

    public void CloseArena()
    {
        _arenaController.waveSpawnedEvent -= HandleWaveSpawned;
        _arenaController.enemySpawnedEvent -= HandleEnemySpawned;
        _arenaController.bossWaveSpawned -= HandleBossWaveSpawned;
    }

    public void Pause()
    {
        _gameStateType = GameStateType.Paused;
        Time.timeScale = 0;
        
        _playerInputState.DisableInput();
    }

    public void Unpause()
    {
        _gameStateType = GameStateType.Running;
        
        _playerInputState.EnableInput();

        StartCoroutine(UnpauseDelayed());
    }

    public void KillFlower(PlayerFlowerType playerFlowerType)
    {
        Debug.Log(playerFlowerType);
        
        Debug.Log(availableFlowers.Count);
        
        availableFlowers = availableFlowers.Where(playerFlower => playerFlower.playerFlowerType != playerFlowerType)
            .ToList();

        Debug.Log(availableFlowers.Count);
    }

    public void EnemySpawned()
    {
        _spawnedEnemyCount++;
    }
    
    public void EnemyDied()
    {
        _spawnedEnemyCount--;
    }

    private IEnumerator UnpauseDelayed()
    {
        yield return new WaitForSecondsRealtime(.4f);
        
        Time.timeScale = 1;
    }
    
    private void Awake()
    {
        ResetState();
    }
    
    private void HandleBossWaveSpawned()
    {
        _bossSpawned = true;
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
