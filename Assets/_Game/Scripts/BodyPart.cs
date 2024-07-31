using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public BodyType bodyType;
    private BaseZombie baseZombie;
    private void Awake()
    {
        baseZombie = GetComponentInParent<BaseZombie>();
    }
    public void TakeDamage(int damage)
    {
        int newDamage = damage;
        switch (bodyType)
        {
            case BodyType.Head:
                newDamage = damage * 4;
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

   
}
