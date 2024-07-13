using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public PlayerMovement MovementM { get; set; }
    public Rigidbody2D rigidBody2D { get; set; }
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        MovementM = GetComponent<PlayerMovement>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }



}
