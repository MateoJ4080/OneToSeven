using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
