using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    public int Health;

    public void DecreaseHealth(int amount)
    {
        Health -= amount;
    }

    public void IncreaseHealth(int amount)
    {
        Health += amount;
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
            DecreaseHealth(spikes.DamageHealth);
            Debug.Log(gameObject.name + " collided with spikes. Current health: " + Health);
        }

        if (portal != null)
        {
            OnPortalCollision(portal);
        }
    }
}