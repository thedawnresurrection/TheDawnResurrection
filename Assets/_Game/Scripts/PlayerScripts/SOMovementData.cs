using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/NewMovementData")]
public class SOMovementData : ScriptableObject
{
    public float movementSpeed;
    public string inputAxisName = "Vertical";
    public float minY = -5f, maxY= 9f;
}
