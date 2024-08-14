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
    public bool Die => die;

    public Transform shadowTransform;
    public GameObject zombieHeadPrefab;
    public GameObject zombieLeftLeg, zombieRightLef;
    public GameObject bloodEffectPrefab;
    public List<SpriteRenderer> renderers;
    private Barricade targetBarricade;
    private float attackTimer;
    public float attackTime = 1f;
    public float damage = 3;
    private bool freeze;

    private void Start()
    {
        GameEvents.ExpolisionFlashBomb.AddListener(ExpolisionFlashBomb);
    }

    private void OnDestroy()
    {
        GameEvents.ExpolisionFlashBomb.RemoveListener(ExpolisionFlashBomb);
    }
    private void ExpolisionFlashBomb(float duration, float freezeTime)
    {
        if (!freeze)
        {
            freeze = true;
            DOVirtual.DelayedCall(freezeTime, delegate ()
            {
                freeze = false;
            });
        }
    }
    private void Awake()
    {
        health = maxHealth;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (freeze) return;

        attackTimer += Time.deltaTime;
        if (!die && targetBarricade && attackTimer >= attackTime)
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
        if (!die && !targetBarricade && !freeze)
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
    public void TakeDamage(int damage, Vector3 bloodPos)
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

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Barricade barricade))
        {
            targetBarricade = barricade;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Barricade barricade))
        {
            targetBarricade = null;
        }
    }
}
