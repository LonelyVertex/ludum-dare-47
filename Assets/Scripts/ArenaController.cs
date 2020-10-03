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
    }

    [Serializable]
    public struct SpawnPoint
    {
        public Transform spawnPoint;
        public WayPointCollection wayPointCollection;
        public GameObject enemyPrefab;
        public int baseCount;
        public int spawnDelay;
    }

    [Serializable]
    public class SpawnPointList : ReorderableArray<SpawnPoint>
    {
    }

    public int level;
    [Space] public Wave wave1;
    [Space] public Wave wave2;
    [Space] public Wave bossWave;
    
    [Inject] DiContainer _container;

    #region Gizmos

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
        foreach (var spawnPoint in wave.spawnPoints)
        {
            StartCoroutine(SpawnSpawnPoint(spawnPoint));
        }
    }

    IEnumerator SpawnSpawnPoint(SpawnPoint spawnPoint)
    {
        var spawnDelay = new WaitForSeconds(spawnPoint.spawnDelay);
        
        for (var i = 0; i < spawnPoint.baseCount; i++)
        {
            SpawnEnemy(spawnPoint);
            yield return spawnDelay;
        }
    }

    void SpawnEnemy(SpawnPoint spawnPoint)
    {
        var rotation = Quaternion.identity;
        if (!spawnPoint.wayPointCollection.wayPoints.IsEmpty())
        {
            var vectorToTarget = spawnPoint.wayPointCollection.GetPosition(0) - spawnPoint.spawnPoint.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        var enemy = _container.InstantiatePrefab(spawnPoint.enemyPrefab, spawnPoint.spawnPoint.position, rotation, null);
        enemy.GetComponent<EnemyWayPoints>().wayPointCollection = spawnPoint.wayPointCollection;
        enemy.GetComponent<EnemyLevelScaler>().SetLevel(level);
    }
}