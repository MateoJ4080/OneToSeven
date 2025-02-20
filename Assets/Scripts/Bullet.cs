using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _type;

    void Start()
    {
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = transform.forward * _speed;
            }
            else
            {
                Debug.LogError("No Rigidbody found");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject + " has collided with " + other.name);
    }

    void OnCollisionEnter(Collision other)
    {
        CharacterHealth _characterHealth = other.gameObject.GetComponent<CharacterHealth>();
        if (_characterHealth != null)
        {
            _characterHealth.DecreaseHealth(_damage);
            Destroy(gameObject);
        }
    }

    public void Shoot(int type, Vector3 _position, Vector3 _direction)
    {
        _type = type;
        transform.position = _position;
        transform.forward = _direction;
    }
}
