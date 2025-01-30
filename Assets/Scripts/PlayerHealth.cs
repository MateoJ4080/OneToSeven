using UnityEngine;
using System;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;
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
        Debug.Log("Player has died.");
    }
}
