using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitPoint : MonoBehaviour
{
    private Collider2D col;

    public event Action<float, float> OnTakeHit; //<mermi hasar�, azalt�lan y�zde>

    public int hitPercent = 100;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void Hitting(Bullet bullet)
    {
        OnTakeHit?.Invoke(bullet.BulletDamage, hitPercent); //OnTakeHit Eventini Tetikliyoruz ve bu evente abone olan fonksiyonlar �al���yor
    }

    public void Deactive()
    {
        if (col == null) return;

        col.isTrigger = true;
    }
}
