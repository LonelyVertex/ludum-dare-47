using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InGameController : MonoBehaviour
{
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
        
        _waveCountText.text = $"{_gameState.spawnedWaveCount} / 3";
        _enemyCountText.text = $"Spawned enemies: {_gameState.spawnedEnemyCount}";
        _healthBar.value = (float)_player.health.currentHealth;
        _ammoCountText.text = $"Ammo: {_ammunitionStorage.currentAmmunitionCount} / {_ammunitionStorage.maxAmmunitionCount}";
    }
}
