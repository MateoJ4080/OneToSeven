using System;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    public event Action OnHealthChanged;

    public override void DecreaseHealth(int amount)
    {
        base.DecreaseHealth(amount);
        OnHealthChanged?.Invoke();
    }
    public override void IncreaseHealth(int amount)
    {
        base.IncreaseHealth(amount);
        OnHealthChanged?.Invoke();
    }
    protected override void Die()
    {
        base.Die();
    }
}
