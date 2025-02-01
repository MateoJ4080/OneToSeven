using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Spikes spikes;
    private Portal portal;
    private PlayerCoins coin;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _maxSpeed = 10;
    [SerializeField] private float _camSens = 7;

    private float _camRotationY = 0;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        playerHealth = new PlayerHealth();
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
        Vector3 forward = axisVertical * playerCamera.transform.forward * _speed * Time.deltaTime;
        Vector3 lateral = axisHorizontal * playerCamera.transform.right * _speed * Time.deltaTime;
        Vector3 increment = forward + lateral;

        Debug.Log(_rb.linearVelocity.magnitude);
        _rb.MovePosition(transform.position + increment);
    }

    private void RotateCamera()
    {
        float rotationX = playerCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _camSens;
        _camRotationY -= Input.GetAxis("Mouse Y") * _camSens;
        _camRotationY = Mathf.Clamp(_camRotationY, -60, 60);
        playerCamera.transform.localEulerAngles = new Vector3(_camRotationY, rotationX, 0);
    }

    void OnTriggerEnter(Collider collision)
    {
        spikes = collision.gameObject.GetComponent<Spikes>();
        portal = collision.gameObject.GetComponent<Portal>();
        coin = collision.gameObject.GetComponent<PlayerCoins>();

        if (spikes != null)
        {
            playerHealth.DecreaseHealth(spikes.DamageHealth);
            Debug.Log("Player has collided with a spikes instance. Life is now " + playerHealth.Health + ".");
        }
        if (collision.gameObject.GetComponent<Portal>() != null)
        {
            playerHealth.IncreaseHealth(10);
            Debug.Log("Player has collided with a portal instance. Life is now " + playerHealth.Health + ".");
        }
        if (collision.gameObject.GetComponent<PlayerCoins>() != null)
        {
            coin.CollectCoin(1);
            Debug.Log("Player has collected a coin. Coins:" + coin.GetCoins());
        }
    }
}
