using System;
using System.Collections;
using Malee.List;
using ModestTree;
using UnityEditor;
using UnityEngine;
using Zenject;

public class ArenaController : MonoBehaviour
{
    [Serializable]
    public struct Wave
    {
        public bool drawGizmos;
        public float spawnTime;
        [Reorderable] public SpawnPointList spawnPoints;
        public bool bossWave;
    }

    [Serializable]
    public struct SpawnPoint
    {
        public float spawnPointDelay;
        public Transform spawnPoint;
        public WayPointCollection wayPointCollection;
        public GameObject enemyPrefab;
        public float spawnDelay;
        public int spawnCount;
        public bool scaleSpawn;
    }

    [Serializable]
    public class SpawnPointList : ReorderableArray<SpawnPoint>
    {
    }

    [Space] public Wave wave1;
    [Space] public Wave wave2;
    [Space] public Wave bossWave;

    public event System.Action waveSpawnedEvent;
    public event System.Action enemySpawnedEvent;
    public event System.Action bossWaveSpawned;

    [Inject] DiContainer _container;
    [Inject] private GameState _gameState;

    #region Gizmos

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        DrawWaveGizmos(wave1);
        DrawWaveGizmos(wave2);
        DrawWaveGizmos(bossWave);
    }

    void DrawWaveGizmos(Wave wave)
    {
        if (!wave.drawGizmos) return;

        foreach (var spawnPoint in wave.spawnPoints)
        {
            DrawSpawnPointGizmos(spawnPoint);
        }
    }

    void DrawSpawnPointGizmos(SpawnPoint spawnPoint)
    {
        if (spawnPoint.wayPointCollection == null) return;

        Handles.color = spawnPoint.wayPointCollection.color;
        Handles.DrawWireDisc(spawnPoint.spawnPoint.position, transform.forward, 0.4f);

        if (!spawnPoint.wayPointCollection.wayPoints.IsEmpty())
        {
            Handles.DrawLine(
                spawnPoint.spawnPoint.position,
                spawnPoint.wayPointCollection.GetPosition(0)
            );
        }

        spawnPoint.wayPointCollection.DrawCollectionGizmos();
    }
#endif

    #endregion

    void Start()
    {
        StartCoroutine(SpawnWave(wave1));
        StartCoroutine(SpawnWave(wave2));
        StartCoroutine(SpawnWave(bossWave));
    }


    IEnumerator SpawnWave(Wave wave)
    {
        yield return new WaitForSeconds(wave.spawnTime);

        Debug.Log("Spawn wave");

        waveSpawnedEvent?.Invoke();

        if (wave.bossWave)
        {
            bossWaveSpawned?.Invoke();
        }

        foreach (var spawnPoint in wave.spawnPoints)
        {
            StartCoroutine(SpawnSpawnPoint(spawnPoint));
        }
    }

    IEnumerator SpawnSpawnPoint(SpawnPoint spawnPoint)
    {
        yield return new WaitForSeconds(spawnPoint.spawnPointDelay);

        var spawnDelay = new WaitForSeconds(spawnPoint.spawnDelay);
        var spawnCount = spawnPoint.scaleSpawn
            ? Scaler.ScaleCount(spawnPoint.spawnCount, _gameState.level)
            : spawnPoint.spawnCount;

        for (var i = 0; i < spawnCount; i++)
        {
            SpawnEnemy(spawnPoint);
            yield return spawnDelay;
        }
    }

    void SpawnEnemy(SpawnPoint spawnPoint)
    {
        var enemy = _container.InstantiatePrefab(spawnPoint.enemyPrefab, spawnPoint.spawnPoint.position,
            Quaternion.identity, null);
        enemy.GetComponent<EnemyWayPoints>().wayPointCollection = spawnPoint.wayPointCollection;
        enemy.GetComponent<EnemyLevelScaler>().SetLevel(_gameState.level);

        enemySpawnedEvent?.Invoke();
    }
}