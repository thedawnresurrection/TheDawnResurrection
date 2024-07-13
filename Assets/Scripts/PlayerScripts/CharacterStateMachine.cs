using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class CharacterStateMachine : MonoBehaviour
{
    [SerializeField] private CharacterState currentState;
    
    //State templates
    [SerializeField] private CharacterIdleState characterIdleStateTemplate;
    [SerializeField] private CharacterWalkState characterWalkStateTemplate;

    //State members 
    private CharacterIdleState characterIdleState;
    private CharacterWalkState characterWalkState;

    //Other
    public bool CanTransitionState { get; private set; } = true;
    public Rigidbody2D Rb { get; private set; }


    private void Awake() 
    {   
        InitFromSO(characterIdleStateTemplate, out characterIdleState);
        InitFromSO(characterWalkStateTemplate, out characterWalkState);

        Rb = GetComponent<Rigidbody2D>();
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

        //Test 
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeState(characterIdleState);
        }
        
        if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeState(characterWalkState);
        }
        //--
    }

    private void FixedUpdate() {
         
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    private void InitFromSO<T>(CharacterState stateTemplate, out T state) where T : CharacterState
    {
        state = (T)Instantiate(stateTemplate);
        state.Initialize(this);
    }

    public void ChangeState(CharacterState newState)
    {
        if (!CanTransitionState)
        {
            return;
        }

        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

}
