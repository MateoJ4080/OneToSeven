using UnityEngine;

public class PlayerPickupHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        IPickup pickup = collision.GetComponent<IPickup>();
        pickup?.Collect(GetComponent<Player>()); // Calls Collect method from the respective object
    }
}