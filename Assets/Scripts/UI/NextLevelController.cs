using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NextLevelController : MonoBehaviour
{
    [SerializeField] private Button _playLevelButton = default;
    
    [SerializeField] private NextLevelView _prevLevel = default;
    [SerializeField] private NextLevelView _nextLevel = default;
    [SerializeField] private NextLevelView _upcomingLevel = default;

    [Inject] private GameState _gameState = default;
    [Inject] private GameScenesController _gameScenesController = default;

    void Awake()
    {
        _playLevelButton.onClick.AddListener(() =>
        {
            _gameScenesController.ToGame(_gameState.level);
        });
    }

    private void OnDestroy()
    {
        _playLevelButton.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        var level = _gameState.level;
        
        var prevLevel = _gameScenesController.GetScene(level);
        var nextLevel = _gameScenesController.GetScene(level + 1);
        var upcomingLevel = _gameScenesController.GetScene(level + 2);

        _prevLevel.Set(prevLevel);
        _nextLevel.Set(nextLevel);
        _upcomingLevel.Set(upcomingLevel);
    }
}
