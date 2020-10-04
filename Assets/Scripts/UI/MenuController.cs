using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _startGameButton = default;

    [Inject] private GameScenesController _gameScenesController = default;
    
    void Start()
    {
        _startGameButton.onClick.AddListener(() => _gameScenesController.ToGame());
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveAllListeners();
    }
}
