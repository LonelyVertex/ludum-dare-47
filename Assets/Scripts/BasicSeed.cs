using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void DealDamage()
{

Debug.Log ("enemy hit");
//spawn projectile to pickup

GameObject.Destroy (this.gameObject);


}
    private void OnCollisionEnter2D(Collision2D other) {
        
        if (other.transform.tag=="Enemy")
        {

                DealDamage();

        }
    }
}
