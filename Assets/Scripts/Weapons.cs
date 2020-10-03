using UnityEngine;
using Zenject;

public class Weapons : MonoBehaviour
{
    [Inject] private readonly PlayerInputState _playerInputState = default;
    [Inject] private readonly AmmunitionStorage _ammunitionStorage = default;
    [Inject] private readonly ProjectileEngineManager _projectileEngineManager = default;

    public float _shootDelay;
    public Transform _bulletStart;

    private float _lastFired = Mathf.NegativeInfinity;
    
    private void Awake()
    {
        _playerInputState.fireEvent += HandleFire;
    }

    private void HandleFire()
    {
        if (Time.time - _lastFired < _shootDelay)
        {
            return;
        }
        
        if (!_ammunitionStorage.GetAmmo())
        {
            return;
        }

        _lastFired = Time.time;
        _projectileEngineManager.SpawnSeed(_bulletStart.position, transform.rotation);
    }
}
