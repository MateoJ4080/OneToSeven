using UnityEngine;

public class Spikes : MonoBehaviour
{
    private int _damageHealth = 50;
    public int DamageHealth
    {
        get => _damageHealth;
        set => _damageHealth = value;
    }
}
