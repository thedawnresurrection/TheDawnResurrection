using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class BodyPart : MonoBehaviour, IDamageable
{
    public BodyType bodyType;
    public bool left, right;
    public SpriteRenderer renderer;
    private BaseZombie baseZombie;
    private Collider2D collider;
    public List<Collider2D> disableColliders;
    private bool rupture;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        baseZombie = GetComponentInParent<BaseZombie>();
    }
    public void TakeDamage(int damage, Vector3 bloodPos)
    {
        int newDamage = damage;
        switch (bodyType)
        {
            case BodyType.Head:
                newDamage = damage * 4;

                if (!rupture)
                    HeadRupture();
                else newDamage = 0;

                break;
            case BodyType.Body:
                newDamage = damage;
                break;
            case BodyType.Arm:
                newDamage = damage / 2;
                break;
            case BodyType.Leg:
                newDamage = damage / 2;

                if (!rupture)
                {
                    LegRupture();
                }
                else newDamage = 0;


                break;
        }

        baseZombie.TakeDamage(newDamage, bloodPos);
    }
    public void TimedDamage(int damage, Vector3 bloodPos, int repeatCount, float repeatTime)
    {
        StartCoroutine(coroutine());
        IEnumerator coroutine()
        {
            for (int i = 0; i < repeatCount; i++)
            {
                yield return new WaitForSeconds(repeatTime);
                if (!baseZombie.Die && bodyType != BodyType.Head)
                    TakeDamage(damage, transform.position);
            }
        }
    }

    private void HeadRupture()
    {
        if (Random.value > 0.5f) return;
        CloseCollider();
        renderer.enabled = false;
        var zombieHead = Instantiate(baseZombie.zombieHeadPrefab, transform.position, Quaternion.identity);
        float randomY = Random.Range(4f, 4.5f);
        float randomX = Random.Range(2f, 4f);
        zombieHead.transform.DOMoveY(zombieHead.transform.position.y - randomY, 0.3f).SetEase(Ease.Linear);
        zombieHead.transform.DOMoveX(zombieHead.transform.position.x - randomX, 0.6f).SetEase(Ease.Linear).OnUpdate(delegate
        {
            zombieHead.transform.Rotate(Vector3.forward * 10);
        });
    }
    private void LegRupture()
    {
        if (Random.value > 0.3f) return;
        baseZombie.LegRupture();
        CloseCollider();

        renderer.enabled = false;
        var prefab = baseZombie.zombieLeftLeg;
        if (right) prefab = baseZombie.zombieRightLeg;

        var zombieLeg = Instantiate(prefab, transform.position, Quaternion.identity);
        float randomY = Random.Range(4f, 4.5f);
        float randomX = Random.Range(2f, 4f);
        //zombieLeg.transform.DOMoveY(zombieLeg.transform.position.y - randomY, 0.3f).SetEase(Ease.Linear);
        zombieLeg.transform.DOMoveX(zombieLeg.transform.position.x - randomX, 0.6f).SetEase(Ease.Linear).OnUpdate(delegate
        {
            zombieLeg.transform.Rotate(Vector3.forward);
        });
    }

    public void CloseCollider()
    {
        foreach (var item in disableColliders)
        {
            item.enabled = false;
        }
        collider.enabled = false;
    }


}
