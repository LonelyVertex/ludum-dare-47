using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingSeedExplosion : MonoBehaviour
{
    public int damage;
    private GameObject firstTarget;
    public GameObject FirstTarget
    {
        set
        {
            firstTarget = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("dealing damage");
        if (firstTarget.tag=="Enemy")
        {
        firstTarget.GetComponent<Health>().DealDamage(damage);
        }
        Destroy (this.gameObject);
    }

    // Update is called once per frame
   
}
