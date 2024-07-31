using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseZombie : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    public int maxHealth;
    private int health;
    private void Awake()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        animator.SetBool("IsMove", rb.velocity.magnitude > 0.2f);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
