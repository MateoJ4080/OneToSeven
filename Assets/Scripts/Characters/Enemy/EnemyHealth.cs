using UnityEngine;

public class EnemyHealth : CharacterCollisionHandler
{
    private CharacterHealth _characterHealth;

    private void Awake()
    {
        _characterHealth = GetComponent<CharacterHealth>();
    }

    protected override void OnSpikeCollision(Spikes spikes)
    {
        _characterHealth.IncreaseHealth(10);
        Debug.Log(gameObject.name + "has collided with a portal. Current health:" + _characterHealth.Health);
    }
}
