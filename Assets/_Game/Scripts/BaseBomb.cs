using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBomb : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    public float radius = 4f;
    public float distance = 2f;
    public int damage = 100;
    public float explosionTime = 2f;
    private void Start()
    {
        Invoke(nameof(Explosion), explosionTime);
    }
    public void Explosion()
    {
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        var hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.up, distance);
        foreach ( var hit in hits)
        {
            if(hit.collider != null)
            {
                if(hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage,hit.point);
                }
            }
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
