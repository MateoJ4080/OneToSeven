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

    // Camera settings   
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform headTransform;
    private CinemachineCamera cmCam;

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
        _controller = GetComponent<CharacterController>();
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            if (cmCam == null)
            {
                cmCam = GameObject.FindWithTag("CMcam").GetComponent<CinemachineCamera>();
            }
            cmCam.Target.TrackingTarget = headTransform;
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected == true) return; // Since every player instance in the scene will have this script, we only let execute the Update of the object the Player is controlling, based on the PhotonView component

        Move();
        HandleState();
        transform.rotation = Quaternion.Euler(0, cameraTransform.rotation.eulerAngles.y, 0);
    }

    private void Move()
    {
        // Jumping
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -0.5f;
        }
        if (_inputManager.PlayerJumpedThisFrame() && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
        }

        // Get input and apply camera rotation
        Vector3 movementInput = _inputManager.GetPlayerMovement();
        Vector3 rawMovement = new(movementInput.x, 0, movementInput.y);
        rawMovement = cameraTransform.forward * rawMovement.z + cameraTransform.right * rawMovement.x;
        rawMovement.y = 0;

        // Smooth movement interpolation
        _currentMovement = Vector3.MoveTowards(_currentMovement, rawMovement, _movementLerpSpeed * Time.deltaTime);
        _controller.Move(_currentMovement * _movementSpeed * Time.deltaTime);


        // Apply gravity
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private bool IsMoving()
    {
        return _movementInput.magnitude > 0;
    }

    // Not doing anything right now but leave it here in case of future implementations. If made, delete this comment.
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
}
