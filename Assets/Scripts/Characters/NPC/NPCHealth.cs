using UnityEngine;

public class NPCHealth : CharacterHealth
{
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
