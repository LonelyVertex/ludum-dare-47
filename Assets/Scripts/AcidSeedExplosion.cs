using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSeedExplosion : MonoBehaviour
{
    public int damagePerTick;
    public float damageTickRate;
    private float _tick;
    float alphaT;
    public float fadeOutSpeed = 1;
    private List<GameObject> enemiesInPool;
    private SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInPool = new List<GameObject>();
        SR = GetComponentInChildren<SpriteRenderer>();
        _tick = 0;
    }
    private void FadeOut()
    {
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, Mathf.Lerp(1, 0, alphaT));
        alphaT += fadeOutSpeed * Time.deltaTime;
        if (SR.color.a <= 0.2)
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag=="Enemy")
        {
         
          enemiesInPool.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
         enemiesInPool.Remove(other.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        FadeOut();
        _tick -= Time.deltaTime;
        if (_tick <=0 && enemiesInPool.Count>0)
        {

            foreach (GameObject enemy in enemiesInPool)
            {

                enemy.GetComponent<Health>().DealDamage(damagePerTick);
                _tick = damageTickRate;
            }
            
        }
    }
}
