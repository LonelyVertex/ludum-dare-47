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

    [Space]
    [SerializeField] private PlayerSelectionController _playerSelectionController = default;
    
    private UIPanel _currentDisplayedPanel;

    protected void Start()
    {
        _playerSelectionPanel.gameObject.SetActive(false);
        _inGameUIPanel.gameObject.SetActive(false);
        _gameOverUIPanel.gameObject.SetActive(false);

        _playerSelectionController.switchViewEvent += HandlePlayerSelectionControllerSwitchView;
        
        ShowPlayerSelectionUI(instant: true);

        _currentDisplayedPanel = _playerSelectionPanel;
    }

    protected void OnDestroy()
    {
        _playerSelectionController.switchViewEvent -= HandlePlayerSelectionControllerSwitchView;
    }

    private void HandlePlayerSelectionControllerSwitchView()
    {
        ShowInGameUI();
    }

    public void ShowInGameUI(bool instant = false)
    {
        if (!_eventSystem.enabled)
        {
            return;
        }

        if (instant)
        {
            if (_currentDisplayedPanel != null)
            {
                _currentDisplayedPanel.gameObject.SetActive(false);
                _currentDisplayedPanel.canvasGroup.alpha = 0.0f;
            }
            
            _inGameUIPanel.gameObject.SetActive(true);
            _inGameUIPanel.canvasGroup.alpha = 1.0f;
            
            return;
        }
        
        StartCoroutine(TransitionPanels(_inGameUIPanel, _currentDisplayedPanel));
    }

    public void ShowPlayerSelectionUI(bool instant = false)
    {
        if (!_eventSystem.enabled)
        {
            return;
        }
        
        if (instant)
        {
            if (_currentDisplayedPanel != null)
            {
                _currentDisplayedPanel.gameObject.SetActive(false);
                _currentDisplayedPanel.canvasGroup.alpha = 0.0f;
            }
            
            _playerSelectionPanel.gameObject.SetActive(true);
            _playerSelectionPanel.canvasGroup.alpha = 1.0f;
            
            return;
        }

        StartCoroutine(TransitionPanels(_playerSelectionPanel, _currentDisplayedPanel));

    }

    public void ShowGameOverUI(bool instant = false)
    {
        if (!_eventSystem.enabled)
        {
            return;
        }

        if (instant)
        {
            if (_currentDisplayedPanel != null)
            {
                _currentDisplayedPanel.gameObject.SetActive(false);
                _currentDisplayedPanel.canvasGroup.alpha = 0.0f;
            }
            
            _gameOverUIPanel.gameObject.SetActive(true);
            _gameOverUIPanel.canvasGroup.alpha = 1.0f;
            
            return;
        }

        StartCoroutine(TransitionPanels(_gameOverUIPanel, _currentDisplayedPanel));
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
            panelOut.canvasGroup.alpha = 1.0f - t / transitionDuration;

            t += Time.unscaledDeltaTime;

            yield return null;
        }

        panelIn.canvasGroup.alpha = 1.0f;
        panelOut.canvasGroup.alpha = 0.0f;
        
        panelOut.gameObject.SetActive(false);
        
        _eventSystem.enabled = true;
    }
}
