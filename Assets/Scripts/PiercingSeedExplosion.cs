using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingSeedExplosion : MonoBehaviour
{
    public int damage;
    private GameObject firstTarget;
    public GameObject FirstTarget
    {
        set => firstTarget = value;
    }
    
    void Start()
    {
        Debug.Log("dealing damage");
        if (firstTarget.CompareTag("Enemy"))
        {
            firstTarget.GetComponent<Health>().DealDamage(damage);
        }

        Destroy (this.gameObject);
    }
}
