using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameState : MonoBehaviour
{
    [Inject] private PlayerFlowerDataModel _playerFlowerDataModel = default;
    [Inject] private PlayerInputState _playerInputState = default;
    

    public List<PlayerFlowerSO> availableFlowers;

    public int spawnedEnemyCount => _spawnedEnemyCount;
    public int spawnedWaveCount => _spawnedWaveCount;
    public bool bossSpawned => _bossSpawned;
    public int score;
    
    public int level;
    
    private ArenaController _arenaController;

    private int _spawnedWaveCount;
    private int _spawnedEnemyCount;
    private bool _bossSpawned;
    
    public void ResetState()
    {
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
        score = 0;
    }

    public void CloseArena()
    {
        _arenaController.waveSpawnedEvent -= HandleWaveSpawned;
        _arenaController.enemySpawnedEvent -= HandleEnemySpawned;
        _arenaController.bossWaveSpawned -= HandleBossWaveSpawned;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        
        _playerInputState.DisableInput();
    }

    public void Unpause()
    {
        _playerInputState.EnableInput();

        StartCoroutine(UnpauseDelayed());
    }

    public void KillFlower(PlayerFlowerType playerFlowerType)
    {
        availableFlowers = availableFlowers.Where(playerFlower => playerFlower.playerFlowerType != playerFlowerType)
            .ToList();
    }

    public void EnemySpawned()
    {
        _spawnedEnemyCount++;
    }
    
    public void EnemyDied()
    {
        score++;
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
