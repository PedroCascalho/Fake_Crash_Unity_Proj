using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private PlayerControls playerControl;
    private Transform playerTransform;
    private Vector3 currentMovement;
    private bool isJumping;
    private bool isMoving;

    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float velocity = 10;

    private void Awake()
    {
        #region Sigleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion
        playerControl = new PlayerControls();

        playerControl.PlayerActions.Move.started += OnMoveInput;
        playerControl.PlayerActions.Move.performed += OnMoveInput;
        playerControl.PlayerActions.Move.canceled += OnMoveInput;

        playerControl.PlayerActions.Jump.started += OnJumpInput;
        playerControl.PlayerActions.Jump.canceled += OnJumpInput;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 inputData = context.ReadValue<Vector2>();
        currentMovement.x = inputData.x;
        currentMovement.y = 0;
        currentMovement.z = inputData.y;
        isMoving = inputData.x != 0 || inputData.y != 0;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
        Jump();
    }
}
