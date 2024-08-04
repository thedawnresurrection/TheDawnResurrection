using DG.Tweening;
using System;
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
    public GameObject bloodEffectPrefab;

    private Barricade targetBarricade;
    private float attackTimer;
    public float attackTime = 1f;
    public float damage = 3;
    private void Awake()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        attackTimer += Time.deltaTime;
        if(!die && targetBarricade && attackTimer>=attackTime)
        {
            attackTimer = 0;
            PlayAttackAnimation();
        }
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
    public void SendAttackDamage()
    {
        if (targetBarricade == null) return;
        targetBarricade.TakeDamage(damage);
    }

    private void FixedUpdate()
    {
        if (!die && !targetBarricade)
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
    public void TakeDamage(int damage,Vector3 bloodPos)
    {
        if (die) return;
        var blood = Instantiate(bloodEffectPrefab, bloodPos, Quaternion.identity);
        blood.transform.SetParent(transform);

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Barricade barricade))
        {
            targetBarricade = barricade;
        }
    }
}
