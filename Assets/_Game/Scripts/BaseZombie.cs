using DG.Tweening;
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
    private bool die;

    public Transform shadowTransform;
    public SpriteRenderer spriteRenderer;
    public GameObject zombieHeadPrefab;
    private void Awake()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        if (!die)
        {
            rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
            animator.SetBool("IsMove", rb.velocity.magnitude > 0.2f);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("IsMove", false);
        }
        
    }
    private void DisableBodyParts()
    {
        var bodyParts = GetComponentsInChildren<BodyPart>();
        foreach (var part in bodyParts)
        {
            part.CloseCollider();
        }
    }
    public void TakeDamage(int damage)
    {
        if (die) return;
        health -= damage;
        if (health <= 0)
        {
            GameEvents.ZombieDieEvent?.Invoke();

            die = true;
            animator.SetBool("Die", true);
            animator.SetTrigger("DieTrigger");
            DisableBodyParts();

            DOVirtual.DelayedCall(3, delegate()
            {
                spriteRenderer.DOColor(Color.clear, 1f).OnComplete(delegate ()
                {
                    Destroy(gameObject);
                });
            });

        }
    }
}
