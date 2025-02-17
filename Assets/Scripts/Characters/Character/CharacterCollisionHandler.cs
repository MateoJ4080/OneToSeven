using Unity.VisualScripting;
using UnityEngine;

public class CharacterCollisionHandler : Character
{
    private CharacterHealth _characterHealth;
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _characterHealth = GetComponent<CharacterHealth>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Colliding registered");
        Spikes spikes = collision.GetComponent<Spikes>();
        Portal portal = collision.GetComponent<Portal>();

        if (spikes != null)
        {
            OnSpikeCollision(spikes);
        }

        if (portal != null)
        {
        }
    }

    protected virtual void OnSpikeCollision(Spikes spikes)
    {
        _characterHealth?.DecreaseHealth(spikes.DamageHealth);
        Debug.Log(gameObject.name + " has collided with spikes, health: " + _characterHealth.Health);
    }
}
