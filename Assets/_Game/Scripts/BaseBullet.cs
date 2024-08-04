using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class BaseBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 dir;
    private float bulletSpeed;
    [NonSerialized]public int damage;
    public void Initialize(Vector2 _dir,float _bulletSpeed,int _damage)
    {
        dir = _dir;
        bulletSpeed = _bulletSpeed;
        damage = _damage;
        rb = GetComponent<Rigidbody2D>();
        Move();
        Destroy(gameObject,1.5f);
    }

    public void Move()
    {
        rb.AddForce(dir * bulletSpeed,ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IDamageable damageable))
        {
            Vector3 collisionPoint = collision.bounds.ClosestPoint(transform.position);
            damageable.TakeDamage(damage,collisionPoint);
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out BodyPart bodyPart))
        {
        //    Debug.Log(" Target Body Type :  " + bodyPart.bodyType.ToString());   
        }
        
    }

}
