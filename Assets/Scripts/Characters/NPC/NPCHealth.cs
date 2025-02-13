using UnityEngine;

public class NPCHealth : CharacterHealth
{
    protected override void OnPortalCollision(Portal portal)
    {
        IncreaseHealth(10);
        Debug.Log(gameObject.name + "has collided with a portal. Current health:" + Health);
    }
}
