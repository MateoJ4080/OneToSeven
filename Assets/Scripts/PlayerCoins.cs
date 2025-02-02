using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    private int _coinsAmount;
    public int Coin => _coinsAmount;

    public int GetCoins()
    {
        return _coinsAmount;
    }

    public void CollectCoin(int amount)
    {
        if (amount > 0) // Only adds valid coins
        {
            _coinsAmount += amount;
        }
    }
}
