using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterState/IdleState")]

public class CharacterIdleState : CharacterState
{
    public override void Enter()
    {
        Debug.Log("entered idle state");
    }

    public override void Exit()
    {
        Debug.Log("exited idle state");
    }

    public override void UpdatePhysics()
    {
        
    }

    public override void UpdateState()
    {
      
    }
}
