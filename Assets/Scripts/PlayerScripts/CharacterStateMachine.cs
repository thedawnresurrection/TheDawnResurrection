using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    [SerializeField]private CharacterState currentState;
    public PlayerManager playerManager;
    
    public bool canTransitionState = true;


    public CharacterIdleState characterIdleStateTemplate;
    public CharacterWalkState characterWalkStateTemplate;

    [HideInInspector]public CharacterIdleState characterIdleState;
    [HideInInspector]public CharacterWalkState characterWalkState;

    private void Awake() 
    {
        playerManager = GetComponent<PlayerManager>();
        
        characterIdleState = Instantiate(characterIdleStateTemplate);
        characterWalkState = Instantiate(characterWalkStateTemplate);

        characterIdleState.Initilaize(this);
        characterWalkState.Initilaize(this);

     
        
    }
    CharacterState InitFromSO(CharacterState stateTemplate){
        CharacterState s = Instantiate(stateTemplate);
        s.Initilaize(this);
        return s;
    }
    void Start()
    {
        ChangeState(characterIdleState);
    }

    
    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeState(characterIdleState);
        }
          if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeState(characterWalkState);
        }
    }
    private void FixedUpdate() {
         if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }
    public void ChangeState(CharacterState newState)
    {
        if (canTransitionState == false)
        {
            return;
        }

        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

}
