using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private float _speed = 100;
    [SerializeField] private float _camSens = 7;
    private float _camRotationY = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateCamera();
    }

    private void Move()
    {
        float axisVertical = Input.GetAxis("Vertical");
        float axisHorizontal = Input.GetAxis("Horizontal");
        Vector3 forward = axisVertical * playerCamera.transform.forward * _speed * Time.deltaTime;
        Vector3 lateral = axisHorizontal * playerCamera.transform.right * _speed * Time.deltaTime;
        Vector3 increment = forward + lateral;
        GetComponent<Rigidbody>().MovePosition(transform.position + increment);
    }

    private void RotateCamera()
    {
        float rotationX = playerCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _camSens;
        _camRotationY -= Input.GetAxis("Mouse Y") * _camSens;
        _camRotationY = Mathf.Clamp(_camRotationY, -60, 60);
        playerCamera.transform.localEulerAngles = new Vector3(_camRotationY, rotationX, 0);
    }

    void OnTriggerEnter(Collider _collision)
    {
        if (_collision.gameObject.GetComponent<Spikes>() != null)
        {
            Debug.Log("PLAYER HAS COLLIDED WITH A SPIKES INSTANCE");
        }
        if (_collision.gameObject.GetComponent<Portal>() != null)
        {
            Debug.Log("PLAYER HAS COLLIDED WITH A PORTAL INSTANCE");
        }
    }
}
