using UnityEngine;
using Zenject;


public class AphidMotherController : SimpleEnemyController
{
    [Header("Aphid Mother")] public AphidMotherLevelScaler levelScaler;
    [Space] public GameObject aphidPrefab;
    public float spawnCooldown;
    public float spawnRange;

    [Inject] private DiContainer _container;
    
    private float _spawnTimer;

    protected override void Start()
    {
        base.Start();

        _spawnTimer = spawnCooldown;
    }

    protected override void Update()
    {
        base.Update();
        
        if (!health.IsAlive) return;

        if (_spawnTimer <= 0)
        {
            SpawnAphid();
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
        }
    }

    private void SpawnAphid()
    {
        _spawnTimer = spawnCooldown;

        var position = transform.position +
                       new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
        var aphid = _container.InstantiatePrefab(aphidPrefab, position, transform.rotation, null);

        aphid.GetComponent<EnemyLevelScaler>().SetLevel(levelScaler.Level);
        aphid.GetComponent<EnemyWayPoints>().CopyState(wayPoints);

        GameState.EnemySpawned();
        
        // if the mother is already chasing spawned aphids will chase, too
        if (ChasingPlayer)
        {
            aphid.GetComponent<SimpleEnemyController>().ChasePlayer();
        }
    }
}