using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/NewBarricadeData")]
public class SOBarricadeData : ScriptableObject
{
    public float maxHealth = 100;
    public float maxArmor = 100;
    [Range(0, 1)] public float armorRating =0.2f;
    public int barricadeLevel = 0;
}
