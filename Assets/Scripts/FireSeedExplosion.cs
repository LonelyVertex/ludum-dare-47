using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSeedExplosion : MonoBehaviour
{   //CircleCollider2D col;
    public float maxsize;
    public float damageTickRate;
    private float _damageTickCounter;
    public float growthSpeed;
    public int damagePerTick;
    private SpriteRenderer SR;

    public float fadeOutSpeed = 1;
    float scaleT;
    float alphaT;
    private float originalScale;
    // Use this for initialization
    private void Start()
    {
        _damageTickCounter = 0;
        //col.GetComponent<CircleCollider2D> ();
        SR = GetComponentInChildren<SpriteRenderer>();
        originalScale = transform.localScale.x;
    }

    // Update is called once per frame
    private void Update()
    {
        FadeOut();
        if (SR.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FadeOut()
    {
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, Mathf.Lerp(1, 0, alphaT));
        alphaT += fadeOutSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _damageTickCounter -= Time.deltaTime;
        transform.localScale = Vector3.one * Mathf.Lerp(originalScale, maxsize, scaleT);
        scaleT += growthSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.tag == "Enemy" )
        {
            other.GetComponent<Health>().DealDamage(damagePerTick);
        }
    }

}
