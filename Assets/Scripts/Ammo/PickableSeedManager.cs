using UnityEngine;
using Zenject;

public class PickableSeedManager
{
    [Inject] private readonly AmmunitionStorage _ammunitionStorage = default;
    [Inject] private readonly PickableSeed.Pool _pickableSeedPool = default;
    
    public void AddPickableSeed(Vector3 position, Quaternion rotation)
    {
        var seed = _pickableSeedPool.Spawn(position, rotation);
        seed.despawnEvent += HandlePickableSeedDespawn;
    }

    private void HandlePickableSeedDespawn(PickableSeed seed)
    {
        _ammunitionStorage.PutAmmo();
        
        seed.despawnEvent -= HandlePickableSeedDespawn;
        
        _pickableSeedPool.Despawn(seed);
    }
}
