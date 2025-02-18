using UnityEngine;

public class EnemyCollision : CharacterCollisionHandler
{
    protected override void OnSpikeCollision(Spikes spikes)
    {
        base.OnSpikeCollision(spikes);
        if (_characterHealth.Health == 0)
        {
            ScoreManager.AddScore(10);
            Destroy(gameObject);
        }
    }
}
