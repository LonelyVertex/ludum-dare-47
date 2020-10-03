using System;
using UnityEditor;
using UnityEngine;
using Zenject;

public class EnemyMeleeAttack : MonoBehaviour
{
    public EnemyVision vision;

    public float range;
    public float timer;
    public int damage;

    [Inject] private Player _player;
    private float _currentTimer;

    private bool InRange => Vector3.Distance(_player.transform.position, transform.position) <= range;
    private bool CanAttack => _currentTimer <= 0;

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }

    void Update()
    {
        if (vision.CanSeePlayer && InRange && CanAttack)
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
        // TODO some fancy effects
        Debug.Log(gameObject.name + " attacked the player.");
        _player.health.DealDamage(damage);
        _currentTimer = timer;
    }
}
