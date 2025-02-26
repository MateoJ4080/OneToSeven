using System.IO;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : PlayerHealth
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Die
    }

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float _camSens = 7;

    private PlayerState _state = PlayerState.Idle;

    private Vector2 _movementInput;

    private InputManager _inputManager;
    private PlayerControls _playerControls;

    private CharacterController _controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float _speed;

    void Awake()
    {
        _inputManager = InputManager.Instance;
        _playerControls = new PlayerControls();
        _controller = GetComponent<CharacterController>();

        cameraTransform = Camera.main.transform;
    }

    void OnEnable()
    {
        _playerControls.Enable();
    }

    void OnDisable()
    {
        _playerControls.Disable();
    }

    void Update()
    {
        Move();
        HandleState();
    }

    private void HandleState()
    {
        switch (_state)
        {
            // Checking isDead first because if put at the bottom, we'd also need to check !isDead in PlayerState.Idle, for example.
            case PlayerState.Die:
                if (isDead == true) _state = PlayerState.Die;
                break;

            case PlayerState.Idle:
                if (!IsMoving()) _state = PlayerState.Idle;
                break;

            case PlayerState.Walk:
                if (IsMoving()) _state = PlayerState.Walk;
                Move();
                break;
        }
    }

    private void Move()
    {
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 movement = _inputManager.GetPlayerMovement();
        Vector3 move = new(movement.x, 0, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;
        _controller.Move(_speed * Time.deltaTime * move);
        Debug.Log(move.magnitude);

        if (_inputManager.PlayerJumpedThisFrame() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
    }

    private bool IsMoving()
    {
        return _movementInput.magnitude > 0;
    }
}