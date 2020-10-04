using UnityEngine;

public class SpiderQueenController : SimpleEnemyController
{
    [Header("Spider Queen")]
    public float poolCooldown;
    public GameObject poolPrefab;

    private float _poolTimer;
    private Rigidbody2D _playerRigidbody;
    
    protected override void Start()
    {
        base.Start();

        _poolTimer = poolCooldown;
    }

    protected override void Update()
    {
        base.Update();
        
        if (!health.IsAlive) return;

        if (vision.CanSeePlayer && _poolTimer <= 0)
        {
            SpawnPool();
        }
        else
        {
            _poolTimer -= Time.deltaTime;
        }
    }

    private void SpawnPool()
    {
        _poolTimer = poolCooldown;
        
        var position = Player.rigidbody2D.position + Player.rigidbody2D.velocity.normalized * 2;
        Instantiate(poolPrefab, position, Quaternion.identity);
    }
}
