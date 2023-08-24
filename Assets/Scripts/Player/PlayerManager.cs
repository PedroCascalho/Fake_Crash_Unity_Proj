using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private PlayerMovementComponent movementComponent;

    private Transform playerTransform;
    
    private bool isJumping;
    private bool isMoving;

    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float velocity = 10;
    [SerializeField] private int lives = 1;

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
        playerTransform = GetComponent<Transform>();
        movementComponent = GetComponent<PlayerMovementComponent>();
        InputManager.onMove += MovePlayer;
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

    private void OnDisable()
    {
        InputManager.onMove -= MovePlayer;
    }


}
