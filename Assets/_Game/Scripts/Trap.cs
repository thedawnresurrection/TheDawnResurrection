using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public enum TrapType { BearTrap, ToxicSlime, TriggeredBomb }
    public TrapType trapType;
    public BearTrapData bearTrapData;
    public ToxicSlimeData toxicSlime;
    public TriggeredBombData triggeredBombData;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BaseZombie baseZombie))
        {
            if (trapType == TrapType.BearTrap)
            {
                GameEvents.ZombieBearTrapEvent?.Invoke(baseZombie, bearTrapData.freezeTime, bearTrapData.damage);
            }
            else if (trapType == TrapType.ToxicSlime)
            {
                GameEvents.ZombieToxicSlimeEnterEvent?.Invoke(baseZombie, toxicSlime.targetMoveSpeed, toxicSlime.damage);
                Debug.Log("Toxic Slime");
            }
            else if (trapType == TrapType.TriggeredBomb)
            {
                GameEvents.ZombieTriggeredBombEvent?.Invoke();
                Debug.Log("Triggered Bomb");
                var baseBomb = Instantiate(triggeredBombData.expolisionBombPrefab, transform.position, Quaternion.identity);
                baseBomb.damage = triggeredBombData.damage;
                baseBomb.radius = triggeredBombData.radius;
                baseBomb.onBombExplodeEvent.AddListener(delegate
                {
                    Destroy(gameObject);
                });
            }


        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out BaseZombie baseZombie))
        {
            GameEvents.ZombieToxicSlimeExitEvent?.Invoke(baseZombie);
        }

    }
}
[Serializable]
public class BearTrapData
{
    public float freezeTime = 5;
    public int damage = 5;
}
[Serializable]
public class ToxicSlimeData
{
    public float targetMoveSpeed = 50;
    public int damage = 3;
}
[Serializable]
public class TriggeredBombData
{
    public BaseBomb expolisionBombPrefab;
    public int damage = 4;
    public float radius = 5;
}