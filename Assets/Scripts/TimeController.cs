using UnityEngine;
using Zenject;

public class TimeController : MonoBehaviour
{
    public AnimationCurve curve;

    [Inject]
    private AmmunitionStorage _ammunitionStorage;

    private int Ammo => _ammunitionStorage.currentAmmunitionCount;
    private int MaxAmmo => _ammunitionStorage.maxAmmunitionCount;

    private void Update()
    {
        if (MaxAmmo > 0)
        {
            Time.timeScale = curve.Evaluate(time: (float) Ammo / MaxAmmo);
        }
    }
}
