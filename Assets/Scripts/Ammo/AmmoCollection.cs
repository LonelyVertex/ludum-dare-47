using UnityEngine;
using Zenject;

public class AmmoCollection : MonoBehaviour
{
    private const string kPickableSeedTag = "PickableSeed";

    [Inject] private readonly AmmunitionStorage _ammunitionStorage = default;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(kPickableSeedTag))
        {
            _ammunitionStorage.PutAmmo();
        }
    }
}
