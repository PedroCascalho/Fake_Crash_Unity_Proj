using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private const float gravityValue = -9.81f;

    [SerializeField] private float velocity;
    [SerializeField] private float jumpHeight;
    #region privateVariables

    #endregion

    #region AnimParameters
    private int isJumpingHash;
    private int isWalkingHash;
    #endregion

    private PlayerControls playerInputs;

    Vector3 currentMovementDirection;
    private bool isJumping;

    private Transform playerTransform;    
    private CharacterController characterController;
    private Animator animator;    

    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        GetAnimationParameters();

        playerInputs = new PlayerControls();

        playerInputs.PlayerActions.Move.started += OnMoveInput;
        playerInputs.PlayerActions.Move.canceled += OnMoveInput;
        playerInputs.PlayerActions.Move.performed += OnMoveInput;

        playerInputs.PlayerActions.Jump.started += OnJumpInput;
        playerInputs.PlayerActions.Jump.performed += OnJumpInput;
    }

    private void GetAnimationParameters()
    {
        isJumpingHash = Animator.StringToHash("isJumping");
        isWalkingHash = Animator.StringToHash("isWalking");
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
        if (characterController.isGrounded)
        {
            currentMovementDirection.y = Mathf.Sqrt(jumpHeight * gravityValue * -1);
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        GravityHandler();
        AnimationHandler();
    }

    private void GravityHandler()
    {
        currentMovementDirection.y += gravityValue * Time.deltaTime;
    }

    private void MovePlayer()
    {
        characterController.Move(currentMovementDirection * velocity * Time.deltaTime);

        if (characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    private void AnimationHandler()
    {
        bool isJumpingAnimation = animator.GetBool(isJumpingHash);
        bool isWalkingAnimation = animator.GetBool(isWalkingHash);
        bool isWalking = currentMovementDirection.x != 0 || currentMovementDirection.z != 0;
        #region Outra maneira de escrever a linha de cima
        /*if(currentMovementDirection.x != 0 || currentMovementDirection.z != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false
        }*/
        #endregion

        if (isWalking && characterController.isGrounded)
        {
            animator.SetBool(isWalkingHash, true);
        }
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
