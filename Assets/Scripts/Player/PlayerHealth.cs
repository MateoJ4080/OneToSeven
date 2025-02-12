using UnityEngine;


public class PlayerHealth : Character
{
    protected override void OnPortalCollision(Portal portal)
    {
        IncreaseHealth(10);
        Debug.Log("El jugador ha colisionado con un portal. Vida actual: " + Health);
    }
}

