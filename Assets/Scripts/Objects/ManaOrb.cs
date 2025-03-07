using UnityEngine;

public class ManaOrb : MonoBehaviour, IPickup
{
    [SerializeField] private int manaAmount = 20;

    public void Collect(PlayerController player)
    {
        player.GetComponent<PlayerMana>()?.IncreaseMana(manaAmount);
        AudioManager.Instance.Play(AudioManager.SoundType.Mana_Orb);
        Debug.Log("Mana increased by " + manaAmount);
        Destroy(gameObject);
    }
}