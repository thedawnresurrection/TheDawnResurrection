using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SOMovementData movementData;

    private Rigidbody2D rb;
    private Animator animator;
    private bool canMove = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        GameEvents.PlayerMagazineReloadStartEvent.AddListener(MagazineReloadStart);
        GameEvents.PlayerMagazineReloadEndEvent.AddListener(MagazineReloadEnd);
        GameEvents.PlayerWeaponChangeStartEvent.AddListener(WeaponChangeStart);
        GameEvents.PlayerWeaponChangeEndEvent.AddListener(WeaponChangeEnd);
    }
    private void OnDestroy()
    {
        GameEvents.PlayerMagazineReloadStartEvent.RemoveListener(MagazineReloadStart);
        GameEvents.PlayerMagazineReloadEndEvent.RemoveListener(MagazineReloadEnd);
        GameEvents.PlayerWeaponChangeStartEvent.RemoveListener(WeaponChangeStart);
        GameEvents.PlayerWeaponChangeEndEvent.RemoveListener(WeaponChangeEnd);
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
            if (!canMove)
            {
                rb.velocity = Vector2.zero;
                return;
            }
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
    private void MagazineReloadStart()
    {
        canMove = false;
    }
    private void MagazineReloadEnd()
    {
        canMove = true;
    }
    private void WeaponChangeStart()
    {
        canMove = false;
    }
    private void WeaponChangeEnd()
    {
        canMove = true;
    }
}
