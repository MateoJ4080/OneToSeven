using UnityEngine;

public class CharacterCollisionHandler : Character
{
    protected CharacterHealth _characterHealth;

    private void Start()
    {
        _characterHealth = GetComponent<CharacterHealth>();
        // Debug.Log(_characterHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        Spikes spikes = other.GetComponent<Spikes>();
        Portal portal = other.GetComponent<Portal>();

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
