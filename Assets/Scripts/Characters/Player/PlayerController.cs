using UnityEngine;
using Photon.Pun;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : PlayerHealth
{
    // Enums
    public enum PlayerState { Idle, Walk, Die }

    // Main components
    private PhotonView photonView;
    private CharacterController _controller;
    private InputManager _inputManager;
    private PlayerControls _playerControls;

    // Camera settings   
    [SerializeField] private GameObject cameraPrefab;
    [SerializeField] private CinemachineCamera cmCameraPrefab;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform headTransform;
    [SerializeField] private float _camSens = 7;

    // Player state
    private PlayerState _state = PlayerState.Idle;
    private Vector2 _movementInput;

    // Movement and physics
    [SerializeField] private float _movementSpeed = 10;
    [SerializeField] private float _movementLerpSpeed = 5;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;
    private Vector3 _currentMovement;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    void Awake()
    {
        // Component initialization
        _inputManager = InputManager.Instance;
        _playerControls = new PlayerControls();
        _controller = GetComponent<CharacterController>();
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            // Instantiate the camera prefab and Cinemachine Camera
            Instantiate(cameraPrefab, headTransform.position, Quaternion.identity, headTransform);
            cmCameraPrefab = Instantiate(cmCameraPrefab, headTransform.position, Quaternion.identity, headTransform);
            cameraTransform = cmCameraPrefab.transform;  // Set the camera's transform for movement and rotation of the player

            PlayerShooting playerShooting = GetComponent<PlayerShooting>();
            if (playerShooting) playerShooting.SetCamera(cameraTransform);
        }
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
            case PlayerState.Die:
                if (isDead) _state = PlayerState.Die;
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

        // Reset player velocity when grounded
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        // Get input and apply camera rotation
        Vector3 movementInput = _inputManager.GetPlayerMovement();
        Vector3 rawMovement = new(movementInput.x, 0, movementInput.y);
        rawMovement = cameraTransform.forward * rawMovement.z + cameraTransform.right * rawMovement.x;
        rawMovement.y = 0;

        // Smooth movement interpolation
        _currentMovement = Vector3.MoveTowards(_currentMovement, rawMovement, _movementLerpSpeed * Time.deltaTime);
        _controller.Move(_currentMovement * _movementSpeed * Time.deltaTime);

        // Handle jumping
        if (_inputManager.PlayerJumpedThisFrame() && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
        }

        // Apply gravity
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private bool IsMoving()
    {
        return _movementInput.magnitude > 0;
    }
}
