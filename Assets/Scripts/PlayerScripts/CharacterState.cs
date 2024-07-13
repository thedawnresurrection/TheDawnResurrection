

public abstract class CharacterState : StateBase
{
   protected CharacterStateMachine characterStateMachine;
   public void Initialize(CharacterStateMachine characterStateMachine)
   {
        this.characterStateMachine = characterStateMachine;
   }
}
