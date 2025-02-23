using UnityEngine;

public class CharacterHealth : Character
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxHealth = 100;
    protected bool isDead;

    public int Health => _health;

    public virtual void IncreaseHealth(int amount)
    {
        _health = Mathf.Clamp(_health + amount, 0, _maxHealth);
    }

    public virtual void DecreaseHealth(int amount)
    {
        _health = Mathf.Clamp(_health - amount, 0, _maxHealth);
        if (_health <= 0) Die();
    }

    protected virtual void Die()
    {
        isDead = true;
        Debug.Log(gameObject.name + " has died.");
    }
}
