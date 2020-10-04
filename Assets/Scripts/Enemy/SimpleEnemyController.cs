using UnityEngine;
using Zenject;


public class SimpleEnemyController : MonoBehaviour
{
    private const float SpriteColorDuration = 0.2f;

    [Inject] protected Player Player;
    [Inject] private GameState _gameState;

    [Header("Simple Enemy Controller ")] public SpriteRenderer spriteRenderer;
    public Animator animator;
    public EnemyNavigation navigation;
    public EnemyVision vision;
    public EnemyWayPoints wayPoints;
    public Health health;
    public float chaseStoppingDistance;

    protected bool ChasingPlayer;
    private float _spriteColorTimer;
    private Color _defaultColor;
    private Color _tintColor;
    private bool _isDying;

    protected virtual void Start()
    {
        health.HealthDepletedEvent += OnHealthDepleted;
        health.DamageTaken += OnDamageTaken;

        _spriteColorTimer = SpriteColorDuration;
        _defaultColor = Color.white;
        _tintColor = Color.red;
        

        if (!ChasingPlayer)
        {
            navigation.SetTarget(wayPoints.CurrentWayPoint);
        }
    }

    private void OnDestroy()
    {
        health.HealthDepletedEvent -= OnHealthDepleted;
        health.DamageTaken -= OnDamageTaken;
    }

    protected virtual void Update()
    {
        UpdateSpriteRenderer();

        if (ChasingPlayer) return;

        if (wayPoints.HasReachedWayPoint)
        {
            navigation.SetTarget(wayPoints.NextWayPoint());
        }

        if (vision.CanSeePlayer || !health.HasFullHealth)
        {
            ChasePlayer();
        }
    }

    private void UpdateSpriteRenderer()
    {
        _spriteColorTimer = Mathf.Min(SpriteColorDuration, _spriteColorTimer + Time.deltaTime);
        spriteRenderer.color = Color.Lerp(_tintColor, _defaultColor, _spriteColorTimer / SpriteColorDuration);

    }

    public void ChasePlayer()
    {
        navigation.SetTarget(Player.transform);
        ChasingPlayer = true;

        if (chaseStoppingDistance > 0)
        {
            navigation.navMeshAgent.stoppingDistance = chaseStoppingDistance;
        }
    }

    private void OnHealthDepleted()
    {
        if (_isDying) return;

        _isDying = true;
        _spriteColorTimer = 0;
        _defaultColor = Color.black;
        _tintColor = Color.white;
        
        animator.SetTrigger("Death");
        
        Invoke(nameof(DestroyLater), SpriteColorDuration + 0.2f);
    }

    private void OnDamageTaken()
    {
        if (health.IsAlive)
        {
            _spriteColorTimer = 0;
        }
    }

    private void DestroyLater()
    {
        _gameState.EnemyDied();
        Destroy(gameObject);
    }
}