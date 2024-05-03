using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    private Vector3 moveDirection;
    
    private PlayerInput playerInput;
    private InputAction movementAction;
    private Vector2 inputDirection;
    private Vector2 currentInputDirection;
    private Vector2 smoothInputVelocity;
    [SerializeField] private float smoothInputSpeed = 0.2f;

    private Rigidbody rb;

    private CharacterController charController;
    private Vector3 horizontalVelocity;
    private float gravity = 9.81f;
    private bool isJumping = false;
    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        movementAction = playerInput.actions["Movement"];

        //rb = GetComponent<Rigidbody>();

        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        inputDirection = movementAction.ReadValue<Vector2>();
        currentInputDirection = Vector2.SmoothDamp(currentInputDirection, inputDirection,
            ref smoothInputVelocity, smoothInputSpeed);
        moveDirection = new Vector3(currentInputDirection.x, 0f, currentInputDirection.y);

        MovePlayerCC();
    }

    void FixedUpdate()
    {
        //MovePlayerRB();
    }

    private void MovePlayerRB()
    {
        Vector3 moveVector = transform.TransformDirection(moveDirection) * moveSpeed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
        
        //rb.MovePosition(transform.position + moveDirection * (moveSpeed * Time.deltaTime));
        
        //rb.AddForce((moveVector * moveSpeed), ForceMode.Force);
    }

    private void MovePlayerCC()
    {
        Vector3 moveVector = transform.TransformDirection(moveDirection);
        if (charController.isGrounded)
        {
            // OPRAVIT NA -1f
            horizontalVelocity.y = 0f;
            if (isJumping)
            {
                horizontalVelocity.y = jumpForce;
                isJumping = false;
            }
        }
        else
        {
            horizontalVelocity.y -= gravity * 2f * Time.deltaTime;
        }
        charController.Move(moveVector * (moveSpeed * Time.deltaTime));
        charController.Move(horizontalVelocity * Time.deltaTime);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;

        if (charController.isGrounded)
            isJumping = true;
    }
}
