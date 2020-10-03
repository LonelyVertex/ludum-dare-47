using UnityEngine;

public class AmmunitionStorage : MonoBehaviour
{
    public PlayerFlowerType playerFlowerType;

    [SerializeField] private int _maxAmmunition = default;
    [SerializeField] private int _currentAmmoCount = default;

    public int maxAmmunitionCount => _maxAmmunition;
    public int currentAmmunitionCount => _currentAmmoCount;
    
    public bool HasAmmo()
    {
        return _currentAmmoCount > 0;
    }

    public bool IsFull()
    {
        return _currentAmmoCount == _maxAmmunition;
    }
    
    public bool GetAmmo()
    {
        if (!HasAmmo())
        {
            return false;
        }
        
        _currentAmmoCount--;
        return true;
    }

    public bool PutAmmo()
    {
        if (IsFull())
        {
            return false;
        }

        _currentAmmoCount++;
        return true;
    }
}
