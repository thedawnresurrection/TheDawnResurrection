using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class BodyPart : MonoBehaviour
{
    public BodyType bodyType;
    private BaseZombie baseZombie;
    private Collider2D collider;
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        baseZombie = GetComponentInParent<BaseZombie>();
    }
    public void TakeDamage(int damage)
    {
        int newDamage = damage;
        switch (bodyType)
        {
            case BodyType.Head:
                newDamage = damage * 4;
                HeadRupture();
                break;
            case BodyType.Body:
                newDamage = damage;
                break;
            case BodyType.Arm:
                newDamage = damage / 2;
                break;
            case BodyType.Leg:
                newDamage = damage / 2;
                break;
        }
        
        baseZombie.TakeDamage(newDamage);
    }

    private void HeadRupture()
    {
        //GameObject parent = new GameObject("Parent");
        //transform.SetParent(parent.transform);
        //parent.transform.position = transform.position - Vector3.up * 2.2f;

        CloseCollider();
        SpriteMask  spriteMask = GetComponentInChildren<SpriteMask>();
        if (spriteMask)
        {
            spriteMask.enabled = true;
        }
        var zombieHead =Instantiate(baseZombie.zombieHeadPrefab, transform.position, Quaternion.identity);
        float randomY = Random.Range(4f, 4.5f);
        float randomX = Random.Range(2f, 4f);
        zombieHead.transform.DOMoveY(zombieHead.transform.position.y - randomY, 0.3f).SetEase(Ease.Linear);
        zombieHead.transform.DOMoveX(zombieHead.transform.position.x - randomX, 0.6f).SetEase(Ease.Linear).OnUpdate(delegate
        {
            zombieHead.transform.Rotate(Vector3.forward * 10);
        });

    }

    public void CloseCollider()
    {
        collider.enabled = false;
    }

   
}
