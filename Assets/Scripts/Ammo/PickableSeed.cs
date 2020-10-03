using UnityEngine;
using Zenject;


public class PickableSeed : MonoBehaviour
{
    private const string kPlayerTag = "Player";

    public event System.Action<PickableSeed> despawnEvent;
    
    public class Pool : MonoMemoryPool<Vector3, Quaternion, PickableSeed>
    {
        protected override void Reinitialize(Vector3 position, Quaternion rotation, PickableSeed seed)
        {
            seed.Reinitialize(position, rotation);
        }   
    }

    private void Reinitialize(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(kPlayerTag))
        {
            despawnEvent?.Invoke(this);
        }
    }
}
