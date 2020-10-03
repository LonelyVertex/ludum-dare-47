using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float projectileSpeed;

    private int _damage;

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = transform.right * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().DealDamage(_damage);
        }
        
        Destroy(gameObject);
    }
}