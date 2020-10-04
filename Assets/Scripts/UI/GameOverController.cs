using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverController : MonoBehaviour
{
    [Inject] private GameScenesController _gameScenesController = default;
    
    [SerializeField] private Button _button = default;
    
    void Start()
    {
        _button.onClick.AddListener(() => _gameScenesController.ToMenu());
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
