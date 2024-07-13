using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public PlayerMovement MovementM { get; set; }
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        MovementM = GetComponent<PlayerMovement>();
    }

    public void ChangeState(States states)
    {
        
    }


}
