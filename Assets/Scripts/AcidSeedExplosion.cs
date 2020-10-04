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
    private SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
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
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Enemy" && _tick <= 0)
        {
            _tick = damageTickRate;
            other.GetComponent<Health>().DealDamage(damagePerTick);
            Debug.Log("damaging enemy");
        }
    }
    // Update is called once per frame
    void Update()
    {
        FadeOut();
        _tick -= Time.deltaTime;
    }
}
