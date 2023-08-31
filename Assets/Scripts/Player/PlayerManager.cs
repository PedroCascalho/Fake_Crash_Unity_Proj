using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public event Action<float> OnMoveInputReceived;

    private Transform playerTransform;
    private bool isJumping;
    private bool isMoving;

    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float velocity = 10;
    [SerializeField] private int lives = 1;

    private void Awake()
    {
        
        movementComponent = GetComponent<PlayerMovementComponent>();
        playerTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
        InputManager.onMove += MovePlayer;
        PlayerManagerSetUp();
    }


    private void PlayerManagerSetUp()
    {
        GameSystem.OnMoveInputContextReceived += MovePlayer;
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        movementComponent.MovePlayer(context);
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public void SetIsMoving(bool isMoving)
    {
        this.isMoving = isMoving;
    }

    public float GetPlayerVelocity()
    {
        return velocity;
    }
    public float GetCurrentVelocity()
    {
        print(characterController.velocity.magnitude);
        return characterController.velocity.magnitude;
    }

    public CharacterController GetCharacterController()
    {
        return characterController;
    }

    public void SetCharacterController(CharacterController characterController)
    {
        this.characterController = characterController;
    }

    public bool GetIsJumping()
    {
        return isJumping;
    }

    private void OnDisable()
    {
        InputManager.onMove -= MovePlayer;
    }

}
