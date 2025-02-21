using UnityEngine;

public class Spikes : MonoBehaviour
{
    private int _damageHealth = 10;
    public int DamageHealth
    {
        get => _damageHealth;
        set => _damageHealth = value;
    }
}
