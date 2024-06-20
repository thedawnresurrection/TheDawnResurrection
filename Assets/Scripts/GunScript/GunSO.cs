using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Silahlar/YeniSilah")]
public class GunSO : ScriptableObject
{
    //SO: Scriptable Object
    // BU SCRIPT HER B�R S�LAHIN AYARLARINI TEK B�R KODDAN YAPMAMIZI SA�LAR

    public string GunName;
    [Space]
    [Header("Mermi Ayar")]
    public float mermiHizi;
    public int mermiDamage;
    [Space]
    [Header("�arj�r Ayar")]
    public float cooldown;
    public int shootCount;
    public float mermiAtisSuresi;





}
