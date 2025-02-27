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
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    [SerializeField] private float _gravityValue = -9.81f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _movementLerpSpeed;
    private Vector3 _currentMovement;

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
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector3 movementInput = _inputManager.GetPlayerMovement();
        Vector3 rawMovement = new(movementInput.x, 0, movementInput.y);
        rawMovement = cameraTransform.forward * rawMovement.z + cameraTransform.right * rawMovement.x;
        rawMovement.y = 0;
        // _controller.Move(_movementSpeed * Time.deltaTime * rawMovement);
        _currentMovement = Vector3.MoveTowards(_currentMovement, rawMovement, _movementLerpSpeed * Time.deltaTime);
        _controller.Move(_currentMovement * _movementSpeed * Time.deltaTime);



        if (_inputManager.PlayerJumpedThisFrame() && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private bool IsMoving()
    {
        return _movementInput.magnitude > 0;
    }
}