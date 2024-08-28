using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/NewWeaponData")]

public class SOWeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponType weaponType;
    public float bulletSpeed = 2f;
    public float fireRate = 0.5f;
    public int damage = 20;
    public int magazineBulletAmount = 7;
    public float resourceAmount;
    public AudioClip fireClip;
    public float minRotAngle, maxRotAngle;
    public float minFireAngle, maxFireAngle;
}
