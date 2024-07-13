

using UnityEngine;

public enum States
{
    None = 0,

}

public abstract class StateManagerBase : ScriptableObject
{
    public abstract void Enter();
    public abstract void UpdateState();
    public abstract void UpdatePhysics();
    public abstract void Exit();
}
