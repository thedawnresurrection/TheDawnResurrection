using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterState/WalkState")]
public class CharacterWalkState : CharacterState
{
    private Rigidbody2D rigidBody2D;
    [SerializeField]private float speed;
    public override void Enter()
    {
        Debug.Log("entered walk state");
        rigidBody2D = CharacterStateMachine.playerManager.rigidBody2D;
    }

    public override void Exit()
    {
        Debug.Log("exited walk state");
    }

    public override void UpdatePhysics()
    {
       rigidBody2D.velocity = new Vector2( speed, rigidBody2D.velocity.y);
    }

    public override void UpdateState()
    {
    
    }
}
