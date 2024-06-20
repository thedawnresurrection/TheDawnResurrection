using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Silahlar/YeniSilah")]
public class GunSO : ScriptableObject
{
    //SO: Scriptable Object
    // BU SCRIPT HER BÝR SÝLAHIN AYARLARINI TEK BÝR KODDAN YAPMAMIZI SAÐLAR

    public string GunName;
    [Space]
    [Header("Mermi Ayar")]
    public float mermiHizi;
    public int mermiDamage;
    [Space]
    [Header("Þarjör Ayar")]
    public float cooldown;
    public int shootCount;
    public float mermiAtisSuresi;





}
