using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseZombie : MonoBehaviour
{

    private float speed;
    public float moveSpeed = 100;
    public float creepSpeed = 50;
    private Rigidbody2D rb;
    private Animator animator;
    public int maxHealth;
    private int health;
    private bool die;
    public bool Die => die;

    public Transform shadowTransform;
    public GameObject zombieHeadPrefab;
    public GameObject zombieLeftLeg, zombieRightLeg;
    public GameObject bloodEffectPrefab;
    public List<SpriteRenderer> renderers;
    private Barricade targetBarricade;
    private float attackTimer;
    public float attackTime = 1f;
    public float damage = 3;
    private bool freeze;
    private bool toxicSlimeDamage;

    private void Start()
    {
        GameEvents.ExpolisionFlashBombEvent.AddListener(ExpolisionFlashBomb);
        GameEvents.ZombieBearTrapEvent.AddListener(ZombieBearTrap);
        GameEvents.ZombieToxicSlimeEnterEvent.AddListener(ZombieToxicSlimeEnterEvent);
        GameEvents.ZombieToxicSlimeExitEvent.AddListener(ZombieToxicSlimeExitEvent);
    }

    private void OnDestroy()
    {
        GameEvents.ExpolisionFlashBombEvent.RemoveListener(ExpolisionFlashBomb);
        GameEvents.ZombieBearTrapEvent.RemoveListener(ZombieBearTrap);
        GameEvents.ZombieToxicSlimeEnterEvent.RemoveListener(ZombieToxicSlimeEnterEvent);
        GameEvents.ZombieToxicSlimeExitEvent.RemoveListener(ZombieToxicSlimeExitEvent);
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
    private void ZombieBearTrap(BaseZombie baseZombie, float freezeTime, int damage)
    {
        if (this != baseZombie) return;
        if (!freeze)
        {
            TakeDamage(damage, transform.position);
            freeze = true;
            DOVirtual.DelayedCall(freezeTime, delegate
            {
                freeze = false;
            });
        }
    }
    private void ZombieToxicSlimeEnterEvent(BaseZombie baseZombie, float targetMoveSpeed, int damage)
    {
        if (baseZombie == this)
        {
            speed = targetMoveSpeed;
            toxicSlimeDamage = true;
            StartCoroutine(ZombieToxicSlimeSendDamage(damage, transform.position, 1));
            IEnumerator ZombieToxicSlimeSendDamage(int damage, Vector3 bloodPos, float damageDuration)
            {
                yield return new WaitForSeconds(damageDuration);
                TakeDamage(damage, bloodPos);
                if (toxicSlimeDamage) StartCoroutine(ZombieToxicSlimeSendDamage(damage, bloodPos, damageDuration));
            }
        }
    }
    private void ZombieToxicSlimeExitEvent(BaseZombie baseZombie)
    {
        if (baseZombie == this)
        {
            toxicSlimeDamage = false;
            speed = moveSpeed;
        }
    }
    private void Awake()
    {
        speed = moveSpeed;
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
            rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
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
        if (health <= 0 && !die)
        {
            ZombieDie();
        }
    }
    public void TakeDamage(int damage)
    {
        if (die) return;
        health -= damage;
        if (health <= 0 && !die)
        {
            ZombieDie();
        }
    }

    private void ZombieDie()
    {
        GameEvents.ZombieDieEvent?.Invoke();

        DisableBodyParts();
        die = true;
        animator.SetBool("Die", true);
        animator.SetTrigger("DieTrigger");

        DOVirtual.DelayedCall(1f, delegate
        {
            foreach (var renderer in renderers)
            {
                Tween colorTween = renderer.DOColor(Color.clear, 0.2f);
            }
            Destroy(gameObject, 0.3f);
        });
    }
    public void LegRupture()
    {
        speed = creepSpeed;
        animator.SetBool("LegRupture", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Barricade barricade))
        {
            if (targetBarricade == null)
                targetBarricade = barricade;
        }
    }
}
