using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    protected override void Die()
    {
        base.Die();
        ScoreManager.AddScore(10);
        Destroy(gameObject);
    }
}
