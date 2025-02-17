using UnityEngine;

public class Player : Character
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float _speed = 50;
    // [SerializeField] private float _maxSpeed = 10;
    [SerializeField] private float _camSens = 7;

    private float _camRotationY = 0;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        RotateCamera();
    }

    private void Move()
    {
        float axisVertical = Input.GetAxis("Vertical");
        float axisHorizontal = Input.GetAxis("Horizontal");

        // Get the camera's forward direction and remove the vertical component to prevent unwanted vertical movement
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0; // Ignore the Y component to keep movement strictly on the horizontal plane
        forward.Normalize();

        // Get the camera's right direction and remove the vertical component
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 direction = axisVertical * forward + axisHorizontal * right;

        // Ensure that diagonal movement doesn't exceed the normal speed
        // Without this, moving diagonally would be faster because both axes contribute to movement
        direction = Vector3.ClampMagnitude(direction, 1f);

        Vector3 movement = direction * _speed * Time.deltaTime;

        _rb.MovePosition(transform.position + movement);
    }

    private void RotateCamera()
    {
        float rotationX = playerCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _camSens;
        _camRotationY -= Input.GetAxis("Mouse Y") * _camSens;
        _camRotationY = Mathf.Clamp(_camRotationY, -60, 60);
        playerCamera.transform.localEulerAngles = new Vector3(_camRotationY, rotationX, 0);
    }
}
