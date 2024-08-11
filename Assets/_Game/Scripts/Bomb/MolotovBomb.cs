using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovBomb : BaseBomb
{
    public int timedDamage;
    public int repeatCount;
    public float repeatTime;
    public override void Explosion()
    {
        base.Explosion();
        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TimedDamage(timedDamage, hit.point, repeatCount, repeatTime);
                }
            }
        }
    }
}
