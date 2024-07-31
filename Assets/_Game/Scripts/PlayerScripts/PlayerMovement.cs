using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SOMovementData movementData;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (movementData)
        {
            Vector3 position = transform.position;
            position.y = Mathf.Clamp(position.y, movementData.minY, movementData.maxY);
            transform.position = position;
        }
        else
        {
            Debug.LogError("Movement Data Empty");
        }

    }
    public void FixedUpdate()
    {
        if (movementData)
        {
            float inputY = Input.GetAxisRaw(movementData.inputAxisName);
            animator.SetBool("IsMove", inputY != 0);
            rb.velocity = new Vector3(0, movementData.movementSpeed * Time.fixedDeltaTime *
                inputY, 0);
        }
        else
        {
            Debug.LogError("Movement Data Empty");
        }
    }
}
