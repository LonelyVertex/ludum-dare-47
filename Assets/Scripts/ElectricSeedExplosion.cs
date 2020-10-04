using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class ElectricSeedExplosion : MonoBehaviour
{
    public float boltRange;
    public int boltDamage;
    public int boltMaxJumps;

    private LayerMask _enemyMask;
    private GameObject _firstTarget;

    public GameObject FirstTarget
    {
        set => _firstTarget = value;
    }

    private GameObject GetRandomEnemy(GameObject target)
    {
        var targets = Physics2D.OverlapCircleAll(target.transform.position, boltRange, _enemyMask)
            .Where(t => t.gameObject != target).ToList();
        return targets[Random.Range(0, targets.Count)].gameObject;
    }

    private void StrikeTarget(GameObject target, int jumps)
    {
        if (target == null || jumps <= 0) return;

        target.GetComponent<Health>().DealDamage(boltDamage);
        StrikeTarget(GetRandomEnemy(target), jumps - 1);
    }

    private void Start()
    {
        _enemyMask = LayerMask.GetMask("Enemy");
        
        if (_firstTarget != null && _firstTarget.CompareTag("Enemy"))
        {
            StrikeTarget(_firstTarget, boltMaxJumps);
        }
    }
}
