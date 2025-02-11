using UnityEngine;
using System;


public class PlayerHealth : MonoBehaviour
{
    private Spikes spikes;
    private Portal portal;

    private int _health = 100;
    public int Health => _health;
    private int _maxHealth = 100;

    public void IncreaseHealth(int amount)
    {
        _health = Math.Clamp(_health + amount, 0, _maxHealth);
    }
    public void DecreaseHealth(int amount)
    {
        _health = Mathf.Clamp(_health - amount, 0, _maxHealth);
        if (_health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("<Die method> Player has died.");
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(Health);
        spikes = collision.gameObject.GetComponent<Spikes>();
        portal = collision.gameObject.GetComponent<Portal>();

        if (spikes != null)
        {
            DecreaseHealth(spikes.DamageHealth);
            Debug.Log("Player has collided with a spikes instance. Life is now " + Health + ".");
        }
        if (portal != null)
        {
            IncreaseHealth(10);
            Debug.Log("Player has collided with a portal instance. Life is now " + Health + ".");
        }
    }
}

