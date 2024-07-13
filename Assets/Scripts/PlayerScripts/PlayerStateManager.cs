using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : StateBase
{
   protected CharacterStateMachine CharacterStateMachine;
   public void Initilaize(CharacterStateMachine characterStateMachine)
   {
        this.CharacterStateMachine = characterStateMachine;
   }
}
