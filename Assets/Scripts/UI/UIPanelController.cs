using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] private float transitionDuration = default;
    [SerializeField] private EventSystem _eventSystem = default;

    [Space]
    [SerializeField] private UIPanel _playerSelectionPanel = default;
    [SerializeField] private UIPanel _inGameUIPanel = default;
    [SerializeField] private UIPanel _gameOverUIPanel = default;
    [SerializeField] private UIPanel _arenaFinishedUIPanel = default;

    [SerializeField] private UIPanel _menuPanel = default;
    [SerializeField] private UIPanel _creditsPanel = default;
    
    [Space]
    [SerializeField] private PlayerSelectionController _playerSelectionController = default;
    [SerializeField] private NextLevelController _nextLevelController = default;
    
    private UIPanel _currentDisplayedPanel;

    protected void Start()
    {
        _eventSystem = FindObjectOfType<EventSystem>();

        if (_playerSelectionController != null)
        {
            _playerSelectionPanel.gameObject.SetActive(false);
        }

        if (_inGameUIPanel != null)
        {
            _inGameUIPanel.gameObject.SetActive(false);
        }

        if (_gameOverUIPanel != null)
        {
            _gameOverUIPanel.gameObject.SetActive(false);
        }

        if (_arenaFinishedUIPanel != null)
        {
            _arenaFinishedUIPanel.gameObject.SetActive(false);
        }

        if (_menuPanel != null)
        {
            _menuPanel.gameObject.SetActive(false);
        }

        if (_creditsPanel != null)
        {
            _creditsPanel.gameObject.SetActive(false);
        }
    }

    public void ShowInGameUI(bool instant = false)
    {
        SwitchPanels(_inGameUIPanel, _currentDisplayedPanel, instant);
    }

    public void ShowPlayerSelectionUI(bool instant = false)
    {
        _playerSelectionController.Show();
        
        SwitchPanels(_playerSelectionPanel, _currentDisplayedPanel, instant);
    }

    public void ShowGameOverUI(bool instant = false)
    {
        SwitchPanels(_gameOverUIPanel, _currentDisplayedPanel, instant);
    }

    public void ShowArenaCompletedUI(bool instant = false)
    {
        _nextLevelController.Show();
        
        SwitchPanels(_arenaFinishedUIPanel, _currentDisplayedPanel, instant);
    }

    public void ShowCredits()
    {
        SwitchPanels(_creditsPanel, _currentDisplayedPanel, instant: false);
    }

    public void ShowMenu()
    {
        SwitchPanels(_menuPanel, _currentDisplayedPanel, instant: false);
    }

    private void SwitchPanels(UIPanel newPanel, UIPanel currentPanel, bool instant)
    {
        if (!_eventSystem.enabled)
        {
            return;
        }

        if (instant)
        {
            if (currentPanel != null)
            {
                currentPanel.gameObject.SetActive(false);
                currentPanel.canvasGroup.alpha = 0.0f;
            }
            
            newPanel.gameObject.SetActive(true);
            newPanel.canvasGroup.alpha = 1.0f;
            
            return;
        }

        StartCoroutine(TransitionPanels(newPanel, currentPanel));
    }
    
    private IEnumerator TransitionPanels(UIPanel panelIn, UIPanel panelOut)
    {
        _eventSystem.enabled = false;
        
        panelIn.gameObject.SetActive(true);
        panelIn.canvasGroup.alpha = 0;

        var t = 0.0f;
        while (t < transitionDuration)
        {
            panelIn.canvasGroup.alpha = t / transitionDuration;

            if (panelOut != null)
            {
                panelOut.canvasGroup.alpha = 1.0f - t / transitionDuration;
            }

            t += Time.unscaledDeltaTime;

            yield return null;
        }

        panelIn.canvasGroup.alpha = 1.0f;
        if (panelOut != null)
        {
            panelOut.canvasGroup.alpha = 0.0f;
            panelOut.gameObject.SetActive(false);
        }

        _currentDisplayedPanel = panelIn;
        
        _eventSystem.enabled = true;
    }
}
