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
    public int resourceAmount;
    public AudioClip fireClip;
}
