using TMPro;
using UnityEngine;
using Zenject;

public class InGameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveCountText = default;
    [SerializeField] private TextMeshProUGUI _enemyCountText = default;

    [Inject] private GameState _gameState = default;

    void Update()
    {
        _waveCountText.text = $"{_gameState.spawnedWaveCount} / 3";
        _enemyCountText.text = $"Spawned enemies: {_gameState.spawnedEnemyCount}";
    }
}
