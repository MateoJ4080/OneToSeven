using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private int maxMana = 100;
    private int currentMana;

    private void Start()
    {
        currentMana = maxMana; // Starts with max mana
    }

    public void IncreaseMana(int amount) // Used in ManaOrb script
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
        Debug.Log("Mana increased. Current mana: " + currentMana);
    }
}