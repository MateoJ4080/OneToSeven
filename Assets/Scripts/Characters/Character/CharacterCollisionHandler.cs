using UnityEngine;

public class CharacterCollisionHandler : MonoBehaviour
{
    private CharacterHealth _characterHealth;

    private void Awake()
    {
        _characterHealth = GetComponent<CharacterHealth>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Spikes spikes = collision.GetComponent<Spikes>();
        Portal portal = collision.GetComponent<Portal>();

        if (spikes != null)
        {
            _characterHealth.DecreaseHealth(spikes.DamageHealth);
            Debug.Log(gameObject.name + " collided with spikes. Current health: " + _characterHealth.Health);
        }

        // Virtual method for handling portal collisions.
        // By default, it does nothing (affects only players when overridden).
        if (portal != null)
        {
            OnPortalCollision(portal);
        }
    }

    protected virtual void OnPortalCollision(Portal portal)
    {
        // Default behavior: no effect on non-player characters.
        // Can be overridden in Player, Enemy, etc.
    }
}