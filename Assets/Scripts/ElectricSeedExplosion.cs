using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ElectricSeedExplosion : MonoBehaviour
{

    public float boltRange;
    public int boltDamage;
    public int boltMaxJumps;

    private Collider2D randomTarget;
    LayerMask enemyMask;
    private GameObject firstTarget;
    private GameObject nextTarget;
    List<GameObject> potentialTargets;
    private List<Vector3> boltPath;
    public GameObject FirstTarget
    {
        set
        {
            firstTarget = value;
        }
    }
    GameObject getRandomEnemyInProximity(Transform origin)
    {
        GameObject enemyInProximity;
        enemyMask = LayerMask.GetMask("Enemy");
        potentialTargets = new List<GameObject>();
        enemyInProximity = null;
        List<Collider2D> allTargets = Physics2D.OverlapCircleAll(origin.position, boltRange, enemyMask).ToList<Collider2D>();
        foreach (Collider2D col in allTargets)
        {
            if (col.gameObject == firstTarget.gameObject)
            {
                continue;
            }
            else
            {
                potentialTargets.Add(col.GetComponent<GameObject>());
            }
        }
        if (potentialTargets.Count > 0)
        {
            Debug.Log("there are " + potentialTargets.Count + " potential targets");
            enemyInProximity = potentialTargets[Random.Range(0, potentialTargets.Count)];
            Debug.Log  (potentialTargets);
            Debug.Log  (potentialTargets.Count);
            Debug.Log  ("whats there" + potentialTargets[0] );
        } 
        Debug.Log ("returning random target " +  enemyInProximity);
        return enemyInProximity;

    }
    void strikeTargets(GameObject FirstTarget)
    {
        boltPath = new List<Vector3>();
        GameObject currentBoltLocation;

        FirstTarget.GetComponent<Health>().DealDamage(boltDamage);
        boltPath.Add(FirstTarget.transform.position);
        currentBoltLocation = FirstTarget;

        for (int i = boltMaxJumps; i > 0; i--)
        {
            Debug.Log ("trying to find target");
            nextTarget = getRandomEnemyInProximity(currentBoltLocation.transform);
             Debug.Log ("found " + nextTarget);
            if (nextTarget != null)
            {
                Debug.Log("dealing damage jumps left" + i);
                nextTarget.GetComponent<Health>().DealDamage(boltDamage);
                currentBoltLocation = nextTarget;
            }
            else
            {
                Debug.Log("no target to hit");
                break;
            }
        }
    }
    void Start()
    {
        if (firstTarget != null && firstTarget.tag == "Enemy")
        {
            Debug.Log("enemy hit starting el. bolt");
            strikeTargets(firstTarget);
        }
    }

}
