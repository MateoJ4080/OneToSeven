using UnityEngine;

public class CharacterHealth : Character
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxHealth = 100;

    public int Health => _health;

    public void IncreaseHealth(int amount)
    {
        _health = Mathf.Clamp(_health + amount, 0, _maxHealth);
    }

    public void DecreaseHealth(int amount)
    {
        _health = Mathf.Clamp(_health - amount, 0, _maxHealth);
        if (_health <= 0) Die();
    }

    protected void Die()
    {
        Debug.Log(gameObject.name + " has died.");
    }
}
