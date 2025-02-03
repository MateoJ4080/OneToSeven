using UnityEngine;

public class ManaOrb : MonoBehaviour, IPickup
{
    [SerializeField] private int manaAmount = 20;

    public void Collect(Player player)
    {
        player.GetComponent<PlayerMana>()?.IncreaseMana(manaAmount);
        Debug.Log("Mana increased by " + manaAmount);
        Destroy(gameObject);
    }
}