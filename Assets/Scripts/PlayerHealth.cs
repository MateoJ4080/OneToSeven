using UnityEngine;
using System;


public class PlayerHealth : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Spikes spikes;
    private Portal portal;

    [SerializeField] private int _health = 100;
    public int Health => _health;
    [SerializeField] private int _maxHealth;

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
        spikes = collision.gameObject.GetComponent<Spikes>();
        portal = collision.gameObject.GetComponent<Portal>();

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
    }
}

