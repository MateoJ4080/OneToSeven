using UnityEngine;

public class Coin : MonoBehaviour, IPickup
{
    private int _coinsAmount = 0;
    public int CoinsAmount => _coinsAmount;

    public int GetCoins()
    {
        return _coinsAmount;
    }
    public void Collect(Player player)
    {
        _coinsAmount += 1;
        AudioManager.Instance.Play(AudioManager.SoundType.Coin);
        Debug.Log("Player has collected a coin. Coins:" + GetCoins());
        Destroy(gameObject);
    }
}
