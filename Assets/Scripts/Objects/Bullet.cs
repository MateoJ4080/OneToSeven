using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int damage;
    [SerializeField] private float maxDistance = 20f;
    private Vector3 startPosition;
    private PoolManager poolManager;

    void Awake()
    {
        startPosition = transform.position;
        poolManager = FindFirstObjectByType<PoolManager>();
    }

    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);

        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            DeactivateBullet(); // Return bullet to pool
        }
    }

    void DeactivateBullet()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        poolManager.ReturnBullet(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        DeactivateBullet();
        if (other.gameObject.TryGetComponent<CharacterHealth>(out var _characterHealth))
        {
            _characterHealth.DecreaseHealth(damage);
        }
    }

    public void Shoot(Vector3 _position, Vector3 _direction)
    {
        transform.position = _position;
        transform.forward = _direction;
    }
}
