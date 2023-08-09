using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    private PlayerControls playerInputs;

    Vector3 currentMovementDirection;
    private bool isJumping;
    private bool canJump;

    private Transform playerTransform;
    private Animator animator;
    private CharacterController characterController;

    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        playerInputs = new PlayerControls();

        playerInputs.PlayerActions.Move.started += OnMoveInput;
        playerInputs.PlayerActions.Move.canceled += OnMoveInput;
        playerInputs.PlayerActions.Move.performed += OnMoveInput;

        playerInputs.PlayerActions.Jump.started += OnJumpInput;
        playerInputs.PlayerActions.Jump.performed += OnJumpInput;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        currentMovementDirection.x = direction.x;
        currentMovementDirection.y = 0;
        currentMovementDirection.z = direction.y;
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
        JumpPlayer();
    }

    private void JumpPlayer()
    {
        characterController.attachedRigidbody.AddForce(Vector3.up * jumpForce);
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        characterController.Move(currentMovementDirection * velocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
