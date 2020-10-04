using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class ElectricSeedExplosion : MonoBehaviour
{
    public GameObject lightningPrefab;
    public float boltRange;
    public int boltDamage;
    public int boltMaxJumps;
    private Transform  _explosionLocation;
    private LayerMask _enemyMask;
    private GameObject _firstTarget;

    public GameObject FirstTarget
    {
        set => _firstTarget = value;
    }
  

    public GameObject GetRandomEnemy(Transform target)
    {
        var targets = Physics2D.OverlapCircleAll(target.position, boltRange, _enemyMask)
            .Where(t => t.gameObject != target).ToList();

        return targets.Count > 0 ? targets[Random.Range(0, targets.Count)].gameObject : null;
    }

    private void StrikeTarget(GameObject target, int jumps)
    {
        target.GetComponent<Health>().DealDamage(boltDamage);
        var next = GetRandomEnemy(target.transform);

        if (jumps <= 0 || next == null) return;

        SpawnLightning(target.transform, next.transform);
        StrikeTarget(next, jumps - 1);
    }

    private void SpawnLightning(Transform target1, Transform target2)
    {
        var lightning = Instantiate(lightningPrefab, transform);
        lightning.GetComponent<Lightning>().SetTargets(target1, target2);
    }

    private void Start()
    {
        _enemyMask = LayerMask.GetMask("Enemy");

        if (_firstTarget != null && _firstTarget.CompareTag("Enemy"))
        {
            StrikeTarget(_firstTarget, boltMaxJumps);
        }
        else
        {
               // FirstTarget=GetRandomEnemy(Transform.position);

        }
        
        Invoke(nameof(DestroyLater), 0.4f);
    }

    private void DestroyLater()
    {
        Destroy(gameObject);
    }
}
