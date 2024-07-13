using UnityEngine;


[RequireComponent(typeof(Sprite))]
public abstract class WeaponBase
{
    [field: SerializeField] public int Damage { get; protected set; }
    [field: SerializeField] public int Clip { get; protected set; }
    [field: SerializeField] public Animator Anim { get; protected set; }


    public abstract void Fire();
    public abstract void Reload();
    

}
