

using UnityEngine;



public abstract class StateBase : ScriptableObject
{
    public abstract void Enter();
    public abstract void UpdateState();
    public abstract void UpdatePhysics();
    public abstract void Exit();
}
