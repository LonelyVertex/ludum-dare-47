using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateInstaller : MonoInstaller
{
    [SerializeField] private GameObject _gameStatePrefab = default;

    public override void InstallBindings()
    {
        Container.Bind<GameState>().FromComponentInNewPrefab(_gameStatePrefab).AsSingle().NonLazy();
    }
}
