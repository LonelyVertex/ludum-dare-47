using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSeedExplosion : MonoBehaviour
{   //CircleCollider2D col;
    public float maxsize;
    public float damageTickRate;
    public float growthSpeed;
    private SpriteRenderer SR;
    public Transform explosion;
    public float fadeOutSpeed = 1;
    float scaleT;
    float alphaT;
    // Use this for initialization
    private void Start()
    {

        //col.GetComponent<CircleCollider2D> ();
        SR = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    private void Update()
    {
        FadeOut();
    }

    private void FadeOut()
    {


        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b,  Mathf.Lerp(1, 0, alphaT));
        alphaT += fadeOutSpeed * Time.deltaTime;
   
    }

    private void FixedUpdate()
    {
        transform.localScale = Vector3.one * Mathf.Lerp(1f, 5f, scaleT);
        scaleT += growthSpeed * Time.deltaTime;
   
    }
 private void OnTriggerStay2D(Collider2D other) {
     
 }

}
