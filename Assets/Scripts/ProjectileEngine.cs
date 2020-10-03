using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEngine : MonoBehaviour
{
    public float projectileSpeed;
    public float lifespan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan-=Time.deltaTime;
       if (lifespan  <=0)
       {

           GameObject.Destroy(this.gameObject);
       }
        transform.Translate(Vector2.right*projectileSpeed*Time.deltaTime);
    }
}
