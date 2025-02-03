using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private int maxMana = 100;
    private int currentMana;

    private void Start()
    {
        currentMana = maxMana; // Inicia con maná lleno
    }

    public void IncreaseMana(int amount) // Method still not used
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
        Debug.Log("Mana increased. Current mana: " + currentMana);
    }
}