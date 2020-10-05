using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InGameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText = default;
    [SerializeField] private Slider _healthBar = default;
    [SerializeField] private TextMeshProUGUI _ammoCountText = default;
    [SerializeField] private TextMeshProUGUI _waveCountText = default;
    [SerializeField] private TextMeshProUGUI _enemyCountText = default;

    [Inject] private GameState _gameState = default;
    [Inject] private Player _player = default;
    [Inject] private AmmunitionStorage _ammunitionStorage = default;

    void Update()
    {
        _healthBar.maxValue = _player.health.maxHealth;

        _scoreText.text = $"Score {_gameState.score}";
        _waveCountText.text = $"{_gameState.spawnedWaveCount} / 3";
        _enemyCountText.text = $"Enemies: {_gameState.spawnedEnemyCount}";
        _healthBar.value = _player.health.currentHealth;
        _ammoCountText.text = $"Ammo: {_ammunitionStorage.currentAmmunitionCount} / {_ammunitionStorage.maxAmmunitionCount}";
    }
}
