using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _startGameButton = default;
    [SerializeField] private Button _creditsButton = default;
    [SerializeField] private Button _backFromCreditsButton = default;
    [SerializeField] private UIPanelController _uiPanelController = default;
    
    [Inject] private GameState _gameState = default;
    [Inject] private GameScenesController _gameScenesController = default;
    
    void Start()
    {   
        _startGameButton.onClick.AddListener(() =>
        {
            _gameState.ResetState();
            _gameScenesController.ToGame(1);
        });
        
        _creditsButton.onClick.AddListener(() =>
        {
            _uiPanelController.ShowCredits();
        });

        _backFromCreditsButton.onClick.AddListener(() =>
        {
            _uiPanelController.ShowMenu();
        });
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _creditsButton.onClick.RemoveAllListeners();
    }
}
