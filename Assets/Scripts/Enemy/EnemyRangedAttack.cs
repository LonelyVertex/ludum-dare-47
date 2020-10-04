using UnityEngine;
using Zenject;

public class EnemyRangedAttack : MonoBehaviour
{
    public Health health;
    public EnemyVision vision;
    public GameObject projectilePrefab;
    public float timer;
    public int damage;

    [Inject] private Player _player;
    private float _currentTimer;

    private bool CanAttack => _currentTimer <= 0;
    
    private void Update()
    {
        if (!health.IsAlive) return;
        
        if (vision.CanSeePlayer && CanAttack)
        {
            AttackPlayer();
        }
        else
        {
            _currentTimer -= Time.deltaTime;
        }
    }

    void AttackPlayer()
    {
        _currentTimer = timer;

        var vectorToTarget = _player.transform.position - transform.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        var projectile = Instantiate(projectilePrefab, transform.position, rotation);
        projectile.GetComponent<EnemyProjectile>().SetDamage(damage);
    }
}
