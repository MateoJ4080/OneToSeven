using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    private CharacterHealth _characterHealth;

    private void Awake()
    {
        _characterHealth = GetComponent<CharacterHealth>();
    }

    // Virtual method for handling portal collisions.
    // By default, it does nothing (affects only players when overridden).
    protected virtual void OnPortalCollision(Portal portal)
    {
        // Default behavior: no effect on non-player characters.
    }

    private void OnTriggerEnter(Collider collision)
    {
        Spikes spikes = collision.gameObject.GetComponent<Spikes>();
        Portal portal = collision.gameObject.GetComponent<Portal>();

        if (spikes != null)
        {
            _characterHealth.DecreaseHealth(spikes.DamageHealth);
            Debug.Log(gameObject.name + " collided with spikes. Current health: " + _characterHealth.Health);
            Debug.Log("OnTriggerEnter");
        }

        if (portal != null)
        {
            OnPortalCollision(portal);
        }
    }
}