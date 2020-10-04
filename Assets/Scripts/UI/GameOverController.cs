using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverController : MonoBehaviour
{
    [Inject] private GameScenesController _gameScenesController = default;
    [Inject] private GameState _gameState = default;

    [SerializeField] private TextMeshProUGUI _scoreText = default;
    [SerializeField] private Button _button = default;
    
    void Start()
    {
        _scoreText.text = $"{_gameState.score}";
        _button.onClick.AddListener(() => _gameScenesController.ToMenu());
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
