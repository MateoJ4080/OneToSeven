using UnityEngine;

public static class ScoreManager
{
    private static int _score;

    public static int Score
    {
        get { return _score; }
        set
        {
            // Prevent the score from going negative
            if (value < 0)
                _score = 0;
            else
                _score = value;
        }
    }

    public static void AddScore(int amount)
    {
        Score += amount;
    }
}