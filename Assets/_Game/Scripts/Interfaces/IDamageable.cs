using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damage, Vector3 bloodPos);
    public void TimedDamage(int damage, Vector3 bloodPos, int repeatCount, float repeatTime);
}
