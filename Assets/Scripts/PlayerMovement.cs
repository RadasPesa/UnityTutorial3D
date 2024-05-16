using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private GameObject target;
    private Vector3 moveDirection;
    
    private Vector2 inputDirection;
    private Vector2 currentInputDirection;
    private Vector2 smoothInputVelocity;
    [SerializeField] private float smoothInputSpeed = 0.2f;

    private Rigidbody rb;

    private CharacterController charController;
    private Vector3 horizontalVelocity;
    private bool isJumping = false;

    private Transform cameraTransform;

    private InputManager inputManager;
    
    void Start()
    {
        inputManager = InputManager.instance;

        //rb = GetComponent<Rigidbody>();

        charController = GetComponent<CharacterController>();

        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        inputDirection = inputManager.GetInputDirection();
        currentInputDirection = Vector2.SmoothDamp(currentInputDirection, inputDirection,
            ref smoothInputVelocity, smoothInputSpeed);
        moveDirection = new Vector3(currentInputDirection.x, 0f, currentInputDirection.y);
        moveDirection = cameraTransform.forward * moveDirection.z +
                        cameraTransform.right * moveDirection.x;
        moveDirection.y = 0f;
        
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
        if (charController.isGrounded)
        {
            horizontalVelocity.y = -1f;
            if (isJumping)
            {
                horizontalVelocity.y = jumpForce;
                isJumping = false;
            }
        }
        else
        {
            horizontalVelocity.y += Physics.gravity.y * 2f * Time.deltaTime;
        }
        charController.Move(moveDirection * (moveSpeed * Time.deltaTime));
        charController.Move(horizontalVelocity * Time.deltaTime);
        
        Vector3 eulerAngles = cameraTransform.eulerAngles;
        target.transform.rotation = Quaternion.Euler(
            eulerAngles.x,
            eulerAngles.y,
            0);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;

        if (charController.isGrounded)
            isJumping = true;
    }
}
