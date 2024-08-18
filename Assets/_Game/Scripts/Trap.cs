using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public enum TrapType { BearTrap, ToxicSlime, TriggeredBomb }
    public TrapType trapType;
    public int damage = 10;
    public int freezeTime = 5;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BaseZombie baseZombie))
        {
            if (trapType == TrapType.BearTrap)
            {
                GameEvents.ZombieBearTrapEvent?.Invoke(baseZombie, freezeTime, damage);
            }
            else if (trapType == TrapType.ToxicSlime)
            {
                Debug.Log("Toxic Slime");
            }
            else if (trapType == TrapType.TriggeredBomb)
            {
                Debug.Log("Triggered Bomb");
            }


        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {

    }
}
